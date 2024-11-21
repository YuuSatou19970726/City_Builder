using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityBuilder
{
    public class TreeController : LoadMonoBehaviour
    {
        public TreeLevel treeLevel;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            this.LoadTreeLevel();
        }

        protected virtual void LoadTreeLevel()
        {
            if (this.treeLevel != null) return;
            this.treeLevel = GetComponent<TreeLevel>();
        }
    }
}