using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityBuilder
{
    public class Tree : ResourceGenerator
    {
        protected override void LoadComponents()
        {
            base.LoadComponents();
            this.LoadResCreate();
            this.SetLimit();
        }

        protected virtual void LoadResCreate()
        {
            Resource res = new Resource
            {
                name = ResourceName.logwood,
                number = 1,
            };

            this.resCreate.Clear();

            this.resCreate.Add(res);
        }

        protected virtual void SetLimit()
        {
            ResourceHolder resourceHolder = this.GetHolder(ResourceName.logwood);
            resourceHolder.SetLimit(1);
        }
    }
}