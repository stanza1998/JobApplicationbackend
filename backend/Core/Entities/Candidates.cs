namespace backend.Core.Entities
{
    public class Candidates : BaseEntity
    {
        public string FirstName { get; set; }

        public  string lastName  { get; set; }

        public string Email { get; set; }


        public string Phone { get; set; }

        public string CoverLetter { get; set; }


        public string ResumeUrl { get; set; }

        //relation

        public long JobId { get; set; }

        public Job Job { get; set; }


    }
}
