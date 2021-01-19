using System.Collections.Generic;
using System.Linq;

namespace ConstructionLine.CodingChallenge
{
	public class SearchOptions
    {
        public SearchOptions(IEnumerable<Size> sizes, IEnumerable<Color> colors)
		{
            Sizes = sizes.Any() ? sizes : Size.All;
            Colors = colors.Any() ? colors : Color.All;
		}

        public IEnumerable<Size> Sizes { get; }
        public IEnumerable<Color> Colors { get; }
    }
}