using App.Data.Model.Entity.Dto;
using App.Data.Model.Model;
using System.Collections.Generic;

namespace App.Interface.Business
{
    public interface ITransaction
    {
        List<ProductTransaction> GetTransaction();
        void SetTransaction();
        TotalTransaction[] GetSkuValue(string sku);
        TotalTransaction[] GetTotalTransaction();
    }
}
