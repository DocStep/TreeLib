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



    public int count => _Count();
    int _Count () {
        int count = 0;
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

    public int depth => _Depth();
    int _Depth () {
        if (root == null) return 0;
        int _depth = 1;
        return DepthNode(root, _depth);

        int DepthNode (Node node, int depth) {
            if (node == null) return depth;

            if (_depth < depth) _depth = depth;
            if (node.left != null) DepthNode(node.left, depth + 1);
            if (node.right != null) DepthNode(node.right, depth + 1);
            return _depth;
        }
    }

    public int width => _Width();
    int _Width () {
        if (root == null) return 0;
        int _left = 0;
        int _right = 0;
        LeftNodes(root, 0);
        RightNodes(root, 0);
        int width = 1 + _right - _left;
        return width;

        void LeftNodes (Node node, int left) {
            if (left < _left) _left = left;
            if (node.left != null) LeftNodes(node.left, --_left);
        }
        void RightNodes (Node node, int right) {
            if (_right < right) _right = right;
            if (node.right != null) RightNodes(node.right, ++_right);
        }
    }

    public int MinValue => _MinValue();
    int _MinValue () {
        if (root == null) return 0;

        int minValue = root.value;
        return MinNode(root);

        int MinNode (Node node) {
            if (node == null) return minValue;

            if (node.value < minValue) minValue = node.value;
            if (node.left != null) MinNode(node.left);
            return minValue;
        }
    }

    public int MaxValue => _MaxValue();
    int _MaxValue () {
        if (root == null) return 0;

        int maxValue = root.value;
        return MaxNode(root);

        int MaxNode (Node node) {
            if (node == null) return maxValue;

            if (maxValue < node.value) maxValue = node.value;
            if (node.right != null) MaxNode(node.right);
            return maxValue;
        }
    }


    public static readonly string separator = "----------";

    public void Write (bool metrics = false, WriteMode mode = WriteMode.Fancy) {
        Console.WriteLine("Tree:");
        switch (mode) {
            case WriteMode.none:
                WriteNode(root);
                break;
            case WriteMode.Fancy:
                WriteBranchesIncreaseNode(root);
                break;
        }
        
        if (metrics) WriteMetrics();
    }


    void WriteNode (Node node) {
        if (node == null) return;

        node.Write();
        if (node.left != null) WriteNode(node.left);
        if (node.right != null) WriteNode(node.right);
    }
    void WriteBranchesIncreaseNode (Node node, string prefix = "", bool isRight = true) {
        if (node == null) {
            lib.WriteEmpty();
            return;
        }

        if (node.left != null)
            WriteBranchesIncreaseNode(node.left, prefix + (isRight ? "│   " : "    "), false);
        Console.Write(prefix + (isRight ? "└──" : "┌──"));
        node.Write();
        if (node.right != null)
            WriteBranchesIncreaseNode(node.right, prefix + (isRight ? "    " : "│   "), true);
    }
    void WriteBranchesRotatedNode (Node node, string prefix = "", bool isRight = true) {
        if (node == null) {
            lib.WriteEmpty();
            return;
        }

        if (node.right != null)
            WriteBranchesRotatedNode(node.right, prefix + (isRight ? "    " : "│   "), true);
        Console.Write(prefix + (isRight ? "└──" : "┌──"));
        node.Write();
        if (node.left != null)
            WriteBranchesRotatedNode(node.left, prefix + (isRight ? "│   " : "    "), false);
    }

    public void WriteMetrics () {
        int count = 0;
        int depth = 0;
        int left = 0;
        int right = 0;
        int width = 0;
        int minValue = root.value;
        int maxValue = root.value;
        if (root != null) {
            MetricsNode(root, 1, 0);
            width = right - left + 1;
        }

        void MetricsNode (Node node, int _depth, int margin) {
            if (depth < _depth) depth = _depth;
            if (margin < left) left = margin;
            if (right < margin) right = margin;
            count += node.count;
            if (node.value < minValue) minValue = node.value;
            if (maxValue < node.value) maxValue = node.value;

            if (node.left != null) MetricsNode(node.left, _depth + 1, margin - 1);
            if (node.right != null) MetricsNode(node.right, _depth + 1, margin + 1);
        }

        Console.WriteLine(/*$"{separator}\n" +*/
            $"Count: {count}\n" +
            $"Depth: {depth}\n" +
            $"Width: {width}\n" +
            $"Min: {minValue}\n" +
            $"Max: {maxValue}");
    }


    public void Add_test () {
        int[] staticValues = new int[] { 5, 2, 7, 4, 8, 9, 11, 3, 1 };
        Add(staticValues);
    }

    public void Clear () {
        root = null;
    }

}
