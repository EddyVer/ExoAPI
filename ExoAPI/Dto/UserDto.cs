﻿using ExoAPI.Type;

namespace ExoAPI.Dto
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public Grades Grade { get; set; }
    }
}
