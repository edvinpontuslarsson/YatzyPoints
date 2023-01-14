namespace TestYatzyPoints;

using YatzyPoints;
using DevYatzyPoints;
using static DevYatzyPoints.DevYatzyPoints;

[TestClass]
public class TestYatzyPoints
{
    [TestMethod]
    public void TestPingPongs()
    {
        string result = YatzyPoengberegning.ping();
        Assert.AreEqual(result, "pong");
        Assert.AreNotEqual(result, "random");
    }

    [TestMethod]
    public void TestPointsOnesToSixes()
    {
        int one = Points("1, 2, 2, 2, 2", Category.ones);
        int two = Points("2, 2, 1, 1, 1", Category.twos);
        int three = Points("3, 3, 3, 1, 1", Category.threes);
        int four = Points("4, 4, 4, 4, 1", Category.fours);
        int five = Points("5, 5, 5, 5, 5", Category.fives);
        
        int aSix = Points("1, 1, 1, 1, 6", Category.sixes);
        int none = Points("1, 1, 1, 1, 2", Category.sixes);

        Assert.AreEqual(one, 1 * 1);
        Assert.AreEqual(two, 2 * 2);
        Assert.AreEqual(three, 3 * 3);
        Assert.AreEqual(four, 4 * 4);
        Assert.AreEqual(five, 5 * 5);
        
        Assert.AreEqual(aSix, 1 * 6);
        Assert.AreEqual(none, 0);
    }

    [TestMethod]
    public void TestPair()
    {
        int resultA = Points("2,2,3,3,3", Category.pair);
        int resultB = Points("3,4,4,4,6", Category.pair);
        int resultC = Points("1,2,3,4,5", Category.pair);

        Assert.AreEqual(resultA, 6);
        Assert.AreEqual(resultB, 8);
        Assert.AreEqual(resultC, 0);
    }

    [TestMethod]
    public void TestTwoPair()
    {
        int resultA = Points("3, 3, 1, 5, 5", Category.two_pair); // 16
        int resultB = Points("4, 5, 1, 5, 3", Category.two_pair);
        int resultC = Points("1,2,3,4,5", Category.two_pair);

        Assert.AreEqual(resultA, 16);
        Assert.AreEqual(resultB, 10);
        Assert.AreEqual(resultC, 0);
    }

    [TestMethod]
    public void TestThreeOfAKind()
    {
        int resultA = Points("5, 1, 5, 2, 5", Category.three_of_a_kind); // 15
        int resultB = Points("5, 5, 5, 2, 5", Category.three_of_a_kind); // still 15
        int resultC = Points("5,5,2,2,1", Category.three_of_a_kind);

        Assert.AreEqual(resultA, 15);
        Assert.AreEqual(resultB, 15);
        Assert.AreEqual(resultC, 0);
    }

    [TestMethod]
    public void TestFourOfAKind()
    {
        int resultA = Points("5,5, 2, 5,5", Category.four_of_a_kind); // 20
        int resultB = Points("5, 1, 5, 2, 5", Category.four_of_a_kind); // 0
        int resultC = Points("5,5,2,2,1", Category.four_of_a_kind);

        Assert.AreEqual(resultA, 20);
        Assert.AreEqual(resultB, 0);
        Assert.AreEqual(resultC, 0);
    }

    [TestMethod]
    public void TestStraight()
    {
        int smallStraight = Points("1,2,3,4,5", Category.small_straight);
        int bigStraight = Points("2,3,4,5,6", Category.big_straight);

        int wantsBigStraightButIsSmall = Points("1,2,3,4,5", Category.big_straight);
        int wantsSmallStraightButIsBig = Points("2,3,4,5,6", Category.small_straight);

        int notStraight = Points("1,2,3,4,6", Category.small_straight);
        int notStraightBig = Points("2,3,5,5,6", Category.big_straight);

        Assert.AreEqual(smallStraight, 15);
        Assert.AreEqual(bigStraight, 20);

        Assert.AreEqual(wantsBigStraightButIsSmall, 0);
        Assert.AreEqual(wantsSmallStraightButIsBig, 0);

        Assert.AreEqual(notStraight, 0);
        Assert.AreEqual(notStraightBig, 0);
    }

    [TestMethod]
    public void TestFullHouse()
    {
        int resultA = Points("2, 5, 5, 2, 2", Category.full_house); // 16
        int resultB = Points("1, 1, 4, 3, 1", Category.full_house); // 0
        int resultC = Points("2,2,5,5,6", Category.full_house); // 0

        Assert.AreEqual(resultA, 16);
        Assert.AreEqual(resultB, 0);
        Assert.AreEqual(resultC, 0);
    }

    [TestMethod]
    public void TestChance()
    {
        int resultA = Points("4, 1, 3, 5, 5", Category.chance); // 18
        int resultB = Points("1,2,3,4,5", Category.chance);
        int resultC = Points("2,3,4,5,6", Category.chance);

        Assert.AreEqual(resultA, 18);
        Assert.AreEqual(resultB, 15);
        Assert.AreEqual(resultC, 20);
    }

    [TestMethod]
    public void TestYatzy()
    {
        int resultA = Points("3, 3, 3, 3, 3", Category.yatzy);
        int resultB = Points("4, 4, 4, 4, 4", Category.yatzy);
        
        int resultC = Points("4, 4, 4, 4, 5", Category.yatzy);
        int resultD = Points("5, 4, 4, 4, 4", Category.yatzy);

        Assert.AreEqual(resultA, 50);
        Assert.AreEqual(resultB, 50);
        Assert.AreEqual(resultC, 0);
        Assert.AreEqual(resultD, 0);
    }

    [TestMethod]
    public void TestWhichCategoryGivesMostPoints()
    {
        Dictionary<Category, int> shouldBeFives = CategoriesWithHighestPoints(
            "5,5,5,5,5",
            new Category[] {Category.yatzy }
        );

        Dictionary<Category, int> shouldBeYatzy = CategoriesWithHighestPoints(
            "5,5,5,5,5"
        );

        Assert.IsTrue(
            shouldBeFives.ContainsKey(Category.fives) &&
            shouldBeFives.ContainsKey(Category.chance) &&
            shouldBeFives.All(item => item.Value == 25) &&
            shouldBeFives.Count == 2
        );

        Assert.IsTrue(
            shouldBeYatzy.ContainsKey(Category.yatzy) &&
            shouldBeYatzy.All(item => item.Value == 50) &&
            shouldBeYatzy.Count == 1
        );
    }
}
