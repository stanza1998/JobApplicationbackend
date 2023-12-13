namespace backend.Core.Dtos.Candidate
{
    public class CandidateDto
    {

        public string FirstName { get; set; }

        public string lastName { get; set; }

        public string Email { get; set; }


        public string Phone { get; set; }

        public string CoverLetter { get; set; }


        //relation

        public long JobId { get; set; }

    }
}
