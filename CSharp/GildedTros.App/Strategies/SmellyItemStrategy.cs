using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GildedTros.App.Strategies
{
    internal class SmellyItemStrategy : BaseItemStrategy
    {
        protected override void UpdateItemCore(Item item)
        {
            item.SellIn--;

            // degrace twice as fast
            int degrade = (item.SellIn < 0) ? 4 : 2;

            item.Quality -= degrade;
        }
    }
}
