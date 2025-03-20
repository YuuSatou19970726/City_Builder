using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityBuilder
{
    public class ResourceHolder : CustomMonoBehaviour
    {
        [SerializeField] protected ResourceName resourceName;
        [SerializeField] protected float resCurrent = 0;
        [SerializeField] protected float rexMax = Mathf.Infinity;

        protected override void LoadComponents()
        {
            this.LoadResName();
        }

        protected virtual void LoadResName()
        {
            string name = transform.name;
            this.resourceName = ResourceNameParser.FromString(name);
            // Debug.Log(transform.name + ": LoadResourceName");
        }

        public virtual ResourceName Name()
        {
            return this.resourceName;
        }

        public virtual float Add(int number)
        {
            this.resCurrent += number;

            if (this.resCurrent > this.rexMax) this.resCurrent = this.rexMax;
            return this.resCurrent;
        }
    }
}