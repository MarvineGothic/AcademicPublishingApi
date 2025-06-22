﻿namespace AcademicPublishingApi.Models
{
    public class AuthorDto
    {
        public int Id { get; set; }

        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public required string Affiliation { get; set; }
    }
}
