﻿using System;

namespace Swimming_Club_2.Models
{
    public class Discipline
    {
        public int SwimmerId { get; set; } = 0;
        public string DisciplineId { get; set; } = "";
        public TimeSpan Time { get; set; }
    }
}
