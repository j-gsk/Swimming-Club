using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Swimming_Club_2.Models
{
    public class Referee
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Registration { get; set; } = string.Empty;

        public class EntityTypeConfiguration
        {
            public void Configure(EntityTypeBuilder<Swimmer> builder)
            {
                builder.HasKey(x => x.Id);
                builder.Property(x => x.FirstName).HasMaxLength(50);

            }
        }
    }
}
