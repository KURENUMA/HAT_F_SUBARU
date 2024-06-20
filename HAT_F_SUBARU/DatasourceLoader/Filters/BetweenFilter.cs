﻿using System;
using System.Linq.Expressions;

namespace Dma.DatasourceLoader.Filters
{
    public class BetweenFilter<T> : FilterBase<T>
    {
        private readonly object minValue;
        private readonly object maxValue;

        public BetweenFilter(string propertyName, object value) : base(propertyName)
        {
            (DateTime, DateTime) tuple = ((DateTime, DateTime))value;
            this.minValue = tuple.Item1;
            this.maxValue = tuple.Item2;
        }

        public override LambdaExpression GetFilterExpression()
        {
            // Create a GreaterThanOrEqual filter for the minimum value
            ParameterExpression parameter = Expression.Parameter(typeof(T));

            GreaterThanOrEqualFilter<T> greaterThanOrEqualFilter = new GreaterThanOrEqualFilter<T>(propertyName, minValue);
            LambdaExpression greaterThanOrEqualExpression = greaterThanOrEqualFilter.GetFilterExpression();
            var invokeExpression = Expression.Invoke(greaterThanOrEqualExpression, parameter); // Invoke the inner filter expression with innerProperty as the argument

            // Create a LessThanOrEqual filter for the maximum value
            LessThanOrEqualFilter<T> lessThanOrEqualFilter = new LessThanOrEqualFilter<T>(propertyName, maxValue);
            LambdaExpression lessThanOrEqualExpression = lessThanOrEqualFilter.GetFilterExpression();
            var invokeExpression2 = Expression.Invoke(lessThanOrEqualExpression, parameter); // Invoke the inner filter expression with innerProperty as the argument

            // Combine the two expressions with AndAlso
            BinaryExpression andExpression = Expression.AndAlso(
                invokeExpression,
                invokeExpression2
            );

            return Expression.Lambda<Func<T, bool>>(andExpression, parameter);
        }
    }

}
