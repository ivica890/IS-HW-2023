﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TicketApplication.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Category Name")]
        public string Name { get; set; }
        [Range(1,100)]
        public int DisplayOrder { get; set; }
    }
}
