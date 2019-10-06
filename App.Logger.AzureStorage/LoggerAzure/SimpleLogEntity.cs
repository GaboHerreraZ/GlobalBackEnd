using log4net.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.WindowsAzure.Storage.Table;
using System.Text;
using System.Threading.Tasks;

namespace App.Utility.LoggerAzure
{
    /// <summary>
    /// Implementaiton of <see cref="ILogEntity"/> inheriting <see cref="TableEntity"/>
    /// used to store log data in an Azure storage table.
    /// </summary>
    public class SimpleLogEntity:BaseLogEntity
    {
        /// <summary>
        /// Default constructor required for use as a <see cref="CloudTable"/>.
        /// </summary>
        public SimpleLogEntity() : base() { }

        /// <summary>
        /// Constructor required by <see cref="AzureStorageTableAppender"/> which initializes the entity to
        /// its ready to log state."/>
        /// </summary>
        /// <param name="loggingEvent"></param>
        public SimpleLogEntity(LoggingEvent loggingEvent) : base(loggingEvent)
        {
            // Set a message based on whether or not there was an exception.
            string message = loggingEvent.RenderedMessage;
            if (loggingEvent.ExceptionObject != null)
            {
                message = String.Format(
                                    "{0}{1}{2}",
                                    message,
                                    Environment.NewLine,
                                    loggingEvent.GetExceptionString());
            }

            // Initialize all concrete properties. Use base class defaults for PartitionKey and RowKey.
            this.Level = loggingEvent.Level.Name;
            this.Logger = loggingEvent.LoggerName;
            this.Message = string.Format("{0}|{1}",message,loggingEvent.ExceptionObject.Message);
            this.Date = DateTime.Now;
            this.StackTrace = loggingEvent.ExceptionObject.StackTrace;
        }


        /// <summary>
        /// Get/set the log level for the entry.
        /// </summary>
        public string Level { get; set; }

        /// <summary>
        /// Get/set the name of the logger used, i.e. source class name.
        /// </summary>
        public string Logger { get; set; }

        /// <summary>
        /// Get/set the log entry's message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Get/set the UTC DateTime stamp that the log entry was created.
        /// </summary>
        public DateTime Date { get; set; }

        public string StackTrace { get; set; }

    }
}
