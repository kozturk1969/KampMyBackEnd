
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess
{
    //Generic consrraint
    //class : reference tip
    //IEntity :IEntity olabilir yada IEntity implemente eder bir nesne olabilir
    //new(); new'lenebilir olmalı. IEntity newlenemeyeceği için parametre olarak verilemez.
    public interface IEntityRepository<T> where T : class, IEntity, new()  //paramteresi referans tip ve Ientity olabilir.
    {
        List<T> GetAll(Expression<Func<T, bool>> filter = null);
        T Get(Expression<Func<T, bool>> filter);


        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);


        //Yukarıdaki generic yapıyı kurunca bu satıra ihtiyaç kalmıyıor.
        //List<T> GetAllByCategory(int categoryId);
    }
}
