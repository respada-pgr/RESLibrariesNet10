using System;
using System.Globalization;
using System.Numerics;
using _1_LibraryClasses.Result;

namespace _1_LibraryClasses
{
    /// <summary>
    /// Static helpers for <see cref="C_Number"/>: parse, format, and factory.
    /// <para><i>[ES] Ayudas de <see cref="C_Number"/>: parseo, formato y factoría.</i></para>
    /// </summary>
    public static class C_Hlp_Number
    {
        #region METHODS — Parse

        #region Generic

        public static T ToNumber<T>(string text, NumberStyles styles, IFormatProvider? provider = null)
            where T : struct, INumber<T>
            => T.Parse(
                text,
                styles,
                provider ?? CultureInfo.InvariantCulture
            );

        public static TTo ToNumber<TFrom, TTo>(TFrom value)
            where TFrom : struct, INumberBase<TFrom>
            where TTo : struct, INumberBase<TTo>
            => TTo.CreateChecked(value);

        private static TTo ToNumber<TFrom, TTo>(TFrom value, Func<TFrom, TFrom> transform, out decimal discarded)
            where TFrom : struct, INumberBase<TFrom>
            where TTo : struct, INumberBase<TTo>
        {
            TFrom transformed = transform(value);
            TTo converted = TTo.CreateChecked(transformed);

            decimal originalDecimal = Convert.ToDecimal(value);
            decimal convertedDecimal = Convert.ToDecimal(converted);

            try
            {
                discarded = originalDecimal - convertedDecimal;
            }
            catch (OverflowException ex)
            {
                throw new OverflowException("Overflow occurred while calculating discarded value.", ex);
            }

            return converted;
        }

        #endregion

        #region int

        // Native conversions (banker's rounding)
        public static int ToInt(long value) => Convert.ToInt32(value);
        public static int ToInt(float value) => Convert.ToInt32(value);
        public static int ToInt(double value) => Convert.ToInt32(value);
        public static int ToInt(decimal value) => Convert.ToInt32(value);

        public static int ToInt(float value, out decimal discarded)
            => ToNumber<float, int>(value, MathF.Round, out discarded);

        public static int ToInt(double value, out decimal discarded)
            => ToNumber<double, int>(value, Math.Round, out discarded);

        public static int ToInt(decimal value, out decimal discarded)
            => ToNumber<decimal, int>(value, Math.Round, out discarded);

        // Native conversions (truncated towards zero)
        public static int ToIntTruncated(float value) => ToNumber<float, int>(value);
        public static int ToIntTruncated(double value) => ToNumber<double, int>(value);
        public static int ToIntTruncated(decimal value) => ToNumber<decimal, int>(value);

        public static int ToIntTruncated(float value, out decimal discarded)
            => ToNumber<float, int>(value, x => x, out discarded);

        public static int ToIntTruncated(double value, out decimal discarded)
            => ToNumber<double, int>(value, x => x, out discarded);

        public static int ToIntTruncated(decimal value, out decimal discarded)
            => ToNumber<decimal, int>(value, x => x, out discarded);

        // Text
        public static int ToInt(string text)
            => ToNumber<int>(text, NumberStyles.Integer, CultureInfo.InvariantCulture);

        public static int ToInt(string text, NumberStyles styles)
            => ToNumber<int>(text, styles, CultureInfo.InvariantCulture);

        public static int ToInt(string text, IFormatProvider? provider)
            => ToNumber<int>(text, NumberStyles.Integer, provider);

        public static int ToInt(string text, NumberStyles styles, IFormatProvider? provider)
            => ToNumber<int>(text, styles, provider);

        // --- SYSTEM / CURRENT CULTURE ---
        public static int ToSystemInt(string text)
            => ToNumber<int>(text, NumberStyles.Integer, CultureInfo.CurrentCulture);

        public static int ToSystemInt(string text, NumberStyles styles)
            => ToNumber<int>(text, styles, CultureInfo.CurrentCulture);

        #endregion

        #region long

        // Native conversions (banker's rounding)
        public static long ToLong(int value) => value;
        public static long ToLong(float value) => Convert.ToInt64(value);
        public static long ToLong(double value) => Convert.ToInt64(value);
        public static long ToLong(decimal value) => Convert.ToInt64(value);

        public static long ToLong(float value, out decimal discarded)
            => ToNumber<float, long>(value, MathF.Round, out discarded);

        public static long ToLong(double value, out decimal discarded)
            => ToNumber<double, long>(value, Math.Round, out discarded);

        public static long ToLong(decimal value, out decimal discarded)
            => ToNumber<decimal, long>(value, Math.Round, out discarded);

        // Native conversions (truncated towards zero)
        public static long ToLongTruncated(float value) => ToNumber<float, long>(value);
        public static long ToLongTruncated(double value) => ToNumber<double, long>(value);
        public static long ToLongTruncated(decimal value) => ToNumber<decimal, long>(value);

        public static long ToLongTruncated(float value, out decimal discarded)
            => ToNumber<float, long>(value, x => x, out discarded);

        public static long ToLongTruncated(double value, out decimal discarded)
            => ToNumber<double, long>(value, x => x, out discarded);

        public static long ToLongTruncated(decimal value, out decimal discarded)
            => ToNumber<decimal, long>(value, x => x, out discarded);

        // Text
        public static long ToLong(string text)
            => ToNumber<long>(text, NumberStyles.Integer, CultureInfo.InvariantCulture);

        public static long ToLong(string text, NumberStyles styles)
            => ToNumber<long>(text, styles, CultureInfo.InvariantCulture);

        public static long ToLong(string text, IFormatProvider? provider)
            => ToNumber<long>(text, NumberStyles.Integer, provider);

        public static long ToLong(string text, NumberStyles styles, IFormatProvider? provider)
            => ToNumber<long>(text, styles, provider);

        // --- SYSTEM / CURRENT CULTURE ---
        public static long ToSystemLong(string text)
            => ToNumber<long>(text, NumberStyles.Integer, CultureInfo.CurrentCulture);

        public static long ToSystemLong(string text, NumberStyles styles)
            => ToNumber<long>(text, styles, CultureInfo.CurrentCulture);

        #endregion

        #region float

        public static float ToFloat(int value) => value;
        public static float ToFloat(long value) => value;
        public static float ToFloat(double value) => Convert.ToSingle(value);
        public static float ToFloat(decimal value) => Convert.ToSingle(value);

        public static float ToFloat(long value, out decimal discarded)
            => ToNumber<long, float>(value, x => x, out discarded);

        public static float ToFloat(double value, out decimal discarded)
            => ToNumber<double, float>(value, x => x, out discarded);

        public static float ToFloat(decimal value, out decimal discarded)
            => ToNumber<decimal, float>(value, x => x, out discarded);

        public static float ToFloat(string text)
            => ToNumber<float>(text, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture);

        public static float ToFloat(string text, NumberStyles styles)
            => ToNumber<float>(text, styles, CultureInfo.InvariantCulture);

        public static float ToFloat(string text, IFormatProvider? provider)
            => ToNumber<float>(text, NumberStyles.Float | NumberStyles.AllowThousands, provider);

        public static float ToFloat(string text, NumberStyles styles, IFormatProvider? provider)
            => ToNumber<float>(text, styles, provider);

        // --- SYSTEM / CURRENT CULTURE ---
        public static float ToSystemFloat(string text)
            => ToNumber<float>(text, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.CurrentCulture);

        public static float ToSystemFloat(string text, NumberStyles styles)
            => ToNumber<float>(text, styles, CultureInfo.CurrentCulture);

        #endregion

        #region double

        public static double ToDouble(int value) => value;
        public static double ToDouble(long value) => value;
        public static double ToDouble(float value) => value;
        public static double ToDouble(decimal value) => Convert.ToDouble(value);
        public static double ToDouble(long value, out decimal discarded)
            => ToNumber<long, double>(value, x => x, out discarded);

        public static double ToDouble(decimal value, out decimal discarded)
            => ToNumber<decimal, double>(value, x => x, out discarded);

        public static double ToDouble(string text)
            => ToNumber<double>(text, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture);

        public static double ToDouble(string text, NumberStyles styles)
            => ToNumber<double>(text, styles, CultureInfo.InvariantCulture);

        public static double ToDouble(string text, IFormatProvider? provider)
            => ToNumber<double>(text, NumberStyles.Float | NumberStyles.AllowThousands, provider);

        public static double ToDouble(string text, NumberStyles styles, IFormatProvider? provider)
            => ToNumber<double>(text, styles, provider);

        // --- SYSTEM / CURRENT CULTURE ---
        public static double ToSystemDouble(string text)
            => ToNumber<double>(text, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.CurrentCulture);

        public static double ToSystemDouble(string text, NumberStyles styles)
            => ToNumber<double>(text, styles, CultureInfo.CurrentCulture);

        #endregion

        #region decimal

        public static decimal ToDecimal(int value) => value;
        public static decimal ToDecimal(long value) => value;
        public static decimal ToDecimal(float value) => Convert.ToDecimal(value);
        public static decimal ToDecimal(double value) => Convert.ToDecimal(value);

        public static decimal ToDecimal(float value, out decimal discarded)
            => ToNumber<float, decimal>(value, x => x, out discarded);

        public static decimal ToDecimal(double value, out decimal discarded)
            => ToNumber<double, decimal>(value, x => x, out discarded);

        public static decimal ToDecimal(string text)
            => ToNumber<decimal>(text, NumberStyles.Number, CultureInfo.InvariantCulture);

        public static decimal ToDecimal(string text, NumberStyles styles)
            => ToNumber<decimal>(text, styles, CultureInfo.InvariantCulture);

        public static decimal ToDecimal(string text, IFormatProvider? provider)
            => ToNumber<decimal>(text, NumberStyles.Number, provider);

        public static decimal ToDecimal(string text, NumberStyles styles, IFormatProvider? provider)
            => ToNumber<decimal>(text, styles, provider);

        // --- SYSTEM / CURRENT CULTURE ---
        public static decimal ToSystemDecimal(string text)
            => ToNumber<decimal>(text, NumberStyles.Number, CultureInfo.CurrentCulture);

        public static decimal ToSystemDecimal(string text, NumberStyles styles)
            => ToNumber<decimal>(text, styles, CultureInfo.CurrentCulture);

        #endregion

        #endregion

        #region METHODS — TryParse

        #region Generic

        public static bool TryToNumber<TFrom, TTo>(TFrom value, out TTo result, out string? errorMessage)
            where TFrom : struct, INumberBase<TFrom>
            where TTo : struct, INumberBase<TTo>
        {
            try
            {
                result = TTo.CreateChecked(value);
                errorMessage = null;
                return true;
            }
            catch (OverflowException)
            {
                result = default;
                errorMessage = "Value is out of range.";
                return false;
            }
        }

        public static bool TryToNumber<TFrom, TTo>(TFrom? value, out TTo result, out string? errorMessage)
            where TFrom : struct, INumberBase<TFrom>
            where TTo : struct, INumberBase<TTo>
        {
            if (value is null)
            {
                result = default;
                errorMessage = "Value is null.";
                return false;
            }

            return TryToNumber(value.Value, out result, out errorMessage);
        }

        private static bool TryToNumber<TFrom, TTo>(TFrom? value, Func<TFrom, TFrom> transform, out TTo result, out string? errorMessage)
            where TFrom : struct, INumberBase<TFrom>
            where TTo : struct, INumberBase<TTo>
        {
            if (value is null)
            {
                result = default;
                errorMessage = "Value is null.";
                return false;
            }

            return TryToNumber(transform(value.Value), out result, out errorMessage);
        }

        public static bool TryToNumber<T>(string? text, NumberStyles styles, IFormatProvider? provider, out T value)
            where T : struct, INumber<T>
            => T.TryParse(
                text,
                styles,
                provider ?? CultureInfo.InvariantCulture,
                out value
            );

        private static bool TryToNumber<TFrom, TTo>(TFrom value, Func<TFrom, TFrom> transform, out TTo result, out decimal discarded, out string? errorMessage)
            where TFrom : struct, INumberBase<TFrom>
            where TTo : struct, INumberBase<TTo>
        {
            try
            {
                result = ToNumber<TFrom, TTo>(value, transform, out discarded);
                errorMessage = null;
                return true;
            }
            catch (Exception ex) when (ex is OverflowException or NotFiniteNumberException)
            {
                result = default;
                discarded = 0m;
                errorMessage = ex.Message;
                return false;
            }
        }

        private static bool TryToNumber<TFrom, TTo>(TFrom? value, Func<TFrom, TFrom> transform, out TTo result, out decimal discarded, out string? errorMessage)
            where TFrom : struct, INumberBase<TFrom>
            where TTo : struct, INumberBase<TTo>
        {
            if (value is null)
            {
                result = default;
                discarded = 0m;
                errorMessage = "Value is null.";
                return false;
            }

            return TryToNumber<TFrom, TTo>(value.Value, transform, out result, out discarded, out errorMessage);
        }

        #endregion

        #region Int

        // Native conversions (banker's rounding)
        public static bool TryToInt(long value, out int result) => TryToNumber(value, out result);
        public static bool TryToInt(float value, out int result) => TryToNumber(MathF.Round(value), out result);
        public static bool TryToInt(double value, out int result) => TryToNumber(Math.Round(value), out result);
        public static bool TryToInt(decimal value, out int result) => TryToNumber(Math.Round(value), out result);

        public static bool TryToInt(float value, out int result, out decimal discarded, out string? errorMessage)
            => TryToNumber<float, int>(value, MathF.Round, out result, out discarded, out errorMessage);

        public static bool TryToInt(double value, out int result, out decimal discarded, out string? errorMessage)
            => TryToNumber<double, int>(value, Math.Round, out result, out discarded, out errorMessage);

        public static bool TryToInt(decimal value, out int result, out decimal discarded, out string? errorMessage)
            => TryToNumber<decimal, int>(value, Math.Round, out result, out discarded, out errorMessage);

        // Native conversions (truncated towards zero)
        public static bool TryToIntTruncated(float value, out int result) => TryToNumber(value, out result);
        public static bool TryToIntTruncated(double value, out int result) => TryToNumber(value, out result);
        public static bool TryToIntTruncated(decimal value, out int result) => TryToNumber(value, out result);

        public static bool TryToIntTruncated(float value, out int result, out decimal discarded, out string? errorMessage)
            => TryToNumber<float, int>(value, x => x, out result, out discarded, out errorMessage);

        public static bool TryToIntTruncated(double value, out int result, out decimal discarded, out string? errorMessage)
            => TryToNumber<double, int>(value, x => x, out result, out discarded, out errorMessage);

        public static bool TryToIntTruncated(decimal value, out int result, out decimal discarded, out string? errorMessage)
            => TryToNumber<decimal, int>(value, x => x, out result, out discarded, out errorMessage);

        // Nullables (banker's rounding)
        public static bool TryToInt(int? value, out int result) => TryToNumber(value, out result);
        public static bool TryToInt(long? value, out int result) => TryToNumber(value, out result);
        public static bool TryToInt(float? value, out int result) => TryToNumber(value, MathF.Round, out result);
        public static bool TryToInt(double? value, out int result) => TryToNumber(value, Math.Round, out result);
        public static bool TryToInt(decimal? value, out int result) => TryToNumber(value, Math.Round, out result);

        public static bool TryToInt(float? value, out int result, out decimal discarded, out string? errorMessage)
            => TryToNumber<float, int>(value, MathF.Round, out result, out discarded, out errorMessage);

        public static bool TryToInt(double? value, out int result, out decimal discarded, out string? errorMessage)
            => TryToNumber<double, int>(value, Math.Round, out result, out discarded, out errorMessage);

        public static bool TryToInt(decimal? value, out int result, out decimal discarded, out string? errorMessage)
            => TryToNumber<decimal, int>(value, Math.Round, out result, out discarded, out errorMessage);

        // Nullables (truncated towards zero)
        public static bool TryToIntTruncated(float? value, out int result) => TryToNumber(value, out result);
        public static bool TryToIntTruncated(double? value, out int result) => TryToNumber(value, out result);
        public static bool TryToIntTruncated(decimal? value, out int result) => TryToNumber(value, out result);

        public static bool TryToIntTruncated(float? value, out int result, out decimal discarded, out string? errorMessage)
            => TryToNumber<float, int>(value, x => x, out result, out discarded, out errorMessage);

        public static bool TryToIntTruncated(double? value, out int result, out decimal discarded, out string? errorMessage)
            => TryToNumber<double, int>(value, x => x, out result, out discarded, out errorMessage);

        public static bool TryToIntTruncated(decimal? value, out int result, out decimal discarded, out string? errorMessage)
            => TryToNumber<decimal, int>(value, x => x, out result, out discarded, out errorMess

        // Text
        public static bool TryToInt(string? text, out int value)
            => TryToNumber(text, NumberStyles.Integer, CultureInfo.InvariantCulture, out value);

        public static bool TryToInt(string? text, NumberStyles styles, out int value)
            => TryToNumber(text, styles, CultureInfo.InvariantCulture, out value);

        public static bool TryToSystemInt(string? text, out int value)
            => TryToNumber(text, NumberStyles.Integer, CultureInfo.CurrentCulture, out value);

        public static bool TryToSystemInt(string? text, NumberStyles styles, out int value)
            => TryToNumber(text, styles, CultureInfo.CurrentCulture, out value);

        public static bool TryToInt(string? text, IFormatProvider? provider, out int value)
            => TryToNumber(text, NumberStyles.Integer, provider, out value);

        public static bool TryToInt(string? text, NumberStyles styles, IFormatProvider? provider, out int value)
            => TryToNumber(text, styles, provider, out value);

        #endregion

        #region Long

        // Native conversions (banker's rounding)
        public static bool TryToLong(int value, out long result, out string? errorMessage) => TryToNumber(value, out result, out errorMessage);
        public static bool TryToLong(float value, out long result, out string? errorMessage) => TryToNumber(MathF.Round(value), out result, out errorMessage);
        public static bool TryToLong(double value, out long result, out string? errorMessage) => TryToNumber(Math.Round(value), out result, out errorMessage);
        public static bool TryToLong(decimal value, out long result, out string? errorMessage) => TryToNumber(Math.Round(value), out result, out errorMessage);

        public static bool TryToLong(float value, out long result, out decimal discarded, out string? errorMessage)
            => TryToNumber<float, long>(value, MathF.Round, out result, out discarded, out errorMessage);

        public static bool TryToLong(double value, out long result, out decimal discarded, out string? errorMessage)
            => TryToNumber<double, long>(value, Math.Round, out result, out discarded, out errorMessage);

        public static bool TryToLong(decimal value, out long result, out decimal discarded, out string? errorMessage)
            => TryToNumber<decimal, long>(value, Math.Round, out result, out discarded, out errorMessage);

        // Native conversions (truncated towards zero)
        public static bool TryToLongTruncated(float value, out long result, out string? errorMessage) => TryToNumber(value, out result, out errorMessage);
        public static bool TryToLongTruncated(double value, out long result, out string? errorMessage) => TryToNumber(value, out result, out errorMessage);
        public static bool TryToLongTruncated(decimal value, out long result, out string? errorMessage) => TryToNumber(value, out result, out errorMessage);

        public static bool TryToLongTruncated(float value, out long result, out decimal discarded, out string? errorMessage)
            => TryToNumber<float, long>(value, x => x, out result, out discarded, out errorMessage);

        public static bool TryToLongTruncated(double value, out long result, out decimal discarded, out string? errorMessage)
            => TryToNumber<double, long>(value, x => x, out result, out discarded, out errorMessage);

        public static bool TryToLongTruncated(decimal value, out long result, out decimal discarded, out string? errorMessage)
            => TryToNumber<decimal, long>(value, x => x, out result, out discarded, out errorMessage);

        // Nullables (banker's rounding)
        public static bool TryToLong(int? value, out long result, out string? errorMessage) => TryToNumber(value, out result, out errorMessage);
        public static bool TryToLong(long? value, out long result, out string? errorMessage) => TryToNumber(value, out result, out errorMessage);
        public static bool TryToLong(float? value, out long result, out string? errorMessage) => TryToNumber(value, MathF.Round, out result, out errorMessage);
        public static bool TryToLong(double? value, out long result, out string? errorMessage) => TryToNumber(value, Math.Round, out result, out errorMessage);
        public static bool TryToLong(decimal? value, out long result, out string? errorMessage) => TryToNumber(value, Math.Round, out result, out errorMessage);

        public static bool TryToLong(float? value, out long result, out decimal discarded, out string? errorMessage)
            => TryToNumber<float, long>(value, MathF.Round, out result, out discarded, out errorMessage);

        public static bool TryToLong(double? value, out long result, out decimal discarded, out string? errorMessage)
            => TryToNumber<double, long>(value, Math.Round, out result, out discarded, out errorMessage);

        public static bool TryToLong(decimal? value, out long result, out decimal discarded, out string? errorMessage)
            => TryToNumber<decimal, long>(value, Math.Round, out result, out discarded, out errorMessage);

        // Nullables (truncated towards zero)
        public static bool TryToLongTruncated(float? value, out long result, out string? errorMessage) => TryToNumber(value, out result, out errorMessage);
        public static bool TryToLongTruncated(double? value, out long result, out string? errorMessage) => TryToNumber(value, out result, out errorMessage);
        public static bool TryToLongTruncated(decimal? value, out long result, out string? errorMessage) => TryToNumber(value, out result, out errorMessage);

        public static bool TryToLongTruncated(float? value, out long result, out decimal discarded, out string? errorMessage)
            => TryToNumber<float, long>(value, x => x, out result, out discarded, out errorMessage);

        public static bool TryToLongTruncated(double? value, out long result, out decimal discarded, out string? errorMessage)
            => TryToNumber<double, long>(value, x => x, out result, out discarded, out errorMessage);

        public static bool TryToLongTruncated(decimal? value, out long result, out decimal discarded, out string? errorMessage)
            => TryToNumber<decimal, long>(value, x => x, out result, out discarded, out errorMessage);

        // Text
        public static bool TryToLong(string? text, out long value)
            => TryToNumber(text, NumberStyles.Integer, CultureInfo.InvariantCulture, out value);

        public static bool TryToLong(string? text, NumberStyles styles, out long value)
            => TryToNumber(text, styles, CultureInfo.InvariantCulture, out value);

        public static bool TryToSystemLong(string? text, out long value)
            => TryToNumber(text, NumberStyles.Integer, CultureInfo.CurrentCulture, out value);

        public static bool TryToSystemLong(string? text, NumberStyles styles, out long value)
            => TryToNumber(text, styles, CultureInfo.CurrentCulture, out value);

        public static bool TryToLong(string? text, IFormatProvider? provider, out long value)
            => TryToNumber(text, NumberStyles.Integer, provider, out value);

        public static bool TryToLong(string? text, NumberStyles styles, IFormatProvider? provider, out long value)
            => TryToNumber(text, styles, provider, out value);

        #endregion

        #region Float

        // Native conversions
        public static bool TryToFloat(int value, out float result, out string? errorMessage) => TryToNumber(value, out result, out errorMessage);
        public static bool TryToFloat(long value, out float result, out string? errorMessage) => TryToNumber(value, out result, out errorMessage);
        public static bool TryToFloat(double value, out float result, out string? errorMessage) => TryToNumber(value, out result, out errorMessage);
        public static bool TryToFloat(decimal value, out float result, out string? errorMessage) => TryToNumber(value, out result, out errorMessage);

        public static bool TryToFloat(long value, out float result, out decimal discarded, out string? errorMessage)
            => TryToNumber<long, float>(value, x => x, out result, out discarded, out errorMessage);

        public static bool TryToFloat(double value, out float result, out decimal discarded, out string? errorMessage)
            => TryToNumber<double, float>(value, x => x, out result, out discarded, out errorMessage);

        public static bool TryToFloat(decimal value, out float result, out decimal discarded, out string? errorMessage)
            => TryToNumber<decimal, float>(value, x => x, out result, out discarded, out errorMessage);

        // Nullables
        public static bool TryToFloat(int? value, out float result, out string? errorMessage) => TryToNumber(value, out result, out errorMessage);
        public static bool TryToFloat(long? value, out float result, out string? errorMessage) => TryToNumber(value, out result, out errorMessage);
        public static bool TryToFloat(float? value, out float result, out string? errorMessage) => TryToNumber(value, out result, out errorMessage);
        public static bool TryToFloat(double? value, out float result, out string? errorMessage) => TryToNumber(value, out result, out errorMessage);
        public static bool TryToFloat(decimal? value, out float result, out string? errorMessage) => TryToNumber(value, out result, out errorMessage);

        public static bool TryToFloat(long? value, out float result, out decimal discarded, out string? errorMessage)
    => TryToNumber<long, float>(value, x => x, out result, out discarded, out errorMessage);

        public static bool TryToFloat(double? value, out float result, out decimal discarded, out string? errorMessage)
            => TryToNumber<double, float>(value, x => x, out result, out discarded, out errorMessage);

        public static bool TryToFloat(decimal? value, out float result, out decimal discarded, out string? errorMessage)
            => TryToNumber<decimal, float>(value, x => x, out result, out discarded, out errorMessage);

        // Text
        public static bool TryToFloat(string? text, out float value)
            => TryToNumber(text, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out value);

        public static bool TryToFloat(string? text, NumberStyles styles, out float value)
            => TryToNumber(text, styles, CultureInfo.InvariantCulture, out value);

        public static bool TryToSystemFloat(string? text, out float value)
            => TryToNumber(text, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.CurrentCulture, out value);

        public static bool TryToSystemFloat(string? text, NumberStyles styles, out float value)
            => TryToNumber(text, styles, CultureInfo.CurrentCulture, out value);

        public static bool TryToFloat(string? text, IFormatProvider? provider, out float value)
            => TryToNumber(text, NumberStyles.Float | NumberStyles.AllowThousands, provider, out value);

        public static bool TryToFloat(string? text, NumberStyles styles, IFormatProvider? provider, out float value)
            => TryToNumber(text, styles, provider, out value);

        #endregion

        #region Double

        // Native conversions
        public static bool TryToDouble(int value, out double result, out string? errorMessage) => TryToNumber(value, out result, out errorMessage);
        public static bool TryToDouble(long value, out double result, out string? errorMessage) => TryToNumber(value, out result, out errorMessage);
        public static bool TryToDouble(float value, out double result, out string? errorMessage) => TryToNumber(value, out result, out errorMessage);
        public static bool TryToDouble(decimal value, out double result, out string? errorMessage) => TryToNumber(value, out result, out errorMessage);

        public static bool TryToDouble(long value, out double result, out decimal discarded, out string? errorMessage)
            => TryToNumber<long, double>(value, x => x, out result, out discarded, out errorMessage);

        public static bool TryToDouble(decimal value, out double result, out decimal discarded, out string? errorMessage)
            => TryToNumber<decimal, double>(value, x => x, out result, out discarded, out errorMessage);

        // Nullables
        public static bool TryToDouble(int? value, out double result, out string? errorMessage) => TryToNumber(value, out result, out errorMessage);
        public static bool TryToDouble(long? value, out double result, out string? errorMessage) => TryToNumber(value, out result, out errorMessage);
        public static bool TryToDouble(float? value, out double result, out string? errorMessage) => TryToNumber(value, out result, out errorMessage);
        public static bool TryToDouble(double? value, out double result, out string? errorMessage) => TryToNumber(value, out result, out errorMessage);
        public static bool TryToDouble(decimal? value, out double result, out string? errorMessage) => TryToNumber(value, out result, out errorMessage);

        public static bool TryToDouble(long? value, out double result, out decimal discarded, out string? errorMessage)
            => TryToNumber<long, double>(value, x => x, out result, out discarded, out errorMessage);

        public static bool TryToDouble(decimal? value, out double result, out decimal discarded, out string? errorMessage)
            => TryToNumber<decimal, double>(value, x => x, out result, out discarded, out errorMessage);

        // Text
        public static bool TryToDouble(string? text, out double value)
            => TryToNumber(text, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out value);

        public static bool TryToDouble(string? text, NumberStyles styles, out double value)
            => TryToNumber(text, styles, CultureInfo.InvariantCulture, out value);

        public static bool TryToSystemDouble(string? text, out double value)
            => TryToNumber(text, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.CurrentCulture, out value);

        public static bool TryToSystemDouble(string? text, NumberStyles styles, out double value)
            => TryToNumber(text, styles, CultureInfo.CurrentCulture, out value);

        public static bool TryToDouble(string? text, IFormatProvider? provider, out double value)
            => TryToNumber(text, NumberStyles.Float | NumberStyles.AllowThousands, provider, out value);

        public static bool TryToDouble(string? text, NumberStyles styles, IFormatProvider? provider, out double value)
            => TryToNumber(text, styles, provider, out value);

        #endregion

        #region Decimal

        // Native conversions
        public static bool TryToDecimal(int value, out decimal result, out string? errorMessage) => TryToNumber(value, out result, out errorMessage);
        public static bool TryToDecimal(long value, out decimal result, out string? errorMessage) => TryToNumber(value, out result, out errorMessage);
        public static bool TryToDecimal(float value, out decimal result, out string? errorMessage) => TryToNumber(value, out result, out errorMessage);
        public static bool TryToDecimal(double value, out decimal result, out string? errorMessage) => TryToNumber(value, out result, out errorMessage);

        public static bool TryToDecimal(float value, out decimal result, out decimal discarded, out string? errorMessage)
            => TryToNumber<float, decimal>(value, x => x, out result, out discarded, out errorMessage);

        public static bool TryToDecimal(double value, out decimal result, out decimal discarded, out string? errorMessage)
            => TryToNumber<double, decimal>(value, x => x, out result, out discarded, out errorMessage);

        // Nullables
        public static bool TryToDecimal(int? value, out decimal result, out string? errorMessage) => TryToNumber(value, out result, out errorMessage);
        public static bool TryToDecimal(long? value, out decimal result, out string? errorMessage) => TryToNumber(value, out result, out errorMessage);
        public static bool TryToDecimal(float? value, out decimal result, out string? errorMessage) => TryToNumber(value, out result, out errorMessage);
        public static bool TryToDecimal(double? value, out decimal result, out string? errorMessage) => TryToNumber(value, out result, out errorMessage);
        public static bool TryToDecimal(decimal? value, out decimal result, out string? errorMessage) => TryToNumber(value, out result, out errorMessage);

        public static bool TryToDecimal(float? value, out decimal result, out decimal discarded, out string? errorMessage)
            => TryToNumber<float, decimal>(value, x => x, out result, out discarded, out errorMessage);

        public static bool TryToDecimal(double? value, out decimal result, out decimal discarded, out string? errorMessage)
            => TryToNumber<double, decimal>(value, x => x, out result, out discarded, out errorMessage);

        // Text
        public static bool TryToDecimal(string? text, out decimal value)
            => TryToNumber(text, NumberStyles.Number, CultureInfo.InvariantCulture, out value);

        public static bool TryToDecimal(string? text, NumberStyles styles, out decimal value)
            => TryToNumber(text, styles, CultureInfo.InvariantCulture, out value);

        public static bool TryToSystemDecimal(string? text, out decimal value)
            => TryToNumber(text, NumberStyles.Number, CultureInfo.CurrentCulture, out value);

        public static bool TryToSystemDecimal(string? text, NumberStyles styles, out decimal value)
            => TryToNumber(text, styles, CultureInfo.CurrentCulture, out value);

        public static bool TryToDecimal(string? text, IFormatProvider? provider, out decimal value)
            => TryToNumber(text, NumberStyles.Number, provider, out value);

        public static bool TryToDecimal(string? text, NumberStyles styles, IFormatProvider? provider, out decimal value)
            => TryToNumber(text, styles, provider, out value);

        #endregion

        #endregion

        #region METHODS — Result

        #region Generic

        public static C_Result<TTo> TryToNumber<TFrom, TTo>(TFrom value)
            where TFrom : struct, INumberBase<TFrom>
            where TTo : struct, INumberBase<TTo>
        {
            try
            {
                return C_Result<TTo>.Ok(TTo.CreateChecked(value));
            }
            catch (Exception ex) when (ex is OverflowException or NotFiniteNumberException)
            {
                return C_Result<TTo>.Fail(ex);
            }
        }

        public static C_Result<TTo> TryToNumber<TFrom, TTo>(TFrom? value)
            where TFrom : struct, INumberBase<TFrom>
            where TTo : struct, INumberBase<TTo>
        {
            if (value is null)
                return C_Result<TTo>.Fail("Value is null.");

            return TryToNumber<TFrom, TTo>(value.Value);
        }

        private static C_Result<TTo> TryToNumber<TFrom, TTo>(TFrom? value, Func<TFrom, TFrom> transform)
            where TFrom : struct, INumberBase<TFrom>
            where TTo : struct, INumberBase<TTo>
        {
            if (value is null)
                return C_Result<TTo>.Fail("Value is null.");

            return TryToNumber<TFrom, TTo>(transform(value.Value));
        }

        public static C_Result<T> TryToNumber<T>(string? text, NumberStyles styles, IFormatProvider? provider = null)
            where T : struct, INumber<T>
        {
            if (string.IsNullOrWhiteSpace(text))
                return C_Result<T>.Fail("Input text is null or empty.");

            try
            {
                T value = T.Parse(text, styles, provider ?? CultureInfo.InvariantCulture);
                return C_Result<T>.Ok(value);
            }
            catch (Exception ex) when (ex is FormatException or OverflowException)
            {
                return C_Result<T>.Fail(ex);
            }
        }

        private static C_Result<TTo> TryToNumberDiscarded<TFrom, TTo>(TFrom value, Func<TFrom, TFrom> transform, out decimal discarded)
            where TFrom : struct, INumberBase<TFrom>
            where TTo : struct, INumberBase<TTo>
        {
            try
            {
                TFrom transformed = transform(value);
                TTo converted = TTo.CreateChecked(transformed);

                decimal originalDecimal = Convert.ToDecimal(value);
                decimal convertedDecimal = Convert.ToDecimal(converted);

                try
                {
                    discarded = originalDecimal - convertedDecimal;
                }
                catch (OverflowException)
                {
                    discarded = 0m;
                }

                return C_Result<TTo>.Ok(converted);
            }
            catch (Exception ex) when (ex is OverflowException or NotFiniteNumberException)
            {
                discarded = 0m;
                return C_Result<TTo>.Fail(ex);
            }
        }

        private static C_Result<TTo> TryToNumberDiscarded<TFrom, TTo>(TFrom? value, Func<TFrom, TFrom> transform, out decimal discarded)
            where TFrom : struct, INumberBase<TFrom>
            where TTo : struct, INumberBase<TTo>
        {
            if (value is null)
            {
                discarded = 0m;
                return C_Result<TTo>.Fail("Value is null.");
            }

            return TryToNumberDiscarded<TFrom, TTo>(value.Value, transform, out discarded);
        }

        #endregion

        #region Int

        // Native conversions (banker's rounding)
        public static C_Result<int> TryToInt(long value) => TryToNumber<long, int>(value);
        public static C_Result<int> TryToInt(float value) => TryToNumber<float, int>(MathF.Round(value));
        public static C_Result<int> TryToInt(double value) => TryToNumber<double, int>(Math.Round(value));
        public static C_Result<int> TryToInt(decimal value) => TryToNumber<decimal, int>(Math.Round(value));

        // Nullables (banker's rounding)
        public static C_Result<int> TryToInt(int? value) => TryToNumber<int, int>(value);
        public static C_Result<int> TryToInt(long? value) => TryToNumber<long, int>(value);
        public static C_Result<int> TryToInt(float? value) => TryToNumber<float, int>(value, MathF.Round);
        public static C_Result<int> TryToInt(double? value) => TryToNumber<double, int>(value, Math.Round);
        public static C_Result<int> TryToInt(decimal? value) => TryToNumber<decimal, int>(value, Math.Round);

        // Native conversions (truncated towards zero)
        public static C_Result<int> TryToIntTruncated(float value) => TryToNumber<float, int>(value);
        public static C_Result<int> TryToIntTruncated(double value) => TryToNumber<double, int>(value);
        public static C_Result<int> TryToIntTruncated(decimal value) => TryToNumber<decimal, int>(value);

        // Nullables (truncated towards zero)
        public static C_Result<int> TryToIntTruncated(float? value) => TryToNumber<float, int>(value);
        public static C_Result<int> TryToIntTruncated(double? value) => TryToNumber<double, int>(value);
        public static C_Result<int> TryToIntTruncated(decimal? value) => TryToNumber<decimal, int>(value);

        // Native conversions with discarded part (banker's rounding)
        public static C_Result<int> TryToInt(float value, out decimal discarded)
            => TryToNumberDiscarded<float, int>(value, MathF.Round, out discarded);

        public static C_Result<int> TryToInt(double value, out decimal discarded)
            => TryToNumberDiscarded<double, int>(value, Math.Round, out discarded);

        public static C_Result<int> TryToInt(decimal value, out decimal discarded)
            => TryToNumberDiscarded<decimal, int>(value, Math.Round, out discarded);

        // Nullables with discarded part (banker's rounding)
        public static C_Result<int> TryToInt(float? value, out decimal discarded)
            => TryToNumberDiscarded<float, int>(value, MathF.Round, out discarded);

        public static C_Result<int> TryToInt(double? value, out decimal discarded)
            => TryToNumberDiscarded<double, int>(value, Math.Round, out discarded);

        public static C_Result<int> TryToInt(decimal? value, out decimal discarded)
            => TryToNumberDiscarded<decimal, int>(value, Math.Round, out discarded);

        // Native conversions with discarded part (truncated towards zero)
        public static C_Result<int> TryToIntTruncated(float value, out decimal discarded)
            => TryToNumberDiscarded<float, int>(value, x => x, out discarded);

        public static C_Result<int> TryToIntTruncated(double value, out decimal discarded)
            => TryToNumberDiscarded<double, int>(value, x => x, out discarded);

        public static C_Result<int> TryToIntTruncated(decimal value, out decimal discarded)
            => TryToNumberDiscarded<decimal, int>(value, x => x, out discarded);

        // Nullables with discarded part (truncated towards zero)
        public static C_Result<int> TryToIntTruncated(float? value, out decimal discarded)
            => TryToNumberDiscarded<float, int>(value, x => x, out discarded);

        public static C_Result<int> TryToIntTruncated(double? value, out decimal discarded)
            => TryToNumberDiscarded<double, int>(value, x => x, out discarded);

        public static C_Result<int> TryToIntTruncated(decimal? value, out decimal discarded)
            => TryToNumberDiscarded<decimal, int>(value, x => x, out discarded);

        // Text
        public static C_Result<int> TryToInt(string? text)
            => TryToNumber<int>(text, NumberStyles.Integer, CultureInfo.InvariantCulture);

        public static C_Result<int> TryToInt(string? text, NumberStyles styles)
            => TryToNumber<int>(text, styles, CultureInfo.InvariantCulture);

        public static C_Result<int> TryToSystemInt(string? text)
            => TryToNumber<int>(text, NumberStyles.Integer, CultureInfo.CurrentCulture);

        public static C_Result<int> TryToSystemInt(string? text, NumberStyles styles)
            => TryToNumber<int>(text, styles, CultureInfo.CurrentCulture);

        public static C_Result<int> TryToInt(string? text, IFormatProvider? provider)
            => TryToNumber<int>(text, NumberStyles.Integer, provider);

        public static C_Result<int> TryToInt(string? text, NumberStyles styles, IFormatProvider? provider)
            => TryToNumber<int>(text, styles, provider);

        #endregion

        #region Long

        // Native conversions (banker's rounding)
        public static C_Result<long> TryToLong(int value) => TryToNumber<int, long>(value);
        public static C_Result<long> TryToLong(float value) => TryToNumber<float, long>(MathF.Round(value));
        public static C_Result<long> TryToLong(double value) => TryToNumber<double, long>(Math.Round(value));
        public static C_Result<long> TryToLong(decimal value) => TryToNumber<decimal, long>(Math.Round(value));

        // Nullables (banker's rounding)
        public static C_Result<long> TryToLong(int? value) => TryToNumber<int, long>(value);
        public static C_Result<long> TryToLong(long? value) => TryToNumber<long, long>(value);
        public static C_Result<long> TryToLong(float? value) => TryToNumber<float, long>(value, MathF.Round);
        public static C_Result<long> TryToLong(double? value) => TryToNumber<double, long>(value, Math.Round);
        public static C_Result<long> TryToLong(decimal? value) => TryToNumber<decimal, long>(value, Math.Round);

        // Native conversions (truncated towards zero)
        public static C_Result<long> TryToLongTruncated(float value) => TryToNumber<float, long>(value);
        public static C_Result<long> TryToLongTruncated(double value) => TryToNumber<double, long>(value);
        public static C_Result<long> TryToLongTruncated(decimal value) => TryToNumber<decimal, long>(value);

        // Nullables (truncated towards zero)
        public static C_Result<long> TryToLongTruncated(float? value) => TryToNumber<float, long>(value);
        public static C_Result<long> TryToLongTruncated(double? value) => TryToNumber<double, long>(value);
        public static C_Result<long> TryToLongTruncated(decimal? value) => TryToNumber<decimal, long>(value);

        // Native conversions with discarded part (banker's rounding)
        public static C_Result<long> TryToLong(float value, out decimal discarded)
            => TryToNumberDiscarded<float, long>(value, MathF.Round, out discarded);

        public static C_Result<long> TryToLong(double value, out decimal discarded)
            => TryToNumberDiscarded<double, long>(value, Math.Round, out discarded);

        public static C_Result<long> TryToLong(decimal value, out decimal discarded)
            => TryToNumberDiscarded<decimal, long>(value, Math.Round, out discarded);

        // Nullables with discarded part (banker's rounding)
        public static C_Result<long> TryToLong(float? value, out decimal discarded)
            => TryToNumberDiscarded<float, long>(value, MathF.Round, out discarded);

        public static C_Result<long> TryToLong(double? value, out decimal discarded)
            => TryToNumberDiscarded<double, long>(value, Math.Round, out discarded);

        public static C_Result<long> TryToLong(decimal? value, out decimal discarded)
            => TryToNumberDiscarded<decimal, long>(value, Math.Round, out discarded);

        // Native conversions with discarded part (truncated towards zero)
        public static C_Result<long> TryToLongTruncated(float value, out decimal discarded)
            => TryToNumberDiscarded<float, long>(value, x => x, out discarded);

        public static C_Result<long> TryToLongTruncated(double value, out decimal discarded)
            => TryToNumberDiscarded<double, long>(value, x => x, out discarded);

        public static C_Result<long> TryToLongTruncated(decimal value, out decimal discarded)
            => TryToNumberDiscarded<decimal, long>(value, x => x, out discarded);

        // Nullables with discarded part (truncated towards zero)
        public static C_Result<long> TryToLongTruncated(float? value, out decimal discarded)
            => TryToNumberDiscarded<float, long>(value, x => x, out discarded);

        public static C_Result<long> TryToLongTruncated(double? value, out decimal discarded)
            => TryToNumberDiscarded<double, long>(value, x => x, out discarded);

        public static C_Result<long> TryToLongTruncated(decimal? value, out decimal discarded)
            => TryToNumberDiscarded<decimal, long>(value, x => x, out discarded);

        // Text
        public static C_Result<long> TryToLong(string? text)
            => TryToNumber<long>(text, NumberStyles.Integer, CultureInfo.InvariantCulture);

        public static C_Result<long> TryToLong(string? text, NumberStyles styles)
            => TryToNumber<long>(text, styles, CultureInfo.InvariantCulture);

        public static C_Result<long> TryToSystemLong(string? text)
            => TryToNumber<long>(text, NumberStyles.Integer, CultureInfo.CurrentCulture);

        public static C_Result<long> TryToSystemLong(string? text, NumberStyles styles)
            => TryToNumber<long>(text, styles, CultureInfo.CurrentCulture);

        public static C_Result<long> TryToLong(string? text, IFormatProvider? provider)
            => TryToNumber<long>(text, NumberStyles.Integer, provider);

        public static C_Result<long> TryToLong(string? text, NumberStyles styles, IFormatProvider? provider)
            => TryToNumber<long>(text, styles, provider);

        #endregion

        #region Float

        // Native conversions
        public static C_Result<float> TryToFloat(int value) => TryToNumber<int, float>(value);
        public static C_Result<float> TryToFloat(long value) => TryToNumber<long, float>(value);
        public static C_Result<float> TryToFloat(double value) => TryToNumber<double, float>(value);
        public static C_Result<float> TryToFloat(decimal value) => TryToNumber<decimal, float>(value);

        // Nullables
        public static C_Result<float> TryToFloat(int? value) => TryToNumber<int, float>(value);
        public static C_Result<float> TryToFloat(long? value) => TryToNumber<long, float>(value);
        public static C_Result<float> TryToFloat(float? value) => TryToNumber<float, float>(value);
        public static C_Result<float> TryToFloat(double? value) => TryToNumber<double, float>(value);
        public static C_Result<float> TryToFloat(decimal? value) => TryToNumber<decimal, float>(value);

        // Native conversions with discarded part
        public static C_Result<float> TryToFloat(decimal value, out decimal discarded)
            => TryToNumberDiscarded<decimal, float>(value, x => x, out discarded);

        // Nullables with discarded part
        public static C_Result<float> TryToFloat(decimal? value, out decimal discarded)
            => TryToNumberDiscarded<decimal, float>(value, x => x, out discarded);

        // Text
        public static C_Result<float> TryToFloat(string? text)
            => TryToNumber<float>(text, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture);

        public static C_Result<float> TryToFloat(string? text, NumberStyles styles)
            => TryToNumber<float>(text, styles, CultureInfo.InvariantCulture);

        public static C_Result<float> TryToSystemFloat(string? text)
            => TryToNumber<float>(text, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.CurrentCulture);

        public static C_Result<float> TryToSystemFloat(string? text, NumberStyles styles)
            => TryToNumber<float>(text, styles, CultureInfo.CurrentCulture);

        public static C_Result<float> TryToFloat(string? text, IFormatProvider? provider)
            => TryToNumber<float>(text, NumberStyles.Float | NumberStyles.AllowThousands, provider);

        public static C_Result<float> TryToFloat(string? text, NumberStyles styles, IFormatProvider? provider)
            => TryToNumber<float>(text, styles, provider);

        #endregion

        #region Double

        // Native conversions
        public static C_Result<double> TryToDouble(int value) => TryToNumber<int, double>(value);
        public static C_Result<double> TryToDouble(long value) => TryToNumber<long, double>(value);
        public static C_Result<double> TryToDouble(float value) => TryToNumber<float, double>(value);
        public static C_Result<double> TryToDouble(decimal value) => TryToNumber<decimal, double>(value);

        // Nullables
        public static C_Result<double> TryToDouble(int? value) => TryToNumber<int, double>(value);
        public static C_Result<double> TryToDouble(long? value) => TryToNumber<long, double>(value);
        public static C_Result<double> TryToDouble(float? value) => TryToNumber<float, double>(value);
        public static C_Result<double> TryToDouble(double? value) => TryToNumber<double, double>(value);
        public static C_Result<double> TryToDouble(decimal? value) => TryToNumber<decimal, double>(value);

        // Native conversions with discarded part
        public static C_Result<double> TryToDouble(decimal value, out decimal discarded)
            => TryToNumberDiscarded<decimal, double>(value, x => x, out discarded);

        // Nullables with discarded part
        public static C_Result<double> TryToDouble(decimal? value, out decimal discarded)
            => TryToNumberDiscarded<decimal, double>(value, x => x, out discarded);

        // Text
        public static C_Result<double> TryToDouble(string? text)
            => TryToNumber<double>(text, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture);

        public static C_Result<double> TryToDouble(string? text, NumberStyles styles)
            => TryToNumber<double>(text, styles, CultureInfo.InvariantCulture);

        public static C_Result<double> TryToSystemDouble(string? text)
            => TryToNumber<double>(text, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.CurrentCulture);

        public static C_Result<double> TryToSystemDouble(string? text, NumberStyles styles)
            => TryToNumber<double>(text, styles, CultureInfo.CurrentCulture);

        public static C_Result<double> TryToDouble(string? text, IFormatProvider? provider)
            => TryToNumber<double>(text, NumberStyles.Float | NumberStyles.AllowThousands, provider);

        public static C_Result<double> TryToDouble(string? text, NumberStyles styles, IFormatProvider? provider)
            => TryToNumber<double>(text, styles, provider);

        #endregion

        #region Decimal

        // Native conversions
        public static C_Result<decimal> TryToDecimal(int value) => TryToNumber<int, decimal>(value);
        public static C_Result<decimal> TryToDecimal(long value) => TryToNumber<long, decimal>(value);
        public static C_Result<decimal> TryToDecimal(float value) => TryToNumber<float, decimal>(value);
        public static C_Result<decimal> TryToDecimal(double value) => TryToNumber<double, decimal>(value);

        // Nullables
        public static C_Result<decimal> TryToDecimal(int? value) => TryToNumber<int, decimal>(value);
        public static C_Result<decimal> TryToDecimal(long? value) => TryToNumber<long, decimal>(value);
        public static C_Result<decimal> TryToDecimal(float? value) => TryToNumber<float, decimal>(value);
        public static C_Result<decimal> TryToDecimal(double? value) => TryToNumber<double, decimal>(value);
        public static C_Result<decimal> TryToDecimal(decimal? value) => TryToNumber<decimal, decimal>(value);

        // Text
        public static C_Result<decimal> TryToDecimal(string? text)
            => TryToNumber<decimal>(text, NumberStyles.Number, CultureInfo.InvariantCulture);

        public static C_Result<decimal> TryToDecimal(string? text, NumberStyles styles)
            => TryToNumber<decimal>(text, styles, CultureInfo.InvariantCulture);

        public static C_Result<decimal> TryToSystemDecimal(string? text)
            => TryToNumber<decimal>(text, NumberStyles.Number, CultureInfo.CurrentCulture);

        public static C_Result<decimal> TryToSystemDecimal(string? text, NumberStyles styles)
            => TryToNumber<decimal>(text, styles, CultureInfo.CurrentCulture);

        public static C_Result<decimal> TryToDecimal(string? text, IFormatProvider? provider)
            => TryToNumber<decimal>(text, NumberStyles.Number, provider);

        public static C_Result<decimal> TryToDecimal(string? text, NumberStyles styles, IFormatProvider? provider)
            => TryToNumber<decimal>(text, styles, provider);

        #endregion

        #endregion
    }
}