#include "DHT.h"
#include <OneWire.h>
#include <DallasTemperature.h>
#include "DeviceInfo.h"
#include <ESP8266WiFi.h>
#include <ESP8266HTTPClient.h>
#include <WiFiClient.h>

#define DHTPIN D1     // Digital pin connected to the DHT sensor
#define ONE_WIRE_BUS D2 //for DS82160 Waterproof sensor
#define SoilDataPIN A0 //for soil moisture sensor

#define DeviceSNO Serial_Number


#define DHTTYPE DHT11   // DHT 11
DHT dht(DHTPIN, DHTTYPE);

// Setup a oneWire instance to communicate with any OneWire devices (not just Maxim/Dallas temperature ICs)
OneWire oneWire(ONE_WIRE_BUS);
// Pass our oneWire reference to Dallas Temperature.
DallasTemperature sensors(&oneWire);


// Replace with your network credentials
const char* ssid   = ssId;
const char* password = Password;

// REPLACE with your Domain name and URL path or IP address with path
const char* serverName = "http://192.168.0.100:4242/api/SoilData/AddSoilData";

String SoilTemperatureInCelsius() {
  sensors.requestTemperatures();
  float soilTemp = sensors.getTempCByIndex(0);
  if (soilTemp == -127.00) {
    Serial.println("Unable to read from sensor data.");
  }
  Serial.print("Soil Temperture is ");
  Serial.println(soilTemp);

  return String(soilTemp);
}

//Soil moisture Data
int SoilMoisture() {
  int getSoilMoistureValue = analogRead(SoilDataPIN);
  //scale moisture value to range 0% to 100%
  if (getSoilMoistureValue >= 1000) {
    return 1000;
  }
  Serial.print("Soil Moisture is ");
  Serial.println(getSoilMoistureValue);
  if (getSoilMoistureValue > 750) {
    Serial.println("Soil is dry");
  } else if (getSoilMoistureValue < 500) {
    Serial.println("Soil is too wet");
  } else {
    Serial.println("Soil moisture is within range");
  }

  return getSoilMoistureValue;
}

//atmospheric temperature
float AtmosphericTemperature() {
  float t = dht.readTemperature();
  // Check if any reads failed and exit early (to try again).
  if (isnan(t)) {
    Serial.println(F("Failed to read from DHT sensor!"));
    return 0.00;
  }
  Serial.print(F("Temperature: "));
  Serial.print(t);
  Serial.println(F("°C "));

  return t;
}

//Humidity
int AtmosphericHumidity() {
  int h = dht.readHumidity();
  // Check if any reads failed and exit early (to try again).
  if (isnan(h)) {
    Serial.println(F("Failed to read from DHT11 sensor!"));
    return 0;
  }
  Serial.print(F("Humidity: "));
  Serial.print(h);
  Serial.println(F("%"));

  return h;
}

//Nitrogen Data
int SoilNitrogen() {
  int nitrogenValue = random(50, 200);

  Serial.print("Soil nitrogen: ");
  Serial.println(nitrogenValue);

  return nitrogenValue;

}

//Phosphorus Data
int SoilPhosphorus() {
  int PhosphorusValue = random(50, 200);

  Serial.print("Soil Phosphorus: ");
  Serial.println(PhosphorusValue);

  return PhosphorusValue;

}

//Potassuim Data
int SoilPotassium() {
  int PotassiumValue = random(50, 200);

  Serial.print("Soil Potassium: ");
  Serial.println(PotassiumValue);
  return PotassiumValue;

}

void setup() {
  Serial.begin(9600);
  dht.begin();

  WiFi.begin(ssid, password);
  Serial.println("Connecting");
  while (WiFi.status() != WL_CONNECTED) {
    delay(500);
    Serial.print(".");
  }
  Serial.println("");
  Serial.print("Connected to WiFi network with IP Address: ");
  Serial.println(WiFi.localIP());
}

void loop() {
  // Wait a few seconds between measurements.
  delay(2000);
  Serial.println("===================================");
  //Atmospheric temperature
  float atmosphericTemperature = AtmosphericTemperature();

  //Atmospheric humdity
  float atmosphericHumidity = AtmosphericHumidity();

  //Soil Temperature data
  String soilTemp = SoilTemperatureInCelsius();

  //Soil moisture data
  int SoilMoistureData = SoilMoisture();
  //soil nitrogen
  int soilNitrogen = SoilNitrogen();
  //soil Phosphorus
  int soilPhosphorus = SoilPhosphorus();
  //soil Potassium
  int soilPotassium = SoilPotassium();

  Serial.print("The device serial number is ");
  Serial.println(Serial_Number);

  //Check WiFi connection status
  if (WiFi.status() == WL_CONNECTED) {

    WiFiClient client;
    HTTPClient http;

    // Your Domain name with URL path or IP address with path
    http.begin(client, serverName);

    // Specify content-type header
    http.addHeader("Content-Type", "application/json");

    // Prepare your HTTP POST request data in json format
    String httpRequestData = "{\"soilMoisture\":\""+ String(SoilMoistureData)+"\",\"temperature\":\""+ String(atmosphericTemperature)+"\",\"humidity\":\""+ String(atmosphericHumidity)+"\",\"soilTemperature\":\""+ soilTemp +"\",\"nitrogen\":\""+ String(soilNitrogen)+"\",\"potassium\":\""+ String(soilPotassium)+"\",\"phosphorus\":\""+ String(soilPhosphorus)+"\",\"serialNumber\":\""+ String(Serial_Number)+"\"}";
    
    Serial.print("httpRequestData: ");
    Serial.println(httpRequestData);

    // Send HTTP POST request
    int httpResponseCode = http.POST(httpRequestData);


    if (httpResponseCode > 0) {
      Serial.print("HTTP Response code: ");
      Serial.println(httpResponseCode);

      const String& payload = http.getString();
      Serial.println("received payload:\n<<");
      Serial.println(payload);
      Serial.println(">>");

    }
    else {
      Serial.printf("[HTTP] POST... failed, error: %s\n", http.errorToString(httpResponseCode).c_str());
    }
    // Free resources
    http.end();
  }
  else {
    Serial.println("WiFi Disconnected");
  }

  //After data is sent go to deep sleep for 30 seconds
  ESP.deepSleep(30e6);
}
