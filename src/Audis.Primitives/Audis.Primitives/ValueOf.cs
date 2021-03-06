﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Audis.Primitives
{
    /// <summary>
    /// Base class for wrapping primitive types.
    /// Used to prevent primitive obsession, see https://refactoring.guru/smells/primitive-obsession.
    /// </summary>
    /// <typeparam name="TValue">The primitive type to wrap.</typeparam>
    /// <typeparam name="TThis">The wrapping type itself.</typeparam>
    public class ValueOf<TValue, TThis>
        where TThis : ValueOf<TValue, TThis>, new()
    {
        private static readonly Func<TThis> Factory;

        protected virtual void Validate()
        {
        }

        protected virtual TValue Create(TValue value)
        {
            return value;
        }

        static ValueOf()
        {
            var ctor = typeof(TThis).GetTypeInfo().DeclaredConstructors.First();
            Expression[] argsExp = new Expression[0];
            NewExpression newExp = Expression.New(ctor, argsExp);
            LambdaExpression lambda = Expression.Lambda(typeof(Func<TThis>), newExp);
            Factory = (Func<TThis>)lambda.Compile();
        }

        public TValue Value { get; protected set; }

        public static TThis From(TValue item)
        {
            var x = Factory();
            x.Value = x.Create(item);
            x.Validate();
            return x;
        }

        protected virtual bool Equals(ValueOf<TValue, TThis> other)
        {
            return EqualityComparer<TValue>.Default.Equals(this.Value, other.Value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return this.Equals((ValueOf<TValue, TThis>)obj);
        }

        public override int GetHashCode()
        {
            return EqualityComparer<TValue>.Default.GetHashCode(this.Value);
        }

        public static bool operator ==(ValueOf<TValue, TThis> a, ValueOf<TValue, TThis> b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
            {
                return true;
            }

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
            {
                return false;
            }

            return a.Equals(b);
        }

        public static bool operator !=(ValueOf<TValue, TThis> a, ValueOf<TValue, TThis> b)
        {
            return !(a == b);
        }

        public override string ToString()
        {
            return this.Value.ToString();
        }
    }
}
