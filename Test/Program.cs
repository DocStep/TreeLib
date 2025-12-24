using System;
using System.Diagnostics.Metrics;

namespace TreeLib;

internal class Program {
    static void Main (string[] args) {
        TreeBinary tree = new TreeBinary();
        tree.InputRandom(count: 10, range: 100);
        tree.Write(metrics: true);
    }
}
