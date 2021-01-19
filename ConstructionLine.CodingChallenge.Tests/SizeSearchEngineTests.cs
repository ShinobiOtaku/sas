using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace ConstructionLine.CodingChallenge.Tests
{
    public class SizeSearchEngineTests
    {
        [Test]
        public void SimpleCase()
        {
            var smallShirt = new Shirt(Guid.NewGuid(), "Red-Small", Size.Small, Color.Red);
            var shirts = new List<Shirt>
            {
                smallShirt,
                new Shirt(Guid.NewGuid(), "Black-Medium", Size.Medium, Color.Black),
                new Shirt(Guid.NewGuid(), "Blue-Large", Size.Large,  Color.Blue),
            };

            var searchEngine = new SizeSearchEngine(shirts);

            var results = searchEngine.Search(new[] { Size.Small });

            Assert.AreEqual(1, results.Shirts.Count());
            Assert.AreEqual(smallShirt, results.Shirts.Single());

            Assert.AreEqual(Size.All.Count(), results.SizeCounts.Count());
            Assert.AreEqual(results.SizeCounts.Single(x => x.Size == Size.Small).Count, 1);
            Assert.IsTrue(results.SizeCounts.Where(x => x.Size != Size.Small).All(x => x.Count == 0));
        }

        [Test]
        public void NoShirtsFound()
        {
            var shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Black-Medium", Size.Medium, Color.Black),
                new Shirt(Guid.NewGuid(), "Blue-Large", Size.Large,  Color.Blue),
            };

            var searchEngine = new SizeSearchEngine(shirts);

            var results = searchEngine.Search(new[] { Size.Small });

            Assert.AreEqual(0, results.Shirts.Count());
            Assert.IsTrue(results.SizeCounts.All(x => x.Count == 0));
        }

        [Test]
        public void FindsMultipleShirtsForSameSize()
        {
            var shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Red-Small-1", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Red-Small-2", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Black-Medium", Size.Medium, Color.Black),
                new Shirt(Guid.NewGuid(), "Blue-Large", Size.Large,  Color.Blue),
            };

            var searchEngine = new SizeSearchEngine(shirts);

            var results = searchEngine.Search(new[] { Size.Small });

            Assert.AreEqual(2, results.Shirts.Count());
            Assert.AreEqual(results.SizeCounts.Single(x => x.Size == Size.Small).Count, 2);
        }

        [Test]
        public void FindsMultipleShirtsForMultipleSizes()
        {
            var shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Red-Small-1", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Red-Small-2", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Black-Medium", Size.Medium, Color.Black),
                new Shirt(Guid.NewGuid(), "Blue-Large", Size.Large,  Color.Blue),
            };

            var searchEngine = new SizeSearchEngine(shirts);

            var results = searchEngine.Search(new[] { Size.Small, Size.Medium });

            Assert.AreEqual(3, results.Shirts.Count());

            // The readme doesn't specify, but the existing test suggests that this relates to the number in the inventory rather than the number in the results
            Assert.AreEqual(results.SizeCounts.Single(x => x.Size == Size.Small).Count, 2);
            Assert.AreEqual(results.SizeCounts.Single(x => x.Size == Size.Medium).Count, 1);
        }
    }
}
