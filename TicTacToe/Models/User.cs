using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TicTacToe.Models
{
    public class User
    {
        [Required(ErrorMessage = "Login cannot be empty")]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Enter between 3 and 15 chars")]
        public string Name { get; set; }
        public int Id { get; set; }

        public virtual ICollection<Game> Games { get; set; }

        public User(string name)
        {
            Name = name;
            Games = new List<Game>();
        }
        public User() { Games = new List<Game>(); }

    }
}