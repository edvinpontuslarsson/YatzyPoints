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
}
