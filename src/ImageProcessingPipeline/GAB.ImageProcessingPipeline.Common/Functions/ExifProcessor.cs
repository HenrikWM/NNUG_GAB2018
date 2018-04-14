using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Dynamic;
using System.IO;
using System.Threading.Tasks;
using ExifLib;
using GAB.ImageProcessingPipeline.Common.Drawing;
using GAB.ImageProcessingPipeline.Common.Storage;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;

namespace GAB.ImageProcessingPipeline.Common.Functions
{
    public static class ExifProcessor
    {
        private const string EmptyJsonString = "{}";

        public const string OutputBlobPath = BlobContainers.Exif + "/" + "{name}.json";

        public static async Task Process(Stream image, CloudBlockBlob jsonBlob, string name, TraceWriter log)
        {
            log.Info($"[Exif] - Triggered for image name: {name}, size: {image.Length} bytes");

            try
            {
                if (NoExifData(image))
                {
                    log.Error("[Exif] - No EXIF data present");
                    await JsonBlobUploader.UploadBlob(jsonBlob, EmptyJsonString);

                    return;
                }

                if (UnsupportedFileType(image))
                {
                    log.Error("[Exif] - Only jpg-files supported for EXIF-data");
                    await JsonBlobUploader.UploadBlob(jsonBlob, EmptyJsonString);

                    return;
                }

                image.Position = 0;

                var data = GetExifData(image);
                var json = JsonConvert.SerializeObject(data);

                await JsonBlobUploader.UploadBlob(jsonBlob, json);

                log.Info($"[Exif] - EXIF-extraction completed for {name}. File location: {jsonBlob.Uri.AbsoluteUri}");
            }
            catch (Exception exception)
            {
                log.Error($"[Exif] - Failed: {exception.Message}", exception);
            }
        }
        
        private static bool UnsupportedFileType(Stream image)
        {
            var imageFormat = Image.FromStream(image).GetImageFormat();
            return imageFormat.Equals(ImageFormat.Jpeg) == false;
        }

        private static bool NoExifData(Stream image)
        {
            try
            {
                // throws exception if no Exif-data found
                new ExifReader(image);
            }
            catch
            {
                return true;
            }
            return false;
        }

        private static dynamic GetExifData(Stream image)
        {
            dynamic exifData = new ExpandoObject();

            using (var reader = new ExifReader(image))
            {
                var type = typeof(ExifTags);
                foreach (var enumName in type.GetEnumNames())
                {
                    var enumValue = (ExifTags)Enum.Parse(typeof(ExifTags), enumName);

                    var value = reader.GetTagValue(enumValue, out object tagValue);
                    if (value)
                    {
                        AddProperty(exifData, enumName, tagValue);
                    }
                }
            }
            return exifData;
        }

        private static void AddProperty(ExpandoObject expando, string propertyName, object propertyValue)
        {
            var expandoDict = expando as IDictionary<string, object>;
            if (expandoDict.ContainsKey(propertyName))
                expandoDict[propertyName] = propertyValue;
            else
                expandoDict.Add(propertyName, propertyValue);
        }
    }
}
