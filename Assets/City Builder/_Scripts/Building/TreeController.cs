using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityBuilder
{
    public class TreeController : LoadMonoBehaviour
    {
        public ResourceGenerator logwood;
        public TreeLevel treeLevel;
        public WorkerController choper;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            this.LoadTreeLevel();
            this.LoadLogwood();
        }

        protected virtual void LoadTreeLevel()
        {
            if (this.treeLevel != null) return;
            this.treeLevel = GetComponent<TreeLevel>();
        }

        protected virtual void LoadLogwood()
        {
            if (logwood != null) return;
            this.logwood = GetComponent<ResourceGenerator>();
        }
    }
}