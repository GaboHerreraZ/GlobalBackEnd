using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using App.Business;
using App.Data.Model.Model;
using App.Data.Model.Entity.Dto;

namespace App.UnitTest.Business
{
    /// <summary>
    /// Descripción resumida de UnitTestTransactionBusiness
    /// </summary>
    [TestClass]
    public class UnitTestTransactionBusiness
    {
        public UnitTestTransactionBusiness()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }

        private TestContext testContextInstance;
        private TransactionBusiness TranscBusiness;

        /// <summary>
        ///Obtiene o establece el contexto de las pruebas que proporciona
        ///información y funcionalidad para la serie de pruebas actual.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }
        /// <summary>
        /// Método que inserta las transacciones en base de datos
        /// </summary>
        [TestMethod]
        public void SetTransaction()
        {
            try
            {
                TranscBusiness = new TransactionBusiness();
                TranscBusiness.SetTransaction();
                Assert.AreEqual(true,true);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
        /// <summary>
        /// Método que inserta las transacciones en base de datos
        /// </summary>
        [TestMethod]
        public void GetTransaction()
        {
            try
            {
                TranscBusiness = new TransactionBusiness();
                List<ProductTransaction> list = TranscBusiness.GetTransaction();

                Assert.IsNotNull(list);
                Assert.IsTrue(list.Count>0);

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
        /// <summary>
        /// Método que prueba la consulta del consolidado de las transacciones
        /// </summary>
        [TestMethod]
        public void GetTotalTransaction()
        {
            try
            {
                TranscBusiness = new TransactionBusiness();
                TotalTransaction[] list = TranscBusiness.GetTotalTransaction();

                Assert.IsNotNull(list);
                Assert.IsTrue(list.Length > 0);

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
        /// <summary>
        /// Método que prueba que consulta la transacción  a partir de un sku
        /// </summary>
        [TestMethod]
        public void GetSkuValue()
        {
            try
            {
                TranscBusiness = new TransactionBusiness();

                List<ProductTransaction> transac = TranscBusiness.GetTransaction();

                TotalTransaction[] list = TranscBusiness.GetSkuValue(transac[0].Sku);

                if(list.Length > 0)
                {
                    Assert.IsTrue(true, "Si existe la transacción");
                }
                else
                {
                    Assert.Fail("No existe la transacción");
                }
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}
