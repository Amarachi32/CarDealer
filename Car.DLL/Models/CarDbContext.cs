using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car.DLL.Models
{
    public class CarDbContext : DbContext
    {
        public CarDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
