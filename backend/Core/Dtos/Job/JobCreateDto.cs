using backend.Core.Enums;

namespace backend.Core.Dtos.Job
{
    public class JobCreateDto
    {
        public string Title { get; set; }

        public JobLevel JobLevel { get; set; }


        //relations
        public long CompanyId { get; set; }


    }
}
