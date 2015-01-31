﻿using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.QuadTree
{
    public class Bucket_Dirt : Bucket
    {

        private GameObject _dirt;
        protected override void Instantiate(Vector3 position, float size)
        {
			_dirt =   MonoBehaviour.Instantiate(GameObject.Find("Dirt"), position, Quaternion.Euler(0,0,0)) as GameObject;
            _dirt.GetComponent<Dirt>().SetAttributes(size , 1);
            base.Instantiate(position,size);
        }

        protected override void UnInstantiate()
        {
			MonoBehaviour.Destroy (_dirt);
            base.UnInstantiate();
        }
    }
}
