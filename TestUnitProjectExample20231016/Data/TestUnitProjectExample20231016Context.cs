using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestUnitProjectExample20231016.Models;

namespace TestUnitProjectExample20231016.Data
{
    public class TestUnitProjectExample20231016Context : DbContext
    {
        public TestUnitProjectExample20231016Context (DbContextOptions<TestUnitProjectExample20231016Context> options)
            : base(options)
        {
        }

        public DbSet<TestUnitProjectExample20231016.Models.Client> Client { get; set; } = default!;
    }
}
