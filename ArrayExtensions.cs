using System;

namespace PadawansTask11
{
    public static class ArrayExtensions
    {
        public static int? FindIndex(double[] array, double accuracy)
        {
            if (array == null)
                throw new ArgumentNullException();
            if (array.Length < 2)
                throw new ArgumentException();
            double sumAllElements = 0;
            foreach (double number in array)
                try
                {
                    sumAllElements =checked(sumAllElements + number);
                }
                catch(OverflowException ex)
                {
                    throw new OverflowException();
                }
            double sumLeftElements = array[0];
            int ind = -2;
            for (int i = 1; i < array.Length - 2; i++) {
                if (Math.Abs(sumLeftElements - (sumAllElements - sumLeftElements - array[i])) < accuracy)
                {
                    ind = i;
                    break;
                }
                sumLeftElements += array[i];
            }
            if (ind == -2)
                return null;
            return ind;
        }
    }
}
