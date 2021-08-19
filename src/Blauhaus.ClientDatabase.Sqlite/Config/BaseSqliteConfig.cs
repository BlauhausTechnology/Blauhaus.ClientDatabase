using System;
using System.Collections.Generic;
using System.IO;
using Blauhaus.DeviceServices.Abstractions.DeviceInfo;

namespace Blauhaus.ClientDatabase.Sqlite.Config
{
    public abstract class BaseSqliteConfig : ISqliteConfig
    {
        protected BaseSqliteConfig(IDeviceInfoService deviceInfoService, string databaseName)
        {

            if (!databaseName.EndsWith(".sqlite"))
            {
                databaseName += ".sqlite";
            }

            DatabasePath = Path.Combine(deviceInfoService.AppDataFolder, databaseName);
            
            TableTypes = new List<Type>
            { 

                //Vessels

            };
        }
         

        public string DatabasePath { get; protected set; }
        public IList<Type> TableTypes { get; protected set; }


    }
}