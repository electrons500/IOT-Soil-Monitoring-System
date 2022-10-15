using System;
using System.Net;
using Zenoph.Notify.Collections;
using Zenoph.Notify.Enums;
using Zenoph.Notify.Request;
using Zenoph.Notify.Response;
using Zenoph.Notify.Store;



namespace Frontend.Models.Data.Service
{
    public class SMSService
    {
        public bool SendSMS(string message, string receiverNumber)
        {
            try
            {
                string DestinationNumber = $"{receiverNumber}";

                NotifyRequest.setHost("api.smsonlinegh.com");

                NotifyRequest.useSecureConnection(true);

                // Initialise request object
                SMSRequest sr = new SMSRequest();

                // set authentication details.
                sr.setAuthModel(AuthModel.API_KEY);
                sr.setAuthApiKey("38ca3047a142a67b92db64b7c88d116de73e572a148e70b9357bb7ba06812c92");

                // message properties
                sr.setMessage(message);
                sr.setMessageType(TextMessageType.TEXT);
                sr.setSender("SMARTSOILGH");       // should be registered/approved by the SMS Company

                // add message destination
                sr.addDestination(DestinationNumber);

                // send message.
                MessageResponse resp = sr.submit() as MessageResponse;
                //get list of report on sent messages.I know is only one.lol
                MessageReportList messageReportlist = resp.getReports();

                //Get the one message Index
                MessageReport messageReport = messageReportlist.getItem(0);
                if (messageReport != null)
                {
                    return true;
                }

            }catch(WebException ex) when ((ex.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.NotExtended)
            {

            }
            catch (Exception ex)
            {
                if(ex is RequestException)
                {
                    RequestHandshake rh = ((RequestException)ex).getRequestHandshake();
                }

               
                throw;
            }

            return false;
        }
    }
}
