﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DogGo.Models
{
    public class Dog
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("Dog's Name")]
        public string Name { get; set; }

        [Required]
        public string Breed { get; set; }

        public string Notes { get; set; }
        public string ImageUrl { get; set; }

        public int OwnerId { get; set; }
        public Owner Owner { get; set; }
    }
}
