using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCommand.Custom
{
    public class Graph
    {
        List<Node> nodes;
        List<Edge> edges;

        public Graph()
        {
            nodes = new List<Node>();
            edges = new List<Edge>();
        }

        public void Add(Node node)
        {
            nodes.Add(node);
        }

        public void Add(Edge edge)
        {
            edges.Add(edge);
        }

        public List<Node> Nodes { get { return nodes; } }
        public List<Edge> Edges { get { return edges; } }

        public override string ToString()
        {
            return "NODES\n-----\n"
                + String.Join("\n", nodes.Select(n => n.ToString()))
                + "\n\nEDGES\n-----\n"
                + String.Join("\n", edges.Select(e => e.ToString()));
        }
    }

    public class Node
    {
        public string Id { get; set; }
        public string Type { get; set; }

        public override string ToString() => Id + "[" + Type + "]";
    }

    public class Edge
    {
        public String From { get; set; }
        public String To { get; set; }

        public override string ToString() => From + "-" + To;
    }
}
