using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityBuilder
{
    public class TreeLevel : BuildLevel
    {
        [SerializeField] protected bool isMaxLevel = false;
        [SerializeField] protected Tree tree;
        [SerializeField] protected float treeTimer = 0;
        [SerializeField] protected float treeDeplay = Mathf.Infinity;

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            this.Growing();
            this.IsMaxLevel();
        }

        protected override void LoadComponents()
        {
            base.LoadComponents();
            this.LoadTree();
        }

        protected virtual void LoadTree()
        {
            if (this.tree != null) return;

            this.tree = GetComponent<Tree>();

            this.GetTreeDelay();
        }

        protected virtual void GetTreeDelay()
        {
            int levelCount = this.levels.Count - 2;
            this.treeDeplay = this.tree.GetCreateDelay() / levelCount;
        }

        protected virtual void Growing()
        {
            this.treeTimer += Time.fixedDeltaTime;
            if (this.treeTimer < this.treeDeplay) return;
            this.treeTimer = 0;
            ShowNextBuild();
        }

        protected virtual bool IsMaxLevel()
        {
            if (this.currentLevel == this.levels.Count - 2) this.isMaxLevel = true;
            else this.isMaxLevel = false;

            return this.isMaxLevel;
        }
    }
}