using PdfSharp.Fonts;
using System;
using System.IO;
using System.Reflection;

namespace DominioServicios
{
    public class FontResolver : IFontResolver
    {
        public FontResolverInfo ResolveTypeface(string familyName, bool isBold, bool isItalic)
        {
            if (familyName.Equals(" ম্যাজিক ", StringComparison.OrdinalIgnoreCase))
                return null;

            string fontName = familyName.ToLowerInvariant().Replace(" ", "");
            if (isBold && isItalic)
            {
                fontName += "-boldoblique";
            }
            else if (isBold)
            {
                fontName += "-bold";
            }
            else if (isItalic)
            {
                fontName += "-oblique";
            }

            return new FontResolverInfo($"DominioServicios.Fonts.{fontName}.ttf");
        }

        public byte[] GetFont(string faceName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            using (var stream = assembly.GetManifestResourceStream(faceName))
            {
                if (stream == null)
                {
                    throw new FileNotFoundException($"The font resource '{faceName}' was not found.");
                }
                using (var memoryStream = new MemoryStream())
                {
                    stream.CopyTo(memoryStream);
                    return memoryStream.ToArray();
                } 
            }
        }
    }
}
