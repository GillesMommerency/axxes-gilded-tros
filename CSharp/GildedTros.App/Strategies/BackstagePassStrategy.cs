using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GildedTros.App.Strategies
{
    public class BackstagePassStrategy : BaseItemStrategy
    {
        protected override void UpdateItemCore(Item item)
        {
            item.SellIn--;

            if (item.Quality < 50)
            {
                item.Quality++;
                if (item.SellIn < 10 && item.Quality < 50)
                {
                    item.Quality++;
                }
                if (item.SellIn < 5 && item.Quality < 50)
                {
                    item.Quality++;
                }
            }

            // after concert
            if (item.SellIn < 0)
            {
                item.Quality = 0;
            }
        }
    }
}
