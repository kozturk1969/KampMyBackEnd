using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
   public  interface IDataResult<T>:IResult
    {

        // Sen bir IResult'ın ve ayrıca yukarıda belirtilen T tipinde data'n var
        T Data { get; }

    }
}
