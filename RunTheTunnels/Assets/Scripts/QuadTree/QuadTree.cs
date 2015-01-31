using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.QuadTree
{
    public class QuadTree : MonoBehaviour
    {
        public const float MAX_BUCKET_SIZE = .3f;
		public const float INFLATE_FACTOR = 1.1f;
		
        private Bucket _base_bucket;

        public void Start() 
        {

            _base_bucket = (Bucket)Activator.CreateInstance(new Bucket_Dirt().GetType());
			_base_bucket.SetRectangle(new Rect(transform.position.x - transform.localScale.x/2.0f, transform.position.y- transform.localScale.y/2.0f,transform.localScale.x, transform.localScale.y));
			_base_bucket.InstantiateBucket ();
        }

		void FixedUpdate() 
		{
			_base_bucket.Update ();
		}

		public Bucket GetBucket()
		{
			return _base_bucket;
		}

    }
}
