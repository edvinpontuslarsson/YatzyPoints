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

        Assert.AreEqual(one, 1 * 1);
        Assert.AreEqual(two, 2 * 2);
        Assert.AreEqual(three, 3 * 3);
        Assert.AreEqual(four, 4 * 4);
        Assert.AreEqual(five, 5 * 5);
        Assert.AreEqual(aSix, 1 * 6);
    }
}
