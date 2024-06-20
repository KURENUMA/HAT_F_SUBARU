﻿using System;
using System.Linq.Expressions;

namespace Dma.DatasourceLoader.Filters
{
    public class IsNullFilter<T> : FilterBase<T>
    {

        public IsNullFilter(string propertyName) : base(propertyName)
        {
        }

        public override LambdaExpression GetFilterExpression()
        {
            ParameterExpression parameter = Expression.Parameter(typeof(T));
            MemberExpression property = Expression.Property(parameter, propertyName);
            ConstantExpression nullValue = Expression.Constant(null, typeof(object));
            BinaryExpression isNullExpression = Expression.Equal(property, nullValue);

            return Expression.Lambda<Func<T, bool>>(isNullExpression, parameter);
        }
    }
}
