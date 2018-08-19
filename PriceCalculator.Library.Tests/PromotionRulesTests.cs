using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace PriceCalculator.Library.Tests
{
    /*
     1. Discount 10% when customer present coupon code "DIS10" or price is more/equal than 2000 baht
     2. Discount 30% when customer present coupon code "STARCARD" for 2 customers (I expected, We will give this pro with exactly 2 customers come)
     3. Come 4 pay 3 when customer present coupon code "STARCARD" (I expected, whatever how many customers come like 4,10,12 we will calculate only one free person)
     4. Discount 25% when price more/equal that 2500 baht.
     */

    [TestFixture]
    public class PromotionRulesTests
    {        
        double totalBill = 0;
        double discount = 0;

        double expectedBill = 0;
        string expectedCouponUsed = "";

        [Test]
        public void Bill_From0To2000_2Customers_DIS10(
            [Values(2)] int noOfCustomer, 
            [Values(300,400,500,900)] int price,
            [Values(PromotionRules.Coupon.DIS10)] PromotionRules.Coupon code)
        {
            totalBill=noOfCustomer * price;
            discount = totalBill * 0.1;   //discount 10%
            expectedBill = totalBill - discount;
            expectedCouponUsed = "\"DIS10\" used";
            
            PromotionRules promotion = new PromotionRules().GetBill(noOfCustomer,price,code);

            Assert.IsTrue(expectedBill == promotion.NetBill &&  expectedCouponUsed == promotion.CouponUsed, "Apply Rule 1");

        }

        [Test]
        public void Bill_From0To2000_2Customers_NONE(
            [Values(2)] int noOfCustomer,
            [Values(300, 400, 550, 800)] int price,
            [Values(PromotionRules.Coupon.NONE)] PromotionRules.Coupon code)
        {
            totalBill = noOfCustomer * price;
            discount = 0; //No Discount
            expectedBill = totalBill-discount;  //No any discount
            expectedCouponUsed = "No coupon used";

            PromotionRules promotion = new PromotionRules().GetBill(noOfCustomer, price, code);

            Assert.IsTrue(expectedBill == promotion.NetBill && expectedCouponUsed == promotion.CouponUsed, "No Rule Apply");

        }

        [Test]
        public void Bill_From0To2000_2Customers_STARCARD(
            [Values(2)] int noOfCustomer,
            [Values(250, 350, 500, 850)] int price,
            [Values(PromotionRules.Coupon.STARCARD)] PromotionRules.Coupon code)
        {
            totalBill = noOfCustomer * price;
            discount = totalBill * 0.3; //Discount 30%
            expectedBill = totalBill - discount;  //No any discount
            expectedCouponUsed = "\"STARCARD\" used";

            PromotionRules promotion = new PromotionRules().GetBill(noOfCustomer, price, code);

            Assert.IsTrue(expectedBill == promotion.NetBill && expectedCouponUsed == promotion.CouponUsed, "Apply Rule 2");

        }

        [Test]
        public void Bill_From0To2000_4CustomersOrMore_NONE(
            [Values(4,6,8,9)] int noOfCustomer,
            [Values(100, 180, 200)] int price,
            [Values(PromotionRules.Coupon.NONE)] PromotionRules.Coupon code)
        {
            totalBill = noOfCustomer * price;
            discount = 0; //No Discount
            expectedBill = totalBill-discount;  //No any discount
            expectedCouponUsed = "No coupon used";

            PromotionRules promotion = new PromotionRules().GetBill(noOfCustomer, price, code);

            Assert.IsTrue(expectedBill == promotion.NetBill && expectedCouponUsed == promotion.CouponUsed, "No Rule Apply");

        }

        [Test]
        public void Bill_From0To2000_4CustomersOrMore_DIS10(
            [Values(4, 6, 8, 9)] int noOfCustomer,
            [Values(100, 180, 200)] int price,
            [Values(PromotionRules.Coupon.DIS10)] PromotionRules.Coupon code)
        {
            totalBill = noOfCustomer * price;
            discount = totalBill * 0.1; //Discount 10%
            expectedBill = totalBill - discount;
            expectedCouponUsed = "\"DIS10\" used";

            PromotionRules promotion = new PromotionRules().GetBill(noOfCustomer, price, code);

            Assert.IsTrue(expectedBill == promotion.NetBill && expectedCouponUsed == promotion.CouponUsed, "Apply Rule 1");

        }

        [Test]
        public void Bill_From0To2000_4CustomersOrMore_STARCARD(
            [Values(4,6,8,9)] int noOfCustomer,
            [Values(100, 180, 200)] int price,
            [Values(PromotionRules.Coupon.STARCARD)] PromotionRules.Coupon code)
        {
            totalBill = noOfCustomer * price;
            discount = price; //Come 4 pay 3 
            expectedBill = totalBill - discount;
            expectedCouponUsed = "\"STARCARD\" used";

            PromotionRules promotion = new PromotionRules().GetBill(noOfCustomer, price, code);

            Assert.IsTrue(expectedBill == promotion.NetBill && expectedCouponUsed == promotion.CouponUsed, "Apply Rule 3");

        }

        [Test]
        public void Bill_From0To2000_DefaultCustomers_DIS10(
            [Values(1,3)] int noOfCustomer,
            [Values(450, 500, 600)] int price,
            [Values(PromotionRules.Coupon.DIS10)] PromotionRules.Coupon code)
            
        {
            totalBill = noOfCustomer * price;
            discount = totalBill * 0.1; //Discount 10%
            expectedBill = totalBill - discount;
            expectedCouponUsed = "\"DIS10\" used";

            PromotionRules promotion = new PromotionRules().GetBill(noOfCustomer, price, code);

            Assert.IsTrue(expectedBill == promotion.NetBill && expectedCouponUsed == promotion.CouponUsed, "Apply Rule 1");

        }

        [Test]
        public void Bill_From0To2000_DefaultCustomers_DefaultCoupon(            
            [Values(1,3)] int noOfCustomer,
            [Values(450, 500, 600)] int price,
            [Values(PromotionRules.Coupon.NONE, PromotionRules.Coupon.STARCARD)] PromotionRules.Coupon code)
        {
            totalBill = noOfCustomer * price;
            discount = 0; //No Discount
            expectedBill = totalBill - discount;
            expectedCouponUsed = "No coupon used";

            PromotionRules promotion = new PromotionRules().GetBill(noOfCustomer, price, code);

            Assert.IsTrue(expectedBill == promotion.NetBill && expectedCouponUsed == promotion.CouponUsed, "No Rule Apply");

        }

        [Test]
        public void Bill_From2000To2500_2Customers_DefaultCoupon(            
            [Values(2)] int noOfCustomer,
            [Values(1000, 1100, 1200)] int price,
            [Values(PromotionRules.Coupon.NONE, PromotionRules.Coupon.DIS10)] PromotionRules.Coupon code)
        {
            totalBill = noOfCustomer * price;
            discount = totalBill*0.1; //discount10%
            expectedBill = totalBill - discount;
            expectedCouponUsed = (code == PromotionRules.Coupon.DIS10) ? "\"DIS10\" used" : "No coupon used";


            PromotionRules promotion = new PromotionRules().GetBill(noOfCustomer, price, code);

            Assert.IsTrue(expectedBill == promotion.NetBill && expectedCouponUsed == promotion.CouponUsed, "Apply Rule 1");

        }


        [Test]
        public void Bill_From2000To2500_2Customers_STARCARD(        
            [Values(2)] int noOfCustomer,
            [Values(1000, 1100, 1200)] int price,
            [Values(PromotionRules.Coupon.STARCARD)] PromotionRules.Coupon code)
        {
            totalBill = noOfCustomer * price;
            discount = totalBill * 0.3; //discount30%
            expectedBill = totalBill - discount;
            expectedCouponUsed = "\"STARCARD\" used";

            PromotionRules promotion = new PromotionRules().GetBill(noOfCustomer, price, code);

            Assert.IsTrue(expectedBill == promotion.NetBill && expectedCouponUsed == promotion.CouponUsed, "Apply Rule 2");

        }

        [Test]
        public void Bill_From2000To2500_4Customers_DefaultCoupon(
            [Values(4)] int noOfCustomer,
            [Values(500,550,600)] int price,
            [Values(PromotionRules.Coupon.NONE,PromotionRules.Coupon.DIS10)] PromotionRules.Coupon code)
        {
            totalBill = noOfCustomer * price;
            discount = totalBill * 0.1; //discount10%
            expectedBill = totalBill - discount;
            expectedCouponUsed = (code == PromotionRules.Coupon.DIS10) ? "\"DIS10\" used" : "No coupon used";

            PromotionRules promotion = new PromotionRules().GetBill(noOfCustomer, price, code);

            Assert.IsTrue(expectedBill == promotion.NetBill && expectedCouponUsed == promotion.CouponUsed, "Apply Rule 1");

        }

        [Test]
        public void Bill_From2000To2500_4Customers_STARCARD(
            [Values(4)] int noOfCustomer,
            [Values(500, 550, 600)] int price,
            [Values(PromotionRules.Coupon.STARCARD)] PromotionRules.Coupon code)
        {
            totalBill = noOfCustomer * price;
            discount = price; //come 4 pay 3
            expectedBill = totalBill - discount;
            expectedCouponUsed = "\"STARCARD\" used";

            PromotionRules promotion = new PromotionRules().GetBill(noOfCustomer, price, code);

            Assert.IsTrue(expectedBill == promotion.NetBill && expectedCouponUsed == promotion.CouponUsed, "Apply Rule 2");

        }

        [Test]
        public void Bill_From2000To2500_DefaultCustomers_DefaultCoupon(
            [Values(3)] int noOfCustomer,
            [Values(700,780,800)] int price,
            [Values(PromotionRules.Coupon.NONE, PromotionRules.Coupon.DIS10,PromotionRules.Coupon.STARCARD)] PromotionRules.Coupon code)
        {
            totalBill = noOfCustomer * price;
            discount = totalBill*0.1; //discount10%
            expectedBill = totalBill - discount;
            expectedCouponUsed = (code == PromotionRules.Coupon.DIS10) ? "\"DIS10\" used" : "No coupon used";

            PromotionRules promotion = new PromotionRules().GetBill(noOfCustomer, price, code);

            Assert.IsTrue(expectedBill == promotion.NetBill && expectedCouponUsed == promotion.CouponUsed, "Apply Rule 1");

        }

        [Test]
        public void Bill_From2500Up_2Customers_DefaultCoupon(
            [Values(2)] int noOfCustomer,
            [Values(1300, 1500, 2000)] int price,
            [Values(PromotionRules.Coupon.NONE, PromotionRules.Coupon.DIS10)] PromotionRules.Coupon code)
        {
            totalBill = noOfCustomer * price;
            discount = totalBill * 0.25; //discount25%
            expectedBill = totalBill - discount;
            expectedCouponUsed = "No coupon used";

            PromotionRules promotion = new PromotionRules().GetBill(noOfCustomer, price, code);

            Assert.IsTrue(expectedBill == promotion.NetBill && expectedCouponUsed == promotion.CouponUsed, "Apply Rule 4");

        }

        [Test]
        public void Bill_From2500Up_2Customers_STARCARD(
            [Values(2)] int noOfCustomer,
            [Values(1300, 1500, 2000)] int price,
            [Values(PromotionRules.Coupon.STARCARD)] PromotionRules.Coupon code)
        {
            totalBill = noOfCustomer * price;
            discount = totalBill * 0.30; //discount30%
            expectedBill = totalBill - discount;
            expectedCouponUsed = "\"STARCARD\" used";

            PromotionRules promotion = new PromotionRules().GetBill(noOfCustomer, price, code);

            Assert.IsTrue(expectedBill == promotion.NetBill && expectedCouponUsed == promotion.CouponUsed, "Apply Rule 2");

        }

        [Test]
        //NONE, DIS10
        public void Bill_From2500Up_4CustomersOrMore_DefaultCoupon(
            [Values(4,5,6)] int noOfCustomer,
            [Values(700,1000, 1200, 2000)] int price,
            [Values(PromotionRules.Coupon.NONE,PromotionRules.Coupon.DIS10)] PromotionRules.Coupon code)
        {
            totalBill = noOfCustomer * price;
            discount = totalBill * 0.25; //discount25%
            expectedBill = totalBill - discount;
            expectedCouponUsed = "No coupon used";

            PromotionRules promotion = new PromotionRules().GetBill(noOfCustomer, price, code);

            Assert.IsTrue(expectedBill == promotion.NetBill && expectedCouponUsed == promotion.CouponUsed, "Apply Rule 4");
        }

        [Test]
        //Some case "STARCARD" get equal discount same as Rule4(Total bill >=2500) So assumption to no coupon used auto apply for Rule4
        public void Bill_From2500Up_4CustomersOrMore_STARCARD(
            [Values(4, 7, 12)] int noOfCustomer,
            [Values(700, 1000, 1200, 2000)] int price,
            [Values(PromotionRules.Coupon.STARCARD)] PromotionRules.Coupon code)
        {
            totalBill = noOfCustomer * price;
            discount = totalBill * 0.25; //discount25%
            expectedBill = totalBill - discount;
            expectedCouponUsed = "No coupon used";

            PromotionRules promotion = new PromotionRules().GetBill(noOfCustomer, price, code);

            Assert.IsTrue(expectedBill == promotion.NetBill && expectedCouponUsed == promotion.CouponUsed, "Apply Rule 4");

        }

        [Test]
        public void Bill_From2500Up_DefaultCustomers_DefaultCoupon(
            [Values(1,3)] int noOfCustomer,
            [Values(2500, 2800,3000)] int price,
            [Values(PromotionRules.Coupon.STARCARD,PromotionRules.Coupon.DIS10,PromotionRules.Coupon.NONE)] PromotionRules.Coupon code)
        {
            totalBill = noOfCustomer * price;
            discount = totalBill * 0.25; //discount25%
            expectedBill = totalBill - discount;
            expectedCouponUsed = "No coupon used";

            PromotionRules promotion = new PromotionRules().GetBill(noOfCustomer, price, code);

            Assert.IsTrue(expectedBill == promotion.NetBill && expectedCouponUsed== promotion.CouponUsed, "Apply Rule 4");

        }

    }
}
