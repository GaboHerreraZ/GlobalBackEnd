using App.Data.Model.Model;
using System.Collections.Generic;


namespace App.Interface.Business
{
    public interface IRate
    {
         List<Rate> GetRate();
        void SetRate();
        void CalculateConversion(string target);

    }
}
