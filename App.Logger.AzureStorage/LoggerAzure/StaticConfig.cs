using Microsoft.Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Utility.LoggerAzure
{ /// <summary>
  /// Static class to handle confiuration settings.
  /// </summary>
    public static class StaticConfig
    {
        /// <summary>
        /// Static constructor to set readonly fields from runtime configuraiton.
        /// </summary>
        static StaticConfig()
        {
            ConnectionString = CloudConfigurationManager.GetSetting(AzureStorageLogTableConnectionStringKey);

            var entityTypeName = CloudConfigurationManager.GetSetting(LogEntityTypeKey);
            if (String.IsNullOrWhiteSpace(entityTypeName)) { LogEntityType = typeof(SimpleLogEntity); }
            else { LogEntityType = Type.GetType(entityTypeName); }

            var tableName = CloudConfigurationManager.GetSetting(AzureStorageLogTableNameKey);
            if (String.IsNullOrWhiteSpace(tableName)) { tableName = "LogTable"; }
            TableName = tableName;
        }


        /// <summary>
        /// Connection string for the log table on Azure table storage.
        /// </summary>
        internal readonly static string ConnectionString;

        /// <summary>
        /// The specific Type of the concrete ILogEntity to log with. Default is <see cref="SimpleLogEntity"/>.
        /// </summary>
        internal readonly static Type LogEntityType;

        /// <summary>
        /// The name of the Azure storage table to use in logging. Default is LogTable.
        /// </summary>
        internal readonly static string TableName;


        /// <summary>
        /// The app settings key for the connection string associated with the Azure storage account used
        /// for log4net table storage. This app setting is required.
        /// </summary>
        public const string AzureStorageLogTableConnectionStringKey = "AzureStorageLogTableConnectionString";

        /// <summary>
        /// The app settings key for the name of the Azure storage table to us in logging. If the app setting does
        /// not exist a default of LogTable will be used.
        /// </summary>
        public const string AzureStorageLogTableNameKey = "AzureStorageLogTableName";

        /// <summary>
        /// The app settings key for the type name ({full type name},{assebly name}) of the concrete LogTableEntity
        /// to use in logging. If the app setting does not exist or is an invalid type the default type 
        /// <see cref="SimpleLogEntity"/> will be used.
        /// </summary>
        public const string LogEntityTypeKey = "LogEntityType";
    }
}
