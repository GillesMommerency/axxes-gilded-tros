using GildedTros.App.Strategies;
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

                // ensuring quality
                if (item.Name != "B-DAWG Keychain")
                {
                    if (item.Quality < 0) item.Quality = 0;
                    if (item.Quality > 50) item.Quality = 50;
                }
            }
        }
    }
}
