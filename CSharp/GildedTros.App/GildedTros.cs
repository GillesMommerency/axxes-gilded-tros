using GildedTros.App.Strategies;
using System;
using System.Collections.Generic;

namespace GildedTros.App
{
    public class GildedTros
    {
        IList<Item> Items;
        public GildedTros(IList<Item> Items)
        {
            this.Items = Items;
        }

        public void UpdateQuality()
        {
            foreach (var item in Items)
            {
                var strategy = ItemStrategyFactory.GetStrategy(item.Name);
                strategy.UpdateItem(item);
            }
        }
    }
}
