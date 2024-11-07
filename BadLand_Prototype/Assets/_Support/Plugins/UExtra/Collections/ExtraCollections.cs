using System.Collections.Generic;
using System.Linq;

namespace UExtra.Collections
{
	public static class ExtraCollections<T>
	{
		public static T Random(IReadOnlyCollection<T> collection) => collection.ElementAt(UnityEngine.Random.Range(0, collection.Count));
	}
}