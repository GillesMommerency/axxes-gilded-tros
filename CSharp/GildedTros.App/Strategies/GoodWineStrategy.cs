using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GildedTros.App.Strategies
{
    public class GoodWineStrategy : BaseItemStrategy
    {
        protected override void UpdateItemCore(Item item)
        {
            item.SellIn--;

            // good wine improves instead of decrease
            if (item.Quality < 50)
            {
                item.Quality++;
            }
        }
    }
}
