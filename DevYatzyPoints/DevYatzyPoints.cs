using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DevYatzyPoints;

public class DevYatzyPoints
{
    // TODO OK, start with just counting ones

    // instruction is string with eyes
    // category can be enum I think but start with string
    // ("1, 2,3,1,1", "ones") 3 ones => 3 * 1 == 3

    // "5, 2, 4, 4, 1" for kategorien firere gir 8 poeng (2 firere = 2 x 4 = 8)
    // ("5, 2, 4, 4, 1", "fours") 2 fours => 2 * 4 == 8

    // TODO remove whitespaces, split by ","

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
