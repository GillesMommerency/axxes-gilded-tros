using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GildedTros.App.Strategies
{
    public class LegendaryItemStrategy : BaseItemStrategy
    {
        protected override void UpdateItemCore(Item item)
        {
            // never changes for now
        }

        protected override void EnforceBoundaries(Item item)
        {
            item.Quality = 80;
        }
    }
}
