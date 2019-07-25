using Microsoft.EntityFrameworkCore;

namespace DemoApp.Web.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
    }
}
