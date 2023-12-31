﻿namespace FakeBloggingSystem.Models.DTO
{
    public class PostAuthorDTO
    {
        public int AuthorId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public AuthorDTO? Author { get; set; }
    }
}
