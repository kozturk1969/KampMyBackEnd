using Core.DataAccess;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IProductDal:IEntityRepository<Product> // Sen bir IentityRepository'ın  ve çalışma tipin Product'tur
    {
        //Yukarıdaki şekilde generic yapı kurunca aşağıdaki kodlara gerek kalmıyor
        //List<Product> GetAll();
        //void Add(Product product);
        //void Update(Product product);
        //void Delete(Product product);
        //List<Product> GetAllByCategory(int categoryId);
    }
}
//Code Refactoring