using System.ComponentModel.DataAnnotations;

namespace Wspolnota.Models
{
    public class Vote
    {
        [Key]
        public int VoteId { get; set; }
        public int AnswerId { get; set; }
        public Answer Answer { get; set; }
        public string AuthorId { get; set; }
        public ApplicationUser Author { get; set; }
    }
}