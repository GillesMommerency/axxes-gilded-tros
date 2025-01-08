using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GildedTros.App.Strategies
{
    public interface IItemUpdateStrategy
    {
        void UpdateItem(Item item);
    }
}
