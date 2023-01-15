﻿using System.Text.RegularExpressions;

namespace YatzyPoints;

using static Helper;

public class YatzyPoints
{
    /// <summary>
    /// Possible categories to use for getting the points of a dice throw
    /// </summary>
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

    /// <summary>
    /// to calculate three_of_a_kind and four_of_a_kind the same private method
    /// is used, this enum is used to tell that method which category to use
    /// </summary>
    private enum XOfAKind
    {
        three = 3,
        four = 4,
    }

    /// <summary>
    /// To tell the method calculating points for straights which straight
    /// category to use
    /// </summary>
    private enum Straight
    {
        small = 15,
        big = 20,
    }

    /// <summary>
    /// You can use this to get the highest scoring category/categories along
    /// with the amount of points.
    /// 
    /// The string parameter eyes needs to consist of numeric characters 1-6
    /// separated with commas, space characters will be ignored so they can be
    /// included or omitted as you wish. 2 examples of valid inputs for the 
    /// string eyes are "1,2,3,4,5" and "2, 3, 4, 5, 6". Exception will be 
    /// thrown if the string eyes is not valid.
    /// 
    /// excludeCategories is and optional parameter, you can use it to 
    /// exclude categories even if they happen to give the highest points. This
    /// method will then return the categories with the highest points after
    /// those.
    /// </summary>
    /// <exception cref="Exception"></exception>
    public static Dictionary<Category, int> CategoriesWithHighestPoints(
        string eyes,
        Category[]? excludeCategories = null
    )
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

    /// <summary>
    /// You can use this method to get the yatzy points for 5 dice eyes given
    /// the chosen category.
    /// 
    /// The string parameter eyes needs to consist of numeric characters 1-6
    /// separated with commas, space characters will be ignored so they can be
    /// included or omitted as you wish. 2 examples of valid inputs for the 
    /// string eyes are "1,2,3,4,5" and "2, 3, 4, 5, 6". Exception will be 
    /// thrown if the string eyes is not valid.
    /// </summary>
    /// <param name="eyes"></param>
    /// <param name="category"></param>
    /// <exception cref="Exception"></exception>
    public static int Points(string eyes, Category category)
    {
        // removes any whitespaces
        eyes = Regex.Replace(eyes, @"\s+", "");

        if (!AreEyesValid(eyes)) throw new Exception("Dice eyes are not valid");

        switch (category)
        {
            case Category.ones:
            case Category.twos:
            case Category.threes:
            case Category.fours:
            case Category.fives:
            case Category.sixes:
                return PointsForOnesToSixes(eyes, category);

            case Category.pair:
                return PointsForPair(eyes);

            case Category.two_pair:
                return PointsForTwoPair(eyes);

            case Category.three_of_a_kind:
                return PointsForXOfAKind(eyes, XOfAKind.three);
            case Category.four_of_a_kind:
                return PointsForXOfAKind(eyes, XOfAKind.four);

            case Category.small_straight:
                return PointsForStraight(eyes, Straight.small);
            case Category.big_straight:
                return PointsForStraight(eyes, Straight.big);

            case Category.full_house:
                return PointsForFullHouse(eyes);

            case Category.chance:
                return PointsForChance(eyes);

            case Category.yatzy:
                return PointsForYatzy(eyes);

            default:
                throw new Exception("Please make sure input is valid");
        }
    }

    private static bool AreEyesValid(string eyes)
    {
        string[] justEyes = eyes.Split(',');

        if (justEyes.Length != 5) return false;

        foreach (string eye in justEyes)
        {
            int numericEye;
            bool isNumeric = int.TryParse(eye, out numericEye);

            if (!isNumeric) return false;
            if (numericEye < 1 || numericEye > 6) return false;
        }

        return true;
    }

    private static int PointsForOnesToSixes(string eyes, Category enumCategory)
    {
        int categoryValue = (int)enumCategory;
        char search = char.Parse(categoryValue.ToString());

        int occuranceAmount = eyes.Count(character => character == search);

        return occuranceAmount * categoryValue;
    }

    private static int PointsForPair(string eyes)
    {
        List<int> duplicates = Duplicates(eyes);

        if (duplicates.Count == 0) return 0;

        int maxValue = duplicates.Max();

        return maxValue * 2;
    }

    private static int PointsForTwoPair(string eyes)
    {
        List<int> duplicates = Duplicates(eyes);

        int sumOfPairs = 0;

        foreach (int pair in duplicates)
        {
            sumOfPairs += pair * 2;
        }

        return sumOfPairs;
    }

    private static int PointsForXOfAKind(string eyes, XOfAKind xOfAKind)
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

    private static int PointsForStraight(string eyes, Straight straight)
    {
        int sum = SumOfEyes(eyes);

        // straight is 15 for small, 20 for big
        if (sum != (int)straight) return 0;

        return sum;
    }

    private static int PointsForFullHouse(string eyes)
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

    private static int PointsForChance(string eyes) => SumOfEyes(eyes);

    private static int PointsForYatzy(string eyes)
    {
        string[] justEyes = eyes.Split(',');
        string firstEye = justEyes[0];
        bool allSame = justEyes
            .Skip(1)
            .All(eye => string.Equals(firstEye, eye));

        if (!allSame) return 0;

        return 50;
    }
}
