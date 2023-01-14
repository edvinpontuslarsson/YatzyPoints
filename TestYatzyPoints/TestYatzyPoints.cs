namespace TestYatzyPoints;

using YatzyPoints;
using DevYatzyPoints;

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
        int one = DevYatzyPoints.Points("1, 2, 2, 2, 2", DevYatzyPoints.Category.ones);
        int two = DevYatzyPoints.Points("2, 2, 1, 1, 1", DevYatzyPoints.Category.twos);
        int three = DevYatzyPoints.Points("3, 3, 3, 1, 1", DevYatzyPoints.Category.threes);
        int four = DevYatzyPoints.Points("4, 4, 4, 4, 1", DevYatzyPoints.Category.fours);
        int five = DevYatzyPoints.Points("5, 5, 5, 5, 5", DevYatzyPoints.Category.fives);
        
        int aSix = DevYatzyPoints.Points("1, 1, 1, 1, 6", DevYatzyPoints.Category.sixes);
        int none = DevYatzyPoints.Points("1, 1, 1, 1, 2", DevYatzyPoints.Category.sixes);

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
        int resultA = DevYatzyPoints.Points("2,2,3,3,3", DevYatzyPoints.Category.pair);
        int resultB = DevYatzyPoints.Points("3,4,4,4,6", DevYatzyPoints.Category.pair);
        int resultC = DevYatzyPoints.Points("1,2,3,4,5", DevYatzyPoints.Category.pair);

        Assert.AreEqual(resultA, 6);
        Assert.AreEqual(resultB, 8);
        Assert.AreEqual(resultC, 0);
    }

    [TestMethod]
    public void TestTwoPair()
    {
        int resultA = DevYatzyPoints.Points("3, 3, 1, 5, 5", DevYatzyPoints.Category.two_pair); // 16
        int resultB = DevYatzyPoints.Points("4, 5, 1, 5, 3", DevYatzyPoints.Category.two_pair);
        int resultC = DevYatzyPoints.Points("1,2,3,4,5", DevYatzyPoints.Category.two_pair);

        Assert.AreEqual(resultA, 16);
        Assert.AreEqual(resultB, 10);
        Assert.AreEqual(resultC, 0);
    }

    [TestMethod]
    public void TestThreeOfAKind()
    {
        int resultA = DevYatzyPoints.Points("5, 1, 5, 2, 5", DevYatzyPoints.Category.three_of_a_kind); // 15
        int resultB = DevYatzyPoints.Points("5, 5, 5, 2, 5", DevYatzyPoints.Category.three_of_a_kind); // still 15
        int resultC = DevYatzyPoints.Points("5,5,2,2,1", DevYatzyPoints.Category.three_of_a_kind);

        Assert.AreEqual(resultA, 15);
        Assert.AreEqual(resultB, 15);
        Assert.AreEqual(resultC, 0);
    }

    [TestMethod]
    public void TestFourOfAKind()
    {
        int resultA = DevYatzyPoints.Points("5,5, 2, 5,5", DevYatzyPoints.Category.four_of_a_kind); // 20
        int resultB = DevYatzyPoints.Points("5, 1, 5, 2, 5", DevYatzyPoints.Category.four_of_a_kind); // 0
        int resultC = DevYatzyPoints.Points("5,5,2,2,1", DevYatzyPoints.Category.four_of_a_kind);

        Assert.AreEqual(resultA, 20);
        Assert.AreEqual(resultB, 0);
        Assert.AreEqual(resultC, 0);
    }

    [TestMethod]
    public void TestStraight()
    {
        int smallStraight = DevYatzyPoints.Points("1,2,3,4,5", DevYatzyPoints.Category.small_straight);
        int bigStraight = DevYatzyPoints.Points("2,3,4,5,6", DevYatzyPoints.Category.big_straight);

        int wantsBigStraightButIsSmall = DevYatzyPoints.Points("1,2,3,4,5", DevYatzyPoints.Category.big_straight);
        int wantsSmallStraightButIsBig = DevYatzyPoints.Points("2,3,4,5,6", DevYatzyPoints.Category.small_straight);

        int notStraight = DevYatzyPoints.Points("1,2,3,4,6", DevYatzyPoints.Category.small_straight);
        int notStraightBig = DevYatzyPoints.Points("2,3,5,5,6", DevYatzyPoints.Category.big_straight);

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
        int resultA = DevYatzyPoints.Points("2, 5, 5, 2, 2", DevYatzyPoints.Category.full_house); // 16
        int resultB = DevYatzyPoints.Points("1, 1, 4, 3, 1", DevYatzyPoints.Category.full_house); // 0
        int resultC = DevYatzyPoints.Points("2,2,5,5,6", DevYatzyPoints.Category.full_house); // 0

        Assert.AreEqual(resultA, 16);
        Assert.AreEqual(resultB, 0);
        Assert.AreEqual(resultC, 0);
    }

    [TestMethod]
    public void TestChance()
    {
        int resultA = DevYatzyPoints.Points("4, 1, 3, 5, 5", DevYatzyPoints.Category.chance); // 18
        int resultB = DevYatzyPoints.Points("1,2,3,4,5", DevYatzyPoints.Category.chance);
        int resultC = DevYatzyPoints.Points("2,3,4,5,6", DevYatzyPoints.Category.chance);

        Assert.AreEqual(resultA, 18);
        Assert.AreEqual(resultB, 15);
        Assert.AreEqual(resultC, 20);
    }

    [TestMethod]
    public void TestYatzy()
    {
        int resultA = DevYatzyPoints.Points("3, 3, 3, 3, 3", DevYatzyPoints.Category.yatzy);
        int resultB = DevYatzyPoints.Points("4, 4, 4, 4, 4", DevYatzyPoints.Category.yatzy);
        
        int resultC = DevYatzyPoints.Points("4, 4, 4, 4, 5", DevYatzyPoints.Category.yatzy);
        int resultD = DevYatzyPoints.Points("5, 4, 4, 4, 4", DevYatzyPoints.Category.yatzy);

        Assert.AreEqual(resultA, 50);
        Assert.AreEqual(resultB, 50);
        Assert.AreEqual(resultC, 0);
        Assert.AreEqual(resultD, 0);
    }

    [TestMethod]
    public void TestWhichCategoryGivesMostPoints()
    {
        DevYatzyPoints.Category[] excludeCategories = Array.Empty<DevYatzyPoints.Category>();
    }
}
