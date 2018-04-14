using System.Collections.Generic;
using System.Drawing;

namespace GAB.ImageProcessingPipeline.Common.Drawing
{
    public static class ImageDimensions
    {
        public static readonly Dictionary<ImageSize, Size> Dimensions = new Dictionary<ImageSize, Size>
        {
            {ImageSize.Large, new Size(800, 600)},
            {ImageSize.Medium, new Size(320, 240)},
            {ImageSize.Small, new Size(220, 148)},
            {
                ImageSize.Minimum,
                new Size(Constants.DefaultMinimumImageWidth, Constants.DefaultMinimumImageHeight)
            }
        };
    }
}
