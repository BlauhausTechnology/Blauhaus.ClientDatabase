using System;
using System.IO;
using Blauhaus.ClientDatabase.LiteDb.Config;
using Blauhaus.ClientDatabase.LiteDb.Service.Base;
using Blauhaus.DeviceServices.Abstractions.DeviceInfo;
using LiteDB;

namespace Blauhaus.ClientDatabase.LiteDb.Service
{
    public class LiteDbDatabaseService : BaseLiteDbDatabaseService
    {
        private readonly ConnectionString _connectionString;

        public LiteDbDatabaseService(
            ILiteDbConfig config,
            IDeviceInfoService deviceInfoService) : base(config)
        {
            var path = Path.Combine(deviceInfoService.AppDataFolder, config.DatabaseName) + ".db";
            _connectionString = new ConnectionString
            {
                UtcDate = true,
                Filename = path,
            };
        }


        public override LiteDatabase GetDatabase()
        {
            return new LiteDatabase(_connectionString);
        }

      
    }
}