using log4net.Core;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Utility.LoggerAzure
{
    public class BaseLogEntity:TableEntity
    {
        /// <summary>
        /// Default constructor required for use as a <see cref="CloudTable"/>.
        /// </summary>
        public BaseLogEntity() : base()
        {
            var utc = DateTime.UtcNow;
            this.PartitionKey = String.Format("{0:yyyy.MM}", utc);
            this.RowKey = String.Format("{0:yyyy.MM.dd HH:mm:ss.fff}-{1}", utc, Guid.NewGuid().ToString());
        }

        // <summary>
        /// Constructor required by <see cref="AzureStorageTableAppender"/>. The concrete class consturstor
        /// should initialize the entity to its ready to log state."/>
        /// </summary>
        /// <param name="loggingEvent"></param>
        public BaseLogEntity(LoggingEvent loggingEvent) : this() { }


        /// <summary>
        /// Virtual method whose implementation on a conctrete entity can be used to handle the results
        /// of the save to Azure storage table, i.e. the <see cref="CloudTable"/> Execute command.
        /// </summary>
        /// <param name="tableResult"></param>
        public virtual void AfterInsert(TableResult tableResult) { }

    }
}
