using API.Model;
using API.Model.DTO;
using API.Models;
using AutoMapper;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ICD10Codes2024, ICD10CodeDto>().ReverseMap();
            CreateMap<RegExaminer, RegExaminerDto>().ReverseMap();
            CreateMap<Doctor, DoctorDTO>().ReverseMap();
            CreateMap<Patient, PatientDTO>().ReverseMap();
            CreateMap<RegisterPatientDTO, Patient>();
            CreateMap<Prescription, PrescriptionDto>().ReverseMap();
            CreateMap<Prescription, PrescriptionUpdateDto>().ReverseMap();
            CreateMap<PrescriptionItemDto, PrescriptionItem>();

            CreateMap<Prescription, PrescriptionDto>();
            CreateMap<PrescriptionItem, PrescriptionItemDto>();

            CreateMap<Appointment, AppointmentDto>();
            CreateMap<CreateAppointmentDto, Appointment>();
            CreateMap<Referral, ReferralDto>().ReverseMap();
            CreateMap<ClinicRequest, ClinicRequestDto>().ReverseMap();

            CreateMap<Employee, EmployeeDTO>().ReverseMap();
            CreateMap<Prescription, PrescriptionDto>()

               .ForMember(dest => dest.CardNumber, opt => opt.MapFrom(src => src.Patient.cardNo))
               .ForMember(dest => dest.NameOfPatient, opt => opt.MapFrom(src => $"{src.Patient.firstName} {src.Patient.lastName}"))
               .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.Patient.age));
                //.ForMember(dest => dest.Sex, opt => opt.MapFrom(src => src.Patient.sex));
          
            CreateMap<PrescriptionItem, PrescriptionItemDto>();
            CreateMap<PrescriptionCreateDto, Prescription>();
            CreateMap<PrescriptionItemCreateDto, PrescriptionItem>();

            CreateMap<Stock, StockReadDto>();        
            CreateMap<StockCreateDto, Stock>();      
            CreateMap<StockUpdateDto, Stock>();


            CreateMap<Referral, ReferralCreateDto>().ReverseMap();
            CreateMap<Referral, ReferralupdateDto>().ReverseMap();
            CreateMap<Referral, ReferralDto>()
            .ForMember(dest => dest.Examiner, opt => opt.MapFrom(src => src.Examiner));
            CreateMap<Examiner, ExaminerDto>();
            CreateMap<ExaminerDto, Examiner>();
            CreateMap<Examiner, UpdateExaminerDto>();
            CreateMap<Examiner, ExaminerDetailDto>()
     .ForMember(dest => dest.PatientCardNumber, opt => opt.MapFrom(src => src.Patient != null ? src.Patient.cardNo : null))
     .ForMember(dest => dest.PatientAge, opt => opt.MapFrom(src => src.Patient != null ? src.Patient.age : 0)) // Assuming default age as 0
     .ForMember(dest => dest.PatientFirstName, opt => opt.MapFrom(src => src.Patient != null ? src.Patient.firstName : null))
     .ForMember(dest => dest.PatientLastName, opt => opt.MapFrom(src => src.Patient != null ? src.Patient.lastName : null))
     .ForMember(dest => dest.Weight, opt => opt.MapFrom(src => src.Assignment != null ? src.Assignment.Weight : 0)) // Assuming default weight as 0
     .ForMember(dest => dest.Pressure, opt => opt.MapFrom(src => src.Assignment != null ? src.Assignment.SystolicPressure : 0)) // Assuming default pressure as 0
     //.ForMember(dest => dest.DoctorName, opt => opt.MapFrom(src => src.Doctor != null ? $"{src.Doctor.Name}" : null))
     .ReverseMap();



            CreateMap<Assignment, AssignmentDTO>().ReverseMap();
            CreateMap<Assignment, AssignDTO>().ReverseMap();
            CreateMap<Assignment, AssignmentDetailDTO>()
                .ForMember(dest => dest.PatientCardNumber, opt => opt.MapFrom(src => src.Patient.cardNo))
               
                .ReverseMap();
            CreateMap<PhysicalExamination, PhysicalExaminationDto>().ReverseMap();
             CreateMap<PhysicalExamination, PhysicalExaminationviewDto>()
            .ForMember(dest => dest.PatientcardNo, opt => opt.MapFrom(src => src.Patient.cardNo));
            CreateMap<PhysicalAssessment, PhysicalAssessmentDTO>().ReverseMap();
            CreateMap<Parasitology, ParasitologyDto>().ReverseMap();
            CreateMap<Urinalysis, UrinalysisDto>().ReverseMap();
            CreateMap<Hematology, HematologyDto>().ReverseMap();
            CreateMap<Biochemistry, BiochemistryDto>().ReverseMap();
            CreateMap<Bacteriology, BacteriologyDto>().ReverseMap();
            CreateMap<Serology, SerologyDto>().ReverseMap();
            CreateMap<OtherLab, OtherLabDto>().ReverseMap();
            CreateMap<Hormone, HormoneDto>().ReverseMap();
            CreateMap<Coagulation, CoagulationDto>().ReverseMap();
            CreateMap<CardiacMarker, CardiacMarkerDto>().ReverseMap();
            CreateMap<CancerMarker, CancerMarkerDto>().ReverseMap();
            CreateMap<Electrolyte, ElectrolyteDto>().ReverseMap();
            CreateMap<LaboratoryRequest, LaboratoryRequestDto>().ReverseMap();
            CreateMap<LaboratoryTestResult, LaboratoryTestResultDto>();
            CreateMap<SickLeave, SickLeaveDto>().ReverseMap();
            CreateMap<SickLeave, SickLeaveUpdateDto>().ReverseMap();

            CreateMap<LaboratoryTestResult, LaboratoryResultDto>().ReverseMap();
            CreateMap<LabBloodFilmTest, LabBloodFilmTestDto>().ReverseMap();
            CreateMap<LaboratorySubTestResult, LaboratorySubTestResultDto>().ReverseMap();
            CreateMap<UrineDipsticksTest, UrineDipsticksTestDto>().ReverseMap();
            CreateMap<AccidentReport, AccidentReportDto>().ReverseMap();
            CreateMap<Witness, WitnessDto>().ReverseMap();
            CreateMap<AccidentReport, AccidentReportCreateDto>().ReverseMap();
        }
    }
   
}
