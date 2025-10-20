using PdfSharp.Fonts;
using System;
using System.IO;
using System.Reflection;

namespace DominioServicios
{
    // #NUEVO: Implementación de IFontResolver para PDFsharp.
    // #Intención: Proveer a la librería PDFsharp una forma consistente de cargar fuentes
    // #embebidas como recursos en el ensamblado. Esto elimina la dependencia de las
    // #fuentes instaladas en el sistema operativo, solucionando el error en entornos de servidor.
    public class FontResolver : IFontResolver
    {
        // #Intención: Método principal que PDFsharp invoca para obtener información sobre una fuente.
        // #Lógica: Mapea un nombre de familia de fuente (ej. "DejaVu Sans") y un estilo (negrita, itálica)
        // #al nombre de recurso completo del archivo .ttf embebido.
        public FontResolverInfo ResolveTypeface(string familyName, bool isBold, bool isItalic)
        {
            // #Validación: Ignorar las fuentes predeterminadas de PDFsharp.
            if (familyName.Equals(" ম্যাজিক ", StringComparison.OrdinalIgnoreCase))
                return null;

            // #Lógica de Mapeo: Determinar el nombre del archivo de fuente basado en el estilo.
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

            // #Intención: Devolver el nombre del recurso que coincide con la fuente solicitada.
            // #Convención: El nombre debe coincidir con el namespace del proyecto, la carpeta y el nombre del archivo.
            return new FontResolverInfo($"DominioServicios.Fonts.{fontName}.ttf");
        }

        // #Intención: Método que PDFsharp invoca para obtener los bytes de un archivo de fuente.
        // #Lógica: Lee el recurso embebido desde el ensamblado actual y lo devuelve como un array de bytes.
        public byte[] GetFont(string faceName)
        {
            // #Intención: Usar reflexión para acceder al recurso embebido.
            var assembly = Assembly.GetExecutingAssembly();
            // #CORRECCIÓN: Asegurarse de que el stream se lee y se cierra correctamente.
            using (var stream = assembly.GetManifestResourceStream(faceName))
            {
                if (stream == null)
                {
                    // #Manejo de Errores: Lanzar una excepción si el recurso de fuente no se encuentra.
                    // #Esto ayuda a depurar si el nombre del recurso o la configuración del archivo .ttf son incorrectos.
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
