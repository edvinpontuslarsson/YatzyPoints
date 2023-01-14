using static DevYatzyPoints.DevYatzyPoints;

namespace DevYatzyPoints;

class Program
{
    static void Main(string[] args)
    {
        // TODO debug here and copy for test

        Dictionary<Category, int> highest = CategoriesWithHighestPoints("1,1,1,1,6");
        Console.WriteLine(highest.ToString());
    }
}
