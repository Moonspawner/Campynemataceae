using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace TreeCollection
{
    public class TreeNode<T> : List<TreeNode<T>>
    {
        public T Value { get; set; }
        public TreeNode<T> Parent { get; private set; }

        public TreeNode(T value = default(T)) { Parent = null; Value = value; }
        private TreeNode(TreeNode<T> parent, T value = default(T)) { this.Parent = parent; this.Value = value; }

        public void Add(T element)
        {
            base.Add(new TreeNode<T>(this, element));
        }
        public IEnumerable<T> EnumerateAll()
        {
            if (this.Value != null) { yield return this.Value; }
            if(this.Count == 0) { yield break; }
            foreach (var node in this) {
                foreach (var item in node.EnumerateAll()) {
                    yield return item;
                }
            }
        }

        public IEnumerable<TreeNode<T>> EnumerateAllNodes()
        {
            if (this.Count == 0) { yield break; }
            foreach (var node in this) {
                yield return node;
                foreach (var subnode in node.EnumerateAllNodes()) {
                    yield return subnode;
                }
            }
        }
    }


}
