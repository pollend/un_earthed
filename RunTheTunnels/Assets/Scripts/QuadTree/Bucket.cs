using System;
using Assets.Scripts.QuadTree;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using AssemblyCSharp;
namespace Assets.Scripts.QuadTree
{
    public class Bucket 
    {
        private Bucket topLeft = null;
        private Bucket topRight = null;
        private Bucket bottomLeft = null;
        private Bucket bottomRight = null;

        private Rect rect;

        private Rect topLeftRectangle;
		private Rect topRightRectangle;
		private Rect bottomLeftRectangle;
		private Rect bottomRightRectangle;

		private bool isInstaniated = false;
		private bool isDivided = false;

		private float health = 1.0f;

        public Bucket()
        {
          
        }


		private Bounds _convertFromRect(Rect r)
		{
			return new Bounds (new Vector3 (r.center.x, r.center.y, -1), new Vector3 (r.width*1.3f, r.height*1.3f,2));
		}


        public void SetRectangle(Rect rectangle)
        {
            rect = new Rect(rectangle);
		
			topLeftRectangle = new Rect(rect.xMin, rect.yMin, (rect.width / 2.0f), rect.height / 2.0f);
			topRightRectangle = new Rect(rect.center.x, rect.yMin, rect.width / 2.0f, rect.height / 2.0f);
			bottomRightRectangle = new Rect(rect.xMin, rect.center.y, rect.width / 2.0f, rect.height / 2.0f);
			bottomLeftRectangle = new Rect(rect.center.x, rect.center.y, rect.width / 2.0f, rect.height / 2.0f);
       
        }
        

        public void Clean()
        {
           /*   Type ltype = _top_left.GetType();
              if (ltype == _top_right.GetType() && ltype == _bottom_right.GetType() && ltype == _bottom_left.GetType())
              {
                    _top_left = null;
                    _top_right= null;
                    _bottom_left= null;
                    _bottom_right = null;
                   	_instantiate(new Vector3(_rect.xMin,_rect.yMin,0),_rect.width);
					_is_divided = false;  
			}*/

        }

		private void InstantiageBuckets()
		{
			UnInstantiateBucket();
			if (((object)topLeft) == null) 
			{
				topLeft = (Bucket)Activator.CreateInstance (GetType());
				topLeft.SetRectangle(topLeftRectangle);
				topLeft.InstantiateBucket();	
			}
			if (((object)topRight) == null) {
				topRight = (Bucket)Activator.CreateInstance (GetType());
				topRight.SetRectangle(topRightRectangle);
				topRight.InstantiateBucket();
			}
			if (((object)bottomRight) == null) {
				bottomRight = (Bucket)Activator.CreateInstance (GetType());
				bottomRight.SetRectangle(bottomRightRectangle);
				bottomRight.InstantiateBucket();
			}
			if (((object)bottomLeft) == null) {
				bottomLeft = (Bucket)Activator.CreateInstance (GetType());
				bottomLeft.SetRectangle(bottomLeftRectangle);
				bottomLeft.InstantiateBucket();
			}
		}

		private bool ProcessBucket(Type bucket_type,Rect bucket_size, Bucket main_bucket, Bucket b1,Bucket b2,Bucket b3)
		{
			if (!isDivided) 
			{
				b1.InstantiateBucket ();
				b2.InstantiateBucket ();
				b3.InstantiateBucket ();
				isDivided = true;
			}
			if(bucket_size.width < QuadTree.MAX_BUCKET_SIZE)
			{
				health -= GetDamageFactor ();
				if(health <= 0)
				{
					health = 0;

					main_bucket.UnInstantiate();
					
					main_bucket = (Bucket)Activator.CreateInstance (bucket_type);
					main_bucket.SetRectangle(bucket_size);
					main_bucket.Instantiate (new Vector3(rect.center.x,rect.center.y, 0), rect.width);

				}
				return true;
			}
			return false;
		}



		public void Divide(Collider2D collider, Type bucket_type)
		{
			InstantiageBuckets ();
			Rect target = Intersection.ConvertBoundedsToRect (collider.bounds);
			if (Intersection.RectIntersection(bottomRightRectangle,target,QuadTree.INFLATE_FACTOR))
			{
				if(!ProcessBucket(bucket_type,bottomRightRectangle,bottomRight,topLeft,topRight,bottomLeft))
					bottomRight.Divide (collider, bucket_type);
			} 
			if (Intersection.RectIntersection(topLeftRectangle,target,QuadTree.INFLATE_FACTOR)) 
			{
				if(!ProcessBucket(bucket_type,topLeftRectangle,topLeft,topRight,bottomRight,bottomLeft))
					topLeft.Divide (collider, bucket_type);
			} 
			if (Intersection.RectIntersection(bottomLeftRectangle,target,QuadTree.INFLATE_FACTOR)) 
			{
				if(!ProcessBucket(bucket_type,bottomLeftRectangle,bottomLeft,topLeft,topRight,bottomRight))
					bottomLeft.Divide (collider, bucket_type);
			} 
			if (Intersection.RectIntersection(topRightRectangle,target,QuadTree.INFLATE_FACTOR)) 
			{
				if(!ProcessBucket(bucket_type,topRightRectangle,topRight,topLeft,bottomRight,bottomLeft))
					topRight.Divide (collider, bucket_type);
			} 

        }

		public void Divide(Rect target, Type bucket_type)
		{
			InstantiageBuckets ();
			if (Intersection.RectIntersection(bottomRightRectangle,target,QuadTree.INFLATE_FACTOR))
			{
				if(!ProcessBucket(bucket_type,bottomRightRectangle,bottomRight,topLeft,topRight,bottomLeft))
					bottomRight.Divide (target, bucket_type);
			} 
			if (Intersection.RectIntersection(topLeftRectangle,target,QuadTree.INFLATE_FACTOR)) 
			{
				if(!ProcessBucket(bucket_type,topLeftRectangle,topLeft,topRight,bottomRight,bottomLeft))
					topLeft.Divide (target, bucket_type);
			} 
			if (Intersection.RectIntersection(bottomLeftRectangle,target,QuadTree.INFLATE_FACTOR)) 
			{
				if(!ProcessBucket(bucket_type,bottomLeftRectangle,bottomLeft,topLeft,topRight,bottomRight))
					bottomLeft.Divide (target, bucket_type);
			} 
			if (Intersection.RectIntersection(topRightRectangle,target,QuadTree.INFLATE_FACTOR)) 
			{
				if(!ProcessBucket(bucket_type,topRightRectangle,topRight,topLeft,bottomRight,bottomLeft))
					topRight.Divide (target, bucket_type);
			} 
			
		}

		public void Divide(Vector2 center, float radius, Type bucket_type)
		{
			InstantiageBuckets ();
			if (Intersection.CircleBoundIntersection(bottomRightRectangle,center,radius))
			{
				if(!ProcessBucket(bucket_type,bottomRightRectangle,bottomRight,topLeft,topRight,bottomLeft))
					bottomRight.Divide (center,radius, bucket_type);
			} 
			if (Intersection.CircleBoundIntersection(topLeftRectangle,center,radius)) 
			{
				if(!ProcessBucket(bucket_type,topLeftRectangle,topLeft,topRight,bottomRight,bottomLeft))
					topLeft.Divide (center,radius, bucket_type);
			} 
			if (Intersection.CircleBoundIntersection(bottomLeftRectangle,center,radius)) 
			{
				if(!ProcessBucket(bucket_type,bottomLeftRectangle,bottomLeft,topLeft,topRight,bottomRight))
					bottomLeft.Divide (center,radius, bucket_type);
			} 
			if (Intersection.CircleBoundIntersection(topRightRectangle,center,radius)) 
			{
				if(!ProcessBucket(bucket_type,topRightRectangle,topRight,topLeft,bottomRight,bottomLeft))
					topRight.Divide (center,radius, bucket_type);
			} 
			
		}

	

		public void Update()
		{
			health += .01f;
			if (health >= 1.0f)
				health = 1;

		
			if (((object)bottomLeft) != null)
				bottomLeft.Update ();
			if (((object)topLeft) != null)
				bottomLeft.Update ();
			if (((object)topRight) != null)
				bottomLeft.Update ();
			if (((object)bottomRight) != null)
				bottomLeft.Update ();

		}

		public void InstantiateBucket()
		{
			if(!isInstaniated)
			Instantiate (new Vector3 (rect.center.x, rect.center.y, 0), rect.width);
			isInstaniated = true;
		}

		public void UnInstantiateBucket()
		{
			if(isInstaniated)
			UnInstantiate ();
			isInstaniated = false;
		}

		public float GetHealth (){return health;}
		protected virtual float GetDamageFactor(){return 0.3f;}
		protected virtual void Instantiate (Vector3 position, float size){}
		protected virtual void UnInstantiate (){}

    
    }
}
