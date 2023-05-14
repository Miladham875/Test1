using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class TestApplicationContext :DbContext
    {
        public TestApplicationContext(DbContextOptions<TestApplicationContext> options) : base(options)
        {
        }
        public DbSet<Report> Reports { get; set; }

        public DbSet<Fruit> Fruits { get; set; }
        


    }
}
