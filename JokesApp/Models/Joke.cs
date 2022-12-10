using System.ComponentModel.DataAnnotations;

namespace JokesApp.Models
{
    public class Joke
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Joke Question")]
        public string JokeQuestion { get; set; }
        [Required]
        [Display(Name = "Joke Answer")]
        public string JokeAnswer { get; set; }
    }
}
