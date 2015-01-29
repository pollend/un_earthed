using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.QuadTree
{
    class Basic : Bucket
    {

        private GameObject _dirt;
        protected override void _instantiate(Vector3 position, float size)
        {
            _dirt = Instantiate(GameObject.Find("Dirt"), position, Quaternion.Euler(0,0,0)) as GameObject;
            _dirt.GetComponent<Dirt>().SetAttributes(size / 2f, 1);
            base._instantiate(position,size);
        }

        protected override void _unInstantiate()
        {
            base._unInstantiate();
        }
    }
}
