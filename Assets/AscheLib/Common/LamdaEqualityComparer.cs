using System;
using System.Collections.Generic;

namespace AscheLib {
	public class LamdaEqualityComparer<T, TKey> : IEqualityComparer<T> {
		private Func<T, TKey> _selector;
		public LamdaEqualityComparer(Func<T, TKey> selector) {
			_selector = selector;
		}
		public bool Equals(T x, T y) {
			return _selector(x).Equals(_selector(y));
		}
		public int GetHashCode(T obj) {
			return _selector(obj).GetHashCode();
		}
	}
}