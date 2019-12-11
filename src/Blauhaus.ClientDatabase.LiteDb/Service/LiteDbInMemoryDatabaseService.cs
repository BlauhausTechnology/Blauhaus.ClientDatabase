using System.IO;
using Blauhaus.ClientDatabase.LiteDb.Config;
using Blauhaus.ClientDatabase.LiteDb.Service.Base;
using LiteDB;

namespace Blauhaus.ClientDatabase.LiteDb.Service
{
    public class LiteDbInMemoryDatabaseService : BaseLiteDbDatabaseService
    {

        public LiteDbInMemoryDatabaseService(ILiteDbConfig config) 
            : base(config)
        {
        }


        public override LiteDatabase GetDatabase()
        {
            return  new LiteDatabase(new MemoryStream());
        }



    }
}