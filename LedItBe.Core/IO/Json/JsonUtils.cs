using System;
using System.IO;
using System.IO.Compression;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LedItBe.Core.IO.Json
{
    internal static class JsonUtils
    {
        private static readonly JsonSerializerOptions _jsonSettings = new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        private static readonly JsonSerializerOptions _jsonReadableSettings = new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            WriteIndented = true
        };

        private static readonly JsonSerializerOptions _jsonReadSettings = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        public static string ToJson<T>(T o) => JsonSerializer.Serialize(o, _jsonSettings);
        public static string ToReadableJson<T>(T o) => JsonSerializer.Serialize(o, _jsonReadableSettings);
        public static T FromJson<T>(string json) => JsonSerializer.Deserialize<T>(json, _jsonReadSettings);

        public static T FromJsonFile<T>(string fileUrl, bool gziped = false)
            where T : new()
        {
            T res = default;

            try
            {
                if (!gziped)
                {
                    using (var reader = new StreamReader(new FileStream(fileUrl, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)))
                    {
                        string json = reader.ReadToEnd();
                        res = FromJson<T>(json);
                    }
                }
                else
                {
                    using (var file = new FileStream(fileUrl, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    using (var gzip = new GZipStream(file, CompressionMode.Decompress))
                    using (var reader = new StreamReader(gzip))
                    {
                        string json = reader.ReadToEnd();
                        res = FromJson<T>(json);
                    }
                }
            }
            catch (JsonException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new JsonException("Error while loading JSON file {0}", ex.Message);
            }

            return res;
        }

        public static void ToJsonFile<T>(string fileUrl, T value, bool readable = true, bool gziped = false)
            where T : class
        {
            string json;

            try
            {
                if (!gziped)
                {
                    using (var writer = new StreamWriter(new FileStream(fileUrl, FileMode.Create, FileAccess.Write, FileShare.ReadWrite)))
                    {
                        json = readable ? ToReadableJson(value) : ToJson(value);
                        writer.Write(json);
                    }
                }
                else
                {
                    using (var file = new FileStream(fileUrl, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
                    using (var gzip = new GZipStream(file, CompressionMode.Compress))
                    using (var writer = new StreamWriter(gzip))
                    {
                        json = readable ? ToReadableJson(value) : ToJson(value);
                        writer.Write(json);
                    }
                }
            }
            catch (JsonException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new JsonException("Error while saving JSON file ({0})", ex.Message);
            }
        }
    }
}
