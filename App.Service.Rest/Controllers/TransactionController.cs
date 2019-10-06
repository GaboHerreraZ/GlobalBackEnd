using App.Data.Model.Model;
using App.Utility.Exceptions;
using App.Utility.Message;
using log4net;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using App.Utility.Constants;
using App.Interface.Business;
using App.Data.Model.Entity.Common;
using App.Data.Model.Entity.Dto;

namespace App.Service.Rest.Controllers
{

    /// <summary>
    /// Servicio que expone todas las funcionalidades disponibles en transacciones
    /// </summary>
    /// <remarks> Autor: Gabriel Herrera</remarks>
    [RoutePrefix("transaction")]
    public class TransactionController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(TransactionController));

        #region Dependency Injection Instancers
        private readonly ITransaction Transaction;
        private readonly IRate Rate;


        #endregion

        #region Controller Constructor
        public TransactionController()
        {

        }
        public TransactionController( ITransaction iTrasaction, IRate iRate)
        {
            Transaction = iTrasaction;
            Rate = iRate;
        }
        #endregion
        /// <summary>
        /// Método del servicio que permite obtener todas las transacciones
        /// </summary>
        /// <remarks> Autor: Gabriel Herrera</remarks>
        [HttpGet]
        [Route("get")]
        [ResponseType(typeof(Response))]
        public HttpResponseMessage GetTransaction()
        {
            try
            {
                IEnumerable<ProductTransaction> transaction = Transaction.GetTransaction();
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, new Response
                {
                    Code = CommonConstants.SuccessCode,
                    Description = null,
                    Data = transaction
                });
                return response;
            }
            catch (ExceptionOperation ex)
            {
                Log.Error(ex.Message, ex);
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.InternalServerError, new Response
                {
                    Code = CommonConstants.ErrorCode,
                    Description = null,
                    Data = ex.Message
                });
                return response;
            }
            catch(Exception ex)
            {
                Log.Error(HelperMessage.GetMessage(CodeMessage.TRN_001), ex);
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.InternalServerError, new Response
                {
                    Code = CommonConstants.ErrorCode,
                    Description = null,
                    Data = ex.Message
                });

                return response;
            }
        }
        /// <summary>
        /// Método del servicio que inserta las transacciones del servicio externo en base de datos
        /// </summary>
        /// <remarks> Autor: Gabriel Herrera</remarks>
        [HttpGet]
        [Route("set")]
        [ResponseType(typeof(Response))]
        public HttpResponseMessage SetTransaction()
       {
            try
            {
                Transaction.SetTransaction();
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, new Response
                {
                    Code = CommonConstants.SuccessCode,
                    Description = null,
                    Data = null
                });
                return response;
            }
            catch (ExceptionOperation ex)
            {
                Log.Error(ex.Message, ex);
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.InternalServerError, new Response
                {
                    Code = CommonConstants.ErrorCode,
                    Description = null,
                    Data = ex.Message
                });
                return response;
            }
            catch (Exception ex)
            {
                Log.Error(HelperMessage.GetMessage(CodeMessage.TRN_001), ex);
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.InternalServerError, new Response
                {
                    Code = CommonConstants.ErrorCode,
                    Description = null,
                    Data = HelperMessage.GetMessage(CodeMessage.TRN_001)
                });

                return response;
            }
        }
        /// <summary>
        /// Método del servicio que calcula las conversiones de todas las tranasacciones
        /// </summary>
        /// <remarks> Autor: Gabriel Herrera</remarks>
        [HttpPost]
        [Route("total")]
        [ResponseType(typeof(Response))]
        public HttpResponseMessage GetConvertion(CurrencyConvert target)
        {
            try
            {
                Rate.CalculateConversion(target.Currency);
                TotalTransaction[] total = Transaction.GetTotalTransaction();
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, new Response
                {
                    Code = CommonConstants.SuccessCode,
                    Description = null,
                    Data = total
                });
                return response;
            }
            catch(ExceptionOperation ex)
            {
                Log.Error(ex.Message, ex);
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.InternalServerError, new Response
                {
                    Code = CommonConstants.ErrorCode,
                    Description = null,
                    Data = ex.Message
                });
                return response;
            }
            catch (Exception ex)
            {
                Log.Error(HelperMessage.GetMessage(CodeMessage.TRN_001), ex);
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.InternalServerError, new Response
                {
                    Code = CommonConstants.ErrorCode,
                    Description = null,
                    Data = HelperMessage.GetMessage(CodeMessage.TRN_001)
                });

                return response;
            }
        }
        /// <summary>
        /// Método del servicio que permite obtener el valor en euros de un sku
        /// </summary>
        /// <remarks> Autor: Gabriel Herrera</remarks>
        [HttpGet]
        [Route("sku")]
        [ResponseType(typeof(Response))]
        public HttpResponseMessage GetSkuValue(string sku)
        {
            try
            {
                Rate.CalculateConversion("EUR");
                TotalTransaction[] total = Transaction.GetSkuValue(sku);
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, new Response
                {
                    Code = CommonConstants.SuccessCode,
                    Description = null,
                    Data = total
                });
                return response;
            }
            catch (ExceptionOperation ex)
            {
                Log.Error(ex.Message, ex);
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.InternalServerError, new Response
                {
                    Code = CommonConstants.ErrorCode,
                    Description = null,
                    Data = ex.Message
                });
                return response;
            }
            catch (Exception ex)
            {
                Log.Error(HelperMessage.GetMessage(CodeMessage.TRN_001), ex);
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.InternalServerError, new Response
                {
                    Code = CommonConstants.ErrorCode,
                    Description = null,
                    Data = HelperMessage.GetMessage(CodeMessage.TRN_001)
                });

                return response;
            }

        }

    }
}
