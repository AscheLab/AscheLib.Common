using System;

namespace AscheLib {
	public class Lazy<T> {
		private readonly object thisLock = new object();

		private Func<T> _func;
		private T _result;
		private bool _isValueCreated = false;
		public Lazy(Func<T> func) {
			_func = func;
		}
		public T Value {
			get {
				lock(thisLock) {
					if(!_isValueCreated) {
						_isValueCreated = true;
						_result = _func();
					}
				}
				return _result;
			}
		}
		public bool IsValueCreated { get { return _isValueCreated; } }
		public override string ToString() {
			return Value.ToString();
		}
	}

	public static class Lazy {
		public static Lazy<T> Create<T>(Func<T> func) {
			return new Lazy<T>(func);
		}
		public static Lazy<TResult> Select<T, TResult>(this Lazy<T> source, Func<T, TResult> selector) {
			return Create(() => selector(source.Value));
		}
		public static Lazy<TResult> SelectMany<T, TResult>(this Lazy<T> source, Func<T, Lazy<TResult>> selector) {
			return Create(() => selector(source.Value).Value);
		}
		public static Lazy<TResult> SelectMany<TFirst, TSecond, TResult>(this Lazy<TFirst> source, Func<TFirst, Lazy<TSecond>> selector, Func<TFirst, TSecond, TResult> projector) {
			return Create(() => projector(source.Value, selector(source.Value).Value));
		}
	}
}