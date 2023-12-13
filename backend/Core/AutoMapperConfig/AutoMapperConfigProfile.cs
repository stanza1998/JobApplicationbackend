using AutoMapper;
using backend.Core.Dtos.Candidate;
using backend.Core.Dtos.Company;
using backend.Core.Dtos.Job;
using backend.Core.Entities;

namespace backend.Core.AutoMapperConfig
{
    public class AutoMapperConfigProfile: Profile
    {

        public AutoMapperConfigProfile()
        {
            //Company
            CreateMap<CompanyDto, Company>();
            CreateMap<Company, CompanyGetDto>();
            // Map Company to CompanyGetDto when getting by ID
            CreateMap<Company, CompanyGetDto>().ForMember(
                dest => dest.ID,  // Assuming Id is a property in CompanyGetDto
                opt => opt.MapFrom(src => src.ID)
            );
            CreateMap<CompanyUpdateDto, Company>();


            //Job
            CreateMap<JobCreateDto, Job>();
            CreateMap<Job, JobGetDto>()
                .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Company.Name));
            CreateMap<JobUpdateDto, Job>();

            //Candidate
            CreateMap<CandidateDto, Candidates>();
            CreateMap<Candidates, CanditateGetDto>()
                .ForMember(dest => dest.JobTitle, opt => opt.MapFrom(src => src.Job.Title));

        }



    }
}



