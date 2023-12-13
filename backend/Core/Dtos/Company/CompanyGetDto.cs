using backend.Core.Enums;

namespace backend.Core.Dtos.Company
{
    public class CompanyGetDto
    {
        public string Name { get; set; }

        public CompanySize Size { get; set; }

        public long ID { get; set; }

        public DateTime CreateAt { get; set; } = DateTime.Now;

     
    }
}
