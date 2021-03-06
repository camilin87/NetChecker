using System.Linq;
using FluentAssertions;
using NetChecker;
using Xunit;

namespace tests
{
    public class MyTest
    {
        private T id<T>(T x) => x;
        private int pow(int x) => x * x;
        private string concat(string a, string b) => a + b; 

        [Fact]
        public void Identity()
        {
            (new IntProducer()).ChooseFrom().ForAll(x => id(x) == x).Should().BeTrue();
        }

        [Fact]
        public void SomeSquares()
        {
            Gen<int>.FromEnumerable(Enumerable.Range(1, 100)).Any(x => pow(x) == x).Should().BeTrue();
        }

        [Fact]
        public void Concat()
        {
           var stringProducer = new StringProducer(); 
           var producer = new TupleProducer<string, string>(stringProducer, stringProducer);

           producer
               .ChooseFrom()
               .ForAll(t => concat(t.Item1, t.Item2).StartsWith(t.Item1) && concat(t.Item1, t.Item2).EndsWith(t.Item2))
               .Should()
               .BeTrue();
        } 
    }
}