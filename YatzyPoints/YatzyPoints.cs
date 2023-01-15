using System.Text.RegularExpressions;

namespace YatzyPoints;

using static Helper;

public class YatzyPoints
{
    /// <summary>
    /// Possible categories to use for getting the points of a dice throw
    /// </summary>
    public enum Categories
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
    private enum XOfAKindVariants
    {
        three = 3,
        four = 4,
    }

    /// <summary>
    /// To tell the method calculating points for straights which straight
    /// category to use
    /// </summary>
    private enum Straights
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
    public static Dictionary<Categories, int> CategoriesWithHighestPoints(
        string eyes,
        Categories[]? excludeCategories = null
    )
    {
        excludeCategories = excludeCategories ?? Array.Empty<Categories>();

        Dictionary<Categories, int> scoreTable = new Dictionary<Categories, int>();

        // iterates through all categories
        foreach (int i in Enum.GetValues(typeof(Categories)))
        {
            // to get currentCategory
            string? categoryName = Enum.GetName(typeof(Categories), i);
            Enum.TryParse(categoryName, out Categories currentCategory);

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
    public static int Points(string eyes, Categories category)
    {
        // removes any whitespaces
        eyes = Regex.Replace(eyes, @"\s+", "");

        if (!AreEyesValid(eyes)) throw new Exception("Dice eyes are not valid");

        switch (category)
        {
            case Categories.ones:
            case Categories.twos:
            case Categories.threes:
            case Categories.fours:
            case Categories.fives:
            case Categories.sixes:
                return PointsForOnesToSixes(eyes, category);

            case Categories.pair:
                return PointsForPair(eyes);

            case Categories.two_pair:
                return PointsForTwoPair(eyes);

            case Categories.three_of_a_kind:
                return PointsForXOfAKind(eyes, XOfAKindVariants.three);
            case Categories.four_of_a_kind:
                return PointsForXOfAKind(eyes, XOfAKindVariants.four);

            case Categories.small_straight:
                return PointsForStraight(eyes, Straights.small);
            case Categories.big_straight:
                return PointsForStraight(eyes, Straights.big);

            case Categories.full_house:
                return PointsForFullHouse(eyes);

            case Categories.chance:
                return PointsForChance(eyes);

            case Categories.yatzy:
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

    private static int PointsForOnesToSixes(string eyes, Categories enumCategory)
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

    private static int PointsForXOfAKind(string eyes, XOfAKindVariants xOfAKind)
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

    private static int PointsForStraight(string eyes, Straights straight)
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
