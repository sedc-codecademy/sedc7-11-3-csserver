using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AwaitAsyncDemo
{
    class AsyncAdd
    {
        public int Add(int first, int second)
        {
            return first + second;
        }

        public async Task<int> AddAsync(int first, int second)
        {
            return first + second;
        }

    }
}
