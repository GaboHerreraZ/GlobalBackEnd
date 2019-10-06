using App.Data.Model.Entity.Common;
using App.Data.Model.Model;
using App.Interface;
using App.Interface.Business;
using App.Utility.Constants;
using App.Utility.Exceptions;
using App.Utility.Message;
using log4net;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace App.Service.Rest.Controllers
{

    /// <summary>
    /// Servicio que expone todas las funcionalidades disponibles de Rate 
    /// </summary>
    /// <remarks> Autor: Gabriel Herrera</remarks>
    [RoutePrefix("rate")]
    public class RateController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(RateController));
        #region Dependency Injection Instancers
        private  IRate Rate;

        #endregion

        #region Controller Constructor
        public RateController() {}

        public RateController(IRate iRate)
        {
            Rate = iRate;
        }
        #endregion

        /// <summary>
        /// Método del servicio que obtiene todas los rate del servicio externo
        /// </summary>
        /// <remarks> Autor: Gabriel Herrera</remarks>
        [HttpGet]
        [Route("get")]
        [ResponseType(typeof(Response))]
        public  HttpResponseMessage GetRate()
        {
            try
            {
                IEnumerable<Rate> objectResponse =  Rate.GetRate();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, new Response
                {
                    Code = CommonConstants.SuccessCode,
                    Description = null,
                    Data = objectResponse
                });

                return response;
            }
            catch(ExceptionOperation ex)
            {
                
                Log.Error(ex.Message,ex);
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
                Log.Error(HelperMessage.GetMessage(CodeMessage.RAT_001), ex);
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
        /// Servicio que guarda en base de datos los rates obtenidos del servicio externo
        /// </summary>
        /// <remarks> Autor: Gabriel Herrera</remarks>
        [HttpGet]
        [Route("set")]
        [ResponseType(typeof(Response))]
        public HttpResponseMessage SetRate()
        {
            try
            {
                Rate.SetRate();
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
                Log.Error(HelperMessage.GetMessage(CodeMessage.RAT_001), ex);
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.InternalServerError, new Response
                {
                    Code = CommonConstants.ErrorCode,
                    Description = null,
                    Data = ex.Message
                });
                return response;
            }
        }



    }
}
