using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GildedTros.App.Strategies
{
    public abstract class BaseItemStrategy : IItemUpdateStrategy
    {
        public void UpdateItem(Item item)
        {
           // item specific logic
            UpdateItemCore(item);

            // boundaries enforcing
            EnforceBoundaries(item);
        }

        protected abstract void UpdateItemCore(Item item);

        protected virtual void EnforceBoundaries(Item item)
        {
            if (item.Quality < 0) item.Quality = 0;
            if (item.Quality > 50) item.Quality = 50;
        }
    }
}
