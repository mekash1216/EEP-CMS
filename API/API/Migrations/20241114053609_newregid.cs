using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class newregid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bacteriologies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AFB = table.Column<bool>(type: "bit", nullable: false),
                    WetSmear = table.Column<bool>(type: "bit", nullable: false),
                    GramStrin = table.Column<bool>(type: "bit", nullable: false),
                    KOH = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bacteriologies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Biochemistries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FBS = table.Column<bool>(type: "bit", nullable: false),
                    RBS = table.Column<bool>(type: "bit", nullable: false),
                    BUN = table.Column<bool>(type: "bit", nullable: false),
                    Creatinine = table.Column<bool>(type: "bit", nullable: false),
                    SGOT = table.Column<bool>(type: "bit", nullable: false),
                    SGPT = table.Column<bool>(type: "bit", nullable: false),
                    AlkaPho = table.Column<bool>(type: "bit", nullable: false),
                    TBilirubin = table.Column<bool>(type: "bit", nullable: false),
                    DBilirubin = table.Column<bool>(type: "bit", nullable: false),
                    Albumin = table.Column<bool>(type: "bit", nullable: false),
                    UricAcid = table.Column<bool>(type: "bit", nullable: false),
                    TProtein = table.Column<bool>(type: "bit", nullable: false),
                    TCholesterol = table.Column<bool>(type: "bit", nullable: false),
                    HDL = table.Column<bool>(type: "bit", nullable: false),
                    LDL = table.Column<bool>(type: "bit", nullable: false),
                    Triglyceride = table.Column<bool>(type: "bit", nullable: false),
                    AMYLASE = table.Column<bool>(type: "bit", nullable: false),
                    GGT = table.Column<bool>(type: "bit", nullable: false),
                    Lipase = table.Column<bool>(type: "bit", nullable: false),
                    LDH = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Biochemistries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CancerMarkers",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CA125 = table.Column<bool>(type: "bit", nullable: false),
                    CA199 = table.Column<bool>(type: "bit", nullable: false),
                    CA153 = table.Column<bool>(type: "bit", nullable: false),
                    CEA = table.Column<bool>(type: "bit", nullable: false),
                    AFP = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CancerMarkers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "CardiacMarkers",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CKMB = table.Column<bool>(type: "bit", nullable: false),
                    TroponinT = table.Column<bool>(type: "bit", nullable: false),
                    DDimer = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardiacMarkers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ClinicRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientFirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientLastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientDepartment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Workplace = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsManager = table.Column<bool>(type: "bit", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Birthdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    age = table.Column<int>(type: "int", nullable: false),
                    RequestDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClinicRequests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Coagulations",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PT = table.Column<bool>(type: "bit", nullable: false),
                    APTT = table.Column<bool>(type: "bit", nullable: false),
                    INR = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coagulations", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Specialty = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Electrolytes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sodium = table.Column<bool>(type: "bit", nullable: false),
                    Potassium = table.Column<bool>(type: "bit", nullable: false),
                    Chloride = table.Column<bool>(type: "bit", nullable: false),
                    Calcium = table.Column<bool>(type: "bit", nullable: false),
                    Magnesium = table.Column<bool>(type: "bit", nullable: false),
                    Phosphorus = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Electrolytes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PatientFirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientLastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientDepartment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Workplace = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsManager = table.Column<bool>(type: "bit", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Birthdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    age = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                });

            migrationBuilder.CreateTable(
                name: "Hematologies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CBC = table.Column<bool>(type: "bit", nullable: false),
                    Hgb = table.Column<bool>(type: "bit", nullable: false),
                    ESR = table.Column<bool>(type: "bit", nullable: false),
                    Bloodgroup = table.Column<bool>(type: "bit", nullable: false),
                    Hba1c = table.Column<bool>(type: "bit", nullable: false),
                    BloodFilm = table.Column<bool>(type: "bit", nullable: false),
                    Periferialmorphology = table.Column<bool>(type: "bit", nullable: false),
                    MalariaByAgCard = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hematologies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Hormones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TSH = table.Column<bool>(type: "bit", nullable: false),
                    FreeT4 = table.Column<bool>(type: "bit", nullable: false),
                    FreeT3 = table.Column<bool>(type: "bit", nullable: false),
                    TotalT4 = table.Column<bool>(type: "bit", nullable: false),
                    TotalT3 = table.Column<bool>(type: "bit", nullable: false),
                    TotalBetaHCGT3 = table.Column<bool>(type: "bit", nullable: false),
                    PSA = table.Column<bool>(type: "bit", nullable: false),
                    FSH = table.Column<bool>(type: "bit", nullable: false),
                    LH = table.Column<bool>(type: "bit", nullable: false),
                    Prolactin = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hormones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ICD10Codes2024",
                columns: table => new
                {
                    CODE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SHORTDESCRIPTION = table.Column<string>(name: "SHORT DESCRIPTION", type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "LaboratoryTestResult",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Test = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Result = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReferenceRange = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Units = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Severity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    age = table.Column<int>(type: "int", nullable: false),
                    IsPregnant = table.Column<bool>(type: "bit", nullable: true),
                    Phase = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LaboratoryTestResult", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OtherLabs",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VitB12e = table.Column<bool>(type: "bit", nullable: false),
                    VitD = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OtherLabs", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Parasitologies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoolDirect = table.Column<bool>(type: "bit", nullable: false),
                    Urinalysis = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parasitologies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PhysicalAssessments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    HEENT = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Glands = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Chest = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CVS = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Abdomen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GenitoUninary = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MusculoSkeleton = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Skin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CNS = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Assessment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhysicalAssessments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PhysicalExaminations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    GeneralAppearance = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VitalSigns = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HeadAndNeck = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cardiovascular = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Respiratory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Abdomen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExaminationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhysicalExaminations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RegExaminers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Specialization = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegExaminers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Serologies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VDRL = table.Column<bool>(type: "bit", nullable: false),
                    Widal = table.Column<bool>(type: "bit", nullable: false),
                    WeilFelix = table.Column<bool>(type: "bit", nullable: false),
                    BloodGroup = table.Column<bool>(type: "bit", nullable: false),
                    XMatch = table.Column<bool>(type: "bit", nullable: false),
                    HBsAg = table.Column<bool>(type: "bit", nullable: false),
                    HBsAb = table.Column<bool>(type: "bit", nullable: false),
                    HPyloriAb = table.Column<bool>(type: "bit", nullable: false),
                    HPyloriAgStool = table.Column<bool>(type: "bit", nullable: false),
                    RF = table.Column<bool>(type: "bit", nullable: false),
                    ASO = table.Column<bool>(type: "bit", nullable: false),
                    CRP = table.Column<bool>(type: "bit", nullable: false),
                    HIV = table.Column<bool>(type: "bit", nullable: false),
                    FOB = table.Column<bool>(type: "bit", nullable: false),
                    HCG = table.Column<bool>(type: "bit", nullable: false),
                    HepatitisCViralLoad = table.Column<bool>(type: "bit", nullable: false),
                    HepatitisBViralLoad = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Serologies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stock",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Manufacturer = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stock", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Urinalyses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Colour = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SpGr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Protein = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Glucose = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Urobilinogen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Urobilin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Acetone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Blood = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RbcMicroscopicCasts = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WbcMicroscopicCasts = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EpthCell = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Granular = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HCG = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hyaline = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Urinalyses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UrineDipsticksTests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PH = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SpecificGravity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LeukocyteEsterase = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LeukocyteEsteraseIntensity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nitrite = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NitriteIntensity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Urobilinogen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UrobilinogenIntensity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Protein = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProteinIntensity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Glucose = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GlucoseIntensity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BloodHemoglobin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BloodHemoglobinIntensity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ketones = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KetonesIntensity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bilirubin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BilirubinIntensity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UrineDipsticksTests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cardNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    firstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    lastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    assigneddate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PatientDepartment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Workplace = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    age = table.Column<int>(type: "int", nullable: true),
                    phoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DoctorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Patients_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LaboratorySubTestResult",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Result = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReferenceRange = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Units = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Severity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LaboratoryTestResultId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LaboratorySubTestResult", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LaboratorySubTestResult_LaboratoryTestResult_LaboratoryTestResultId",
                        column: x => x.LaboratoryTestResultId,
                        principalTable: "LaboratoryTestResult",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccidentReports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InjuryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InjuryTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CauseOfInjury = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IncidentDetails = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PreventiveAction = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReporterName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartmentHeadName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccidentReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccidentReports_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Assignments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    AssignedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PatientAge = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<float>(type: "real", nullable: false),
                    SystolicPressure = table.Column<float>(type: "real", nullable: false),
                    DiastolicPressure = table.Column<float>(type: "real", nullable: false),
                    RespiratoryRate = table.Column<float>(type: "real", nullable: false),
                    PulseRate = table.Column<float>(type: "real", nullable: false),
                    Temperature = table.Column<float>(type: "real", nullable: false),
                    SpO2 = table.Column<float>(type: "real", nullable: false),
                    Triage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DoctorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Assignments_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Assignments_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BloodFilmTests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Test = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Consistency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Selectionone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CellCount = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloodFilmTests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BloodFilmTests_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LaboratoryRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateOfRequest = table.Column<DateTime>(type: "datetime2", nullable: false),
                    hospitalName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsUrinalysisChecked = table.Column<bool>(type: "bit", nullable: false),
                    IsBacteriologyChecked = table.Column<bool>(type: "bit", nullable: false),
                    IsBiochemistryChecked = table.Column<bool>(type: "bit", nullable: false),
                    IsHematologyChecked = table.Column<bool>(type: "bit", nullable: false),
                    IsParasitologyChecked = table.Column<bool>(type: "bit", nullable: false),
                    IsSerologyChecked = table.Column<bool>(type: "bit", nullable: false),
                    IsElectrolyteChecked = table.Column<bool>(type: "bit", nullable: false),
                    IsCancerMarkerChecked = table.Column<bool>(type: "bit", nullable: false),
                    IsCardiacMarkerChecked = table.Column<bool>(type: "bit", nullable: false),
                    IsCoagulationChecked = table.Column<bool>(type: "bit", nullable: false),
                    IsHormoneChecked = table.Column<bool>(type: "bit", nullable: false),
                    IsOtherLabChecked = table.Column<bool>(type: "bit", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    ParasitologyId = table.Column<int>(type: "int", nullable: true),
                    HematologyId = table.Column<int>(type: "int", nullable: true),
                    BiochemistryId = table.Column<int>(type: "int", nullable: true),
                    BacteriologyId = table.Column<int>(type: "int", nullable: true),
                    SerologyId = table.Column<int>(type: "int", nullable: true),
                    Electrolyteid = table.Column<int>(type: "int", nullable: true),
                    CancerMarkerid = table.Column<int>(type: "int", nullable: true),
                    CardiacMarkerid = table.Column<int>(type: "int", nullable: true),
                    Coagulationid = table.Column<int>(type: "int", nullable: true),
                    Hormoneid = table.Column<int>(type: "int", nullable: true),
                    OtherLabid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LaboratoryRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LaboratoryRequests_Bacteriologies_BacteriologyId",
                        column: x => x.BacteriologyId,
                        principalTable: "Bacteriologies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_LaboratoryRequests_Biochemistries_BiochemistryId",
                        column: x => x.BiochemistryId,
                        principalTable: "Biochemistries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_LaboratoryRequests_CancerMarkers_CancerMarkerid",
                        column: x => x.CancerMarkerid,
                        principalTable: "CancerMarkers",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_LaboratoryRequests_CardiacMarkers_CardiacMarkerid",
                        column: x => x.CardiacMarkerid,
                        principalTable: "CardiacMarkers",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_LaboratoryRequests_Coagulations_Coagulationid",
                        column: x => x.Coagulationid,
                        principalTable: "Coagulations",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_LaboratoryRequests_Electrolytes_Electrolyteid",
                        column: x => x.Electrolyteid,
                        principalTable: "Electrolytes",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_LaboratoryRequests_Hematologies_HematologyId",
                        column: x => x.HematologyId,
                        principalTable: "Hematologies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_LaboratoryRequests_Hormones_Hormoneid",
                        column: x => x.Hormoneid,
                        principalTable: "Hormones",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LaboratoryRequests_OtherLabs_OtherLabid",
                        column: x => x.OtherLabid,
                        principalTable: "OtherLabs",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_LaboratoryRequests_Parasitologies_ParasitologyId",
                        column: x => x.ParasitologyId,
                        principalTable: "Parasitologies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_LaboratoryRequests_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LaboratoryRequests_Serologies_SerologyId",
                        column: x => x.SerologyId,
                        principalTable: "Serologies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Prescriptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false),
                    IsInpatient = table.Column<bool>(type: "bit", nullable: false),
                    IsOutpatient = table.Column<bool>(type: "bit", nullable: false),
                    IsEmergency = table.Column<bool>(type: "bit", nullable: false),
                    Diagnosis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Approved = table.Column<bool>(type: "bit", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prescriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prescriptions_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SickLeaves",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SickLeaves", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SickLeaves_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Witnesses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccidentReportId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Witnesses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Witnesses_AccidentReports_AccidentReportId",
                        column: x => x.AccidentReportId,
                        principalTable: "AccidentReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Examiners",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssignedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExaminerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: true),
                    AssignmentId = table.Column<int>(type: "int", nullable: true),
                    DoctorId = table.Column<int>(type: "int", nullable: true),
                    RegExaminerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Examiners", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Examiners_Assignments_AssignmentId",
                        column: x => x.AssignmentId,
                        principalTable: "Assignments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Examiners_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Examiners_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Examiners_RegExaminers_RegExaminerId",
                        column: x => x.RegExaminerId,
                        principalTable: "RegExaminers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PrescriptionItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrescriptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StockId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    IsInternal = table.Column<bool>(type: "bit", nullable: false),
                    ApprovedQuantity = table.Column<int>(type: "int", nullable: false),
                    StockAvailable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrescriptionItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrescriptionItems_Prescriptions_PrescriptionId",
                        column: x => x.PrescriptionId,
                        principalTable: "Prescriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PrescriptionItems_Stock_StockId",
                        column: x => x.StockId,
                        principalTable: "Stock",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppointmentDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExaminerId = table.Column<int>(type: "int", nullable: true),
                    PatientCardNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientFirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientLastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegExaminerFirstName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appointments_Examiners_ExaminerId",
                        column: x => x.ExaminerId,
                        principalTable: "Examiners",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Referrals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReferralDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InvestigationResult = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rxgiven = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Diagnosis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Clinicalfinding = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    To = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    From = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExaminerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Referrals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Referrals_Examiners_ExaminerId",
                        column: x => x.ExaminerId,
                        principalTable: "Examiners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccidentReports_PatientId",
                table: "AccidentReports",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_ExaminerId",
                table: "Appointments",
                column: "ExaminerId");

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_DoctorId",
                table: "Assignments",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_PatientId",
                table: "Assignments",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_BloodFilmTests_PatientId",
                table: "BloodFilmTests",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Examiners_AssignmentId",
                table: "Examiners",
                column: "AssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Examiners_DoctorId",
                table: "Examiners",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Examiners_PatientId",
                table: "Examiners",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Examiners_RegExaminerId",
                table: "Examiners",
                column: "RegExaminerId");

            migrationBuilder.CreateIndex(
                name: "IX_LaboratoryRequests_BacteriologyId",
                table: "LaboratoryRequests",
                column: "BacteriologyId");

            migrationBuilder.CreateIndex(
                name: "IX_LaboratoryRequests_BiochemistryId",
                table: "LaboratoryRequests",
                column: "BiochemistryId");

            migrationBuilder.CreateIndex(
                name: "IX_LaboratoryRequests_CancerMarkerid",
                table: "LaboratoryRequests",
                column: "CancerMarkerid");

            migrationBuilder.CreateIndex(
                name: "IX_LaboratoryRequests_CardiacMarkerid",
                table: "LaboratoryRequests",
                column: "CardiacMarkerid");

            migrationBuilder.CreateIndex(
                name: "IX_LaboratoryRequests_Coagulationid",
                table: "LaboratoryRequests",
                column: "Coagulationid");

            migrationBuilder.CreateIndex(
                name: "IX_LaboratoryRequests_Electrolyteid",
                table: "LaboratoryRequests",
                column: "Electrolyteid");

            migrationBuilder.CreateIndex(
                name: "IX_LaboratoryRequests_HematologyId",
                table: "LaboratoryRequests",
                column: "HematologyId");

            migrationBuilder.CreateIndex(
                name: "IX_LaboratoryRequests_Hormoneid",
                table: "LaboratoryRequests",
                column: "Hormoneid");

            migrationBuilder.CreateIndex(
                name: "IX_LaboratoryRequests_OtherLabid",
                table: "LaboratoryRequests",
                column: "OtherLabid");

            migrationBuilder.CreateIndex(
                name: "IX_LaboratoryRequests_ParasitologyId",
                table: "LaboratoryRequests",
                column: "ParasitologyId");

            migrationBuilder.CreateIndex(
                name: "IX_LaboratoryRequests_PatientId",
                table: "LaboratoryRequests",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_LaboratoryRequests_SerologyId",
                table: "LaboratoryRequests",
                column: "SerologyId");

            migrationBuilder.CreateIndex(
                name: "IX_LaboratorySubTestResult_LaboratoryTestResultId",
                table: "LaboratorySubTestResult",
                column: "LaboratoryTestResultId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_DoctorId",
                table: "Patients",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_PrescriptionItems_PrescriptionId",
                table: "PrescriptionItems",
                column: "PrescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_PrescriptionItems_StockId",
                table: "PrescriptionItems",
                column: "StockId");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_PatientId",
                table: "Prescriptions",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Referrals_ExaminerId",
                table: "Referrals",
                column: "ExaminerId");

            migrationBuilder.CreateIndex(
                name: "IX_SickLeaves_PatientId",
                table: "SickLeaves",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Witnesses_AccidentReportId",
                table: "Witnesses",
                column: "AccidentReportId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "BloodFilmTests");

            migrationBuilder.DropTable(
                name: "ClinicRequests");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "ICD10Codes2024");

            migrationBuilder.DropTable(
                name: "LaboratoryRequests");

            migrationBuilder.DropTable(
                name: "LaboratorySubTestResult");

            migrationBuilder.DropTable(
                name: "PhysicalAssessments");

            migrationBuilder.DropTable(
                name: "PhysicalExaminations");

            migrationBuilder.DropTable(
                name: "PrescriptionItems");

            migrationBuilder.DropTable(
                name: "Referrals");

            migrationBuilder.DropTable(
                name: "SickLeaves");

            migrationBuilder.DropTable(
                name: "Urinalyses");

            migrationBuilder.DropTable(
                name: "UrineDipsticksTests");

            migrationBuilder.DropTable(
                name: "Witnesses");

            migrationBuilder.DropTable(
                name: "Bacteriologies");

            migrationBuilder.DropTable(
                name: "Biochemistries");

            migrationBuilder.DropTable(
                name: "CancerMarkers");

            migrationBuilder.DropTable(
                name: "CardiacMarkers");

            migrationBuilder.DropTable(
                name: "Coagulations");

            migrationBuilder.DropTable(
                name: "Electrolytes");

            migrationBuilder.DropTable(
                name: "Hematologies");

            migrationBuilder.DropTable(
                name: "Hormones");

            migrationBuilder.DropTable(
                name: "OtherLabs");

            migrationBuilder.DropTable(
                name: "Parasitologies");

            migrationBuilder.DropTable(
                name: "Serologies");

            migrationBuilder.DropTable(
                name: "LaboratoryTestResult");

            migrationBuilder.DropTable(
                name: "Prescriptions");

            migrationBuilder.DropTable(
                name: "Stock");

            migrationBuilder.DropTable(
                name: "Examiners");

            migrationBuilder.DropTable(
                name: "AccidentReports");

            migrationBuilder.DropTable(
                name: "Assignments");

            migrationBuilder.DropTable(
                name: "RegExaminers");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "Doctors");
        }
    }
}
