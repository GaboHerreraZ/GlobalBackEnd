using App.Data.Model.Base;
using App.Data.Model.Entity.Dto;
using App.Data.Model.Model;
using App.Integration;
using App.Interface.Business;
using App.Utility.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace App.Business
{
    /// <summary>
    /// Clase  que maneja toda la lógica del negocio de las divisas
    /// </summary>
    /// <remarks> Autor: Gabriel Herrera</remarks>
    public class RateBusiness:IRate
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        #region Dispose
        /// <summary>
        /// Método público que permite liberar recursos no administrados de la clase RateBusiness.
        /// </summary>
        /// <remarks> Autor: Gabriel Herrera</remarks>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                unitOfWork.Dispose();
            }
        }
        #endregion

        #region Variable
        public List<Rate> rates = new List<Rate>();
        public Node nodeRoot;
        public double totalRate = 1;
        public  List<Currency> conversion = new List<Currency>();

        #endregion
        /// <summary>
        /// Método que inserta  las divisas del servicio en base de datos
        /// </summary>
        /// <remarks> Autor: Gabriel Herrera</remarks>
        public void InsertRate(List<Rate> rates)
        {

            TransactionOptions transactionOption = new TransactionOptions()
            {
                Timeout = TransactionManager.DefaultTimeout,
                IsolationLevel = IsolationLevel.Serializable
            };


                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, transactionOption))
                {
                    using (unitOfWork = new UnitOfWork())
                    {
                        unitOfWork.RateRepository.AllDelete();
                        unitOfWork.RateRepository.BulkInsert(rates);
                        unitOfWork.Save();
                    }

                scope.Complete();
                }

        }

        /// <summary>
        /// Método que convierte a decimal para poder insertar a base de datos
        /// </summary>
        /// <remarks> Autor: Gabriel Herrera</remarks>
        public List<Rate> ConvertRateDtoRate(IEnumerable<RateDto> rates)
        {

            List<Rate> newRate = new List<Rate>();
            foreach (var item in rates.ToList())
            {
                newRate.Add(new Rate()
                {
                    Origin = item.from,
                    Target = item.to,
                    Value = (decimal)item.rate
                });
            }

            return newRate;
        }
        /// <summary>
        /// Método que inserta las divisas del servicio
        /// </summary>
        /// <remarks> Autor: Gabriel Herrera</remarks>
        public void SetRate()
        {
            int code = 1;
            List<Rate> rates = new List<Rate>();
            ServiceIntegration Service = new ServiceIntegration();

            List<RateDto> objectResponse = Service.ResponseTransaction<RateDto>(CommonConstants.UrlRate, ref code);
            
            rates = ConvertRateDtoRate(objectResponse);

            if (code == 200)
            {
                InsertRate(rates);
            }
        }
        /// <summary>
        /// Método que obtiene las divisas de base de datos
        /// </summary>
        /// <remarks> Autor: Gabriel Herrera</remarks>
        public List<Rate> GetRate()
        {
            IEnumerable<Rate> response = null;

            using (unitOfWork = new UnitOfWork())
            {
                response = unitOfWork.RateRepository.Get();
                return response.ToList();
            }
        }
        /// <summary>
        /// Método que calcula las tasas de conversión
        /// </summary>
        /// <remarks> Autor: Gabriel Herrera</remarks>
        public void CalculateConversion(string target)
        {
            conversion = new List<Currency>();
            List<Rate> ratesDataBase = GetRate();

            nodeRoot = new Node() { Name = target, Rate = 1, Children = new List<Node>() };
            List<string> badge = ratesDataBase.Select(x => x.Origin).Distinct().ToList();
            this.rates = ratesDataBase.ToList();

            ArmTree(nodeRoot);

            foreach (var i in badge)
            {
                SearchConversion(nodeRoot,i,target);
            }

            using (unitOfWork = new UnitOfWork())
            {
                unitOfWork.CurrencyRepository.AllDelete();
                unitOfWork.CurrencyRepository.BulkInsert(conversion);
            }

        }
        /// <summary>
        /// Método que arma el árbol para el algoritmo de busqueda
        /// </summary>
        /// <remarks> Autor: Gabriel Herrera</remarks>
        private void ArmTree(Node root)
        {
            var rateAux = (from r in rates where r.Origin == root.Name select r).ToList();
            if (rateAux.Count <= 0) return;
            foreach (var r in rateAux)
            {
                Node children = new Node() { Name = r.Target, Rate = (double)r.Value, Children = new List<Node>() };
                root.Children.Add(children);
                removePairRates(r.Origin, r.Target);
                ArmTree(children);

            }
        }
        /// <summary>
        /// Método que remueve los pares de la lista
        /// </summary>
        /// <remarks> Autor: Gabriel Herrera</remarks>
        private void removePairRates(string fromVal, string toVal)
        {
            var rateAux = (from r in rates where r.Origin == fromVal && r.Target == toVal select r).FirstOrDefault();
            rates.Remove(rateAux);
            rateAux = (from r in rates where r.Origin == toVal && r.Target == fromVal select r).FirstOrDefault();
            rates.Remove(rateAux);
        }
        /// <summary>
        /// Método que recorre el árbol para encontrar la conversion.
        /// </summary>
        /// <remarks> Autor: Gabriel Herrera</remarks>
        private Node SearchConversion(Node tree, string target, string origin)
        {
            totalRate = totalRate * tree.Rate;
            if (tree.Name == target)
            {
                conversion.Add(new Currency() { Origin = target, Target = origin, Value =(decimal)Math.Round((1 / totalRate),2) });
            }

            if (tree.Children.Count() == 0)
            {
                totalRate = 1;
            }
            Node nodeFound = null;
            foreach (var i in tree.Children)
            {
                nodeFound = SearchConversion(i, target,origin);
                if (nodeFound != null)
                    break;
            }
            return nodeFound;
        }

    }
}
