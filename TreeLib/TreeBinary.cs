using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml.Linq;

namespace TreeLib;

public enum WriteMode {
    none,
    Fancy,
}


public class TreeBinary {
    public TreeBinary () { }

    public Random rand = new Random();

    public Node root;
    public int inputCount;

    public int count;
    public int depth;
    public int width;


    public void InputRandom (int count, int range) {
        Console.WriteLine("Elements: " + count + "\nRange: " + range + "\n" + separator);
        for (int i = 0; i < count; i++)
            Add(rand.Next(range));
    }
    public void InputLine () {
        Console.WriteLine("Elements:");
        int[] values = lib.string_numbers();
        if (values == null) return;
        for (int n = 0; n < values.Length; n++)
            Add(values[n]);
        Console.WriteLine(separator);
    }



    public void Add (Node node) {
        AddNode(node);

        void AddNode (Node node, Node nodeParent = null) {
            if (root == null) {
                root = node;
                return;
            }
            if (nodeParent == null) nodeParent = root;

            if (node.value < nodeParent.value) {
                if (nodeParent.left != null) AddNode(node, nodeParent.left);
                else nodeParent.left = node;
            }
            if (node.value == nodeParent.value)
                nodeParent.count++;
            if (nodeParent.value < node.value) {
                if (nodeParent.right != null) AddNode(node, nodeParent.right);
                else nodeParent.right = node;
            }
        }
    }
    public void Add (Node[] nodes) {
        for (int n = 0; n < nodes.Length; n++)
            Add(nodes[n]);
    }

    public void Add (int value) => Add(new Node(value));
    public void Add (int[] values) {
        for (int v = 0; v < values.Length; v++)
            Add(values[v]);
    }



    public int Count => _Count();
    int _Count () {
        count = 0;
        count = CountNode(root, ref count);
        return count;

        int CountNode (Node node, ref int count) {
            if (node == null) return count;
            count += node.count;
            if (node.left != null) CountNode(node.left, ref count);
            if (node.right != null) CountNode(node.right, ref count);
            return count;
        }
    }


    public static readonly string separator = "----------";

    public void Write (bool metrics = false, WriteMode mode = WriteMode.Fancy) {
        Console.WriteLine("Tree:");
        switch (mode) {
            case WriteMode.none:
                string tree = "";
                NodeOut(root, ref tree);
                Console.WriteLine(tree);
                break;

                string NodeOut (Node node, ref string tree) {
                    if (node == null) return tree;

                    tree += node.ToString() + "\n";
                    if (node.left != null) NodeOut(node.left, ref tree);
                    if (node.right != null) NodeOut(node.right, ref tree);
                    return tree;
                }
            case WriteMode.Fancy:
                WriteBranchesIncrease(root);
                break;

                void WriteBranchesIncrease (Node node, string prefix = "", bool isRight = true) {
                    if (node == null) {
                        lib.WriteEmpty();
                        return;
                    }

                    if (node.left != null)
                        WriteBranchesIncrease(node.left, prefix + (isRight ? "│   " : "    "), false);
                    Console.Write(prefix + (isRight ? "└──" : "┌──"));
                    node.Write();
                    if (node.right != null)
                        WriteBranchesIncrease(node.right, prefix + (isRight ? "    " : "│   "), true);
                }
                void WriteBranchesRotated (Node node, string prefix = "", bool isRight = true) {
                    if (node == null) {
                        lib.WriteEmpty();
                        return;
                    }

                    if (node.right != null)
                        WriteBranchesRotated(node.right, prefix + (isRight ? "    " : "│   "), true);
                    Console.Write(prefix + (isRight ? "└──" : "┌──"));
                    node.Write();
                    if (node.left != null)
                        WriteBranchesRotated(node.left, prefix + (isRight ? "│   " : "    "), false);
                }
        }
        
        if (metrics) {
            Metrics();
            WriteMetrics();
        }
    }

    public void Metrics () {
        int left = 0;
        int right = 0;
        if (root != null) {
            MetricsNode(root, 1, 0);
            width = right - left + 1;
        }

        void MetricsNode (Node node, int depth, int margin) {
            if (this.depth < depth) this.depth = depth;
            if (right < margin) right = margin;
            if (margin < left) left = margin;
            count += node.count;

            if (node.left != null) MetricsNode(node.left, depth + 1, margin - 1);
            if (node.right != null) MetricsNode(node.right, depth + 1, margin + 1);
        }
    }
    public void WriteMetrics () {
        Console.WriteLine($"{separator}\n" +
            $"Count: {count}\n" +
            $"Depth: {depth}\n" +
            $"Width: {width}");
    }


    public void Add_test () {
        int[] staticValues = new int[] { 5, 2, 7, 4, 8, 9, 11, 3, 1 };
        Add(staticValues);
    }

    public void Clear () {
        root = null;
    }

}
