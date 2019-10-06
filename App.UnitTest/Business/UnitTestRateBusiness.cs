using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using App.Business;
using App.Data.Model.Model;

namespace App.UnitTest.Business
{
    /// <summary>
    /// Descripción resumida de UnitTestBusiness
    /// </summary>
    [TestClass]
    public class UnitTestRateBusiness
    {

        /// <summary>
        /// Método que inserta una lista de transacciones
        /// </summary>
        [TestMethod]
        public void InsertRate()
        {
            try
            {
                RateBusiness rateBusiness = new RateBusiness();
                rateBusiness = new RateBusiness();
                List<Rate> list = new List<Rate>();

                list.Add(new Rate() { Origin = "EUR", Target = "USD", Value = Convert.ToDecimal(1.2) });
                list.Add(new Rate() { Origin = "EUR", Target = "CAD", Value = Convert.ToDecimal(1.2) });
                list.Add(new Rate() { Origin = "EUR", Target = "AUD", Value = Convert.ToDecimal(1.2) });
                rateBusiness.InsertRate(list);
                Assert.AreEqual(true, true);

            }catch(Exception ex)
            {
                Assert.Fail(ex.Message);

            }
        }
        /// <summary>
        /// Método que consulta en el servicio e inserta en base de datos
        /// </summary>
        [TestMethod]
        public void SetRate()
        {
            try
            {
                RateBusiness rateBusiness = new RateBusiness();
                rateBusiness = new RateBusiness();
                rateBusiness.SetRate();
                Assert.AreEqual(true, true);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);

            }
        }
        /// <summary>
        /// Método que consulta en el servicio e inserta en base de datos
        /// </summary>
        [TestMethod]
        public void GetRate()
        {
            try
            {
                RateBusiness rateBusiness = new RateBusiness();
                rateBusiness = new RateBusiness();
                List<Rate>  listRate =rateBusiness.GetRate();
                Assert.IsNotNull(listRate);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);

            }
        }
        /// <summary>
        /// Método que prueba la construcción del arbol de nodos a partir de un 
        /// objetivo de entrada
        /// </summary>
        [TestMethod]
        public void CalculateConversion()
        {
            try
            {
                RateBusiness rateBusiness = new RateBusiness();
                rateBusiness = new RateBusiness();
                rateBusiness.CalculateConversion("EUR");
                Assert.AreEqual(true,true);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);

            }
        }
    }
}
