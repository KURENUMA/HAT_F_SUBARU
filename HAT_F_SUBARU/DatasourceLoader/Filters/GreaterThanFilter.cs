using Dma.DatasourceLoader.Helpers;
using System;
using System.Linq.Expressions;

namespace Dma.DatasourceLoader.Filters
{
    public class GreaterThanFilter<T> : FilterBase<T>
    {
        private readonly object value;

        public GreaterThanFilter(string propertyName, object value) : base(propertyName)
        {
            this.value = value;
        }

        public override LambdaExpression GetFilterExpression()
        {
            ParameterExpression parameter = Expression.Parameter(typeof(T));
            MemberExpression property = Expression.Property(parameter, propertyName);
            ConstantExpression constant = Expression.Constant(value);
            Expression propertyValue = Expressions.CastNonNullable(property);
            BinaryExpression greaterThanExpression = Expression.GreaterThan(propertyValue, constant);

            return Expression.Lambda<Func<T, bool>>(greaterThanExpression, parameter);
        }
    }

}
