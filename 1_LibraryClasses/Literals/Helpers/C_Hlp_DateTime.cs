using System.Diagnostics;
using _1_LibraryClasses;

namespace _1_LibraryClasses
{
    /// <summary>
    /// Helpers to validate and parse <see cref="DateTime"/> values from digit strings and format masks.
    /// <para><i>[ES] Ayudas para validar y parsear valores <see cref="DateTime"/> a partir de cadenas de dígitos y máscaras de formato.</i></para>
    /// </summary>
    /// <example>
    /// <code>
    /// if (C_Hlp_DateTime.IsDateTime("20261006", "yyyyMMdd", out DateTime dt))
    /// {
    ///     // dt = 2026-10-06
    /// }
    /// </code>
    /// </example>
    public static class C_Hlp_DateTime
    {
        /// <summary>
        /// Validates whether <paramref name="text"/> matches a digit mask in <paramref name="format"/>.
        /// Format uses consecutive letters: <c>y</c> year, <c>M</c> month, <c>d</c> day, <c>h</c> hour, <c>m</c> minute, <c>s</c> second, <c>f</c> fraction.
        /// Text length must equal format length and contain only digits.
        /// <para><i>[ES] Valida si <paramref name="text"/> encaja con una máscara de dígitos en <paramref name="format"/>.
        /// Letras: <c>y</c> año, <c>M</c> mes, <c>d</c> día, <c>h</c> hora, <c>m</c> minuto, <c>s</c> segundo, <c>f</c> fracción.
        /// La longitud del texto debe coincidir con el formato y ser solo dígitos.</i></para>
        /// </summary>
        /// <param name="text">Digit string to parse. / Cadena de dígitos a interpretar.</param>
        /// <param name="format">Layout mask (e.g. <c>yyyyMMdd</c>). / Máscara (p. ej. <c>yyyyMMdd</c>).</param>
        /// <param name="dateTime">Parsed value when the method returns <see langword="true"/>. / Valor parseado si devuelve <see langword="true"/>.</param>
        /// <returns><see langword="true"/> if parsing succeeds. / <see langword="true"/> si el parseo es correcto.</returns>
        /// <example>
        /// <code>
        /// if (C_Hlp_DateTime.IsDateTime("20261006", "yyyyMMdd", out DateTime dt))
        /// {
        ///     // dt = 2026-10-06
        /// }
        /// </code>
        /// </example>
        public static bool IsDateTime(string text, string format, out DateTime dateTime)
        {
            dateTime = new DateTime();

            if (C_Hlp_String.IsNumber(text) && text.Length == format.Length)
            {
                try
                {
                    char c = 'y';
                    int i = format.IndexOf(c);
                    string sYear = i >= 0 ? text.Substring(i, C_Hlp_String.CountChars(text, c, true, i)).TrimStart('0') : "0";
                    int iYear = int.Parse(sYear);
                    if (iYear < 1)
                        return false;

                    c = 'M';
                    i = format.IndexOf(c);
                    string sMonth = i >= 0 ? text.Substring(i, C_Hlp_String.CountChars(text, c, true, i)).TrimStart('0') : "0";
                    int iMonth = int.Parse(sMonth);
                    if (iMonth > 12 || iMonth < 1)
                        return false;

                    c = 'd';
                    i = format.IndexOf(c);
                    string sDay = i >= 0 ? text.Substring(i, C_Hlp_String.CountChars(text, c, true, i)).TrimStart('0') : "0";
                    int iDay = int.Parse(sDay);
                    if (iDay > 31 || iDay < 1)
                        return false;

                    c = 'h';
                    i = format.IndexOf(c);
                    string sHour = i >= 0 ? text.Substring(i, C_Hlp_String.CountChars(text, c, true, i)).TrimStart('0') : "0";
                    int iHour = int.Parse(sHour);
                    if (iHour > 24 || iHour < 1)
                        return false;

                    c = 'm';
                    i = format.IndexOf(c);
                    string sMinute = i >= 0 ? text.Substring(i, C_Hlp_String.CountChars(text, c, true, i)).TrimStart('0') : "0";
                    int iMinute = int.Parse(sMinute);
                    if (iMinute > 60 || iMinute < 1)
                        return false;

                    c = 's';
                    i = format.IndexOf(c);
                    string sSecond = i >= 0 ? text.Substring(i, C_Hlp_String.CountChars(text, c, true, i)).TrimStart('0') : "0";
                    int iSecond = int.Parse(sSecond);
                    if (iSecond > 60 || iSecond < 1)
                        return false;

                    c = 'f';
                    i = format.IndexOf(c);
                    string sMilisecond = i >= 0 ? text.Substring(i, C_Hlp_String.CountChars(text, c, true, i)).TrimStart('0') : "0";
                    int iMilisecond = int.Parse(sMilisecond);
                    if (iMilisecond > 60 || iMilisecond < 1)
                        return false;

                    dateTime = new DateTime(iYear, iMonth, iDay, iHour, iMinute, iSecond, iMilisecond);
                    return true;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"C_Hlp_DateTime.IsDateTime: {ex}");
                    return false;
                }

            }

            return false;
        }

        /// <summary>
        /// Tries each format in order until one succeeds (see single-format overload).
        /// <para><i>[ES] Prueba cada formato en orden hasta que uno funcione (ver sobrecarga de un formato).</i></para>
        /// </summary>
        /// <param name="formats">Candidate masks. / Máscaras candidatas.</param>
        public static bool IsDateTime(string text, string[] formats, out DateTime dateTime)
        {
            bool result = false;
            dateTime = new DateTime();

            foreach (string format in formats)
            {
                result = IsDateTime(text, format, out dateTime);
                if (result)
                    return true;
            }

            return result;
        }

        /// <summary>
        /// Parses a date using standard or custom rules; sets <paramref name="dateTime"/> and returns success.
        /// <para><i>[ES] Parsea una fecha con reglas estándar o personalizadas; asigna <paramref name="dateTime"/> y devuelve si tuvo éxito.</i></para>
        /// </summary>
        /// <param name="text">Input text. / Texto de entrada.</param>
        /// <param name="format">Format or mask. / Formato o máscara.</param>
        /// <param name="dateTime">Result when successful. / Resultado si tiene éxito.</param>
        /// <returns><see langword="true"/> if a valid date was produced.</returns>
        public static bool GetDateTime(string text, string format, out DateTime dateTime)
        {
            dateTime = new DateTime();

            try
            {
                int i = 0;
                string sYear = "";
                string sMonth = "";
                string sDay = "";
                string sHour = "";
                string sMinute = "";
                string sSecond = "";
                string sMilisecond = "";

                foreach (char c in text.ToCharArray())
                {
                    if (C_Hlp_String.IsNumber(c))
                    {
                        switch (format[i])
                        {
                            case 'y':
                                if (!(c == '0' && sYear == ""))
                                    sYear += c;
                                i++;
                                break;
                            case 'M':
                                if (!(c == '0' && sMonth == ""))
                                    sMonth += c;
                                i++;
                                break;
                            case 'd':
                                if (!(c == '0' && sDay == ""))
                                    sDay += c;
                                i++;
                                break;
                            case 'h':
                                if (!(c == '0' && sHour == ""))
                                    sHour += c;
                                i++;
                                break;
                            case 'm':
                                if (!(c == '0' && sMinute == ""))
                                    sMinute += c;
                                i++;
                                break;
                            case 's':
                                if (!(c == '0' && sSecond == ""))
                                    sSecond += c;
                                i++;
                                break;
                            case 'f':
                                if (!(c == '0' && sMilisecond == ""))
                                    sMilisecond += c;
                                i++;
                                break;
                            default:
                                return false;
                        }
                    }
                    else if (c == format[i])
                    {
                        i++;
                    }
                    else
                    {
                        i = 0;
                    }

                    if (i >= format.Length)
                    {
                        int iYear = int.Parse(sYear);
                        if (iYear < 1)
                            return false;

                        int iMonth = int.Parse(sMonth);
                        if (iMonth > 12 || iMonth < 1)
                            return false;

                        int iDay = int.Parse(sDay);
                        if (iDay > 31 || iDay < 1)
                            return false;

                        int iHour = int.Parse(sHour);
                        if (iHour > 24 || iHour < 1)
                            return false;

                        int iMinute = int.Parse(sMinute);
                        if (iMinute > 60 || iMinute < 1)
                            return false;

                        int iSecond = int.Parse(sSecond);
                        if (iSecond > 60 || iSecond < 1)
                            return false;

                        int iMilisecond = int.Parse(sMilisecond);
                        if (iMilisecond > 60 || iMilisecond < 1)
                            return false;

                        dateTime = new DateTime(iYear, iMonth, iDay, iHour, iMinute, iSecond, iMilisecond);
                        return true;
                    }

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"C_Hlp_DateTime.GetDateTime(string text, string format, out DateTime dateTime): {ex}");
                return false;
            }

            return false;
        }

        /// <summary>
        /// Tries multiple formats until one parses successfully.
        /// <para><i>[ES] Prueba varios formatos hasta que uno parsea correctamente.</i></para>
        /// </summary>
        public static bool GetDateTime(string text, string[] formats, out DateTime dateTime)
        {
            bool result = false;
            dateTime = new DateTime();

            foreach (string format in formats)
            {
                result = GetDateTime(text, format, out dateTime);
                if (result)
                    return true;
            }

            return result;
        }        

    }
}
