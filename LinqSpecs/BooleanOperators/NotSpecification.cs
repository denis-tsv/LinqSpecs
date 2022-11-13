﻿using System;
using System.Linq.Expressions;

namespace LinqSpecs
{
    /// <summary>
    /// Negates a source specification.
    /// </summary>
    internal class NotSpecification<T> : Specification<T>
    {
        private readonly Specification<T> _spec;

        public NotSpecification(Specification<T> spec)
        {
            _spec = spec ?? throw new ArgumentNullException(nameof(spec));
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            var expr = _spec.ToExpression();
            return Expression.Lambda<Func<T, bool>>(Expression.Not(expr.Body), expr.Parameters);
        }

        public override bool Equals(object other)
        {
            if (other is null)
                return false;
            if (ReferenceEquals(this, other))
                return true;
            if (other is NotSpecification<T> otherSpec)
                return _spec.Equals(otherSpec._spec);
            return false;
        }

        public override int GetHashCode()
        {
            return _spec.GetHashCode() ^ GetType().GetHashCode();
        }
    }
}
