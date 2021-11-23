using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{

    //Static olunca newlemeye gerek kalmıyor
   public static  class Messages
    {
        public static string ProductListed ="Ürünler Listelendi";
        public static string MaintenanceTime ="Bakım Yapılıyor";

        //Public değişkenler Pascal case yazılır
        public static string ProductAdded = "Ürün eklendi";
        public static string ProductNameInvalid = "Ür,n ismi geçersiz";
    }
}
