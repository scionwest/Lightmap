using System.Data;
using System.Threading.Tasks;

namespace Lightmap
{
    public interface IDatabaseManager
    {
        string Database { get; }

        IDbConnection OpenConnection();

        Task<IDbConnection> OpenConnectionAsync();
    }
}
