using System;
using System.Collections.Generic;

namespace TreeLib;


#pragma warning disable CS8981
public static class lib {
#pragma warning restore CS8981

    public static int[] string_numbers () {
        string line = Console.ReadLine();
        string[] words = line.Split(" ");
        int[] numbers = new int[words.Length];
        for (int i = 0; i < numbers.Length; i++)
            if (!int.TryParse(words[i].Trim(), out numbers[i]))
                return null;
        return numbers;
    }

    public static void WriteEmpty (bool isRight = true) {
        Console.WriteLine(isRight ? "└──()" : "┌──()");
    }

}
