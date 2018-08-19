using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceCalculator.Library
{
    public class PromotionRules
    {
        public enum Coupon {NONE, DIS10, STARCARD}

        double _netBill;
        string _couponUsed;


        public double NetBill 
        {
            get { return _netBill;}
            set { _netBill = value; }
        }

        public string CouponUsed
        {
            get { return _couponUsed; }
            set { _couponUsed = value; }
        }

        public PromotionRules()
        {
        }

        public PromotionRules(double netBill, string couponUsed)
        {
           this._netBill = netBill;
           this._couponUsed = couponUsed;
        }

        public PromotionRules GetBill(int _noOfCustomer, int _pricePerPerson, Coupon _code)
        {
            double totalBill = _noOfCustomer * _pricePerPerson;
            double netBill=0;
            double discount = 0;
            string couponUsed = "";

            if (totalBill >= 0 && totalBill < 2000)
            {
                //Default customer 1 or 3
                couponUsed = "No coupon used";

                if (_code == Coupon.DIS10)
                {
                    discount = totalBill * 0.10; //discount10%
                    couponUsed = "\"DIS10\" used";
                }

                if (_code == Coupon.STARCARD)
                {
                    if (_noOfCustomer == 2)
                    {
                        discount = totalBill * 0.30; //discount30%
                        couponUsed = "\"STARCARD\" used";
                    }

                    if (_noOfCustomer >= 4)
                    {
                        if (discount < _pricePerPerson)
                        {
                            discount = _pricePerPerson;
                            couponUsed = "\"STARCARD\" used";
                        }
                    }
                }                
            }

            if (totalBill >= 2000 && totalBill < 2500)
            {
                //Default customer 1 or 3
                discount = totalBill * 0.10; //discount10%

                if (_code == Coupon.DIS10)
                {
                    couponUsed = "\"DIS10\" used";
                }
                else
                {
                    couponUsed = "No coupon used";
                }

                if (_code == Coupon.STARCARD)
                {                  
                    if(_noOfCustomer ==2){
                        if (discount < totalBill * 0.30)
                        {
                            discount = totalBill * 0.30; //discount30%
                            couponUsed = "\"STARCARD\" used";
                        }
                    }

                    if(_noOfCustomer>=4)
                    {
                        if (discount < _pricePerPerson)
                        {
                            discount = _pricePerPerson; //come 4 pay 3
                            couponUsed = "\"STARCARD\" used";
                        }
                    }
                }
            }

            if(totalBill>=2500){
                //Default customer 1 or 3
                discount = totalBill * 0.25; //discount25%
                couponUsed = "No coupon used";

                if (_code == Coupon.STARCARD)
                {
                    if (_noOfCustomer == 2)
                    {
                        if (discount < totalBill * 0.30)
                        {
                            discount = totalBill * 0.30; //discount30%
                            couponUsed = "\"STARCARD\" used";
                        }
                    }

                    if (_noOfCustomer >= 4)
                    {
                        if (discount < _pricePerPerson)
                        {
                            discount = _pricePerPerson; //come 4 pay 3
                            couponUsed = "\"STARCARD\" used";
                        }
                    }
                }
            }

            netBill = totalBill - discount;

            return new PromotionRules(netBill, couponUsed);
                
        }
    }
}
