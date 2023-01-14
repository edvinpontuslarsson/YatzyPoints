﻿using System.Data;
using System.Text.RegularExpressions;

namespace DevYatzyPoints;

public class DevYatzyPoints
{
    public enum Category
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

    private enum XOfAKind
    {
        three = 3,
        four = 4,
    }

    private enum Straight
    {
        small = 15,
        big = 20,
    }

    public static Dictionary<Category, int> CategoriesWithHighestPoints(
        string eyes, 
        Category[]? excludeCategories = null
    )
    {
        try
        {
            excludeCategories = excludeCategories ?? Array.Empty<Category>();

            Dictionary<Category, int> scoreTable = new Dictionary<Category, int>();

            // iterates through all categories
            foreach (int i in Enum.GetValues(typeof(Category)))
            {
                // to get currentCategory
                string? categoryName = Enum.GetName(typeof(Category), i);
                Enum.TryParse(categoryName, out Category currentCategory);

                if (excludeCategories.Contains(currentCategory)) continue;

                int currentPoints = Points(eyes, currentCategory);

                scoreTable.Add(currentCategory, currentPoints);
            }

            int highestPoints = scoreTable.MaxBy(row => row.Value).Value;

            return scoreTable
                .Where(pair => pair.Value == highestPoints)
                .ToDictionary(pair => pair.Key, pair => pair.Value);
        }
        catch
        {
            throw new Exception("Please make sure input is valid");
        }
    }

    public static int Points(string eyes, Category category)
    {
        // removes any whitespaces
        eyes = Regex.Replace(eyes, @"\s+", "");

        // TODO validate input

        switch (category)
        {
            case Category.ones:
            case Category.twos:
            case Category.threes:
            case Category.fours:
            case Category.fives:
            case Category.sixes:
                return pointsForOnesToSixes(eyes, category);

            case Category.pair:
                return pointsForPair(eyes);

            case Category.two_pair:
                return pointsForTwoPair(eyes);

            case Category.three_of_a_kind:
                return pointsForXOfAKind(eyes, XOfAKind.three);
            case Category.four_of_a_kind:
                return pointsForXOfAKind(eyes, XOfAKind.four);

            case Category.small_straight:
                return pointsForStraight(eyes, Straight.small);
            case Category.big_straight:
                return pointsForStraight(eyes, Straight.big);

            case Category.full_house:
                return pointsForFullHouse(eyes);

            case Category.chance:
                return pointsForChance(eyes);

            case Category.yatzy:
                return pointsForYatzy(eyes);

            default:
                throw new Exception("Please make sure input is valid");
        }
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

    private static int pointsForXOfAKind(string eyes, XOfAKind xOfAKind)
    {
        // x is 3 or 4
        int x = (int)xOfAKind;

        Dictionary<int, int> frequencyTable = FrequencyTable(eyes);
        
        // if any occurs x or more times
        // return that multiplied with x
        foreach (KeyValuePair<int, int> keyValue in frequencyTable)
        {
            if (keyValue.Value >= x) return keyValue.Key * x;
        }

        return 0;
    }

    private static int pointsForStraight(string eyes, Straight straight)
    {
        int sum = SumOfEyes(eyes);

        // straight is 15 for small, 20 for big
        if (sum != (int)straight) return 0;

        return sum;
    }

    private static int pointsForFullHouse(string eyes)
    {
        Dictionary<int, int> frequencyTable = FrequencyTable(eyes);

        if (!frequencyTable.ContainsValue(3) || !frequencyTable.ContainsValue(2)) 
        {
            return 0;
        }

        int sum = 0;

        foreach (KeyValuePair<int, int> keyValue in frequencyTable)
        {
            sum += keyValue.Key * keyValue.Value;
        }

        return sum;
    }

    private static int pointsForChance(string eyes) => SumOfEyes(eyes);

    private static int pointsForYatzy(string eyes)
    {
        string[] justEyes = eyes.Split(',');
        string firstEye = justEyes[0];
        bool allSame = justEyes
            .Skip(1)
            .All(eye => string.Equals(firstEye, eye));

        if (!allSame) return 0;

        return 50;
    }

    private static int SumOfEyes(string eyes)
    {
        int sum = 0;

        string[] justEyes = eyes.Split(',');

        foreach (string eye in justEyes)
        {
            sum += int.Parse(eye);
        }

        return sum;
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
