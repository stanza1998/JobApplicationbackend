using backend.Core.Entities;
using backend.Core.Enums;

namespace backend.Core.Dtos.Job
{
    public class JobGetDto
    {
        public long ID { get; set; }

        public DateTime CreateAt { get; set; } = DateTime.Now;

        public string Title { get; set; }

        public JobLevel JobLevel { get; set; }

        public long CompanyId { get; set; }

        public string CompanyName { get; set; }

    }
}
