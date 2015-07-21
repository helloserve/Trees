using helloserve.com.Trees.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace helloserve.com.Trees.Core
{
    public abstract class LeafBase<T> : ILeaf<T>
    {
        public bool HasValue { get; set; }

        public ILeaf<T> Parent { get; set; }
        public List<ILeaf<T>> Leafs { get; set; }
        public abstract T Value { get; set; }
        
        internal abstract void Add(T item, ILeaf<T> parent);
        protected abstract ILeaf<T> AddLeafAt(T item);
        internal abstract ILeaf<T> FindLeaf(object key);
        internal abstract void Clear();
        internal abstract bool Remove();
    }
}
