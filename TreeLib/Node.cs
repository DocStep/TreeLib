using System;
using System.Collections.Generic;
using System.Xml.Linq;
using TreeLib;

namespace TreeLib;

public class Node {
    public Node (int value) {
        this.value = value;
        count = 1;
    }

    public int value;
    public int count;
    public Node left;
    public Node right;


    public static readonly string multipleChar = "x";
    public static readonly ConsoleColor colorSingle = ConsoleColor.Gray;
    public static readonly ConsoleColor colorMultiple = ConsoleColor.Cyan;
    public static ConsoleColor color;

    public void Write (bool newLine = false) {
        changeColor();
        if (newLine) Console.WriteLine(ToString());
        else Console.Write(ToString());
        if (color != colorSingle) Console.ForegroundColor = color = colorSingle;
    }

    public override string ToString () {
        if (count == 1) 
            return "(" + value + ")";
        else 
            return "(" + value + ")" + multipleChar + count;
    }

    public void changeColor () {
        if (count == 1) 
            Console.ForegroundColor = color = colorSingle;
        else 
            Console.ForegroundColor = color = colorMultiple;
    }

    public static void WritePath (List<Node> path, string add = "") {
        Console.WriteLine($"Path: " + (add == "" ? "" : add));
        for (int i = 0; i < path.Count; i++) {
            path[i].Write(newLine: true);
            //Console.WriteLine();
        }
    }

}
