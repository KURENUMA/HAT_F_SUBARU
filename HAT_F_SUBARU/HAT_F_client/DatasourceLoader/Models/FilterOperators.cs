using System;
using System.Collections.Generic;

namespace Dma.DatasourceLoader.Models
{
    public class FilterOperators : IEquatable<FilterOperators>
    {
        private string _value;
        public string Value => _value;

        public static readonly FilterOperators Eq = new FilterOperators("=");
        public static readonly FilterOperators Between = new FilterOperators("間");
        public static readonly FilterOperators NotEq = new FilterOperators("≠");
        public static readonly FilterOperators Contains = new FilterOperators("含む");
        public static readonly FilterOperators NotContains = new FilterOperators("Not contains");
        public static readonly FilterOperators StartsWith = new FilterOperators("前方一致");
        public static readonly FilterOperators EndsWith = new FilterOperators("後方一致");
        public static readonly FilterOperators In = new FilterOperators("In");
        public static readonly FilterOperators NotIn = new FilterOperators("Not in");
        public static readonly FilterOperators Gt = new FilterOperators(">");
        public static readonly FilterOperators Gte = new FilterOperators(">=");
        public static readonly FilterOperators Lt = new FilterOperators("<");
        public static readonly FilterOperators Lte = new FilterOperators("<=");
        public static readonly FilterOperators Null = new FilterOperators("Null");
        public static readonly FilterOperators NotNull = new FilterOperators("Not null");

        public static readonly FilterOperators[] ComplexFilters = new FilterOperators[]
        {
            In, NotIn
        };

        public static List<FilterOperators> GetDivFilters()
        {
            return new List<FilterOperators>
            {
                Eq,
                NotEq,
            };
        }

        public static List<FilterOperators> GetNumericFilters()
        {
            return new List<FilterOperators>
            {
                Eq,
                NotEq,
                Gt,
                Gte,
                Lt,
                Lte
            };
        }

        public static List<FilterOperators> GetStringFilters()
        {
            return new List<FilterOperators>
            {
                Contains, StartsWith, EndsWith
            };
        }

        public static List<FilterOperators> GetDateFilters()
        {
            return new List<FilterOperators>
            {
                Between,
                Eq,
                NotEq,
                Gt,
                Gte,
                Lt,
                Lte
            };
        }

        private FilterOperators(string value)
        {
            _value = value;
        }

        public static implicit operator string(FilterOperators op) => op.Value;
        //public static implicit operator FilterOperators(string op) => new(op);

        public bool Equals(FilterOperators other)
        {
            return other?.Value == _value;
        }
    }
}
