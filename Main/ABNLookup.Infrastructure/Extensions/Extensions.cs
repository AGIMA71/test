using System;
using System.Linq;
using System.Linq.Expressions;
using ABNLookup.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ABNLookup.Infrastructure.Extensions
{
    public static class Extensions
    {
        public static IQueryable<Abn> LikeAny(this IQueryable<Abn> products, string property, params string[] words)
        {
            var parameter = Expression.Parameter(typeof(Abn));

            words = words.Select(w => $"%{w}%").ToArray();

            var body = words.Select(word => Expression.Call(typeof(DbFunctionsExtensions).GetMethod(nameof(DbFunctionsExtensions.Like),
                        new[]
                        {
                            typeof(DbFunctions), typeof(string), typeof(string)
                        }),
                    Expression.Constant(EF.Functions),
                    Expression.Property(parameter, typeof(Abn).GetProperty(property)),
                    Expression.Constant(word)))
                .Aggregate<MethodCallExpression, Expression>(null, (current, call) => current != null ? Expression.OrElse(current, call) : (Expression)call);

            return products.Where(Expression.Lambda<Func<Abn, bool>>(body, parameter));
        }
    }
}