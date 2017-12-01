using System;

namespace HerbTrans.Infrastructure.Files
{
    public class RandomContext
    {
        public Random RandomNumber { get; }

        public RandomContext()
        {
            RandomNumber = new Random(DateTime.Now.Millisecond);
        }

        public static RandomContext Instance => Nested.NestedInstance;

        class Nested
        {
            static Nested() { }
            internal static readonly RandomContext NestedInstance = new RandomContext();
        }
    }
}
