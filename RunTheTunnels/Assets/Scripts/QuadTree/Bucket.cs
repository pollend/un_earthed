using System;
using Assets.Scripts.QuadTree;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.QuadTree
{
    class Bucket : MonoBehaviour
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

        public Bucket()
        {
            
        }

        public void SetRectangle(Rect rectangle)
        {
            _rect = new Rect(rectangle);

            _top_left_rectangle = new Rect(_rect.xMin, _rect.yMin, _rect.width / 2.0f, _rect.height / 2.0f);
            _top_right_rectangle = new Rect(_rect.center.x, _rect.yMin, _rect.width / 2.0f, _rect.height / 2.0f);
            _bottom_left_rectangle = new Rect(_rect.xMin, _rect.center.y, _rect.width / 2.0f, _rect.height / 2.0f);
            _bottom_right_rectangle = new Rect(_rect.center.x, _rect.center.y, _rect.width / 2.0f, _rect.height / 2.0f);
       
        }
        

        public void Clean()
        {
              Type ltype = _top_left.GetType();
              if (ltype == _top_right.GetType() && ltype == _bottom_right.GetType() && ltype == _bottom_left.GetType())
              {
                    _top_left = null;
                    _top_right= null;
                    _bottom_left= null;
                    _bottom_right = null;
                   _instantiate(new Vector3(_rect.xMin,_rect.yMin,0),_rect.width);
              }
        }

        public bool Divide(Rect rectangle, Type bucket)
        {
     
            if (_top_left != null) _top_left.Clean();
            if (_top_right != null) _top_right.Clean();
            if (_bottom_right != null) _bottom_right.Clean();
            if (_bottom_left != null) _bottom_left.Clean();

            if (QuadTree.MAX_BUCKET_SIZE > _rect.width)
            {
             
                if (_top_left_rectangle.Overlaps(rectangle))
                {
                    if (_top_left == null)
                    {
                        
                        _top_left =(Bucket)Activator.CreateInstance(bucket);
                        _top_left.SetRectangle(_top_left_rectangle);
                    }
                    else
                    {
                        _unInstantiate();
                        _top_left.Divide(rectangle, bucket);
                    }

                }
                else if (_top_right_rectangle.Overlaps(rectangle))
                {
                    if (_top_right == null)
                    {
                        _top_right = (Bucket)Activator.CreateInstance(bucket);
                        _top_right.SetRectangle(_top_left_rectangle);
                    }
                    else
                    {
                        _unInstantiate();
                        _top_right.Divide(rectangle, bucket);
                    }

                }
                else if (_bottom_left_rectangle.Overlaps(rectangle))
                {
                    if (_bottom_left == null)
                    {
                        _bottom_left = (Bucket)Activator.CreateInstance(bucket);
                        _bottom_left.SetRectangle(_top_left_rectangle);
                    }
                    else
                    {
                        _unInstantiate();
                        _bottom_left.Divide(rectangle, bucket);
                    }

                }
                else if (_bottom_right_rectangle.Overlaps(rectangle))
                {
                    if (_bottom_right == null)
                    {
                        _bottom_right = (Bucket)Activator.CreateInstance(bucket);
                        _bottom_right.SetRectangle(_top_left_rectangle);
                    }
                    else
                    {
                        _unInstantiate();
                        _bottom_right.Divide(rectangle, bucket);
                    }

                }
                else
                {
                    _instantiate(new Vector3(_rect.xMin,_rect.yMin,0),_rect.width);
                }
            }
            else
            {
                _instantiate(new Vector3(_rect.xMin, _rect.yMin, 0), _rect.width);
            }

            return true;
        }

		public void Instantiate()
		{
			_instantiate(new Vector3(_rect.xMin, _rect.yMin, 0), _rect.width);
		}

        protected virtual void _instantiate(Vector3 position, float size)
        {

        }

        protected virtual void _unInstantiate()
        {

        }

        public bool Intersect(Rect rectangle)
        {
           return rectangle.Overlaps(_rect);
        }
    
    }
}
