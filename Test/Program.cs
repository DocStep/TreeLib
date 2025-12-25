using System;
using System.Diagnostics.Metrics;

namespace TreeLib;

internal class Program {
    static void Main (string[] args) {
        TreeBinary tree = new TreeBinary();
        tree.InputRandom(count: 10, range: 100);
        tree.Write();
        //tree.WriteMetrics();
        //Console.WriteLine();
        List<Node> path = tree.NodePath(tree.deepest);
        //Node.WritePath(path, "Deepest");
    }
}
