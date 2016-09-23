using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace StarDeMarket
{
    class Node : IEquatable<Node>
    {
        public Point p;
        public Node prev;
        public float h;             // heuristic
        public float g;             // distance to start point
        public float f;

        public Node(Point _p, Point _t)
        {
            p = _p;
            h = Math.Abs(_t.X - p.X) + Math.Abs(_t.Y - p.Y);
            prev = null;
            g = 0;
            f = g + h;
        }

        public Node(Point _p, Node father, Point _t)
        {
            p = _p;
            h = Math.Abs(_t.X - p.X) + Math.Abs(_t.Y - p.Y);
            g = father.g + Math.Abs(p.X - father.p.X) + Math.Abs(p.Y - father.p.Y);
            prev = father;
            f = g + h;
        }

        public void UpdateG(Node newFather)
        {
            prev = newFather;
            g = prev.g + Math.Abs(prev.p.X - p.X) + Math.Abs(prev.p.X - p.X);
        }


        // not truly equal tho
        public bool Equals(Node other)
        {
            return other.p == this.p;
        }
    }
}
