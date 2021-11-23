using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    public class Result : IResult
    {
 

        public Result(bool success, string message):this(success) // burada : this(success ) ile diğer costructorunda çalışması sağlanıyor succesin dönmesi sağlanıyor
        {
            //ReadOnly 'ler constructorda set edilebilir'
            Message = message;
            //Success = success;  //: this(success ) ile diğer costructorunda çalışması sağlanıyor 
        }


        public Result(bool success)
        {
            //ReadOnly 'ler constructorda set edilebilir'
              Success = success;
        }

        public bool Success {get;}

        public string Message { get; }
    }
}
