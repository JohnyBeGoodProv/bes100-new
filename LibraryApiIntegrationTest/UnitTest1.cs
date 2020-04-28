using System;
using Xunit;

namespace LibraryApiIntegrationTest
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            int a = 10, b = 20;

            int answer = a + b;
            Assert.Equal(30, answer);
        }

        [Theory]
        [InlineData(2,2,4)]
        [InlineData(2,5,7)]
        [InlineData(3,3,6)]
        public void CanAdd(int a, int b, int expected)
        {
            Assert.Equal(expected, a + b);
        }
    }
}
