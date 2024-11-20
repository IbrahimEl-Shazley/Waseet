using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wasit.Context.Migrations
{
    /// <inheritdoc />
    public partial class Phase3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactUs");

            migrationBuilder.DropColumn(
                name: "ProjectName",
                table: "DeviceIds");

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedById = table.Column<long>(type: "bigint", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedById = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedById = table.Column<long>(type: "bigint", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NameAr = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    NameEn = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MaintenanceOrders",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceCost = table.Column<double>(type: "float", nullable: false),
                    TypePay = table.Column<int>(type: "int", nullable: false),
                    IsPay = table.Column<bool>(type: "bit", nullable: false),
                    EstateNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstateName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstateImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstateArea = table.Column<double>(type: "float", nullable: false),
                    OrderStatus = table.Column<int>(type: "int", nullable: false),
                    UserRate = table.Column<double>(type: "float", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ProviderId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    RegionId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedById = table.Column<long>(type: "bigint", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedById = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedById = table.Column<long>(type: "bigint", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaintenanceOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaintenanceOrders_AspNetUsers_ProviderId",
                        column: x => x.ProviderId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MaintenanceOrders_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MaintenanceOrders_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "P4AddPriceToEstates",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubscriptionDays = table.Column<int>(type: "int", nullable: false),
                    AddPriceCount = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    DescriptionAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedById = table.Column<long>(type: "bigint", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedById = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedById = table.Column<long>(type: "bigint", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NameAr = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    NameEn = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_P4AddPriceToEstates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RentalManagementOrders",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EstateNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstateName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstateImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderStatus = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    RegionId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedById = table.Column<long>(type: "bigint", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedById = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedById = table.Column<long>(type: "bigint", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentalManagementOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RentalManagementOrders_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RentalManagementOrders_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "S4AddPriceToEstates",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PNameAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PNameEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PSubscriptionDays = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    PDescriptionAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PDescriptionEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubscriptionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpireDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AddPriceCount = table.Column<int>(type: "int", nullable: false),
                    RemainingAddPriceCount = table.Column<int>(type: "int", nullable: false),
                    TypePay = table.Column<int>(type: "int", nullable: false),
                    IsPay = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedById = table.Column<long>(type: "bigint", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedById = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedById = table.Column<long>(type: "bigint", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_S4AddPriceToEstates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_S4AddPriceToEstates_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServiceCost = table.Column<double>(type: "float", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedById = table.Column<long>(type: "bigint", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedById = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedById = table.Column<long>(type: "bigint", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Specifications",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedById = table.Column<long>(type: "bigint", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedById = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedById = table.Column<long>(type: "bigint", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NameAr = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    NameEn = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EstateTypes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedById = table.Column<long>(type: "bigint", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedById = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedById = table.Column<long>(type: "bigint", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NameAr = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    NameEn = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstateTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EstateTypes_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EstateApartments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApartmentNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApartmentName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RentalPrice = table.Column<double>(type: "float", nullable: false),
                    PaymentDeadline = table.Column<int>(type: "int", nullable: false),
                    IsRentPaid = table.Column<bool>(type: "bit", nullable: false),
                    RentalOrderId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedById = table.Column<long>(type: "bigint", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedById = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedById = table.Column<long>(type: "bigint", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstateApartments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EstateApartments_RentalManagementOrders_RentalOrderId",
                        column: x => x.RentalOrderId,
                        principalTable: "RentalManagementOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DailyRentEstates",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EstateNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstateName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lng = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstateArea = table.Column<double>(type: "float", nullable: false),
                    EstateDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstateFeatures = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DayRentPrice = table.Column<double>(type: "float", nullable: false),
                    Developable = table.Column<bool>(type: "bit", nullable: false),
                    IsShow = table.Column<bool>(type: "bit", nullable: false),
                    IsRented = table.Column<bool>(type: "bit", nullable: false),
                    BookingFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BookingTo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstateTypeId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    RegionId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedById = table.Column<long>(type: "bigint", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedById = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedById = table.Column<long>(type: "bigint", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyRentEstates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DailyRentEstates_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DailyRentEstates_EstateTypes_EstateTypeId",
                        column: x => x.EstateTypeId,
                        principalTable: "EstateTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DailyRentEstates_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EntertainmentEstates",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EstateNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstateName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lng = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstateArea = table.Column<double>(type: "float", nullable: false),
                    EstateDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstateFeatures = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DayRentPrice = table.Column<double>(type: "float", nullable: false),
                    Developable = table.Column<bool>(type: "bit", nullable: false),
                    IsShow = table.Column<bool>(type: "bit", nullable: false),
                    IsRented = table.Column<bool>(type: "bit", nullable: false),
                    BookingFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BookingTo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstateTypeId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    RegionId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedById = table.Column<long>(type: "bigint", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedById = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedById = table.Column<long>(type: "bigint", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntertainmentEstates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntertainmentEstates_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EntertainmentEstates_EstateTypes_EstateTypeId",
                        column: x => x.EstateTypeId,
                        principalTable: "EstateTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EntertainmentEstates_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EstateTypeSpecifications",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EstateTypeId = table.Column<long>(type: "bigint", nullable: false),
                    SpecificationId = table.Column<long>(type: "bigint", nullable: false),
                    IsRequired = table.Column<bool>(type: "bit", nullable: false),
                    CreatedById = table.Column<long>(type: "bigint", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedById = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedById = table.Column<long>(type: "bigint", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstateTypeSpecifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EstateTypeSpecifications_EstateTypes_EstateTypeId",
                        column: x => x.EstateTypeId,
                        principalTable: "EstateTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EstateTypeSpecifications_Specifications_SpecificationId",
                        column: x => x.SpecificationId,
                        principalTable: "Specifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RentEstates",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EstateNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstateName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lng = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstateArea = table.Column<double>(type: "float", nullable: false),
                    EstateDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstateFeatures = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MonthRentPrice = table.Column<double>(type: "float", nullable: false),
                    Developable = table.Column<bool>(type: "bit", nullable: false),
                    IsShow = table.Column<bool>(type: "bit", nullable: false),
                    IsReserved = table.Column<bool>(type: "bit", nullable: false),
                    IsRented = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    EstateTypeId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    RegionId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedById = table.Column<long>(type: "bigint", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedById = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedById = table.Column<long>(type: "bigint", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentEstates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RentEstates_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RentEstates_EstateTypes_EstateTypeId",
                        column: x => x.EstateTypeId,
                        principalTable: "EstateTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RentEstates_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SaleEstates",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EstateNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstateName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lng = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstateArea = table.Column<double>(type: "float", nullable: false),
                    EstateDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstateFeatures = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstatePrice = table.Column<double>(type: "float", nullable: false),
                    Deposit = table.Column<double>(type: "float", nullable: false),
                    Developable = table.Column<bool>(type: "bit", nullable: false),
                    IsShow = table.Column<bool>(type: "bit", nullable: false),
                    IsReviewed = table.Column<bool>(type: "bit", nullable: false),
                    IsReserved = table.Column<bool>(type: "bit", nullable: false),
                    IsSold = table.Column<bool>(type: "bit", nullable: false),
                    EstateTypeId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    RegionId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedById = table.Column<long>(type: "bigint", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedById = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedById = table.Column<long>(type: "bigint", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleEstates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SaleEstates_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SaleEstates_EstateTypes_EstateTypeId",
                        column: x => x.EstateTypeId,
                        principalTable: "EstateTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SaleEstates_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApartmentRentPays",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RentalPrice = table.Column<double>(type: "float", nullable: false),
                    AppTax = table.Column<double>(type: "float", nullable: false),
                    TypePay = table.Column<int>(type: "int", nullable: false),
                    IsPay = table.Column<bool>(type: "bit", nullable: false),
                    EstateApartmentId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedById = table.Column<long>(type: "bigint", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedById = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedById = table.Column<long>(type: "bigint", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApartmentRentPays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApartmentRentPays_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ApartmentRentPays_EstateApartments_EstateApartmentId",
                        column: x => x.EstateApartmentId,
                        principalTable: "EstateApartments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DailyRentEstateFavorites",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DailyRentEstateId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedById = table.Column<long>(type: "bigint", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedById = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedById = table.Column<long>(type: "bigint", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyRentEstateFavorites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DailyRentEstateFavorites_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DailyRentEstateFavorites_DailyRentEstates_DailyRentEstateId",
                        column: x => x.DailyRentEstateId,
                        principalTable: "DailyRentEstates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DailyRentEstateImages",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DailyRentEstateId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedById = table.Column<long>(type: "bigint", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedById = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedById = table.Column<long>(type: "bigint", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyRentEstateImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DailyRentEstateImages_DailyRentEstates_DailyRentEstateId",
                        column: x => x.DailyRentEstateId,
                        principalTable: "DailyRentEstates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DailyRentRequests",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CancelStatus = table.Column<int>(type: "int", nullable: false),
                    CancelDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ArrivalDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LeaveDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalDays = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<double>(type: "float", nullable: false),
                    TypePay = table.Column<int>(type: "int", nullable: false),
                    IsPay = table.Column<bool>(type: "bit", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsEstateReceived = table.Column<bool>(type: "bit", nullable: false),
                    UserRate = table.Column<double>(type: "float", nullable: false),
                    EstateRate = table.Column<double>(type: "float", nullable: false),
                    EstateComment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShowComment = table.Column<bool>(type: "bit", nullable: false),
                    DailyRentEstateId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedById = table.Column<long>(type: "bigint", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedById = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedById = table.Column<long>(type: "bigint", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyRentRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DailyRentRequests_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DailyRentRequests_DailyRentEstates_DailyRentEstateId",
                        column: x => x.DailyRentEstateId,
                        principalTable: "DailyRentEstates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EntertainmentEstateFavorites",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntertainmentEstateId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedById = table.Column<long>(type: "bigint", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedById = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedById = table.Column<long>(type: "bigint", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntertainmentEstateFavorites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntertainmentEstateFavorites_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EntertainmentEstateFavorites_EntertainmentEstates_EntertainmentEstateId",
                        column: x => x.EntertainmentEstateId,
                        principalTable: "EntertainmentEstates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EntertainmentEstateImages",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EntertainmentEstateId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedById = table.Column<long>(type: "bigint", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedById = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedById = table.Column<long>(type: "bigint", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntertainmentEstateImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntertainmentEstateImages_EntertainmentEstates_EntertainmentEstateId",
                        column: x => x.EntertainmentEstateId,
                        principalTable: "EntertainmentEstates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EntertainmentRequests",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CancelStatus = table.Column<int>(type: "int", nullable: false),
                    CancelDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ArrivalDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LeaveDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalDays = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<double>(type: "float", nullable: false),
                    TypePay = table.Column<int>(type: "int", nullable: false),
                    IsPay = table.Column<bool>(type: "bit", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsEstateReceived = table.Column<bool>(type: "bit", nullable: false),
                    UserRate = table.Column<double>(type: "float", nullable: false),
                    EstateRate = table.Column<double>(type: "float", nullable: false),
                    EstateComment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShowComment = table.Column<bool>(type: "bit", nullable: false),
                    EntertainmentEstateId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedById = table.Column<long>(type: "bigint", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedById = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedById = table.Column<long>(type: "bigint", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntertainmentRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntertainmentRequests_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EntertainmentRequests_EntertainmentEstates_EntertainmentEstateId",
                        column: x => x.EntertainmentEstateId,
                        principalTable: "EntertainmentEstates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DailyRentEstateSpecificationValues",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SpecificationValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DailyRentEstateId = table.Column<long>(type: "bigint", nullable: false),
                    EstateTypeSpecificationId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedById = table.Column<long>(type: "bigint", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedById = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedById = table.Column<long>(type: "bigint", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyRentEstateSpecificationValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DailyRentEstateSpecificationValues_DailyRentEstates_DailyRentEstateId",
                        column: x => x.DailyRentEstateId,
                        principalTable: "DailyRentEstates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DailyRentEstateSpecificationValues_EstateTypeSpecifications_EstateTypeSpecificationId",
                        column: x => x.EstateTypeSpecificationId,
                        principalTable: "EstateTypeSpecifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EntertainmentEstateSpecificationValues",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SpecificationValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EntertainmentEstateId = table.Column<long>(type: "bigint", nullable: false),
                    EstateTypeSpecificationId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedById = table.Column<long>(type: "bigint", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedById = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedById = table.Column<long>(type: "bigint", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntertainmentEstateSpecificationValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntertainmentEstateSpecificationValues_EntertainmentEstates_EntertainmentEstateId",
                        column: x => x.EntertainmentEstateId,
                        principalTable: "EntertainmentEstates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EntertainmentEstateSpecificationValues_EstateTypeSpecifications_EstateTypeSpecificationId",
                        column: x => x.EstateTypeSpecificationId,
                        principalTable: "EstateTypeSpecifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RentEstateFavorites",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RentEstateId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedById = table.Column<long>(type: "bigint", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedById = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedById = table.Column<long>(type: "bigint", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentEstateFavorites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RentEstateFavorites_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RentEstateFavorites_RentEstates_RentEstateId",
                        column: x => x.RentEstateId,
                        principalTable: "RentEstates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RentEstateImages",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RentEstateId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedById = table.Column<long>(type: "bigint", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedById = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedById = table.Column<long>(type: "bigint", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentEstateImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RentEstateImages_RentEstates_RentEstateId",
                        column: x => x.RentEstateId,
                        principalTable: "RentEstates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RentEstateSpecificationValues",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SpecificationValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RentEstateId = table.Column<long>(type: "bigint", nullable: false),
                    EstateTypeSpecificationId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedById = table.Column<long>(type: "bigint", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedById = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedById = table.Column<long>(type: "bigint", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentEstateSpecificationValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RentEstateSpecificationValues_EstateTypeSpecifications_EstateTypeSpecificationId",
                        column: x => x.EstateTypeSpecificationId,
                        principalTable: "EstateTypeSpecifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RentEstateSpecificationValues_RentEstates_RentEstateId",
                        column: x => x.RentEstateId,
                        principalTable: "RentEstates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RentRatingRequests",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RatingStatus = table.Column<int>(type: "int", nullable: false),
                    ServiceCost = table.Column<double>(type: "float", nullable: false),
                    TypePay = table.Column<int>(type: "int", nullable: false),
                    IsPay = table.Column<bool>(type: "bit", nullable: false),
                    ReportUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserRate = table.Column<double>(type: "float", nullable: false),
                    RentEstateId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ProviderId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedById = table.Column<long>(type: "bigint", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedById = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedById = table.Column<long>(type: "bigint", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentRatingRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RentRatingRequests_AspNetUsers_ProviderId",
                        column: x => x.ProviderId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RentRatingRequests_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RentRatingRequests_RentEstates_RentEstateId",
                        column: x => x.RentEstateId,
                        principalTable: "RentEstates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RentRequests",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<int>(type: "int", nullable: false),
                    YearCount = table.Column<int>(type: "int", nullable: false),
                    MonthCount = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<double>(type: "float", nullable: false),
                    Deposit = table.Column<double>(type: "float", nullable: false),
                    TypePay = table.Column<int>(type: "int", nullable: false),
                    IsPay = table.Column<bool>(type: "bit", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsEstateRented = table.Column<bool>(type: "bit", nullable: false),
                    UserRate = table.Column<double>(type: "float", nullable: false),
                    RentEstateId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedById = table.Column<long>(type: "bigint", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedById = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedById = table.Column<long>(type: "bigint", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RentRequests_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RentRequests_RentEstates_RentEstateId",
                        column: x => x.RentEstateId,
                        principalTable: "RentEstates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RentReservationRequests",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReservationStatus = table.Column<int>(type: "int", nullable: false),
                    ServiceCost = table.Column<double>(type: "float", nullable: false),
                    TypePay = table.Column<int>(type: "int", nullable: false),
                    IsPay = table.Column<bool>(type: "bit", nullable: false),
                    ReservationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RentEstateId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedById = table.Column<long>(type: "bigint", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedById = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedById = table.Column<long>(type: "bigint", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentReservationRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RentReservationRequests_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RentReservationRequests_RentEstates_RentEstateId",
                        column: x => x.RentEstateId,
                        principalTable: "RentEstates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PricingRequests",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdminPrice = table.Column<double>(type: "float", nullable: false),
                    ServiceCost = table.Column<double>(type: "float", nullable: false),
                    TypePay = table.Column<int>(type: "int", nullable: false),
                    IsPay = table.Column<bool>(type: "bit", nullable: false),
                    SaleEstateId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedById = table.Column<long>(type: "bigint", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedById = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedById = table.Column<long>(type: "bigint", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PricingRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PricingRequests_SaleEstates_SaleEstateId",
                        column: x => x.SaleEstateId,
                        principalTable: "SaleEstates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseRequests",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Deposit = table.Column<double>(type: "float", nullable: false),
                    TypePay = table.Column<int>(type: "int", nullable: false),
                    IsPay = table.Column<bool>(type: "bit", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HasRefundRequest = table.Column<bool>(type: "bit", nullable: false),
                    RefundReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsAccepted = table.Column<bool>(type: "bit", nullable: false),
                    IsEstateReceived = table.Column<bool>(type: "bit", nullable: false),
                    SaleEstateId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedById = table.Column<long>(type: "bigint", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedById = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedById = table.Column<long>(type: "bigint", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseRequests_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PurchaseRequests_SaleEstates_SaleEstateId",
                        column: x => x.SaleEstateId,
                        principalTable: "SaleEstates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SaleEstateFavorites",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SaleEstateId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedById = table.Column<long>(type: "bigint", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedById = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedById = table.Column<long>(type: "bigint", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleEstateFavorites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SaleEstateFavorites_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SaleEstateFavorites_SaleEstates_SaleEstateId",
                        column: x => x.SaleEstateId,
                        principalTable: "SaleEstates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SaleEstateImages",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SaleEstateId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedById = table.Column<long>(type: "bigint", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedById = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedById = table.Column<long>(type: "bigint", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleEstateImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SaleEstateImages_SaleEstates_SaleEstateId",
                        column: x => x.SaleEstateId,
                        principalTable: "SaleEstates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SaleEstateSpecificationValues",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SpecificationValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SaleEstateId = table.Column<long>(type: "bigint", nullable: false),
                    EstateTypeSpecificationId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedById = table.Column<long>(type: "bigint", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedById = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedById = table.Column<long>(type: "bigint", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleEstateSpecificationValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SaleEstateSpecificationValues_EstateTypeSpecifications_EstateTypeSpecificationId",
                        column: x => x.EstateTypeSpecificationId,
                        principalTable: "EstateTypeSpecifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SaleEstateSpecificationValues_SaleEstates_SaleEstateId",
                        column: x => x.SaleEstateId,
                        principalTable: "SaleEstates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SaleRatingRequests",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RatingStatus = table.Column<int>(type: "int", nullable: false),
                    ServiceCost = table.Column<double>(type: "float", nullable: false),
                    TypePay = table.Column<int>(type: "int", nullable: false),
                    IsPay = table.Column<bool>(type: "bit", nullable: false),
                    ReportUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserRate = table.Column<double>(type: "float", nullable: false),
                    SaleEstateId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ProviderId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedById = table.Column<long>(type: "bigint", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedById = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedById = table.Column<long>(type: "bigint", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleRatingRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SaleRatingRequests_AspNetUsers_ProviderId",
                        column: x => x.ProviderId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SaleRatingRequests_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SaleRatingRequests_SaleEstates_SaleEstateId",
                        column: x => x.SaleEstateId,
                        principalTable: "SaleEstates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SaleReservationRequests",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReservationStatus = table.Column<int>(type: "int", nullable: false),
                    ServiceCost = table.Column<double>(type: "float", nullable: false),
                    TypePay = table.Column<int>(type: "int", nullable: false),
                    IsPay = table.Column<bool>(type: "bit", nullable: false),
                    ReservationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SaleEstateId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedById = table.Column<long>(type: "bigint", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedById = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedById = table.Column<long>(type: "bigint", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleReservationRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SaleReservationRequests_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SaleReservationRequests_SaleEstates_SaleEstateId",
                        column: x => x.SaleEstateId,
                        principalTable: "SaleEstates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserPriceToEstates",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<double>(type: "float", nullable: false),
                    SaleEstateId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedById = table.Column<long>(type: "bigint", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedById = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedById = table.Column<long>(type: "bigint", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPriceToEstates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserPriceToEstates_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserPriceToEstates_SaleEstates_SaleEstateId",
                        column: x => x.SaleEstateId,
                        principalTable: "SaleEstates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReportDailyRentComments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReasonForReport = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DailyRentRequestId = table.Column<long>(type: "bigint", nullable: false),
                    ReporterId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedById = table.Column<long>(type: "bigint", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedById = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedById = table.Column<long>(type: "bigint", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportDailyRentComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportDailyRentComments_AspNetUsers_ReporterId",
                        column: x => x.ReporterId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReportDailyRentComments_DailyRentRequests_DailyRentRequestId",
                        column: x => x.DailyRentRequestId,
                        principalTable: "DailyRentRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReportEntertainmentComments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReasonForReport = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EntertainmentRequestId = table.Column<long>(type: "bigint", nullable: false),
                    ReporterId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedById = table.Column<long>(type: "bigint", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedById = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedById = table.Column<long>(type: "bigint", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportEntertainmentComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportEntertainmentComments_AspNetUsers_ReporterId",
                        column: x => x.ReporterId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReportEntertainmentComments_EntertainmentRequests_EntertainmentRequestId",
                        column: x => x.EntertainmentRequestId,
                        principalTable: "EntertainmentRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApartmentRentPays_EstateApartmentId",
                table: "ApartmentRentPays",
                column: "EstateApartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ApartmentRentPays_UserId",
                table: "ApartmentRentPays",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyRentEstateFavorites_DailyRentEstateId",
                table: "DailyRentEstateFavorites",
                column: "DailyRentEstateId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyRentEstateFavorites_UserId",
                table: "DailyRentEstateFavorites",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyRentEstateImages_DailyRentEstateId",
                table: "DailyRentEstateImages",
                column: "DailyRentEstateId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyRentEstates_EstateTypeId",
                table: "DailyRentEstates",
                column: "EstateTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyRentEstates_RegionId",
                table: "DailyRentEstates",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyRentEstates_UserId",
                table: "DailyRentEstates",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyRentEstateSpecificationValues_DailyRentEstateId",
                table: "DailyRentEstateSpecificationValues",
                column: "DailyRentEstateId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyRentEstateSpecificationValues_EstateTypeSpecificationId",
                table: "DailyRentEstateSpecificationValues",
                column: "EstateTypeSpecificationId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyRentRequests_DailyRentEstateId",
                table: "DailyRentRequests",
                column: "DailyRentEstateId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyRentRequests_UserId",
                table: "DailyRentRequests",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_EntertainmentEstateFavorites_EntertainmentEstateId",
                table: "EntertainmentEstateFavorites",
                column: "EntertainmentEstateId");

            migrationBuilder.CreateIndex(
                name: "IX_EntertainmentEstateFavorites_UserId",
                table: "EntertainmentEstateFavorites",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_EntertainmentEstateImages_EntertainmentEstateId",
                table: "EntertainmentEstateImages",
                column: "EntertainmentEstateId");

            migrationBuilder.CreateIndex(
                name: "IX_EntertainmentEstates_EstateTypeId",
                table: "EntertainmentEstates",
                column: "EstateTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EntertainmentEstates_RegionId",
                table: "EntertainmentEstates",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_EntertainmentEstates_UserId",
                table: "EntertainmentEstates",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_EntertainmentEstateSpecificationValues_EntertainmentEstateId",
                table: "EntertainmentEstateSpecificationValues",
                column: "EntertainmentEstateId");

            migrationBuilder.CreateIndex(
                name: "IX_EntertainmentEstateSpecificationValues_EstateTypeSpecificationId",
                table: "EntertainmentEstateSpecificationValues",
                column: "EstateTypeSpecificationId");

            migrationBuilder.CreateIndex(
                name: "IX_EntertainmentRequests_EntertainmentEstateId",
                table: "EntertainmentRequests",
                column: "EntertainmentEstateId");

            migrationBuilder.CreateIndex(
                name: "IX_EntertainmentRequests_UserId",
                table: "EntertainmentRequests",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_EstateApartments_RentalOrderId",
                table: "EstateApartments",
                column: "RentalOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_EstateTypes_CategoryId",
                table: "EstateTypes",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_EstateTypeSpecifications_EstateTypeId",
                table: "EstateTypeSpecifications",
                column: "EstateTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EstateTypeSpecifications_SpecificationId",
                table: "EstateTypeSpecifications",
                column: "SpecificationId");

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceOrders_ProviderId",
                table: "MaintenanceOrders",
                column: "ProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceOrders_RegionId",
                table: "MaintenanceOrders",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceOrders_UserId",
                table: "MaintenanceOrders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PricingRequests_SaleEstateId",
                table: "PricingRequests",
                column: "SaleEstateId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseRequests_SaleEstateId",
                table: "PurchaseRequests",
                column: "SaleEstateId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseRequests_UserId",
                table: "PurchaseRequests",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RentalManagementOrders_RegionId",
                table: "RentalManagementOrders",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_RentalManagementOrders_UserId",
                table: "RentalManagementOrders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RentEstateFavorites_RentEstateId",
                table: "RentEstateFavorites",
                column: "RentEstateId");

            migrationBuilder.CreateIndex(
                name: "IX_RentEstateFavorites_UserId",
                table: "RentEstateFavorites",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RentEstateImages_RentEstateId",
                table: "RentEstateImages",
                column: "RentEstateId");

            migrationBuilder.CreateIndex(
                name: "IX_RentEstates_EstateTypeId",
                table: "RentEstates",
                column: "EstateTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_RentEstates_RegionId",
                table: "RentEstates",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_RentEstates_UserId",
                table: "RentEstates",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RentEstateSpecificationValues_EstateTypeSpecificationId",
                table: "RentEstateSpecificationValues",
                column: "EstateTypeSpecificationId");

            migrationBuilder.CreateIndex(
                name: "IX_RentEstateSpecificationValues_RentEstateId",
                table: "RentEstateSpecificationValues",
                column: "RentEstateId");

            migrationBuilder.CreateIndex(
                name: "IX_RentRatingRequests_ProviderId",
                table: "RentRatingRequests",
                column: "ProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_RentRatingRequests_RentEstateId",
                table: "RentRatingRequests",
                column: "RentEstateId");

            migrationBuilder.CreateIndex(
                name: "IX_RentRatingRequests_UserId",
                table: "RentRatingRequests",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RentRequests_RentEstateId",
                table: "RentRequests",
                column: "RentEstateId");

            migrationBuilder.CreateIndex(
                name: "IX_RentRequests_UserId",
                table: "RentRequests",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RentReservationRequests_RentEstateId",
                table: "RentReservationRequests",
                column: "RentEstateId");

            migrationBuilder.CreateIndex(
                name: "IX_RentReservationRequests_UserId",
                table: "RentReservationRequests",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportDailyRentComments_DailyRentRequestId",
                table: "ReportDailyRentComments",
                column: "DailyRentRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportDailyRentComments_ReporterId",
                table: "ReportDailyRentComments",
                column: "ReporterId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportEntertainmentComments_EntertainmentRequestId",
                table: "ReportEntertainmentComments",
                column: "EntertainmentRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportEntertainmentComments_ReporterId",
                table: "ReportEntertainmentComments",
                column: "ReporterId");

            migrationBuilder.CreateIndex(
                name: "IX_S4AddPriceToEstates_UserId",
                table: "S4AddPriceToEstates",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleEstateFavorites_SaleEstateId",
                table: "SaleEstateFavorites",
                column: "SaleEstateId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleEstateFavorites_UserId",
                table: "SaleEstateFavorites",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleEstateImages_SaleEstateId",
                table: "SaleEstateImages",
                column: "SaleEstateId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleEstates_EstateTypeId",
                table: "SaleEstates",
                column: "EstateTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleEstates_RegionId",
                table: "SaleEstates",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleEstates_UserId",
                table: "SaleEstates",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleEstateSpecificationValues_EstateTypeSpecificationId",
                table: "SaleEstateSpecificationValues",
                column: "EstateTypeSpecificationId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleEstateSpecificationValues_SaleEstateId",
                table: "SaleEstateSpecificationValues",
                column: "SaleEstateId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleRatingRequests_ProviderId",
                table: "SaleRatingRequests",
                column: "ProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleRatingRequests_SaleEstateId",
                table: "SaleRatingRequests",
                column: "SaleEstateId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleRatingRequests_UserId",
                table: "SaleRatingRequests",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleReservationRequests_SaleEstateId",
                table: "SaleReservationRequests",
                column: "SaleEstateId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleReservationRequests_UserId",
                table: "SaleReservationRequests",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPriceToEstates_SaleEstateId",
                table: "UserPriceToEstates",
                column: "SaleEstateId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPriceToEstates_UserId",
                table: "UserPriceToEstates",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApartmentRentPays");

            migrationBuilder.DropTable(
                name: "DailyRentEstateFavorites");

            migrationBuilder.DropTable(
                name: "DailyRentEstateImages");

            migrationBuilder.DropTable(
                name: "DailyRentEstateSpecificationValues");

            migrationBuilder.DropTable(
                name: "EntertainmentEstateFavorites");

            migrationBuilder.DropTable(
                name: "EntertainmentEstateImages");

            migrationBuilder.DropTable(
                name: "EntertainmentEstateSpecificationValues");

            migrationBuilder.DropTable(
                name: "MaintenanceOrders");

            migrationBuilder.DropTable(
                name: "P4AddPriceToEstates");

            migrationBuilder.DropTable(
                name: "PricingRequests");

            migrationBuilder.DropTable(
                name: "PurchaseRequests");

            migrationBuilder.DropTable(
                name: "RentEstateFavorites");

            migrationBuilder.DropTable(
                name: "RentEstateImages");

            migrationBuilder.DropTable(
                name: "RentEstateSpecificationValues");

            migrationBuilder.DropTable(
                name: "RentRatingRequests");

            migrationBuilder.DropTable(
                name: "RentRequests");

            migrationBuilder.DropTable(
                name: "RentReservationRequests");

            migrationBuilder.DropTable(
                name: "ReportDailyRentComments");

            migrationBuilder.DropTable(
                name: "ReportEntertainmentComments");

            migrationBuilder.DropTable(
                name: "S4AddPriceToEstates");

            migrationBuilder.DropTable(
                name: "SaleEstateFavorites");

            migrationBuilder.DropTable(
                name: "SaleEstateImages");

            migrationBuilder.DropTable(
                name: "SaleEstateSpecificationValues");

            migrationBuilder.DropTable(
                name: "SaleRatingRequests");

            migrationBuilder.DropTable(
                name: "SaleReservationRequests");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "UserPriceToEstates");

            migrationBuilder.DropTable(
                name: "EstateApartments");

            migrationBuilder.DropTable(
                name: "RentEstates");

            migrationBuilder.DropTable(
                name: "DailyRentRequests");

            migrationBuilder.DropTable(
                name: "EntertainmentRequests");

            migrationBuilder.DropTable(
                name: "EstateTypeSpecifications");

            migrationBuilder.DropTable(
                name: "SaleEstates");

            migrationBuilder.DropTable(
                name: "RentalManagementOrders");

            migrationBuilder.DropTable(
                name: "DailyRentEstates");

            migrationBuilder.DropTable(
                name: "EntertainmentEstates");

            migrationBuilder.DropTable(
                name: "Specifications");

            migrationBuilder.DropTable(
                name: "EstateTypes");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.AddColumn<string>(
                name: "ProjectName",
                table: "DeviceIds",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ContactUs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Msg = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactUs", x => x.Id);
                });
        }
    }
}
