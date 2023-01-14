using static DevYatzyPoints.DevYatzyPoints;

namespace DevYatzyPoints;

class Program
{
    static void Main(string[] args)
    {
        // TODO debug here and copy for test

        Dictionary<Category, int> highest = CategoriesWithHighestPoints("1,1,1,1,4",
            new Category[] { Category.chance });
        Console.WriteLine(highest.ToString());
    }
}
