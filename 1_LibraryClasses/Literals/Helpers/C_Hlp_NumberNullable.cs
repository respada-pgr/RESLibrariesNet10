using System;
using _1_LibraryClasses.Result;

namespace _1_LibraryClasses
{
    /// <summary>
    /// Static helpers for <see cref="C_NumberNullable"/>: factory and nullable conversions.
    /// Shared parse/format remain on <see cref="C_Hlp_Number"/>.
    /// <para><i>[ES] Ayudas de <see cref="C_NumberNullable"/>: factoría y conversiones anulables.
    /// Parseo/formato compartido en <see cref="C_Hlp_Number"/>.</i></para>
    /// </summary>
    public static class C_Hlp_NumberNullable
    {
#region PUBLIC METHODS — convert decimal? → typed (C_Result)

        public static C_Result<int> TryToInt(decimal? value, out decimal discarded)
        {
            if (value is null)
            {
                discarded = 0m;
                return C_Result<int>.Fail("Value is null.");
            }
            return C_Hlp_Number.TryToInt(value.Value, out discarded);
        }

        public static C_Result<int> TryToInt(decimal? value)
            => TryToInt(value, out _);

        public static C_Result<long> TryToLong(decimal? value, out decimal discarded)
        {
            if (value is null)
            {
                discarded = 0m;
                return C_Result<long>.Fail("Value is null.");
            }
            return C_Hlp_Number.TryToLong(value.Value, out discarded);
        }

        public static C_Result<long> TryToLong(decimal? value)
            => TryToLong(value, out _);

        public static C_Result<double> TryToDouble(decimal? value, out decimal discarded)
        {
            if (value is null)
            {
                discarded = 0m;
                return C_Result<double>.Fail("Value is null.");
            }
            return C_Hlp_Number.TryToDouble(value.Value, out discarded);
        }

        public static C_Result<double> TryToDouble(decimal? value)
            => TryToDouble(value, out _);

        public static C_Result<float> TryToFloat(decimal? value, out decimal discarded)
        {
            if (value is null)
            {
                discarded = 0m;
                return C_Result<float>.Fail("Value is null.");
            }
            return C_Hlp_Number.TryToFloat(value.Value, out discarded);
        }

        public static C_Result<float> TryToFloat(decimal? value)
            => TryToFloat(value, out _);

#endregion


#region PUBLIC METHODS — to Nullable (no throw)

        /// <summary>
        /// Returns <see cref="int"/> when convertible; otherwise <see langword="null"/>.
        /// <para><i>[ES] Devuelve <see cref="int"/> si es convertible; si no, <see langword="null"/>.</i></para>
        /// </summary>
        public static int? ToIntOrNull(decimal? value)
        {
            C_Result<int> r = TryToInt(value);
            return r.IsFail ? null : r.Value;
        }

        public static long? ToLongOrNull(decimal? value)
        {
            C_Result<long> r = TryToLong(value);
            return r.IsFail ? null : r.Value;
        }

        public static double? ToDoubleOrNull(decimal? value)
        {
            C_Result<double> r = TryToDouble(value);
            return r.IsFail ? null : r.Value;
        }

        /// <summary>
        /// Returns the decimal value or <see langword="null"/> (identity).
        /// </summary>
        public static decimal? ToDecimalOrNull(decimal? value) => value;

#endregion

    }
}
