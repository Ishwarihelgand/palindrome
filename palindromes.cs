using System;
using System.Collections.Generic;

class PalindromeFinder
{
    // Helper method to expand around the center and return the longest palindrome
    static (int, int) ExpandAroundCenter(string s, int left, int right)
    {
        while (left >= 0 && right < s.Length && s[left] == s[right])
        {
            left--;
            right++;
        }
        // Adjusting to get the correct start index and length
        return (left + 1, right - left - 1);  // Return (start index, length)
    }

    public static List<(string text, int index, int length)> FindLongestPalindromes(string s)
    {
        var palindromes = new Dictionary<string, (int, int)>();

        for (int i = 0; i < s.Length; i++)
        {
            // Odd-length palindrome (centered at i)
            var (start, length) = ExpandAroundCenter(s, i, i);
            if (length > 0)
                palindromes[s.Substring(start, length)] = (start, length);

            // Even-length palindrome (centered between i and i+1)
            (start, length) = ExpandAroundCenter(s, i, i + 1);
            if (length > 0)
                palindromes[s.Substring(start, length)] = (start, length);
        }

        // Sort the palindromes by length in descending order
        var sortedPalindromes = new List<(string text, int index, int length)>();
        foreach (var p in palindromes)
        {
            sortedPalindromes.Add((p.Key, p.Value.Item1, p.Value.Item2));
        }
        
        sortedPalindromes.Sort((a, b) => b.length.CompareTo(a.length));

        // Return the top 3 longest palindromes
        return sortedPalindromes.GetRange(0, Math.Min(3, sortedPalindromes.Count));
    }

    static void Main()
    {
        string input = "sqrrgabccbatudefggfedw hijkllkjihxymnnmzpop";
        var result = FindLongestPalindromes(input);

        foreach (var palindrome in result)
        {
            Console.WriteLine($"Text: {palindrome.text}, Index: {palindrome.index}, Length: {palindrome.length}");
        }
    }
}
