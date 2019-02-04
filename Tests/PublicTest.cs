using NUnit.Framework;

namespace PadawansTask11.Tests
{
    [TestFixture]
    public class PublicTest
    {
        [TestCase(new[] { 0.0001, 1.0002, -0.0003, 1, 0.0003, 1, -0.0003 }, 0.001, ExpectedResult = 3)]
        [TestCase(new[] { 1.0001, 1.0002, -3.0003, 1, 0.0003, 8.9, -0.0003, 0.901 }, 0.001, ExpectedResult = null)]
        public int? FindIndexTests(double[] array, double accuracy)
            => ArrayExtensions.FindIndex(array, accuracy);
    }
}