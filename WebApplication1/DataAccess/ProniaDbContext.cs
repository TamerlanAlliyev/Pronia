using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.DataAccess
{
    public class ProniaDbContext:DbContext
    {

        // Bu kod burada müəyyən edilmiş verilənlər bazasını "AddDbContext" metodu üçün program.cs faylına göndərir.

        // Bu constructor ,bu class-da yaradilmis database-ni "options" parametrine alir ve
        // base(options) ile "Program.cs" faylindaki "AddDbContext" metodunun icine gonderir
        public ProniaDbContext(DbContextOptions options):base(options)
        {
            
        }

        public DbSet<Slider> Sliders { get; set; }
    }
}
