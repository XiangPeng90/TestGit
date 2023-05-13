using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Config
{
    internal class TrolleyConfig : IEntityTypeConfiguration<Trolley>
    {
        public void Configure(EntityTypeBuilder<Trolley> builder)
        {
            builder.ToTable("t_trolley");
            builder.HasKey(t => t.Id);
        }
    }
}
