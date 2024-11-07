using UnityEngine;

namespace UExtra.Color
{
	public static class ExtraColor
	{
		public static UnityEngine.Color RandomRGB() => new(Random.Range(0, 256), Random.Range(0, 256), Random.Range(0, 256), 1);

		public static UnityEngine.Color RandomRGBA() => new(Random.Range(0, 256), Random.Range(0, 256), Random.Range(0, 256), Random.Range(0f, 1f));
	}
}
