using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Frontend.Migrations
{
    public partial class Identity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "FarmerImage",
                schema: "dbo",
                columns: table => new
                {
                    FarmerImageId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FarmerPhoto = table.Column<byte[]>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FarmerImage", x => x.FarmerImageId)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "Gender",
                schema: "dbo",
                columns: table => new
                {
                    GenderId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GenderName = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gender", x => x.GenderId)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "Region",
                schema: "dbo",
                columns: table => new
                {
                    RegionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegionName = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Region", x => x.RegionId)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "SoilCategory",
                schema: "dbo",
                columns: table => new
                {
                    SoilCategoryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SoilName = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoilCategory", x => x.SoilCategoryId)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    MiddleName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    FullName = table.Column<string>(nullable: true),
                    BirthDate = table.Column<DateTime>(nullable: false),
                    GenderId = table.Column<int>(nullable: false),
                    HomeTown = table.Column<string>(nullable: true),
                    RegionId = table.Column<int>(nullable: false),
                    Residence = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    ProfilePic = table.Column<byte[]>(nullable: true),
                    RegistrationDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_Users_Gender_GenderId",
                        column: x => x.GenderId,
                        principalSchema: "dbo",
                        principalTable: "Gender",
                        principalColumn: "GenderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_Region_RegionId",
                        column: x => x.RegionId,
                        principalSchema: "dbo",
                        principalTable: "Region",
                        principalColumn: "RegionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_RoleClaims_Role_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "dbo",
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Arduino",
                schema: "dbo",
                columns: table => new
                {
                    ArduinoId = table.Column<string>(maxLength: 100, nullable: false),
                    SerialNumber = table.Column<string>(maxLength: 100, nullable: false),
                    VID = table.Column<string>(maxLength: 10, nullable: false),
                    PID = table.Column<string>(maxLength: 10, nullable: false),
                    BN = table.Column<string>(maxLength: 100, nullable: false),
                    DeploymentDate = table.Column<string>(maxLength: 100, nullable: false),
                    IsVerified = table.Column<bool>(nullable: true),
                    IsActivated = table.Column<bool>(nullable: true),
                    DateOfActivation = table.Column<string>(maxLength: 100, nullable: true),
                    LastPowerOnDate = table.Column<string>(maxLength: 100, nullable: true),
                    LastPowerOnTime = table.Column<string>(maxLength: 100, nullable: true),
                    IsActive = table.Column<bool>(nullable: true),
                    IsOnsite = table.Column<bool>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Arduino", x => new { x.ArduinoId, x.SerialNumber })
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_Arduino_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Farmer",
                schema: "dbo",
                columns: table => new
                {
                    FarmerId = table.Column<string>(maxLength: 250, nullable: false),
                    Firstname = table.Column<string>(maxLength: 250, nullable: false),
                    MiddleName = table.Column<string>(maxLength: 250, nullable: true),
                    LastName = table.Column<string>(maxLength: 250, nullable: false),
                    Address = table.Column<string>(maxLength: 250, nullable: false),
                    Location = table.Column<string>(maxLength: 100, nullable: false),
                    Contact = table.Column<string>(unicode: false, fixedLength: true, maxLength: 20, nullable: false),
                    DateCreated = table.Column<string>(maxLength: 100, nullable: false),
                    RegionId = table.Column<int>(nullable: false),
                    GenderId = table.Column<int>(nullable: false),
                    FarmerImageId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Farmer", x => x.FarmerId)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_FarmerImage_Farmer_",
                        column: x => x.FarmerImageId,
                        principalSchema: "dbo",
                        principalTable: "FarmerImage",
                        principalColumn: "FarmerImageId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Gender_Farmer_",
                        column: x => x.GenderId,
                        principalSchema: "dbo",
                        principalTable: "Gender",
                        principalColumn: "GenderId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Region_Farmer_",
                        column: x => x.RegionId,
                        principalSchema: "dbo",
                        principalTable: "Region",
                        principalColumn: "RegionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Farmer_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_UserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                schema: "dbo",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: true),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => x.LoginProvider)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_UserLogins_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                schema: "dbo",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.RoleId, x.UserId })
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_UserRoles_Role_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "dbo",
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                schema: "dbo",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => x.LoginProvider)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_UserTokens_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SoilData",
                schema: "dbo",
                columns: table => new
                {
                    SoilDataId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SoilMoisture = table.Column<string>(maxLength: 50, nullable: false),
                    Temperature = table.Column<string>(maxLength: 50, nullable: false),
                    Humidity = table.Column<string>(maxLength: 50, nullable: false),
                    SoilTemperature = table.Column<string>(maxLength: 50, nullable: false),
                    Nitrogen = table.Column<string>(maxLength: 50, nullable: false),
                    Potassium = table.Column<string>(maxLength: 50, nullable: false),
                    Phosphorus = table.Column<string>(maxLength: 50, nullable: false),
                    Date = table.Column<string>(maxLength: 100, nullable: false),
                    Time = table.Column<string>(maxLength: 100, nullable: false),
                    SerialNumber = table.Column<string>(maxLength: 100, nullable: false),
                    ArduinoId = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoilData", x => x.SoilDataId)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_Arduino_SoilData_",
                        columns: x => new { x.ArduinoId, x.SerialNumber },
                        principalSchema: "dbo",
                        principalTable: "Arduino",
                        principalColumns: new[] { "ArduinoId", "SerialNumber" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Farm",
                schema: "dbo",
                columns: table => new
                {
                    FarmId = table.Column<string>(maxLength: 250, nullable: false),
                    FarmName = table.Column<string>(maxLength: 250, nullable: false),
                    Location = table.Column<string>(maxLength: 100, nullable: false),
                    DateCreated = table.Column<string>(maxLength: 100, nullable: false),
                    FarmerId = table.Column<string>(maxLength: 250, nullable: false),
                    RegionId = table.Column<int>(nullable: false),
                    SoilCategoryId = table.Column<int>(nullable: false),
                    ArduinoId = table.Column<string>(maxLength: 100, nullable: false),
                    SerialNumber = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Farm", x => x.FarmId)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_Farmer_Farm_",
                        column: x => x.FarmerId,
                        principalSchema: "dbo",
                        principalTable: "Farmer",
                        principalColumn: "FarmerId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Region_Farm_",
                        column: x => x.RegionId,
                        principalSchema: "dbo",
                        principalTable: "Region",
                        principalColumn: "RegionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SoilCategory_Farm_",
                        column: x => x.SoilCategoryId,
                        principalSchema: "dbo",
                        principalTable: "SoilCategory",
                        principalColumn: "SoilCategoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Arduino_Farm_",
                        columns: x => new { x.ArduinoId, x.SerialNumber },
                        principalSchema: "dbo",
                        principalTable: "Arduino",
                        principalColumns: new[] { "ArduinoId", "SerialNumber" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FarmMapLocation",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaplocationId = table.Column<string>(maxLength: 100, nullable: false),
                    Title = table.Column<string>(maxLength: 100, nullable: false),
                    Descriptions = table.Column<string>(maxLength: 100, nullable: true),
                    Latitude = table.Column<string>(maxLength: 200, nullable: true),
                    Longitude = table.Column<string>(maxLength: 200, nullable: true),
                    FarmId = table.Column<string>(maxLength: 250, nullable: false),
                    ArduinoId = table.Column<string>(maxLength: 100, nullable: false),
                    SerialNumber = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaplocationId", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "RefFarm11",
                        column: x => x.FarmId,
                        principalSchema: "dbo",
                        principalTable: "Farm",
                        principalColumn: "FarmId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "RefArduino12",
                        columns: x => new { x.ArduinoId, x.SerialNumber },
                        principalSchema: "dbo",
                        principalTable: "Arduino",
                        principalColumns: new[] { "ArduinoId", "SerialNumber" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Arduino_UserId",
                schema: "dbo",
                table: "Arduino",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Farm_FarmerId",
                schema: "dbo",
                table: "Farm",
                column: "FarmerId");

            migrationBuilder.CreateIndex(
                name: "IX_Farm_RegionId",
                schema: "dbo",
                table: "Farm",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Farm_SoilCategoryId",
                schema: "dbo",
                table: "Farm",
                column: "SoilCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Farm_ArduinoId_SerialNumber",
                schema: "dbo",
                table: "Farm",
                columns: new[] { "ArduinoId", "SerialNumber" });

            migrationBuilder.CreateIndex(
                name: "IX_Farmer_FarmerImageId",
                schema: "dbo",
                table: "Farmer",
                column: "FarmerImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Farmer_GenderId",
                schema: "dbo",
                table: "Farmer",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Farmer_RegionId",
                schema: "dbo",
                table: "Farmer",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Farmer_UserId",
                schema: "dbo",
                table: "Farmer",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FarmMapLocation_FarmId",
                schema: "dbo",
                table: "FarmMapLocation",
                column: "FarmId");

            migrationBuilder.CreateIndex(
                name: "IX_FarmMapLocation_ArduinoId_SerialNumber",
                schema: "dbo",
                table: "FarmMapLocation",
                columns: new[] { "ArduinoId", "SerialNumber" });

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "dbo",
                table: "Role",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_RoleId",
                schema: "dbo",
                table: "RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_SoilData_ArduinoId_SerialNumber",
                schema: "dbo",
                table: "SoilData",
                columns: new[] { "ArduinoId", "SerialNumber" });

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                schema: "dbo",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_UserId",
                schema: "dbo",
                table: "UserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UserId",
                schema: "dbo",
                table: "UserRoles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_GenderId",
                schema: "dbo",
                table: "Users",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "dbo",
                table: "Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "dbo",
                table: "Users",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RegionId",
                schema: "dbo",
                table: "Users",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTokens_UserId",
                schema: "dbo",
                table: "UserTokens",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FarmMapLocation",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "RoleClaims",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "SoilData",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "UserClaims",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "UserLogins",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "UserRoles",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "UserTokens",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Farm",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Role",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Farmer",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "SoilCategory",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Arduino",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "FarmerImage",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Gender",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Region",
                schema: "dbo");
        }
    }
}
