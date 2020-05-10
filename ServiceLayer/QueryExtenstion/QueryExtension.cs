using EntityModels.Entities.Posts;
using EntityModels.Entities.Products;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace ServiceLayer.QueryExtenstion
{
    public static class QueryExtension
    {
        public enum ProductOrder
        {
            date,alpha
        }

        public static IQueryable<Product> SkipAndTake(this IQueryable<ProductKeyword> query, int page, int count, OrderBy order)
        {
            switch (order)
            {
                case OrderBy.date:
                    return query.Include(x=>x.Product).Select(x=>x.Product).OrderByDescending(x=>x.PublishDate).Skip(page * count).Take(count);

                case OrderBy.alpha:
                    new NotImplementedException();
                    break;

            }
            return null;
        }

        public static IQueryable<Product> SkipAndTake(this IQueryable<Product> query, int page, int count, ProductOrder order)
        {
            switch (order)
            {
                case ProductOrder.date:
                    return query.OrderByDescending(x => x.PublishDate).Skip(page * count).Take(count);

                case ProductOrder.alpha:
                    new NotImplementedException();
                    break;

            }
            return query;
        }


        #region query for Post
        public enum OrderBy
        {
            date,
            alpha
        }
        public static IQueryable<Post> SkipAndTake(this IQueryable<Post> query, int page, int count, OrderBy order)
        {
            switch (order)
            {
                case OrderBy.date:
                    return query.OrderByDescending(x => x.PublishDate).Skip(page * count).Take(count);

                case OrderBy.alpha:
                    new NotImplementedException();
                    break;

            }
            return query;
        }
        #endregion
    }
}
