using Dma.DatasourceLoader.Helpers;
using System;
using System.Linq.Expressions;

namespace Dma.DatasourceLoader.Filters
{
    public class EqualFilter<T> : FilterBase<T>
    {
        private readonly object _value;
        public EqualFilter(string propertyName, object value) : base(propertyName)
        {
            _value = value;
        }

        public override LambdaExpression GetFilterExpression()
        {
            ParameterExpression parameter = Expression.Parameter(typeof(T));
            MemberExpression property = Expression.Property(parameter, propertyName);
            ConstantExpression constant = Expression.Constant(_value);
            Expression propertyValue = Expressions.CastNonNullable(property);

            BinaryExpression equalExpression = Expression.Equal(propertyValue, constant);

            return Expression.Lambda<Func<T, bool>>(equalExpression, parameter);
        }
    }
}
