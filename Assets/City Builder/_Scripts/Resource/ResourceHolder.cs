using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityBuilder
{
    public class ResourceHolder : LoadMonoBehaviour
    {
        [SerializeField] protected ResourceName resourceName;
        [SerializeField] protected float resCurrent = 0;
        [SerializeField] private float resMax = Mathf.Infinity;

        protected override void LoadComponents()
        {
            this.LoadResName();
        }

        protected virtual void LoadResName()
        {
            string name = transform.name;
            this.resourceName = ResNameParser.FromString(name);
        }

        public virtual ResourceName Name()
        {
            return this.resourceName;
        }

        public virtual float Add(int number)
        {
            this.resCurrent += number;
            if (this.resCurrent > this.resMax) this.resCurrent = this.resMax;
            return this.resCurrent;
        }

        public virtual void SetLimit(int count)
        {
            this.resMax = count;
        }
    }
}
