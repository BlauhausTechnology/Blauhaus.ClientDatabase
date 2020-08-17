using System.Threading.Tasks;

namespace Blauhaus.ClientDatabase.Abstractions
{
    public interface IClientDatabaseService 
    {
        Task DeleteDataAsync();

    }
}