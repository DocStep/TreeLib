using System.Xml.Linq;

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

    public void Write () {
        changeColor();
        Console.WriteLine(ToString());
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

}
