using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace helloserve.com.Trees.Core.Interfaces
{
    public interface ILeaf<T>
    {
        T Value { get; set; }
        ILeaf<T> Parent { get; set; }
        List<ILeaf<T>> Leafs { get; set; }
    }
}
