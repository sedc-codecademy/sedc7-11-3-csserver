using System;
using System.Collections.Generic;
using System.Text;

namespace AwaitAsyncDemo
{
    class NonIEnumerableEnumerable
    {
        public NonIEnumeratorEnumerator GetEnumerator()
        {
            return new NonIEnumeratorEnumerator();
        }
    }

    class NonIEnumeratorEnumerator
    {
        public int Index = 0;

        public object Current
        {
            get
            {
                return Index;
            }
        }

        public bool MoveNext()
        {
            if (Index == 10)
            {
                return false;
            }
            Index += 1;
            return true;
        }

    }

    static class Helpers
    {
        public static IEnumerable<int> GetAllEvenNumbers()
        {
            for (var index = 2; true; index += 2)
            {
                yield return index;
            }
        }
    }
}
