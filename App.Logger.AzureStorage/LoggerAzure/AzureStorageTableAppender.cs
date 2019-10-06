using log4net.Appender;
using log4net.Core;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage;
using System;

namespace App.Utility.LoggerAzure
{
    /// <summary>
    /// Log4net appender usado para escribir en el storage de azure
    /// </summary>
    public class AzureStorageTableAppender : AppenderSkeleton
    {
        /// <summary>
        /// Override of the log4net Append event to write the log to an Azure storage table.
        /// </summary>
        /// <param name="loggingEvent"></param>
        protected override void Append(LoggingEvent loggingEvent)
        {
            // Obtener cuenta de azure y creat tabla de ser necesario
            var storageAccount = CloudStorageAccount.Parse(StaticConfig.ConnectionString);
            var tableClient = storageAccount.CreateCloudTableClient();
            var table = tableClient.GetTableReference(StaticConfig.TableName);
            table.CreateIfNotExists();

            // Insertar log en la tabla de azure
            BaseLogEntity logEntity = Activator.CreateInstance(StaticConfig.LogEntityType, loggingEvent) as BaseLogEntity;
            var result = table.Execute(TableOperation.Insert(logEntity));
            logEntity.AfterInsert(result);
        }
    }
}
