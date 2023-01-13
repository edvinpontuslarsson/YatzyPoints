﻿using System.Text.RegularExpressions;

namespace YatzyPoints;

public static class YatzyPoengberegning
{
    public static string ping() => "pong";

    // TODO OK, start with just counting ones

    // instruction is string with eyes
    // category can be enum I think but start with string
    // ("1, 2,3,1,1", "ones") 3 ones => 3 * 1 == 3

    // "5, 2, 4, 4, 1" for kategorien firere gir 8 poeng (2 firere = 2 x 4 = 8)
    // ("5, 2, 4, 4, 1", "fours") 2 fours => 2 * 4 == 8

    // TODO remove whitespaces, split by ","

    public static int points(string eyesInString, string category)
    {
        // OK just ones, ignore category first

        string eyesInStringNoSpaces = Regex.Replace(eyesInString, @"\s+", "");

        string[] eyes = eyesInStringNoSpaces.Split(',');

        return eyes.Length;
    }
}
