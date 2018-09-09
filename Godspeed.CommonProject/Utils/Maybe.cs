using System.Collections.Generic;
using System.Linq;
using System.Collections;

namespace Godspeed
{
    public interface Maybe<T> : IEnumerable<T> { }

    public static class MaybeExtensions
    {
        public static Maybe<T> ToMaybe<T>(this T value)
        {
            return new One<T>(value);
        }

        public static Maybe<T> ToMaybe<T>(this List<T> value)
        {
            return new Many<T>(value);
        }

        public static Maybe<T> ToMaybe<T>(this T[] value)
        {
            return new Many<T>(value);
        }
    }

    public class One<T> : Maybe<T>
    {
        private T Value;

        public One(T Value)
        {
            this.Value = Value;
        }

        public IEnumerator<T> GetEnumerator()
        {
            if (this)
                yield return Value;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            if (this)
                yield return Value;
        }

        public static bool operator true(One<T> maybe)
        {
            return maybe != null
                && maybe.Value != null;
        }

        public static bool operator false(One<T> maybe)
        {
            return maybe == null
                || maybe.Value == null;
        }

        public override string ToString()
        {
            return $"{typeof(T).FullName} [{this.Count()}]";
        }
    }

    public class Many<T> : Maybe<T>
    {
        private IEnumerable<T> Value;

        public Many(IEnumerable<T> Value)
        {
            this.Value = Value;
        }

        public IEnumerator<T> GetEnumerator()
        {
            if (this)
                return Value.GetEnumerator();
            else
                return Enumerable.Empty<T>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            if (this)
                return Value.GetEnumerator();
            else
                return Enumerable.Empty<T>().GetEnumerator();
        }

        public static bool operator true(Many<T> maybe)
        {
            return maybe != null
                && maybe.Value != null
                && maybe.Value.Count() > 0;
        }

        public static bool operator false(Many<T> maybe)
        {
            return maybe == null
                || maybe.Value == null
                || maybe.Value.Count() == 0;
        }

        public override string ToString()
        {
            return $"{typeof(T).FullName} [{this.Count()}]";
        }
    }
}
