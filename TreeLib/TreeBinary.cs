using System;
using System.Collections.Generic;

namespace TreeLib;

public enum WriteMode {
    none,
    Fancy,
}


public class TreeBinary : Tree {
    public TreeBinary () { }

    public Node root;


    public override void InputRandom (int count, int range) {
        Console.WriteLine("Elements: " + count + "\nRange: " + range + "\n" + separator);
        for (int i = 0; i < count; i++)
            Add(rand.Next(range));
    }
    public override void InputLine (string line) {
        Console.WriteLine("Elements:");
        int[] values = lib.string_numbers(line);
        if (values == null) return;
        for (int n = 0; n < values.Length; n++)
            Add(values[n]);
        Console.WriteLine(separator);
    }



    public override void Add (Node node) {
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
    public override void Add (Node[] nodes) {
        for (int n = 0; n < nodes.Length; n++)
            Add(nodes[n]);
    }

    public override void Add (int value) => Add(new Node(value));
    public override void Add (int[] values) {
        for (int v = 0; v < values.Length; v++)
            Add(values[v]);
    }



    public override int count => Count();
    int Count () {
        int count = 0;
        count = CountNode(root, ref count);
        return count;

        int CountNode (Node node, ref int count) {
            count += node.count;
            if (node.left != null) CountNode(node.left, ref count);
            if (node.right != null) CountNode(node.right, ref count);
            return count;
        }
    }

    public override int depth => Depth();
    int Depth () {
        if (root == null) return 0;
        int _depth = 1;
        return DepthNode(root, _depth);

        int DepthNode (Node node, int depth) {
            if (_depth < depth) _depth = depth;
            if (node.left != null) DepthNode(node.left, depth + 1);
            if (node.right != null) DepthNode(node.right, depth + 1);
            return _depth;
        }
    }

    public override int width => Width();
    int Width () {
        if (root == null) return 0;
        int left = 0;
        int right = 0;
        LeftNodes(root, 0);
        RightNodes(root, 0);
        int width = 1 + right - left;
        return width;

        void LeftNodes (Node node, int margin) {
            if (margin < left) left = margin;
            if (node.left != null) LeftNodes(node.left, --left);
        }
        void RightNodes (Node node, int margin) {
            if (right < margin) right = margin;
            if (node.right != null) RightNodes(node.right, ++right);
        }
    }

    public override Node min => Min();
    Node Min () {
        if (root == null) return null;

        Node minValue = root;
        return MinNode(root);

        Node MinNode (Node node) {
            if (node.value < minValue.value) minValue = node;
            if (node.left != null) MinNode(node.left);
            return minValue;
        }
    }

    public override Node max => Max();
    Node Max () { 
        if (root == null) return null;

        Node maxValue = root;
        return MinNode(root);

        Node MinNode (Node node) {
            if (maxValue.value < node.value) maxValue = node;
            if (node.right != null) MinNode(node.right);
            return maxValue;
        }
    }


    public override Node deepest => Deepest();
    Node Deepest () {
        if (root == null) return null;
        Node furthest = root;
        int _depth = 1;
        DepthNode(root, _depth);
        return furthest;

        bool DepthNode (Node node, int depth) {
            if (_depth < depth) {
                _depth = depth;
                furthest = node;
            }
            if (node.left != null) DepthNode(node.left, depth + 1);
            if (node.right != null) DepthNode(node.right, depth + 1);
            return false;
        }
    }

    public override List<Node> NodePath (Node nodeTarget, bool reversed = false) {
        List<Node> path = new List<Node>();
        if (nodeTarget == null) return path;
        DepthNode(root);
        return path;

        bool DepthNode (Node node) {
            bool found = node == nodeTarget;
            if (node.left != null) found |= DepthNode(node.left);
            if (node.right != null) found |= DepthNode(node.right);
            if (found) {
                if (reversed) path.Add(node);
                else path.Insert(0, node);
            }
            return found;
        }
    }


    public static readonly string separator = "----------";

    public void Write (WriteMode mode = WriteMode.Fancy) {
        Console.WriteLine("Tree:");
        switch (mode) {
            case WriteMode.none:
                WriteNode(root);
                break;
            case WriteMode.Fancy:
                WriteBranchesIncreaseNode(root);
                break;
        }
    }


    static  void WriteNode (Node node) {
        if (node == null) return;

        node.Write();
        if (node.left != null) WriteNode(node.left);
        if (node.right != null) WriteNode(node.right);
    }
    void WriteBranchesIncreaseNode (Node node, string prefix = "", bool isRight = true) {
        if (node == null) {
            Tree.WriteEmpty();
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
            Tree.WriteEmpty();
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
        Console.WriteLine($"Count: {count}");
        Console.WriteLine($"Depth: {depth}");
        Console.WriteLine($"Width: {width}");
        Console.WriteLine($"Min: {min.value}");
        Console.WriteLine($"Max: {max.value}");
        Console.WriteLine($"Deepest: {deepest.value}");
        //lib.WritePath(furthestPath);
    }



    public void Clear () {
        root = null;
    }



    [Obsolete]
    public void Add_test () {
        int[] staticValues = new int[] { 5, 2, 7, 4, 8, 9, 11, 3, 1 };
        Add(staticValues);
    }

}
