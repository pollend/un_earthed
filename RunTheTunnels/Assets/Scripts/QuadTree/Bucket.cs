using System;
using Assets.Scripts.QuadTree;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

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
		
			topLeftRectangle = new Rect(rect.xMin, rect.yMin, rect.width / 2.0f, rect.height / 2.0f);
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
				b1.Instantiate (new Vector3(rect.center.x,rect.center.y, 0), rect.width);
				b2.Instantiate (new Vector3(rect.center.x,rect.center.y, 0), rect.width);
				b3.Instantiate (new Vector3(rect.center.x,rect.center.y, 0), rect.width);
				isDivided = true;
			}
			if(bucket_size.width < QuadTree.MAX_BUCKET_SIZE)
			{
				main_bucket.UnInstantiate();
				
				main_bucket = (Bucket)Activator.CreateInstance (bucket_type);
				main_bucket.SetRectangle(bucket_size);
				main_bucket.Instantiate (new Vector3(rect.center.x,rect.center.y, 0), rect.width);
				return true;
			}
			return false;
		}



		public bool Divide(Collider2D collider, Type bucket_type)
		{
			if (_convertFromRect(bottomRightRectangle).Intersects (collider.bounds))
			{
				if(!ProcessBucket(bucket_type,bottomRightRectangle,bottomRight,topLeft,topRight,bottomLeft))
					bottomRight.Divide (collider, bucket_type);
			} 
			if (_convertFromRect(topLeftRectangle).Intersects (collider.bounds)) 
			{
				if(!ProcessBucket(bucket_type,topLeftRectangle,topLeft,topRight,bottomRight,bottomLeft))
					topLeft.Divide (collider, bucket_type);
			} 
			if (_convertFromRect (bottomLeftRectangle).Intersects (collider.bounds)) 
			{
				if(!ProcessBucket(bucket_type,bottomLeftRectangle,bottomLeft,topLeft,topRight,bottomRight))
					bottomLeft.Divide (collider, bucket_type);
			} 
			if (_convertFromRect (topRightRectangle).Intersects (collider.bounds)) 
			{
				if(!ProcessBucket(bucket_type,topRightRectangle,topRight,topLeft,bottomRight,bottomLeft))
					topRight.Divide (collider, bucket_type);
			} 

			return true;
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

		protected virtual float GetDamageFactor(){return 1.0f;}
		protected virtual void Instantiate (Vector3 position, float size){}
		protected virtual void UnInstantiate (){}

    
    }
}
