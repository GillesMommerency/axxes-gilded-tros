using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GildedTros.App.Strategies
{
    public class RegularItemStrategy : IItemUpdateStrategy
    {
        public void UpdateItem(Item item)
        {
            item.SellIn--;

            // default: decrease 1 normally
            if (item.Quality > 0)
            {
                item.Quality--;
            }

            // default: SellIn <0 degrade twice as fast
            if (item.SellIn < 0 && item.Quality > 0)
            {
                item.Quality--;
            }
        }
    }
}
