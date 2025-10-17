using System;

namespace MyMvcApp.Models.ViewModels
{
    public class EditTagsRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
    }
}


