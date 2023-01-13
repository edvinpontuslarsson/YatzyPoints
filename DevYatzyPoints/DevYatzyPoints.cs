using System;
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
    }

    public static int points(string eyes, string category)
    {
        Enum.TryParse(category.ToLower(), out Category enumCategory);

        int searchValue = (int) enumCategory;
        char search = char.Parse(searchValue.ToString());

        int occuranceAmount = eyes.Count(eye => eye == search);

        return occuranceAmount * searchValue;
    }
}
