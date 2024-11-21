using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityBuilder
{
    public class TreeManager : LoadMonoBehaviour
    {
        private static TreeManager instance;
        public static TreeManager Instance { get { return instance; } }

        [SerializeField] protected List<GameObject> trees;

        protected override void Awake()
        {
            base.Awake();
            if (instance == null)
                instance = this;
            else
                Destroy(gameObject);
        }

        protected override void LoadComponents()
        {
            base.LoadComponents();
            this.LoadTrees();
        }

        protected virtual void LoadTrees()
        {
            if (this.trees.Count > 0) return;
            foreach (Transform tree in transform)
                this.TreeAdd(tree.gameObject);
        }

        public virtual void TreeAdd(GameObject tree)
        {
            if (this.trees.Contains(tree)) return;
            trees.Add(tree);
            tree.transform.parent = transform;
        }

        public virtual List<GameObject> Trees()
        {
            return this.trees;
        }
    }
}