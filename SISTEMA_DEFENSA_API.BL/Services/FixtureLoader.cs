using SISTEMA_DEFENSA_API.EL.DbContexts;
using SISTEMA_DEFENSA_API.EL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SISTEMA_DEFENSA_API.BL.Services
{
    public static class FixtureLoader
    {
        public static void LoadFixtures(DefenseDbContext context)
        {
            if (!context.Provinces.Any())
            {
                var provinceJson = File.ReadAllText("Fixtures/provinces.json");
                var provinces = JsonSerializer.Deserialize<List<Province>>(provinceJson);
                context.Provinces.AddRange(provinces!);
                context.SaveChanges();
            }

            if (!context.Municipalities.Any())
            {
                var munJson = File.ReadAllText("Fixtures/municipalities.json");
                var municipalities = JsonSerializer.Deserialize<List<Municipality>>(munJson);
                context.Municipalities.AddRange(municipalities!);
                context.SaveChanges();
            }
        }
    }
}
