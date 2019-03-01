namespace Tilapia.Tools.Extensions
{
    using System;

    /// <summary>
    /// A basic set of functions to compare numeric values.
    /// </summary>
    public static class Comparision
    {
        /// <summary>
        /// Binary logic expression to compare two object.
        /// </summary>
        /// <typeparam name="T1">Generic type 1</typeparam>
        /// <typeparam name="T2">Generic type 2</typeparam>
        /// <param name="value">Object or value A to compare.</param>
        /// <param name="value2">Object or value to compare the first object/value.</param>
        /// <returns>True if both values are equals.</returns>
        public static bool IsEqual<T1, T2>(this T1 value, T2 value2)
        {
            bool result = false;

            if (((object)value2).IsNull())
            {
                return false;
            }

            dynamic val1 = Convert.ChangeType(value, value.GetType());
            dynamic val2 = Convert.ChangeType(value2, value2.GetType());

            try
            {
                if (val1 == val2)
                {
                    result = true;
                }
            }
            catch
            {
                return false;
            }

            return result;
        }

        /// <summary>
        /// Evaluate if the <see cref="int"/> value is less than value2 parameter.
        /// </summary>
        /// <param name="value">Value 1.</param>
        /// <param name="value2">Object or value to compare the first value.</param>
        /// <returns>True if both values are equals.</returns>
        public static bool IsLessThan(this int value, int value2)
        {
            bool result = false;

            if (value < value2)
            {
                result = true;
            }

            return result;
        }

        /// <summary>
        /// Evaluate if the <see cref="float"/> value is less than value2 parameter.
        /// </summary>
        /// <param name="value">Value 1.</param>
        /// <param name="value2">Object or value to compare the first value.</param>
        /// <returns>True if both values are equals.</returns>
        public static bool IsLessThan(this float value, float value2)
        {
            bool result = false;

            if (value < value2)
            {
                result = true;
            }

            return result;
        }

        /// <summary>
        /// Evaluate if the <see cref="decimal"/> value is less than value2 parameter.
        /// </summary>
        /// <param name="value">Value 1.</param>
        /// <param name="value2">Object or value to compare the first value.</param>
        /// <returns>True if both values are equals.</returns>
        public static bool IsLessThan(this double value, double value2)
        {
            bool result = false;

            if (value < value2)
            {
                result = true;
            }

            return result;
        }

        /// <summary>
        /// Evaluate if the <see cref="decimal"/> value is less than value2 parameter.
        /// </summary>
        /// <param name="value">Value 1.</param>
        /// <param name="value2">Object or value to compare the first value.</param>
        /// <returns>True if both values are equals.</returns>
        public static bool IsLessThan(this decimal value, decimal value2)
        {
            bool result = false;

            if (value < value2)
            {
                result = true;
            }

            return result;
        }

        /// <summary>
        /// Evaluate if the <see cref="int"/> value is bigger than value2 parameter.
        /// </summary>
        /// <param name="value">Value 1.</param>
        /// <param name="value2">Object or value to compare the first value.</param>
        /// <returns>True if both values are equals.</returns>
        public static bool IsBiggerThan(this int value, int value2)
        {
            bool result = false;

            if (value > value2)
            {
                result = true;
            }

            return result;
        }

        /// <summary>
        /// Evaluate if the <see cref="float"/> value is bigger than value2 parameter.
        /// </summary>
        /// <param name="value">Value 1.</param>
        /// <param name="value2">Object or value to compare the first value.</param>
        /// <returns>True if both values are equals.</returns>
        public static bool IsBiggerThan(this float value, float value2)
        {
            bool result = false;

            if (value > value2)
            {
                result = true;
            }

            return result;
        }

        /// <summary>
        /// Evaluate if the <see cref="double"/> value is bigger than value2 parameter.
        /// </summary>
        /// <param name="value">Value 1.</param>
        /// <param name="value2">Object or value to compare the first value.</param>
        /// <returns>True if both values are equals.</returns>
        public static bool IsBiggerThan(this double value, double value2)
        {
            bool result = false;

            if (value > value2)
            {
                result = true;
            }

            return result;
        }

        /// <summary>
        /// Evaluate if the <see cref="decimal"/> value is bigger than value2 parameter.
        /// </summary>
        /// <param name="value">Value 1.</param>
        /// <param name="value2">Object or value to compare the first value.</param>
        /// <returns>True if both values are equals.</returns>
        public static bool IsBiggerThan(this decimal value, decimal value2)
        {
            bool result = false;

            if (value > value2)
            {
                result = true;
            }

            return result;
        }
    }
}
