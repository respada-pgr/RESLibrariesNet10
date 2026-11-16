using _1_LibraryClasses.Result;
using System;
using System.Globalization;

namespace _1_LibraryClasses
{
    /// <summary>
    /// Static helpers for <see cref="C_Number"/>: parse, format, and factory.
    /// <para><i>[ES] Ayudas de <see cref="C_Number"/>: parseo, formato y factoría.</i></para>
    /// </summary>
    public static class C_Hlp_Number
    {
#region PUBLIC METHODS — parse text

        public static bool TryToInt(string? text, out int value) => C_Hlp_String.TryToInt(text, out value);
        public static bool TryToLong(string? text, out long value) => C_Hlp_String.TryToLong(text, out value);
        public static bool TryToDouble(string? text, out double value) => C_Hlp_String.TryToDouble(text, out value);
        public static bool TryToDecimal(string? text, out decimal value) => C_Hlp_String.TryToDecimal(text, out value);

        public static int ToInt(string text)
        {
            if (!TryToInt(text, out int value))
                throw new FormatException("Text is not an Int32.");
            return value;
        }

        public static long ToLong(string text)
        {
            if (!TryToLong(text, out long value))
                throw new FormatException("Text is not an Int64.");
            return value;
        }

        public static double ToDouble(string text)
        {
            if (!TryToDouble(text, out double value))
                throw new FormatException("Text is not a Double.");
            return value;
        }

        public static decimal ToDecimal(string text)
        {
            if (!TryToDecimal(text, out decimal value))
                throw new FormatException("Text is not a Decimal.");
            return value;
        }

#endregion

#region PUBLIC METHODS — format

        public static string FromInt(int value) => value.ToString(CultureInfo.InvariantCulture);
        public static string FromLong(long value) => value.ToString(CultureInfo.InvariantCulture);
        public static string FromDouble(double value) => value.ToString(CultureInfo.InvariantCulture);
        public static string FromDecimal(decimal value) => value.ToString(CultureInfo.InvariantCulture);

#endregion


#region PUBLIC METHODS — convert decimal → typed (C_Result / native)

        /// <summary>
        /// Converts using <see cref="Convert.ToInt32(decimal)"/>.
        /// Ok or Fail (overflow); does not report rounding as Warning (caller knows integers may lose fraction).
        /// <para><i>[ES] Ok o Fail (overflow); el redondeo no es Warning (se asume al convertir a entero).</i></para>
        /// </summary>
        public static C_Result<int> TryToInt(decimal value)
        {
            try
            {
                return C_Result<int>.Ok(Convert.ToInt32(value));
            }
            catch (OverflowException ex)
            {
                return C_Result<int>.Fail(ex);
            }
        }

        /// <summary>
        /// Same conversion; also returns the discarded part (<c>value - result</c>) when needed.
        /// Does not use Warning — only fills <paramref name="discarded"/>.
        /// <para><i>[ES] Misma conversión; rellena <paramref name="discarded"/> si se pide. Sin Warning.</i></para>
        /// </summary>
        public static C_Result<int> TryToInt(decimal value, out decimal discarded)
        {
            try
            {
                int i = Convert.ToInt32(value);
                discarded = value - i;
                return C_Result<int>.Ok(i);
            }
            catch (OverflowException ex)
            {
                discarded = 0m;
                return C_Result<int>.Fail(ex);
            }
        }

        public static C_Result<long> TryToLong(decimal value)
        {
            try
            {
                return C_Result<long>.Ok(Convert.ToInt64(value));
            }
            catch (OverflowException ex)
            {
                return C_Result<long>.Fail(ex);
            }
        }

        public static C_Result<long> TryToLong(decimal value, out decimal discarded)
        {
            try
            {
                long i = Convert.ToInt64(value);
                discarded = value - i;
                return C_Result<long>.Ok(i);
            }
            catch (OverflowException ex)
            {
                discarded = 0m;
                return C_Result<long>.Fail(ex);
            }
        }

        public static C_Result<double> TryToDouble(decimal value)
        {
            return C_Result<double>.Ok(Convert.ToDouble(value));
        }

        public static C_Result<double> TryToDouble(decimal value, out decimal discarded)
        {
            double d = Convert.ToDouble(value);
            discarded = value - Convert.ToDecimal(d);
            return C_Result<double>.Ok(d);
        }

        public static C_Result<float> TryToFloat(decimal value)
        {
            try
            {
                return C_Result<float>.Ok(Convert.ToSingle(value));
            }
            catch (OverflowException ex)
            {
                return C_Result<float>.Fail(ex);
            }
        }

        public static C_Result<float> TryToFloat(decimal value, out decimal discarded)
        {
            try
            {
                float f = Convert.ToSingle(value);
                discarded = value - Convert.ToDecimal(Convert.ToDouble(f));
                return C_Result<float>.Ok(f);
            }
            catch (OverflowException ex)
            {
                discarded = 0m;
                return C_Result<float>.Fail(ex);
            }
        }

        /// <summary>
        /// Native conversion; throws framework <see cref="OverflowException"/> if out of range.
        /// </summary>
        public static int ToInt(decimal value) => Convert.ToInt32(value);

        public static long ToLong(decimal value) => Convert.ToInt64(value);

        public static double ToDouble(decimal value) => Convert.ToDouble(value);

        public static float ToFloat(decimal value) => Convert.ToSingle(value);

#endregion

#region PUBLIC METHODS — try create

        /// <summary>
        /// Tries to create a <see cref="C_Number"/> from text; <see langword="null"/> if empty or not a number.
        /// <para><i>[ES] Intenta crear un <see cref="C_Number"/>; <see langword="null"/> si está vacío o no es número.</i></para>
        /// </summary>
        public static C_Number? TryCreate(string? text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return null;
            if (!TryToDecimal(text, out decimal value))
                return null;
            return new C_Number(value);
        }

#endregion

    }
}
