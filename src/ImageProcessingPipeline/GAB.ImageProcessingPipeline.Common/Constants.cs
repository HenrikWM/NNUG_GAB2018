namespace GAB.ImageProcessingPipeline.Common
{
    public static class Constants
    {
        public const string SiteName = "Image Pipeline";
        public static readonly string[] AcceptedImageFileFormats = { ".jpg", ".png", ".jpeg", ".gif" };
        public const string BlobStorageConnectionName = "BlobStorage";

        // Azure Computer Vision API minimums are 50x50 image and maximum 4MiB file size
        // Source: https://docs.microsoft.com/en-us/azure/cognitive-services/computer-vision/vision-api-how-to-topics/howtocallvisionapi#a-nameprerequisitesprerequisitesa
        public const int DefaultMinimumImageHeight = 50;
        public const int DefaultMinimumImageWidth = 50;
        public const int DefaultMaxFileUploadSize = 4 * 1024 * 1024;
    }

    public static class BlobContainers
    {
        public static readonly string[] All =
        {
            Originals,
            ScaledLarge,
            ScaledMedium,
            ScaledSmall,
            Exif,
            CognitiveServices,
            ColorMatrixLarge,
            ColorMatrixMedium,
            ColorMatrixSmall
        };

        public const string Originals = "imagepipeline-originals";

        public const string ScaledLarge = "imagepipeline-scaled-lg";
        public const string ScaledMedium = "imagepipeline-scaled-md";
        public const string ScaledSmall = "imagepipeline-scaled-sm";

        public const string Exif = "imagepipeline-exif";
        public const string CognitiveServices = "imagepipeline-cognitiveservices";

        public const string ColorMatrixLarge = "imagepipeline-colormatrix-lg";
        public const string ColorMatrixMedium = "imagepipeline-colormatrix-md";
        public const string ColorMatrixSmall = "imagepipeline-colormatrix-sm";
    }
}
