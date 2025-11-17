using System;
using System.ComponentModel.DataAnnotations;

namespace MyMvcApp.Models.ViewModels
{
    public class AddTagsRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string DisplayName{ get; set; }
        
    }
}


