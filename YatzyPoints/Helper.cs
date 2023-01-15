namespace YatzyPoints;

internal static class Helper
{
    /// <returns>Numeric combined value of all dice eyes</returns>
    internal static int SumOfEyes(string eyes)
    {
        int sum = 0;

        string[] justEyes = eyes.Split(',');

        foreach (string eye in justEyes)
        {
            sum += int.Parse(eye);
        }

        return sum;
    }

    /// <returns>
    /// Numeric dice eye values that occur multiple times in the string parameter eyes
    /// </returns>
    internal static List<int> Duplicates(string eyes)
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

    /// <returns>
    /// Dictionary/Frequency table where:
    /// Key is the numeric representation of each occuring dice eye
    /// Value represents the amount of dices with that value
    /// </returns>
    internal static Dictionary<int, int> FrequencyTable(string eyes)
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
