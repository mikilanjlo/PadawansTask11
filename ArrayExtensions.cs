using System;

namespace PadawansTask11
{
    public static class ArrayExtensions
    {
        public static int? FindIndex(double[] array, double accuracy)
        {
            if (accuracy <= 0 || accuracy >= 1)
                throw new ArgumentOutOfRangeException();
            if (array == null)
                throw new ArgumentNullException();
            if (array.Length < 2)
                throw new ArgumentException();
            
            //if (IsBigNumbers(array))
                return FindIndexWithBigNumber(array,accuracy);
            //double sumAllElements = 0;
            //foreach (double number in array)
            //    try
            //    {
            //        sumAllElements =checked(sumAllElements + number);
            //    }
            //    catch(OverflowException ex)
            //    {
            //        throw new OverflowException();
            //    }
            //double sumLeftElements = array[0];
            //int ind = -2;
            //for (int i = 1; i < array.Length - 2; i++) {
            //    if (Math.Abs(sumLeftElements - (sumAllElements - sumLeftElements - array[i])) < accuracy)
            //    {
            //        ind = i;
            //        break;
            //    }
            //    sumLeftElements += array[i];
            //}
            //if (ind == -2)
            //    return null;
            //return ind;
        }

        private static bool IsBigNumbers(double[] array)
        {
            bool result = false;
            foreach(double number in array)
            {
                if (number == double.MaxValue)
                    result = true;
                if (number == double.MinValue)
                    result = true;
            }
            return result;
        }

        private static int? FindIndexWithBigNumber(double[] array, double accuracy)
        {
            double[,] prefixSum = new double[array.Length, 2];
            double[,] sufixSum = new double[array.Length, 2];
            for (int i = 0; i < array.Length; i++)
                prefixSum[i, 1] = 0;
            prefixSum[0,0] = array[0];
            for (int i = 1; i < array.Length; i++)
                try
                {
                    prefixSum[i, 0] =checked( prefixSum[i - 1, 0] + array[i]);
                }
                catch (OverflowException ex)
                {
                    double big = prefixSum[i - 1, 0] > array[i] ? prefixSum[i - 1, 0] : array[i];
                    double min = prefixSum[i - 1, 0] <= array[i] ? prefixSum[i - 1, 0] : array[i];
                    prefixSum[i, 1] += big > 0 ? 1 : -1;
                    prefixSum[i, 0] = big - (big > 0 ? double.MaxValue : double.MinValue) + min;
                }
            sufixSum[array.Length - 1, 0] = array[array.Length - 1];
            for (int i = array.Length - 2; i >= 0; i--)
                try
                {
                    sufixSum[i, 0] = checked(sufixSum[i + 1, 0] + array[i]);
                }
                catch (OverflowException ex)
                {
                    double big = sufixSum[i + 1, 0] > array[i] ? sufixSum[i + 1, 0] : array[i];
                    double min = sufixSum[i + 1, 0] <= array[i] ? sufixSum[i + 1, 0] : array[i];
                    sufixSum[i, 1] += big > 0 ? 1 : -1;
                    sufixSum[i, 0] = big - (big > 0 ? double.MaxValue : double.MinValue) + min;
                }
            for (int i = 1; i < array.Length; i++)
                if (Math.Abs(prefixSum[i, 0] - sufixSum[i, 0]) <= accuracy && prefixSum[i, 1] == sufixSum[i, 1])
                    return i;
            return null;
        }
    }
}
