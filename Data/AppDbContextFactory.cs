using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-MMF70IT;Initial Catalog=temp;User ID=sa;Password=15022000Vy!;Multiple Active Result Sets=True;Trust Server Certificate=True");

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
