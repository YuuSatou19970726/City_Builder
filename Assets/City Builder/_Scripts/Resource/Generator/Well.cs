using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityBuilder
{
    public class Well : ResourceGenerator
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
                name = ResourceName.water,
                number = 1,
            };

            this.resCreate.Clear();

            this.resCreate.Add(res);
        }

        protected virtual void SetLimit()
        {
            ResourceHolder resourceHolder = this.GetHolder(ResourceName.water);
            resourceHolder.SetLimit(7);
        }
    }
}