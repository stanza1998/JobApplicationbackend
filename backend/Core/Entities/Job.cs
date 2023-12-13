using backend.Core.Enums;

namespace backend.Core.Entities
{
    public class Job: BaseEntity
    {
        public string Title { get; set; }

        public JobLevel JobLevel { get; set; }


        //relations
        public long CompanyId { get; set; }

        public Company Company { get; set; }

        public ICollection<Candidates> Candidates { get; set; }
    }
}
