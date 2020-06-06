using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CostPrice.Models
{
    public class shareResponse
    {
        public double cp { get; set; }
        public double gain { get; set; }
        public int sharesRem { get; set; }
        public double cpRem { get; set; }
    }
    public static class Calculation
    {
       public static List<CostModel> li = new List<CostModel>()
            { new CostModel{ purchaseDate=DateTime.Parse("1/1/2005"),shareNo=100,price=10.0F },
                new CostModel{ purchaseDate=DateTime.Parse("2/2/2005"),shareNo=40,price=12.0F }
                , new CostModel{ purchaseDate=DateTime.Parse("3/3/2005"),shareNo=50,price=11.0F }

            };
        public static shareResponse FIFO(CostModel s, string type)
        {

            List<CostModel> liDate = li.Where(x => (x.purchaseDate <= s.purchaseDate)).ToList<CostModel>();
            var liRem = li.Where(x => (x.purchaseDate > s.purchaseDate)).Select(x => x.price * x.shareNo);
                int sharesPurchased = li.Sum(x => x.shareNo);
            int sharesPurchasedDate = liDate.Sum(x => x.shareNo);
                int shareRemaining = sharesPurchased - s.shareNo;
            //sharesPurchased = liNew.Sum(x => x.shareNo);
            double totalCost, costPricePerShare, profit, cpshareRemaining = 0.0;
            if (sharesPurchased >= s.shareNo)
            {

                switch (type)
                {
                    case "FIFO":
                        totalCost = li[0].price * sharesPurchasedDate + liRem.Sum() ;
                        break;
                    case "LIFO":
                        totalCost = li[li.Count - 1].price * sharesPurchasedDate + liRem.Sum();
                        break;
                    case "Highest Cost":
                        double max = li.Max(r => r.price);
                        totalCost = max * sharesPurchasedDate + liRem.Sum();
                        break;
                    case "Lowest Cost":
                        double min = li.Min(r => r.price);
                        totalCost = min * sharesPurchasedDate + liRem.Sum();
                        break;
                    case "Weighted Average":
                        double sum = li.Sum(r => r.shareNo * r.price);
                        totalCost = sum;
                        break;
                    default:
                        totalCost = li[0].price * sharesPurchasedDate + liRem.Sum();
                        break;

                }

                costPricePerShare = (double)totalCost / sharesPurchased;

                profit = s.shareNo * s.price - s.shareNo * costPricePerShare;

                cpshareRemaining = (double)(totalCost - s.shareNo * costPricePerShare) / shareRemaining;








                return new shareResponse { cp = costPricePerShare, gain = profit, sharesRem = shareRemaining, cpRem = cpshareRemaining };

            }
            else
            return new shareResponse { cp = 0, gain = 0, sharesRem = 0, cpRem = 0 };
        }
    }
}
