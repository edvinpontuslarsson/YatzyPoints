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
        int one = DevYatzyPoints.points("1, 2, 2, 2, 2", "ones");
        int two = DevYatzyPoints.points("2, 2, 1, 1, 1", "Twos");
        int three = DevYatzyPoints.points("3, 3, 3, 1, 1", "threes");
        int four = DevYatzyPoints.points("4, 4, 4, 4, 1", "Fours");
        int five = DevYatzyPoints.points("5, 5, 5, 5, 5", "fives");
        
        int aSix = DevYatzyPoints.points("1, 1, 1, 1, 6", "Sixes");
        int none = DevYatzyPoints.points("1, 1, 1, 1, 2", "Sixes");

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
        int resultA = DevYatzyPoints.points("2,2,3,3,3", "pair");
        int resultB = DevYatzyPoints.points("3,4,4,4,6", "pair");
        int resultC = DevYatzyPoints.points("1,2,3,4,5", "pair");

        Assert.AreEqual(resultA, 6);
        Assert.AreEqual(resultB, 8);
        Assert.AreEqual(resultC, 0);
    }

    [TestMethod]
    public void TestTwoPair()
    {
        int resultA = DevYatzyPoints.points("3, 3, 1, 5, 5", "two_pair"); // 16
        int resultB = DevYatzyPoints.points("4, 5, 1, 5, 3", "two_pair");
        int resultC = DevYatzyPoints.points("1,2,3,4,5", "two_pair");

        Assert.AreEqual(resultA, 16);
        Assert.AreEqual(resultB, 10);
        Assert.AreEqual(resultC, 0);
    }

    [TestMethod]
    public void TestThreeOfAKind()
    {
        int resultA = DevYatzyPoints.points("5, 1, 5, 2, 5", "three_of_a_kind"); // 15
        int resultB = DevYatzyPoints.points("5, 5, 5, 2, 5", "three_of_a_kind"); // still 15
        int resultC = DevYatzyPoints.points("5,5,2,2,1", "three_of_a_kind");

        Assert.AreEqual(resultA, 15);
        Assert.AreEqual(resultB, 15);
        Assert.AreEqual(resultC, 0);
    }

    [TestMethod]
    public void TestFourOfAKind()
    {
        int resultA = DevYatzyPoints.points("5,5, 2, 5,5", "four_of_a_kind"); // 20
        int resultB = DevYatzyPoints.points("5, 1, 5, 2, 5", "four_of_a_kind"); // 0
        int resultC = DevYatzyPoints.points("5,5,2,2,1", "four_of_a_kind");

        Assert.AreEqual(resultA, 20);
        Assert.AreEqual(resultB, 0);
        Assert.AreEqual(resultC, 0);
    }

    [TestMethod]
    public void TestStraight()
    {
        int smallStraight = DevYatzyPoints.points("1,2,3,4,5", "small_straight");
        int bigStraight = DevYatzyPoints.points("2,3,4,5,6", "big_straight");

        int wantsBigStraightButIsSmall = DevYatzyPoints.points("1,2,3,4,5", "big_straight");
        int wantsSmallStraightButIsBig = DevYatzyPoints.points("2,3,4,5,6", "small_straight");

        int notStraight = DevYatzyPoints.points("1,2,3,4,6", "small_straight");
        int notStraightBig = DevYatzyPoints.points("2,3,5,5,6", "big_straight");

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
        int resultA = DevYatzyPoints.points("2, 5, 5, 2, 2", "full_house"); // 16
        int resultB = DevYatzyPoints.points("1, 1, 4, 3, 1", "full_house"); // 0
        int resultC = DevYatzyPoints.points("2,2,5,5,6", "full_house"); // 0

        Assert.AreEqual(resultA, 16);
        Assert.AreEqual(resultB, 0);
        Assert.AreEqual(resultC, 0);
    }
}
