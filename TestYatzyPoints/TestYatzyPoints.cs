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
    public void TestPoengInitial()
    {
        int result = DevYatzyPoints.points("1, 2,3,1,1", "ones");
    }
}
