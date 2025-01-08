using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GildedTros.App.Strategies
{
    public static class ItemStrategyFactory
    {
        public static IItemUpdateStrategy GetStrategy(string itemName)
        {
            if (itemName == "B-DAWG Keychain")
                return new LegendaryItemStrategy();

            if (itemName == "Good Wine")
                return new GoodWineStrategy();

            if (itemName == "Backstage passes for Re:factor"
             || itemName == "Backstage passes for HAXX")
                return new BackstagePassStrategy();

            return new RegularItemStrategy();
        }
    }
}
