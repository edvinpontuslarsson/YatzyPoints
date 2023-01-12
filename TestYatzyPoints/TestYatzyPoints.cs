using YatzyPoints;

namespace TestYatzyPoints;

[TestClass]
public class TestYatzyPoints
{
    [TestMethod]
    public void TestPingPongs()
    {
        string result = YatzyPoints.YatzyPoints.ping();
        Assert.AreEqual(result, "pong");
        Assert.AreNotEqual(result, "random");
    }
}