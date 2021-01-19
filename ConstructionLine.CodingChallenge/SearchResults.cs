using System.Collections.Generic;

namespace ConstructionLine.CodingChallenge
{
    public class SearchResults
    {
        public IEnumerable<Shirt> Shirts { get; set; }


        public IEnumerable<SizeCount> SizeCounts { get; set; }


        public IEnumerable<ColorCount> ColorCounts { get; set; }
    }

    public class SizeSearchResults
    {
        public IEnumerable<Shirt> Shirts { get; set; }


        public IEnumerable<SizeCount> SizeCounts { get; set; }
    }

    public class SizeCount
    {
        public Size Size { get; set; }

        public int Count { get; set; }
    }

    public class ColorSearchResults
    {
        public IEnumerable<Shirt> Shirts { get; set; }

        public IEnumerable<ColorCount> ColorCounts { get; set; }
    }

    public class ColorCount
    {
        public Color Color { get; set; }

        public int Count { get; set; }
    }
}