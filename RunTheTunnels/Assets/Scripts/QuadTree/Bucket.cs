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
        private Bucket _top_left = null;
        private Bucket _top_right = null;
        private Bucket _bottom_left = null;
        private Bucket _bottom_right = null;

        private Rect _rect;

        private Rect _top_left_rectangle;
		private Rect _top_right_rectangle;
		private Rect _bottom_left_rectangle;
		private Rect _bottom_right_rectangle;

		private bool _is_instaniated = false;
		private bool _is_divided = false;

        public Bucket()
        {
          
        }



		private Bounds _convertFromRect(Rect r)
		{
			return new Bounds (new Vector3 (r.center.x, r.center.y, -1), new Vector3 (r.width+.5f, r.height+.5f,2));
		}

		public void Debug()
		{
		
		}
	

        public void SetRectangle(Rect rectangle)
        {
            _rect = new Rect(rectangle);
		
			_top_left_rectangle = new Rect(_rect.xMin, _rect.yMin, _rect.width / 2.0f, _rect.height / 2.0f);
			_top_right_rectangle = new Rect(_rect.center.x, _rect.yMin, _rect.width / 2.0f, _rect.height / 2.0f);
			_bottom_right_rectangle = new Rect(_rect.xMin, _rect.center.y, _rect.width / 2.0f, _rect.height / 2.0f);
			_bottom_left_rectangle = new Rect(_rect.center.x, _rect.center.y, _rect.width / 2.0f, _rect.height / 2.0f);
       
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


		public bool Divide(Collider2D collider, Type bucket_type)
		{
			if (QuadTree.MAX_BUCKET_SIZE < _rect.width) 
			{
				if (_convertFromRect (_rect).Intersects (collider.bounds)) 
				{
					if(_is_instaniated)
					_unInstantiate();

					if (((object)_top_left) == null) 
					{
						_top_left = (Bucket)Activator.CreateInstance (GetType());
						_top_left.SetRectangle(_top_left_rectangle);
						_top_left.Instantiate ();	
					}
					if (((object)_top_right) == null) {
						_top_right = (Bucket)Activator.CreateInstance (GetType());
						_top_right.SetRectangle(_top_right_rectangle);
						_top_right.Instantiate ();
					}
					if (((object)_bottom_right) == null) {
						_bottom_right = (Bucket)Activator.CreateInstance (GetType());
						_bottom_right.SetRectangle(_bottom_right_rectangle);
						_bottom_right.Instantiate ();
					}
					if (((object)_bottom_left) == null) {
						_bottom_left = (Bucket)Activator.CreateInstance (GetType());
						_bottom_left.SetRectangle(_bottom_left_rectangle);
						_bottom_left.Instantiate ();
					}

					if (_convertFromRect(_top_left_rectangle).Intersects (collider.bounds)) 
					{
						if (!_is_divided) 
						{

							_top_right.Instantiate ();
							_bottom_right.Instantiate ();
							_bottom_left.Instantiate ();

						}
						if(_top_left_rectangle.width < QuadTree.MAX_BUCKET_SIZE)
						{
							_top_left._unInstantiate();
							
							_top_left = (Bucket)Activator.CreateInstance (bucket_type);
							_top_left.SetRectangle(_top_right_rectangle);
							_top_left.Instantiate ();
						}
						else
						{
							_top_left.Divide (collider, bucket_type);
						}

					} 
					if (_convertFromRect (_top_right_rectangle).Intersects (collider.bounds)) 
					{
						if (!_is_divided) 
						{
							
							_top_left.Instantiate ();
							_bottom_right.Instantiate ();
							_bottom_left.Instantiate ();
						}

						if(_top_right_rectangle.width < QuadTree.MAX_BUCKET_SIZE)
						{
							_top_right._unInstantiate();
							
							_top_right = (Bucket)Activator.CreateInstance (bucket_type);
							_top_right.SetRectangle(_top_right_rectangle);
							_top_right.Instantiate ();
						}
						else
						{
							_top_right.Divide (collider, bucket_type);
						}

					} 
					if (_convertFromRect (_bottom_left_rectangle).Intersects (collider.bounds)) 
					{
						if (!_is_divided) 
						{
							
							_top_left.Instantiate ();
							_top_right.Instantiate ();
							_bottom_right.Instantiate ();
							
						}
						if(_bottom_right_rectangle.width < QuadTree.MAX_BUCKET_SIZE)
						{
							_bottom_right._unInstantiate();
							
							_bottom_right = (Bucket)Activator.CreateInstance (bucket_type);
							_bottom_right.SetRectangle(_bottom_right_rectangle);
							_bottom_right.Instantiate ();
						}
						else
						{
							_bottom_left.Divide (collider, bucket_type);
						}


					} 
					 if (_convertFromRect (_bottom_right_rectangle).Intersects (collider.bounds))
					{
						if (!_is_divided) 
						{
							_top_left.Instantiate ();
							_top_right.Instantiate ();
							_bottom_left.Instantiate ();
							
						}
						if(_bottom_left_rectangle.width < QuadTree.MAX_BUCKET_SIZE)
						{
							_bottom_left._unInstantiate();
							
							_bottom_left = (Bucket)Activator.CreateInstance (bucket_type);
							_bottom_left.SetRectangle(_bottom_left_rectangle);
							_bottom_left.Instantiate ();
						}
						else
						{
			
							_bottom_right.Divide (collider, bucket_type);
						}


					} 
			

					if (((object)_top_left) != null)_top_left.Clean ();
					if (((object)_top_right) != null)_top_right.Clean ();
					if (((object)_bottom_right) != null)_bottom_right.Clean ();
					 if (((object)_bottom_left) != null)_bottom_left.Clean ();
					_is_divided = true;
				}
			} 
		
			return true;
        }

		public void Instantiate()
		{
			if(!_is_instaniated)
				_instantiate(new Vector3(_rect.center.x,_rect.center.y, 0), _rect.width);
		}

        protected virtual void _instantiate(Vector3 position, float size)
        {
			_is_instaniated = true;
        }

        protected virtual void _unInstantiate()
        {
			_is_instaniated = false;
        }

        public bool Intersect(Rect rectangle)
        {

           return rectangle.Overlaps(_rect);
        }
    
    }
}
