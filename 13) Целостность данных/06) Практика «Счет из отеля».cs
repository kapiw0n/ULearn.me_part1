using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace HotelAccounting
{
    class AccountingModel : ModelBase
    {
        private double PriceValue;
        private int NightsCountValue;
        private double DiscountValue;
        private double TotalValue;

        public double Price
        {
            get { return PriceValue; }
            set
            {
                if (value >= 0) PriceValue = value;
                else throw new ArgumentException();
                UpdateTotal();
                Notify(nameof(Price));
            }
        }

        public int NightsCount
        {
            get { return NightsCountValue; }
            set
            {
                if (value > 0) NightsCountValue = value;
                else throw new ArgumentException();
                UpdateTotal();
                Notify(nameof(NightsCount));
            }
        }

        public double Discount
        {
            get { return DiscountValue; }
            set
            {
                DiscountValue = value;
                if (DiscountValue != ((-1) * TotalValue / (PriceValue * NightsCountValue) + 1) * 100)
                    UpdateTotal();
                Notify(nameof(Discount));
            }
        }

        public double Total
        {
            get { return TotalValue; }
            set
            {
                if (value > 0) TotalValue = value;
                else throw new ArgumentException();
                if (TotalValue != PriceValue * NightsCountValue * (1 - DiscountValue / 100))
                    UpdateDiscount();
                Notify(nameof(Total));
            }
        }

        public AccountingModel()
        {
            PriceValue = 0;
            NightsCountValue = 1;
            DiscountValue = 0;
            TotalValue = 0;
        }

        private void UpdateTotal()
        {
            Total = Price * NightsCount * (1 - Discount / 100);
        }

        private void UpdateDiscount()
        {
            Discount = ((-1) * Total / (Price * NightsCount) + 1) * 100;
        }
    }
}