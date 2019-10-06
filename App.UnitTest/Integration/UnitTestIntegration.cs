using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using App.Integration;
using App.Data.Model.Model;
using System.Collections.Generic;
using App.Utility.Constants;
using App.Data.Model.Entity.Dto;

namespace App.UnitTest
{
    [TestClass]
    public class UnitTestIntegration
    {
        /// <summary>
        /// Método que prueba el servicios de transacciones
        /// </summary>
        [TestMethod]
        public void GetServiceTransaction()
        {
            try
            {
                ServiceIntegration Service = new ServiceIntegration();
                int code = 1;
                IEnumerable<ProductTransaction> objeto = Service.ResponseTransaction<ProductTransaction>(CommonConstants.UrlTransaction, ref code);
                Assert.IsNotNull(objeto);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

        }
        /// <summary>
        /// Método que prueba el servicio de Rate
        /// </summary>
        [TestMethod]
        public void GetServiceRatea()
        {
            try
            {
                ServiceIntegration Service = new ServiceIntegration();
                int code = 1;
                IEnumerable<Rate> objeto = Service.ResponseTransaction<Rate>(CommonConstants.UrlRate, ref code);
                Assert.IsNotNull(objeto);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

        }
        /// <summary>
        /// Método que prueba el código de error cuando el servicio está caido
        /// </summary>
        [TestMethod]
        public void GetErrorService()
        {
            try
            {
                ServiceIntegration Service = new ServiceIntegration();
                int code = 1;
                IEnumerable<RateDto> objeto = Service.ResponseTransaction<RateDto>("", ref code);
                Assert.IsFalse(code==200);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

        }
    }
}
