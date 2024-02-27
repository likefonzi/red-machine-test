using System.Collections.Generic;
using UnityEngine;

public static class RectUtils
{
	public static Rect GetBoundingRect(IEnumerable<Vector2> points)
	{
		float xMin = float.MaxValue;
		float yMin = float.MaxValue;
		float xMax = float.MinValue;
		float yMax = float.MinValue;
		foreach(var point in points)
		{
			xMin = Mathf.Min(xMin, point.x);
			yMin = Mathf.Min(yMin, point.y);
			xMax = Mathf.Max(xMax, point.x);
			yMax = Mathf.Max(yMax, point.y);
		}
		return new Rect(xMin, yMin, xMax - xMin, yMax - yMin);
	}
}
