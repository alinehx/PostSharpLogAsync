using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PostSharpLogAsync.Logging;

namespace PostSharpLogAsync
{
    [Log]
    public class SampleAsync
    {
        public async Task<int> GetExceptionAsync(int value, string name)
        {
            int result = await CreateError();
            return result;
        }

        private async Task<int> CreateError()
        {
            throw new NotImplementedException();
        }
    }
}
