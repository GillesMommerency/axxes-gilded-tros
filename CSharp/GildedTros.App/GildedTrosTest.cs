using System.Collections.Generic;
using Xunit;

namespace GildedTros.App
{
    public class GildedTrosTest
    {
        //  Regular Items
        [Theory]
        [InlineData("Ring of Cleansening Code", 10, 20, 9, 19)]
        [InlineData("Elixir of the SOLID", 5, 7, 4, 6)]
        [InlineData("foo", 0, 0, -1, 0)] // degrade can't go below 0
        [InlineData("foo", 0, 10, -1, 8)] // degrade 2 after SellIn < 0
        public void RegularItems_DegradeProperly_OneUpdate(
            string name,
            int initialSellIn,
            int initialQuality,
            int expectedSellIn,
            int expectedQuality)
        {
            #region Arrange
            IList<Item> items = new List<Item>
            {
                new Item { Name = name, SellIn = initialSellIn, Quality = initialQuality }
            };
            var app = new GildedTros(items);
            #endregion

            #region Act
            app.UpdateQuality();
            #endregion

            #region Assert
            Assert.Equal(expectedSellIn, items[0].SellIn);
            Assert.Equal(expectedQuality, items[0].Quality);
            #endregion
        }

        // Good Wine
        [Theory]
        [InlineData("Good Wine", 2, 0, 1, 1)]   // increases by 1
        [InlineData("Good Wine", 0, 10, -1, 11)] // "once the sell by date has passed, quality DEGRADES twice as fast so should not double in this instance"
        [InlineData("Good Wine", 0, 50, -1, 50)] // never exceed 50
        public void GoodWine_IncreasesQualityPerRequirements(
            string name,
            int initialSellIn,
            int initialQuality,
            int expectedSellIn,
            int expectedQuality)
        {
            #region Arrange
            IList<Item> items = new List<Item>
            {
                new Item { Name = name, SellIn = initialSellIn, Quality = initialQuality }
            };
            var app = new GildedTros(items);
            #endregion

            #region Act
            app.UpdateQuality();
            #endregion

            #region Assert
            Assert.Equal(expectedSellIn, items[0].SellIn);
            Assert.Equal(expectedQuality, items[0].Quality);
            #endregion
        }

        // B-DAWG Keychain (Legendary)
        [Theory]
        [InlineData("B-DAWG Keychain", 0, 80)]
        [InlineData("B-DAWG Keychain", -1, 80)]
        [InlineData("B-DAWG Keychain", 5, 80)]
        public void LegendaryItem_BDawgKeychain_NeverChanges(
            string name,
            int initialSellIn,
            int initialQuality)
        {
            #region Arrange
            IList<Item> items = new List<Item>
            {
                new Item { Name = name, SellIn = initialSellIn, Quality = initialQuality }
            };
            var app = new GildedTros(items);
            #endregion

            #region Act
            app.UpdateQuality();
            #endregion

            #region Assert
            Assert.Equal(initialSellIn, items[0].SellIn);
            Assert.Equal(initialQuality, items[0].Quality);
            #endregion
        }

        // Backstage Passes
        [Theory]
        [InlineData("Backstage passes for Re:factor", 15, 20, 14, 21)]
        [InlineData("Backstage passes for Re:factor", 10, 45, 9, 47)]  // +2
        [InlineData("Backstage passes for Re:factor", 5, 45, 4, 48)]   // +3
        [InlineData("Backstage passes for Re:factor", 1, 49, 0, 50)]   // cap at 50
        [InlineData("Backstage passes for Re:factor", 0, 49, -1, 0)]   // after concert =>0

        [InlineData("Backstage passes for HAXX", 15, 20, 14, 21)]
        [InlineData("Backstage passes for HAXX", 10, 45, 9, 47)]
        [InlineData("Backstage passes for HAXX", 5, 45, 4, 48)]
        [InlineData("Backstage passes for HAXX", 1, 49, 0, 50)]
        [InlineData("Backstage passes for HAXX", 0, 49, -1, 0)]
        public void BackstagePasses_QualityBehavior(
            string name,
            int initialSellIn,
            int initialQuality,
            int expectedSellIn,
            int expectedQuality)
        {
            #region Arrange
            IList<Item> items = new List<Item>
            {
                new Item { Name = name, SellIn = initialSellIn, Quality = initialQuality }
            };
            var app = new GildedTros(items);
            #endregion

            #region Act
            app.UpdateQuality();
            #endregion

            #region Assert
            Assert.Equal(expectedSellIn, items[0].SellIn);
            Assert.Equal(expectedQuality, items[0].Quality);
            #endregion
        }

        // Smelly Items
        [Theory]
        [InlineData("Duplicate Code", 3, 6, 2, 4)]  // degrade 2 => 4
        [InlineData("Duplicate Code", 0, 6, -1, 2)] // degrade 4 => 2
        [InlineData("Long Methods", 3, 6, 2, 4)]
        [InlineData("Long Methods", 0, 6, -1, 2)]
        [InlineData("Ugly Variable Names", 3, 6, 2, 4)]
        [InlineData("Ugly Variable Names", 0, 6, -1, 2)]
        public void SmellyItems_DegradeTwiceAsFast(
            string name,
            int initialSellIn,
            int initialQuality,
            int expectedSellIn,
            int expectedQuality)
        {
            #region Arrange
            IList<Item> items = new List<Item>
            {
                new Item { Name = name, SellIn = initialSellIn, Quality = initialQuality }
            };
            var app = new GildedTros(items);
            #endregion

            #region Act
            app.UpdateQuality();
            #endregion

            #region Assert
            Assert.Equal(expectedSellIn, items[0].SellIn);
            Assert.Equal(expectedQuality, items[0].Quality);
            #endregion
        }

        //Edge Cases
        [Theory]
        [InlineData("Elixir of the SOLID", 0, 0, -1, 0)]  // cant go below 0
        [InlineData("Elixir of the SOLID", 0, 1, -1, 0)]  // degrade 2 => 0
        [InlineData("Good Wine", 0, 50, -1, 50)]          // cantt exceed 50
        [InlineData("Backstage passes for Re:factor", 1, 50, 0, 50)] // if already 50 => remains 50
        [InlineData("Backstage passes for Re:factor", 0, 50, -1, 0)] // after show => 0
        [InlineData("B-DAWG Keychain", 0, 80, 0, 80)]     // always 80
        public void QualityBoundaries_AndSpecialItems(
            string name,
            int initialSellIn,
            int initialQuality,
            int expectedSellIn,
            int expectedQuality)
        {
            #region Arrange
            IList<Item> items = new List<Item>
            {
                new Item { Name = name, SellIn = initialSellIn, Quality = initialQuality }
            };
            var app = new GildedTros(items);
            #endregion

            #region Act
            app.UpdateQuality();
            #endregion

            #region Assert
            Assert.Equal(expectedSellIn, items[0].SellIn);
            Assert.Equal(expectedQuality, items[0].Quality);
            #endregion
        }

        [Theory]
        [InlineData("Duplicate Code", 0, 1, 0)] // Sellin 0 => degrade 4 => floor 0
        [InlineData("Elixir of the SOLID", 0, 1, 0)]  // normal item => degrade 2 => floor 0
        [InlineData("Good Wine", 0, 49, 50)] // good wine => gain 2 => cap 50
        [InlineData("Good Wine", 0, 50, 50)] // already 50 => stay 50
        public void EdgeCases(string name, int sellIn, int startQuality, int expectedQuality)
        {
            var items = new List<Item> { new Item { Name = name, SellIn = sellIn, Quality = startQuality } };
            var app = new GildedTros(items);

            app.UpdateQuality();
            Assert.Equal(expectedQuality, items[0].Quality);
        }
    }
}