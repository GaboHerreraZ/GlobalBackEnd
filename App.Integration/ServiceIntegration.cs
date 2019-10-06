using App.Utility.Message;
using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;

namespace App.Integration
{
    public class ServiceIntegration
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(ServiceIntegration));

        public List<T> ResponseTransaction<T>(string service,ref int code)
            where T:class
        {
            List<T> objectResponse = null;
            try
            {

                string json = null;
                using (WebClient wc = new WebClient())
                {
                    json = wc.DownloadString(ConfigurationManager.AppSettings[service].ToString());
                }

                objectResponse = JsonConvert.DeserializeObject<List<T>>(json);

                code = 200;
                return objectResponse;
            }
            catch(Exception ex)
            {
                code = 500;
                Log.Error(HelperMessage.GetMessage(CodeMessage.INT_001), ex);
                return objectResponse;
            }


        }

    }
}
