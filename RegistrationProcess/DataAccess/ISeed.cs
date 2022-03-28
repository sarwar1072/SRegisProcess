using System.Threading.Tasks;

namespace DataAccess
{
    public interface ISeed
    {
        Task MigrateAsync();
        Task SeedAsync();
    }
}
