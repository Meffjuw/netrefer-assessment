using System;
using System.Text;

class Program
{
    /// <summary>
    /// Return a StringBuilder that contains the patter provided repeated for the number of iterations provided.
    /// </summary>
    /// <param name="pattern">The pattern to repeat.</param>
    /// <param name="repeats">The number of iterations to repeat it for.</param>
    /// <returns></returns>
    private static StringBuilder ProcessPattern(StringBuilder pattern, int repeats)
    {
        StringBuilder stringBuilder = new StringBuilder();

        while (repeats > 0)
        {
            stringBuilder.Append(pattern);
            repeats--;
        }

        return stringBuilder;
    }

    /// <summary>
    /// Recursively parses the input starting from the provided index, and processes characters.
    /// </summary>
    /// <param name="input">The input string to parse.</param>
    /// <param name="current">The current decompressed output.</param>
    /// <param name="index">The current index.</param>
    /// <returns></returns>
    private static int Parse(string input, StringBuilder current, int index)
    {
        int iterator = 0;

        int i;
        // Start at the provided index.
        for (i = index; i < input.Length; i++)
        {
            char c = input[i];

            // If the character is a digit, add it to the iterator
            if (char.IsDigit(c))
            {
                iterator = int.Parse($"{iterator}{c}");
            }
            else
            {
                switch (c)
                {
                    // We have reached an end of pattern, return the current index.
                    case ']':
                        return i;
                    // We have reached the start of a new pattern, process the contents recursively.
                    case '[':
                        {
                            StringBuilder temp = new StringBuilder();

                            // Pass an empty StringBuilder to serve as the current for the sub-call.
                            // Update index to skip the processed characters.
                            // i + 1 is to skip the current '['.
                            i = Parse(input, temp, i + 1);

                            // Process the returned pattern and add it to the current StringBuilder.
                            current.Append(ProcessPattern(temp, iterator));

                            // Reset the iterator.
                            iterator = 0;
                        }; break;
                    // We have encountered a normal character, add it as is.
                    default:
                        { 
                            current.Append(c);
                        }; break;
                }
            }
        }

        return i;
    }

    private static string Decompress(string input)
    {
        StringBuilder output = new StringBuilder();

        // Call the first recursion with a 0 index.
        Parse(input, output, 0);

        return output.ToString();
    }

    static void Main(string[] args)
    {
        Console.WriteLine(Decompress("3[abc]4[ab]c"));
        Console.WriteLine(Decompress("10[a]"));
        Console.WriteLine(Decompress("2[3[a]b]"));
        Console.WriteLine(Decompress("2[ab5[2[cd]e]fg]5[x]6[y2[z]]"));
        Console.WriteLine(Decompress("2[a3[bc]de]"));
    }
}

