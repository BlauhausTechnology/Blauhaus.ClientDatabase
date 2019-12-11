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
        private readonly string _path;

        public LiteDbDatabaseService(
            ILiteDbConfig config,
            IDeviceInfoService deviceInfoService) : base(config)
        {
            _path = Path.Combine(deviceInfoService.AppDataFolder, config.DatabaseName) + ".db";
        }


        public override LiteDatabase GetDatabase()
        {
            return new LiteDatabase(_path);
        }

      
    }
}