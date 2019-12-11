using System.IO;
using Blauhaus.ClientDatabase.Sqlite.Config;
using Blauhaus.ClientDatabase.Sqlite.Service._Base;
using Blauhaus.DeviceServices.Abstractions.DeviceInfo;

namespace Blauhaus.ClientDatabase.Sqlite.Service
{
    public class SqliteDatabaseService : BaseSqliteDatabaseService
    {

        public SqliteDatabaseService(
            ISqliteConfig config,
            IDeviceInfoService deviceInfoService) 
                : base(Path.Combine(deviceInfoService.AppDataFolder, config.DatabaseName), config.TableTypes)
        {
        }

    }
}