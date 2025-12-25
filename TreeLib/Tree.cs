using System;
using System.Collections.Generic;
using TreeLib;

namespace TreeLib;

public class Tree {

    public Random rand = new Random();

    public virtual void InputRandom (int count, int range) { }
    public virtual void InputLine (string line) { }

    public virtual void Add (Node node) { }
    public virtual void Add (Node[] nodes) { }

    public virtual void Add (int value) => Add(new Node(value));
    public virtual void Add (int[] values) { }


    public virtual int count => 0;
    public virtual int depth => 0;
    public virtual int width => 0;

    public virtual Node min => null;
    public virtual Node max => null;
    public virtual Node deepest => null;


    public virtual List<Node> NodePath (Node nodeTarget, bool reversed = false) => null;

    public static void WriteEmpty (bool isRight = true) {
        Console.WriteLine(isRight ? "└──()" : "┌──()");
    }

}
