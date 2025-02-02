﻿using DLL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DLL.ResponseViewModel
{
    public class StudentCourseViewModel
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
        public List<Course> Courses { get; set; } = new List<Course>();


    }
}
