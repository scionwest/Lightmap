using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lightmap
{
    public interface IDataStoreProvider
    {
        Task OpenConnectionAsync();

        void OpenConnection();
    }
}
