using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.QuadTree
{
    class QuadTree : MonoBehaviour
    {
        public const float MAX_BUCKET_SIZE = .25f;
        
        private Bucket _base_bucket;

       

        public void Start() 
        {
            _base_bucket = (Bucket)Activator.CreateInstance(new Basic().GetType());
            _base_bucket.SetRectangle(new Rect(transform.position.x, transform.position.y, transform.localScale.x, transform.localScale.y));
			_base_bucket.Instantiate ();
            //_base_bucket = new Bucket();
           // _base_bucket.SetRectangle(new Rect(transform.position.x,transform.position.y,transform.localScale.x,transform.localScale.y));
           
        }

        public void Intersect(Rect rectangle, Type bucket)
        {
            _base_bucket.Divide(rectangle, bucket);

        }
    }
}
