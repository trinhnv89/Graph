using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Graph
{
    public class Graph<T, W>
    {
        private List<Node<T, W>> _nodes;

        public Graph()
        {
            _nodes = new List<Node<T, W>>();
        }

        public void AddNode(Node<T, W> node)
        {
            // check if node is already in graph
            if (_nodes.Contains(node))
                return;

            _nodes.Add(node);
        }

        public void RemoveNode(Node<T, W> node)
        {
            // remove node and all arcs of node
            if (_nodes.Contains(node))
            {
                foreach (var item in _nodes)
                {
                    if (item != node)
                    {
                        Arc<T, W> arc = item.GetArc(node);
                        if (arc != null)
                        {
                            item.RemoveArc(arc);
                        }
                    }
                }

                _nodes.Remove(node);
            }
        }

        public bool AddArc(Node<T, W> from, Node<T, W> to, W weight)
        {
            if (_nodes.Contains(from) && _nodes.Contains(to))
            {
                if (from.GetArc(to) == null)
                {
                    from.AddArc(to, weight);
                    return true;
                }
            }

            return false;
        }

        public bool RemoveArc(Node<T, W> from, Node<T, W> to)
        {
            if (_nodes.Contains(from) && _nodes.Contains(to))
            {
                var arc = from.GetArc(to);
                if (arc != null)
                {
                    from.RemoveArc(arc);
                    return true;
                }
            }

            return false;
        }

    }

    public class Node<T, W>
    { 
        private T _data;
        private List<Arc<T, W>> _arcs;
        public bool _isMarked;

        public T Data
        {
            get { return _data; }
        }

        public Node(T data)
        {
            _data = data;
            _isMarked = false;
            _arcs = new List<Arc<T, W>>();
        }

        public void AddArc(Node<T, W> node, W weight)
        {
            Arc<T, W> arc = new Arc<T, W>();
            arc.Node = node;
            arc.Weight = weight;

            _arcs.Add(arc);
        }

        public Arc<T, W> GetArc(Node<T, W> node)
        {
            foreach (var arc in _arcs)
            {
                if (arc.Node == node)
                    return arc;
            }

            return null;
        }

        public void RemoveArc(Arc<T, W> arc)
        {
            _arcs.Remove(arc);
        }


    }

    public class Arc<T, W>
    {
        public W Weight;
        public Node<T, W> Node; 

        public Arc()
        {
            Weight = default(W);
            Node = default(Node<T, W>);
        }

        public Arc(W weight, Node<T, W> node)
        {
            Weight = weight;
            Node = node;
        }
    }
}
