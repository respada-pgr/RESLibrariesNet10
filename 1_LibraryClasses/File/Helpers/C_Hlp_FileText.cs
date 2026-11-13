using System.Text;

namespace _1_LibraryClasses.File
{
    /// <summary>
    /// Text file helpers (append lines, write full text or line lists).
    /// <para><i>[ES] Ayudas de fichero de texto (añadir líneas, escribir texto completo o listas de líneas).</i></para>
    /// </summary>
    public static class C_Hlp_FileText
    {
        /// <summary>
        /// Appends a line using a stream. If the file does not end with a newline, one is written first.
        /// <para><i>[ES] Añade una línea con stream. Si el fichero no termina en salto de línea, se escribe uno antes.</i></para>
        /// </summary>
        /// <param name="text">Line content (without requiring a trailing newline). / Contenido de la línea.</param>
        /// <param name="filePath">Target file path. / Ruta del fichero.</param>
        public static void AddLine(string text, string filePath)
        {
            C_Hlp_File.EnsureFolder(filePath);

            using FileStream fs = C_Hlp_FileStream.OpenReadWrite(filePath);

            if (fs.Length > 0)
            {
                fs.Seek(-1, SeekOrigin.End);
                int last = fs.ReadByte();
                fs.Seek(0, SeekOrigin.End);
                if (last != '\n')
                {
                    byte[] nl = Encoding.UTF8.GetBytes(Environment.NewLine);
                    fs.Write(nl, 0, nl.Length);
                }
            }

            using StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
            sw.WriteLine(text ?? string.Empty);
        }

        /// <summary>
        /// Legacy implementation: reads the whole file, appends the line in memory, and rewrites the file.
        /// Prefer <see cref="AddLine"/> (stream-based).
        /// <para><i>[ES] Implementación antigua: lee todo el fichero, añade la línea en memoria y lo reescribe.
        /// Preferir <see cref="AddLine"/> (basado en streams).</i></para>
        /// </summary>
        public static void AddLineOld(string text, string filePath)
        {
            var textFile = new System.Text.StringBuilder();
            if (C_Hlp_File.Exists(filePath))
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string? line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        textFile.AppendLine(line);
                    }
                }
            }

            textFile.AppendLine(text ?? string.Empty);

            C_Hlp_File.EnsureFolder(filePath);

            using (StreamWriter sw = new StreamWriter(C_Hlp_FileStream.OpenWrite(filePath)))
            {
                sw.Write(textFile.ToString());
            }
        }

        /// <summary>
        /// Writes the full text content (overwrites if the file exists).
        /// <para><i>[ES] Escribe el contenido de texto completo (sobrescribe si el fichero existe).</i></para>
        /// </summary>
        public static void Write(string text, string filePath)
        {
            C_Hlp_File.EnsureFolder(filePath);
            System.IO.File.WriteAllText(filePath, text ?? string.Empty);
        }

        /// <summary>
        /// Writes all lines (overwrites if the file exists).
        /// <para><i>[ES] Escribe todas las líneas (sobrescribe si el fichero existe).</i></para>
        /// </summary>
        /// <param name="textList">Lines to write. / Líneas a escribir.</param>
        /// <param name="filePath">Target file path. / Ruta del fichero.</param>
        public static void Write(List<string> textList, string filePath)
        {
            C_Hlp_File.EnsureFolder(filePath);
            System.IO.File.WriteAllLines(filePath, textList ?? new List<string>());
        }
    }
}
