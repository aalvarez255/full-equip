﻿using System;

namespace FullEquip.Core.Entities
{
    public class StudentAddress
    {
        public Guid Id { get; set; }

        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }

        public Guid StudentId { get; set; }
        public virtual Student Student { get; set; }
    }
}
