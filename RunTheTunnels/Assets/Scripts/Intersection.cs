using UnityEngine;
using System;

namespace AssemblyCSharp
{
	public class Intersection
	{


		public static bool CircleBoundIntersection(Rect b, Vector2 center, float radius)
		{
			if(b.Contains(center)||
			   PointInsideCircle(new Vector2(b.xMin,b.yMin),center,radius)||
			   PointInsideCircle(new Vector2(b.xMax,b.yMin),center,radius)||
			   PointInsideCircle(new Vector2(b.xMax,b.yMax),center,radius)||
			   PointInsideCircle(new Vector2(b.xMin,b.yMax),center,radius)||
			   Intersection.LineCircleIntersection(new Vector2(b.xMin,b.yMin),new Vector2(b.xMax,b.yMin),radius,center)||
			   Intersection.LineCircleIntersection(new Vector2(b.xMin,b.yMax),new Vector2(b.xMax,b.yMax),radius,center)||
			   Intersection.LineCircleIntersection(new Vector2(b.xMin,b.yMin),new Vector2(b.xMin,b.yMax),radius,center)||
			   Intersection.LineCircleIntersection(new Vector2(b.xMax,b.yMin),new Vector2(b.xMax,b.yMax),radius,center)
			   )
			return true;

			return false;
		}

		public static bool PointInsideCircle(Vector2 point, Vector2 center, float radius)
		{
			return Vector2.Distance (point, center) < radius;
		}


		public static bool RectIntersection(Rect a, Rect b,float factor)
		{
			float inflate = factor * a.size.x;
			a.size = new Vector2 (a.size.x + (inflate * 2), a.size.y + (inflate * 2));
			a.position = new Vector2 (a.position.x - inflate, a.position.y - inflate);
			return a.Overlaps (b);
		}

		public static Rect ConvertBoundedsToRect(Bounds b)
		{
			return new Rect (b.center.x - (b.size.x / 2.0f), b.center.y - (b.size.x / 2.0f), b.size.x, b.size.y);
		}

		public static bool LineCircleIntersection(Vector2 p1, Vector2 p2,float radius, Vector2 circleCenter)
		{
			Vector2 d = p2 - p1;
			Vector2 f = p1 - circleCenter;
			
			float a = Vector2.Dot(d,d);
			float b = 2 * Vector2.Dot(f,d);
			float c = Vector2.Dot(f,f) - radius * radius;
			
			float discriminant = b*b-4*a*c;
			if( discriminant >= 0 )
			{
				
				discriminant = Mathf.Sqrt( discriminant );

				float t1 = (-b - discriminant)/(2*a);
				float t2 = (-b + discriminant)/(2*a);

				
				if( t1 >= 0 && t1 <= 1 )
				{
	
					return true ;
				}
				

				if( t2 >= 0 && t2 <= 1 )
				{
					return true ;
				}

				return false ;
			}
			
			return false;
		}
	}
}

