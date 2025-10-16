using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudioUp.Models.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContentTypes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LinkHP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Link2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Data = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InternalHomeLinks",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsExternal = table.Column<bool>(type: "bit", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InternalHomeLinks", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Login",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Login", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_Contacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsWatch = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Contacts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_CustomerTypes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_CustomerTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "T_HMOs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ArrangementName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrainingsPerMonth = table.Column<int>(type: "int", nullable: true),
                    TrainingPrice = table.Column<double>(type: "float", nullable: true),
                    MinimumAge = table.Column<double>(type: "float", nullable: true),
                    MaximumAge = table.Column<double>(type: "float", nullable: true),
                    TrainingDescription = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_HMOs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "T_LeumitCommimentTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", maxLength: 5, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_LeumitCommimentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_PaymentOptions",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_PaymentOptions", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "T_SubscriptionTypes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    TotalTraining = table.Column<int>(type: "int", nullable: true),
                    PriceForTraining = table.Column<float>(type: "real", nullable: true),
                    NumberOfTrainingPerWeek = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_SubscriptionTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "T_Trainers",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Mail = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Tel = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Trainers", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "T_TrainigTypes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_TrainigTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ContentSections",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Section1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Section2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Section3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Section4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageData = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ViewInHP = table.Column<bool>(type: "bit", nullable: false),
                    ContentTypeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentSections", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ContentSections_ContentTypes_ContentTypeID",
                        column: x => x.ContentTypeID,
                        principalTable: "ContentTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "T_Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tz = table.Column<string>(type: "nvarchar(9)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    Tel = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CustomerTypeId = table.Column<int>(type: "int", nullable: true),
                    HMOId = table.Column<int>(type: "int", nullable: true),
                    PaymentOptionId = table.Column<int>(type: "int", nullable: true),
                    SubscriptionTypeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_T_Customers_T_CustomerTypes_CustomerTypeId",
                        column: x => x.CustomerTypeId,
                        principalTable: "T_CustomerTypes",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_T_Customers_T_HMOs_HMOId",
                        column: x => x.HMOId,
                        principalTable: "T_HMOs",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_T_Customers_T_PaymentOptions_PaymentOptionId",
                        column: x => x.PaymentOptionId,
                        principalTable: "T_PaymentOptions",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_T_Customers_T_SubscriptionTypes_SubscriptionTypeId",
                        column: x => x.SubscriptionTypeId,
                        principalTable: "T_SubscriptionTypes",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "T_TrainingCustomerTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerTypeID = table.Column<int>(type: "int", nullable: false),
                    TrainingTypeId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_TrainingCustomerTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_T_TrainingCustomerTypes_T_CustomerTypes_CustomerTypeID",
                        column: x => x.CustomerTypeID,
                        principalTable: "T_CustomerTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_T_TrainingCustomerTypes_T_TrainigTypes_TrainingTypeId",
                        column: x => x.TrainingTypeId,
                        principalTable: "T_TrainigTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "T_CustomerHMOS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerID = table.Column<int>(type: "int", nullable: true),
                    HMOID = table.Column<int>(type: "int", nullable: true),
                    FreeFitId = table.Column<string>(type: "nvarchar(15)", nullable: true),
                    FiledId = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_CustomerHMOS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_T_CustomerHMOS_T_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "T_Customers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_T_CustomerHMOS_T_HMOs_HMOID",
                        column: x => x.HMOID,
                        principalTable: "T_HMOs",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "T_CustomerSubscription",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerID = table.Column<int>(type: "int", nullable: false),
                    SubscriptionTypeId = table.Column<int>(type: "int", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_CustomerSubscription", x => x.ID);
                    table.ForeignKey(
                        name: "FK_T_CustomerSubscription_T_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "T_Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_T_CustomerSubscription_T_SubscriptionTypes_SubscriptionTypeId",
                        column: x => x.SubscriptionTypeId,
                        principalTable: "T_SubscriptionTypes",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "T_LeumitCommitments",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: false),
                    CommitmentTypeId = table.Column<int>(type: "int", nullable: true),
                    CustomerId = table.Column<int>(type: "int", nullable: true),
                    CommitmentTz = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: false),
                    BirthDate = table.Column<DateOnly>(type: "date", nullable: false),
                    FileUploadId = table.Column<int>(type: "int", nullable: true),
                    Validity = table.Column<DateOnly>(type: "date", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_LeumitCommitments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_T_LeumitCommitments_Files_FileUploadId",
                        column: x => x.FileUploadId,
                        principalTable: "Files",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_T_LeumitCommitments_T_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "T_Customers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_T_LeumitCommitments_T_LeumitCommimentTypes_CommitmentTypeId",
                        column: x => x.CommitmentTypeId,
                        principalTable: "T_LeumitCommimentTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "T_Trainings",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrainingCustomerTypeId = table.Column<int>(type: "int", nullable: true),
                    TrainerID = table.Column<int>(type: "int", nullable: true),
                    DayOfWeek = table.Column<int>(type: "int", nullable: false),
                    Hour = table.Column<int>(type: "int", nullable: false),
                    Minute = table.Column<int>(type: "int", nullable: false),
                    ParticipantsCount = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Trainings", x => x.ID);
                    table.ForeignKey(
                        name: "FK_T_Trainings_T_Trainers_TrainerID",
                        column: x => x.TrainerID,
                        principalTable: "T_Trainers",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_T_Trainings_T_TrainingCustomerTypes_TrainingCustomerTypeId",
                        column: x => x.TrainingCustomerTypeId,
                        principalTable: "T_TrainingCustomerTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "T_AvailableTrainings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrainingId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ParticipantsCount = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_AvailableTrainings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_T_AvailableTrainings_T_Trainings_TrainingId",
                        column: x => x.TrainingId,
                        principalTable: "T_Trainings",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "T_CustomerFixedTrainings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: true),
                    TrainingId = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_CustomerFixedTrainings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_T_CustomerFixedTrainings_T_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "T_Customers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_T_CustomerFixedTrainings_T_Trainings_TrainingId",
                        column: x => x.TrainingId,
                        principalTable: "T_Trainings",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "T_TrainingsCustomers",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrainingID = table.Column<int>(type: "int", nullable: true),
                    CustomerID = table.Column<int>(type: "int", nullable: true),
                    Attended = table.Column<bool>(type: "bit", nullable: false),
                    CustomerSubscriptionId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_TrainingsCustomers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_T_TrainingsCustomers_T_AvailableTrainings_TrainingID",
                        column: x => x.TrainingID,
                        principalTable: "T_AvailableTrainings",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_T_TrainingsCustomers_T_CustomerSubscription_CustomerSubscriptionId",
                        column: x => x.CustomerSubscriptionId,
                        principalTable: "T_CustomerSubscription",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_T_TrainingsCustomers_T_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "T_Customers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContentSections_ContentTypeID",
                table: "ContentSections",
                column: "ContentTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_T_AvailableTrainings_TrainingId",
                table: "T_AvailableTrainings",
                column: "TrainingId");

            migrationBuilder.CreateIndex(
                name: "IX_T_CustomerFixedTrainings_CustomerId",
                table: "T_CustomerFixedTrainings",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_T_CustomerFixedTrainings_TrainingId",
                table: "T_CustomerFixedTrainings",
                column: "TrainingId");

            migrationBuilder.CreateIndex(
                name: "IX_T_CustomerHMOS_CustomerID",
                table: "T_CustomerHMOS",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_T_CustomerHMOS_HMOID",
                table: "T_CustomerHMOS",
                column: "HMOID");

            migrationBuilder.CreateIndex(
                name: "IX_T_Customers_CustomerTypeId",
                table: "T_Customers",
                column: "CustomerTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_T_Customers_HMOId",
                table: "T_Customers",
                column: "HMOId");

            migrationBuilder.CreateIndex(
                name: "IX_T_Customers_PaymentOptionId",
                table: "T_Customers",
                column: "PaymentOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_T_Customers_SubscriptionTypeId",
                table: "T_Customers",
                column: "SubscriptionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_T_CustomerSubscription_CustomerID",
                table: "T_CustomerSubscription",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_T_CustomerSubscription_SubscriptionTypeId",
                table: "T_CustomerSubscription",
                column: "SubscriptionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_T_LeumitCommitments_CommitmentTypeId",
                table: "T_LeumitCommitments",
                column: "CommitmentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_T_LeumitCommitments_CustomerId",
                table: "T_LeumitCommitments",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_T_LeumitCommitments_FileUploadId",
                table: "T_LeumitCommitments",
                column: "FileUploadId");

            migrationBuilder.CreateIndex(
                name: "IX_T_TrainingCustomerTypes_CustomerTypeID",
                table: "T_TrainingCustomerTypes",
                column: "CustomerTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_T_TrainingCustomerTypes_TrainingTypeId",
                table: "T_TrainingCustomerTypes",
                column: "TrainingTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_T_Trainings_TrainerID",
                table: "T_Trainings",
                column: "TrainerID");

            migrationBuilder.CreateIndex(
                name: "IX_T_Trainings_TrainingCustomerTypeId",
                table: "T_Trainings",
                column: "TrainingCustomerTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_T_TrainingsCustomers_CustomerID",
                table: "T_TrainingsCustomers",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_T_TrainingsCustomers_CustomerSubscriptionId",
                table: "T_TrainingsCustomers",
                column: "CustomerSubscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_T_TrainingsCustomers_TrainingID",
                table: "T_TrainingsCustomers",
                column: "TrainingID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContentSections");

            migrationBuilder.DropTable(
                name: "InternalHomeLinks");

            migrationBuilder.DropTable(
                name: "Login");

            migrationBuilder.DropTable(
                name: "T_Contacts");

            migrationBuilder.DropTable(
                name: "T_CustomerFixedTrainings");

            migrationBuilder.DropTable(
                name: "T_CustomerHMOS");

            migrationBuilder.DropTable(
                name: "T_LeumitCommitments");

            migrationBuilder.DropTable(
                name: "T_TrainingsCustomers");

            migrationBuilder.DropTable(
                name: "ContentTypes");

            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.DropTable(
                name: "T_LeumitCommimentTypes");

            migrationBuilder.DropTable(
                name: "T_AvailableTrainings");

            migrationBuilder.DropTable(
                name: "T_CustomerSubscription");

            migrationBuilder.DropTable(
                name: "T_Trainings");

            migrationBuilder.DropTable(
                name: "T_Customers");

            migrationBuilder.DropTable(
                name: "T_Trainers");

            migrationBuilder.DropTable(
                name: "T_TrainingCustomerTypes");

            migrationBuilder.DropTable(
                name: "T_HMOs");

            migrationBuilder.DropTable(
                name: "T_PaymentOptions");

            migrationBuilder.DropTable(
                name: "T_SubscriptionTypes");

            migrationBuilder.DropTable(
                name: "T_CustomerTypes");

            migrationBuilder.DropTable(
                name: "T_TrainigTypes");
        }
    }
}
