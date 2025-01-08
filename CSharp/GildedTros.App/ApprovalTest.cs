using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ApprovalTests;
using ApprovalTests.Reporters;
using Xunit;

namespace GildedTros.App
{
    [UseReporter(typeof(DiffReporter))]
    public class ApprovalTest
    {
        //[Fact]
        //public void ThirtyDays()
        //{
        //    var fakeoutput = new StringBuilder();
        //    Console.SetOut(new StringWriter(fakeoutput));
        //    Console.SetIn(new StringReader("a\n"));

        //    Program.Main(new string[] { });
        //    var output = fakeoutput.ToString();

        //    Approvals.Verify(output);
        //}

        [Fact]
        public void AllItems_Should_MatchFinalValues_AfterThirtyDays()
        {
            #region Arrange
            // same as in main
            IList<Item> items = new List<Item>
            {
                new Item {Name = "Ring of Cleansening Code", SellIn = 10, Quality = 20},
                new Item {Name = "Good Wine", SellIn = 2, Quality = 0},
                new Item {Name = "Elixir of the SOLID", SellIn = 5, Quality = 7},
                new Item {Name = "B-DAWG Keychain", SellIn = 0, Quality = 80},
                new Item {Name = "B-DAWG Keychain", SellIn = -1, Quality = 80},
                new Item {Name = "Backstage passes for Re:factor", SellIn = 15, Quality = 20},
                new Item {Name = "Backstage passes for Re:factor", SellIn = 10, Quality = 49},
                new Item {Name = "Backstage passes for HAXX", SellIn = 5, Quality = 49},
                new Item {Name = "Duplicate Code", SellIn = 3, Quality = 6},
                new Item {Name = "Long Methods", SellIn = 3, Quality = 6},
                new Item {Name = "Ugly Variable Names", SellIn = 3, Quality = 6}
            };

          
            // final values
            var expectedAfter30Days = new[]
            {
                new { Name = "Ring of Cleansening Code", FinalSellIn = -20, FinalQuality = 0 },
                new { Name = "Good Wine", FinalSellIn = -28, FinalQuality = 30 },
                new { Name = "Elixir of the SOLID", FinalSellIn = -25, FinalQuality = 0 },
                new { Name = "B-DAWG Keychain", FinalSellIn = 0, FinalQuality = 80 },
                new { Name = "B-DAWG Keychain", FinalSellIn = -1, FinalQuality = 80 },
                new { Name = "Backstage passes for Re:factor", FinalSellIn = -15, FinalQuality = 0 },
                new { Name = "Backstage passes for Re:factor", FinalSellIn = -20, FinalQuality = 0 },
                new { Name = "Backstage passes for HAXX", FinalSellIn = -25, FinalQuality = 0 },
                new { Name = "Duplicate Code", FinalSellIn = -27, FinalQuality = 0 },
                new { Name = "Long Methods", FinalSellIn = -27, FinalQuality = 0 },
                new { Name = "Ugly Variable Names", FinalSellIn = -27, FinalQuality = 0 }
            };

            var app = new GildedTros(items);
            #endregion

            #region Act
            // sim 30 days
            for (int day = 0; day < 30; day++)
            {
                app.UpdateQuality();
            }
            #endregion

            #region Assert
            // Verify each item matches the expected final SellIn/Quality
            for (int i = 0; i < items.Count; i++)
            {
                Assert.Equal(expectedAfter30Days[i].Name, items[i].Name);
                Assert.Equal(expectedAfter30Days[i].FinalSellIn, items[i].SellIn);
                Assert.Equal(expectedAfter30Days[i].FinalQuality, items[i].Quality);
            }
            #endregion
        }
    }
}
