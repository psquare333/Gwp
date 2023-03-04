using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Gwp.Models;
using static System.Reflection.Metadata.BlobBuilder;
using System.Text.Json;

namespace Gwp.Data
{
    public class GwpContext : DbContext
    {
        public GwpContext (DbContextOptions<GwpContext> options)
            : base(options)
        {
        }

        public DbSet<Gwp.Models.CounytryPremiums> CounytryPremiums { get; set; } = default!;

        public void initiateData()
        {
            if (CounytryPremiums.Any())
            {
                return; 
            }

            var json = File.ReadAllText("initialData.json");
            var initialData = JsonSerializer.Deserialize<List<CounytryPremiums>>(json);

            CounytryPremiums.AddRange(initialData);
            SaveChanges();
        }
    }
}
