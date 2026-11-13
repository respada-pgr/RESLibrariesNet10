using System.Text.RegularExpressions;
using _1_LibraryClasses.Random;

namespace _1_LibraryClasses.File
{
    /// <summary>
    /// Helpers that transform a file path string (sanitize name, dated names, random prefix on the file name).
    /// <para>
    /// Segment parsing (name / extension / folder): <see cref="C_Hlp_Path"/>.
    /// </para>
    /// <para><i>[ES] Ayudas que transforman una ruta de fichero (sanitizar nombre, nombres fechados, prefijo aleatorio).
    /// Parseo de segmentos (nombre / extensión / carpeta): <see cref="C_Hlp_Path"/>.</i></para>
    /// </summary>
    public static class C_Hlp_FilePath
    {
#region PUBLIC METHODS — sanitize

        /// <summary>
        /// Sanitizes a file name: invalid characters, max length 255, trims trailing spaces/dots and control chars.
        /// <para><i>[ES] Normaliza un nombre de fichero: caracteres inválidos, máximo 255, quita espacios/puntos finales y controles.</i></para>
        /// </summary>
        public static string NormalizeName(string name, string replaceCharBy = " ")
        {
            string pattern = $"[{Regex.Escape(new string(Path.GetInvalidFileNameChars()))}]";
            string nameWithoutInvalidCharacters = Regex.Replace(name, pattern, replaceCharBy);

            if (nameWithoutInvalidCharacters.Length > 255)
                nameWithoutInvalidCharacters = nameWithoutInvalidCharacters.Substring(0, 255);

            nameWithoutInvalidCharacters = nameWithoutInvalidCharacters.TrimEnd(' ', '.');
            nameWithoutInvalidCharacters = new string(nameWithoutInvalidCharacters.Where(c => !char.IsControl(c)).ToArray());

            return nameWithoutInvalidCharacters;
        }

#endregion

#region PUBLIC METHODS — transform

        /// <summary>
        /// Builds a dated file name keeping the original extension (name_yyyyMMddHHmmssffff.ext).
        /// <para><i>[ES] Construye un nombre fechado conservando la extensión (nombre_yyyyMMddHHmmssffff.ext).</i></para>
        /// </summary>
        public static string? GetFileNameDated(string? filePath, string formatDateTime = "yyyyMMddHHmmssffff")
        {
            if (filePath is null) return null;
            return GetFileNameDatedWithoutExtension(filePath, formatDateTime) + C_Hlp_Path.GetExtensionWithDot(filePath);
        }

        /// <summary>
        /// Builds a dated file name without extension (name_yyyyMMddHHmmssffff).
        /// <para><i>[ES] Construye un nombre fechado sin extensión (nombre_yyyyMMddHHmmssffff).</i></para>
        /// </summary>
        public static string? GetFileNameDatedWithoutExtension(string? filePath, string formatDateTime = "yyyyMMddHHmmssffff")
        {
            if (filePath is null) return null;
            return C_Hlp_Path.GetFileNameWithoutExtension(filePath) + "_" + DateTime.Now.ToString(formatDateTime);
        }

        /// <summary>
        /// Prepends a random alphabetic segment to the file name of a path, preserving the directory.
        /// <para><i>[ES] Antepone un segmento alfabético aleatorio al nombre de fichero de una ruta, conservando el directorio.</i></para>
        /// </summary>
        public static string AddRandomAlphaPrefixToFileName(string? filePath, int length = 8, string? separator = "_")
        {
            if (string.IsNullOrEmpty(filePath))
            {
                if (length <= 0) return filePath ?? string.Empty;
                return C_Hlp_Random.Alpha(length) + (separator ?? string.Empty) + (filePath ?? string.Empty);
            }

            string? directory = C_Hlp_Path.GetFolderPath(filePath);
            string fileName = C_Hlp_Path.GetFileName(filePath) ?? string.Empty;
            string newName = length <= 0
                ? fileName
                : C_Hlp_Random.Alpha(length) + (separator ?? string.Empty) + fileName;

            if (string.IsNullOrEmpty(directory))
                return newName;
            return Path.Combine(directory, newName);
        }

#endregion
    }
}
