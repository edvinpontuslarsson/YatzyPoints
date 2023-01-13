﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DevYatzyPoints;

public class DevYatzyPoints
{
    enum Category
    {
        ones = 1,
        twos,
        threes,
        fours,
        fives,
        sixes,

        pair,
        two_pair,
        three_of_a_kind,
        four_of_a_kind,
        small_straight,
        big_straight,
        full_house,
        chance,
        yatzy
    }

    public static int points(string eyes, string category)
    {
        Enum.TryParse(category.ToLower(), out Category enumCategory);

        // removes any whitespaces
        eyes = Regex.Replace(eyes, @"\s+", "");

        // TODO validate input

        switch (enumCategory)
        {
            case Category.pair:
                return pointsForPair(eyes);

            case Category.two_pair:
                return pointsForTwoPair(eyes);

            case Category.three_of_a_kind:
                return pointsForThreeOfAKind(eyes);

            case Category.ones:
            case Category.twos:
            case Category.threes:
            case Category.fours:
            case Category.fives:
            case Category.sixes:
                return pointsForOnesToSixes(eyes, enumCategory);
        }

        return pointsForOnesToSixes(eyes, enumCategory);
    }

    private static int pointsForOnesToSixes(string eyes, Category enumCategory)
    {
        int categoryValue = (int)enumCategory;
        char search = char.Parse(categoryValue.ToString());

        int occuranceAmount = eyes.Count(character => character == search);

        return occuranceAmount * categoryValue;
    }

    private static int pointsForPair(string eyes)
    {
        List<int> duplicates = Duplicates(eyes);

        if (duplicates.Count == 0) return 0;

        int maxValue = duplicates.Max();

        return maxValue * 2;
    }

    private static int pointsForTwoPair(string eyes)
    {
        List<int> duplicates = Duplicates(eyes);

        int sumOfPairs = 0;

        foreach (int pair in duplicates) 
        {
            sumOfPairs += pair * 2;
        }

        return sumOfPairs;
    }

    private static int pointsForThreeOfAKind(string eyes)
    {
        Dictionary<int, int> frequencyTable = FrequencyTable(eyes);
        
        // if any occurs 3 or more times
        // return that multiplied with 3
        foreach (KeyValuePair<int, int> keyValue in frequencyTable)
        {
            if (keyValue.Value >= 3) return keyValue.Key * 3;
        }

        return 0;
    }

    private static List<int> Duplicates(string eyes)
    {
        List<int> duplicates = new List<int>();
        
        string[] justEyes = eyes.Split(',');

        foreach (string character in justEyes) 
        {
            int value = int.Parse(character);

            if (duplicates.Contains(value)) continue;

            int indexOfFirstOccurance = eyes.IndexOf(character);
            int indexOfLastOccurance = eyes.LastIndexOf(character);

            // if first and last index isn't the same, it occurs more than once
            if (indexOfFirstOccurance != indexOfLastOccurance) 
            {
                duplicates.Add(value);
            }
        }

        return duplicates;
    }
    
    private static Dictionary<int, int> FrequencyTable(string eyes)
    {
        string[] justEyes = eyes.Split(',');

        Dictionary<int, int> frequencyTable = new Dictionary<int, int>();

        foreach (string eye in justEyes)
        {
            int numericEye = int.Parse(eye);

            if (frequencyTable.ContainsKey(numericEye))
            {
                frequencyTable[numericEye] += 1;
            }
            else
            {
                frequencyTable[numericEye] = 1;
            }
        }

        return frequencyTable;
    }
}
