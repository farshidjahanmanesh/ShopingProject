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
            dateDes,dateAs, alpha, priceDes,priceAs
        }

        public static IQueryable<Product> SkipAndTake(this IQueryable<ProductKeyword> query, int page, int count, ProductOrder order)
        {
            switch (order)
            {
                case ProductOrder.dateDes:
                    return query.Include(x => x.Product).Select(x => x.Product).OrderByDescending(x => x.PublishDate).Skip(page * count).Take(count);

                case ProductOrder.alpha:
                    new NotImplementedException();
                    break;

                case ProductOrder.dateAs:
                    return query.Include(x => x.Product).Select(x => x.Product).OrderBy(x => x.PublishDate).Skip(page * count).Take(count);

                case ProductOrder.priceAs:
                    return query.Include(x => x.Product).Select(x => x.Product).OrderBy(x => x.Price).Skip(page * count).Take(count);

                case ProductOrder.priceDes:
                    return query.Include(x => x.Product).Select(x => x.Product).OrderByDescending(x => x.Price).Skip(page * count).Take(count);

            }
            return null;
        }

        public static IQueryable<Product> SkipAndTake(this IQueryable<Product> query, int page, int count, ProductOrder order)
        {
            switch (order)
            {
                case ProductOrder.dateDes:
                    return query.OrderByDescending(x => x.PublishDate).Skip(page * count).Take(count);

                case ProductOrder.dateAs:
                    return query.OrderBy(x => x.PublishDate).Skip(page * count).Take(count);

                case ProductOrder.alpha:
                    new NotImplementedException();
                    break;

                case ProductOrder.priceDes:
                    return query.OrderByDescending(x=>x.Price).Skip(page * count).Take(count);

                case ProductOrder.priceAs:
                    return query.OrderBy(x => x.Price).Skip(page * count).Take(count);
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
