using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.CCS;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _ProductDal;
        ICategoryService _categoryService;

        public ProductManager(IProductDal productDal, ICategoryService categoryService)
        {
            _ProductDal = productDal;
            _categoryService = categoryService;
        }


        [SecuredOperation("admin.add,admin")]
        [ValidationAspect(typeof(ProductValidator))]
        public IResult Add(Product product)
        {

            //////////////////////////////////////

            //validation
            //

            //// aşağıdaki kodlar fluint validation ile yapılacak
            //if (product.UnitPrice <= 0)
            //{

            //}
            //if (product.ProductName .Length <2)
            //{

            //    return new ErrorResult(Messages.ProductNameInvalid );
            //}



            ////Fluent validation ile validasyon kontrolü
            //var context = new ValidationContext<Product>(product);
            //ProductValidator productValidator = new ProductValidator();
            //var result = productValidator.Validate(context);

            //if (!result.IsValid)
            //{
            //    throw new ValidationException(result.Errors);
            //}


            //Bu kodu yapıyı kurunca yukarıdaki kodlar gerek kalmıyor
            //ValidationAspect ilede bu koda gerek kalmıyor
            //ValidationTool.Validate(new ProductValidator(), product);

            //////////////////////////////////////

            //Business Rules Codes

            ////Bir kategoride en fazla 15 ürün olabilir
            //if (CheckIfProductOfCategoryCorrect(product.CategoryId).Success == false)
            //{
            //    return new ErrorResult();
            //}

            ////Aynı isimde ürün eklenemez
            //if (CheckIfProductNameExist(product.ProductName).Success == false)
            //{
            //    return new ErrorResult();
            //}

           IResult result= BusinessRules.Run
                (CheckIfProductNameExist(product.ProductName)
                ,
                CheckIfProductOfCategoryCorrect(product.CategoryId)
                ,
                CheckIfCategoryLimitExceded()
                );

            if (result !=null)
            {
                return result;

            }

                        _ProductDal.Add(product);
            return new Result(true, Messages.ProductAdded);



        }

        public IDataResult<List<Product>> GetAll()
        {

            if (DateTime.Now.Hour == 17)
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            }

            return new SuccessDataResult<List<Product>>(_ProductDal.GetAll(), Messages.ProductListed);

        }

        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {
            return new SuccessDataResult<List<Product>>(_ProductDal.GetAll(p => p.CategoryId == id));
        }

        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_ProductDal.Get(p => p.ProductId == productId));
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_ProductDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max));
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            return new SuccessDataResult<List<ProductDetailDto>>(_ProductDal.GetProductDetails());
        }

        [ValidationAspect(typeof(ProductValidator))]
        public IResult Update(Product product)
        {
            throw new NotImplementedException();
        }


        private IResult CheckIfProductOfCategoryCorrect(int categoryId)
        {
            var result = _ProductDal.GetAll(p => p.CategoryId == categoryId).Count;
            if (result >= 15)
            {
                return new ErrorResult(Messages.ProductionCountOfCategoryError);
            }
            return new SuccessResult();
        }

        private IResult CheckIfProductNameExist(string productName)
        {
            var result = _ProductDal.GetAll(p => p.ProductName == productName).Any();
            if (result)
            {
                return new ErrorResult(Messages.ProductNameAlreadyExist);
            }
            return new SuccessResult();
        }

        private IResult CheckIfCategoryLimitExceded()
        {
            var result = _categoryService.GetAll();

            if (result.Data.Count  > 15)
            {
                return new ErrorResult(Messages.CategoryLimitExceded);
            }
            return new SuccessResult();
        }
    }
}
