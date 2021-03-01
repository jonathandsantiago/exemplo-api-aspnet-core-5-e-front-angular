using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FavoDeMel.Framework.Helpers
{
    public static class MimeTypeMap
    {
        private static readonly Lazy<IDictionary<string, string>> _mappings = new Lazy<IDictionary<string, string>>(BuildMappings);

        private static IDictionary<string, string> BuildMappings()
        {
            var mappings = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase) {

                #region Big freaking list of mime types

                {"image/bmp", ".bmp"},
                {"image/jpeg", ".jpg"},
                {"image/png", ".png"}, //Defined in [RFC-2045], [RFC-2048]
                {"image/x-png", ".png"}, //See https://www.w3.org/TR/PNG/#A-Media-type :"It is recommended that implementations also recognize the media type "image/x-png"."

                #endregion

                };

            var cache = mappings.ToList();

            foreach (var mapping in cache)
            {
                if (!mappings.ContainsKey(mapping.Value))
                {
                    mappings.Add(mapping.Value, mapping.Key);
                }
            }

            return mappings;
        }

        public static string GetMimeType(string extension)
        {
            if (extension == null)
            {
                throw new ArgumentNullException("extension");
            }

            if (!extension.StartsWith("."))
            {
                extension = "." + extension;
            }

            string mime;

            return _mappings.Value.TryGetValue(extension, out mime) ? mime : "application/octet-stream";
        }

        public static string GetMimeTypeFromFileName(string fileName)
        {
            if (fileName == null)
            {
                throw new ArgumentNullException("extension");
            }

            var extension = fileName.Substring(fileName.LastIndexOf('.') + 1);

            return GetMimeType(extension);
        }

        public static string GetExtension(string mimeType)
        {
            return GetExtension(mimeType, true);
        }

        public static string GetExtension(string mimeType, bool throwErrorIfNotFound)
        {
            if (mimeType == null)
            {
                throw new ArgumentNullException("mimeType");
            }

            if (mimeType.StartsWith("."))
            {
                throw new ArgumentException("O mime type enviado não é válido: " + mimeType);
            }

            string extension;

            if (_mappings.Value.TryGetValue(mimeType, out extension))
            {
                return extension;
            }
            if (throwErrorIfNotFound)
            {
                throw new ArgumentException("O tipo de arquivo não é permitido.");
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
