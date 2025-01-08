using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GildedTros.App.Strategies
{
    public class GoodWineStrategy : IItemUpdateStrategy
    {
        public void UpdateItem(Item item)
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
