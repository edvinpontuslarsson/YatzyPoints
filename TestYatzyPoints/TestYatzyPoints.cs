namespace TestYatzyPoints;

using static YatzyPoints.YatzyPoints;

[TestClass]
public class TestYatzyPoints
{
    [TestMethod]
    public void TestValidatesEyes()
    {
        // 5 valid eyes
        int result = Points("1,2,3,4,5", Categories.ones);
        Assert.AreEqual(result, 1);

        // more than 5 eyes
        try
        {
            Points("1,2,3,4,5,6", Categories.ones);
            Assert.Fail();
        }
        catch {}

        // one item is not numeric
        try
        {
            Points("1,2,3,4,s", Categories.ones);
            Assert.Fail();
        }
        catch { }

        // one eye is 0
        try
        {
            Points("0,1,2,3,4", Categories.ones);
            Assert.Fail();
        }
        catch { }

        // one eye is 7
        try
        {
            Points("3,4,5,6,7", Categories.ones);
            Assert.Fail();
        }
        catch { }

        // one eye is 11
        try
        {
            Points("11,2,3,4,5", Categories.ones);
            Assert.Fail();
        }
        catch { }

        // one eye is -1
        try
        {
            Points("-1,2,3,4,5", Categories.ones);
            Assert.Fail();
        }
        catch { }
    }

    [TestMethod]
    public void TestPointsOnesToSixes()
    {
        int one = Points("1, 2, 2, 2, 2", Categories.ones);
        int two = Points("2, 2, 1, 1, 1", Categories.twos);
        int three = Points("3, 3, 3, 1, 1", Categories.threes);
        int four = Points("4, 4, 4, 4, 1", Categories.fours);
        int five = Points("5, 5, 5, 5, 5", Categories.fives);
        
        int aSix = Points("1, 1, 1, 1, 6", Categories.sixes);
        int none = Points("1, 1, 1, 1, 2", Categories.sixes);

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
        int resultA = Points("2,2,3,3,3", Categories.pair);
        int resultB = Points("3,4,4,4,6", Categories.pair);
        int resultC = Points("1,2,3,4,5", Categories.pair);

        Assert.AreEqual(resultA, 6);
        Assert.AreEqual(resultB, 8);
        Assert.AreEqual(resultC, 0);
    }

    [TestMethod]
    public void TestTwoPair()
    {
        int resultA = Points("3, 3, 1, 5, 5", Categories.two_pair); // 16
        int resultB = Points("4, 5, 1, 5, 3", Categories.two_pair);
        int resultC = Points("1,2,3,4,5", Categories.two_pair);

        Assert.AreEqual(resultA, 16);
        Assert.AreEqual(resultB, 10);
        Assert.AreEqual(resultC, 0);
    }

    [TestMethod]
    public void TestThreeOfAKind()
    {
        int resultA = Points("5, 1, 5, 2, 5", Categories.three_of_a_kind); // 15
        int resultB = Points("5, 5, 5, 2, 5", Categories.three_of_a_kind); // still 15
        int resultC = Points("5,5,2,2,1", Categories.three_of_a_kind);

        Assert.AreEqual(resultA, 15);
        Assert.AreEqual(resultB, 15);
        Assert.AreEqual(resultC, 0);
    }

    [TestMethod]
    public void TestFourOfAKind()
    {
        int resultA = Points("5,5, 2, 5,5", Categories.four_of_a_kind); // 20
        int resultB = Points("5, 1, 5, 2, 5", Categories.four_of_a_kind); // 0
        int resultC = Points("5,5,2,2,1", Categories.four_of_a_kind);

        Assert.AreEqual(resultA, 20);
        Assert.AreEqual(resultB, 0);
        Assert.AreEqual(resultC, 0);
    }

    [TestMethod]
    public void TestStraight()
    {
        int smallStraight = Points("1,2,3,4,5", Categories.small_straight);
        int bigStraight = Points("2,3,4,5,6", Categories.big_straight);

        int wantsBigStraightButIsSmall = Points("1,2,3,4,5", Categories.big_straight);
        int wantsSmallStraightButIsBig = Points("2,3,4,5,6", Categories.small_straight);

        int notStraight = Points("1,2,3,4,6", Categories.small_straight);
        int notStraightBig = Points("2,3,5,5,6", Categories.big_straight);

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
        int resultA = Points("2, 5, 5, 2, 2", Categories.full_house); // 16
        int resultB = Points("1, 1, 4, 3, 1", Categories.full_house); // 0
        int resultC = Points("2,2,5,5,6", Categories.full_house); // 0

        Assert.AreEqual(resultA, 16);
        Assert.AreEqual(resultB, 0);
        Assert.AreEqual(resultC, 0);
    }

    [TestMethod]
    public void TestChance()
    {
        int resultA = Points("4, 1, 3, 5, 5", Categories.chance); // 18
        int resultB = Points("1,2,3,4,5", Categories.chance);
        int resultC = Points("2,3,4,5,6", Categories.chance);

        Assert.AreEqual(resultA, 18);
        Assert.AreEqual(resultB, 15);
        Assert.AreEqual(resultC, 20);
    }

    [TestMethod]
    public void TestYatzy()
    {
        int resultA = Points("3, 3, 3, 3, 3", Categories.yatzy);
        int resultB = Points("4, 4, 4, 4, 4", Categories.yatzy);
        
        int resultC = Points("4, 4, 4, 4, 5", Categories.yatzy);
        int resultD = Points("5, 4, 4, 4, 4", Categories.yatzy);

        Assert.AreEqual(resultA, 50);
        Assert.AreEqual(resultB, 50);
        Assert.AreEqual(resultC, 0);
        Assert.AreEqual(resultD, 0);
    }

    [TestMethod]
    public void TestWhichCategoryGivesMostPoints()
    {
        Dictionary<Categories, int> shouldBeFivesAndChance = CategoriesWithHighestPoints(
            "5,5,5,5,5",
            new Categories[] {Categories.yatzy }
        );

        Dictionary<Categories, int> shouldBeYatzy = CategoriesWithHighestPoints(
            "5,5,5,5,5"
        );

        Dictionary<Categories, int> shouldBeOnesFoursAndFourOfAKind = CategoriesWithHighestPoints(
            "4,1,1,1,1",
            new Categories[] { Categories.chance }
        );

        Assert.IsTrue(
            shouldBeFivesAndChance.ContainsKey(Categories.fives) &&
            shouldBeFivesAndChance.ContainsKey(Categories.chance) &&
            shouldBeFivesAndChance.All(item => item.Value == 25) &&
            shouldBeFivesAndChance.Count == 2
        );

        Assert.IsTrue(
            shouldBeYatzy.ContainsKey(Categories.yatzy) &&
            shouldBeYatzy.All(item => item.Value == 50) &&
            shouldBeYatzy.Count == 1
        );

        Assert.IsTrue(
            shouldBeOnesFoursAndFourOfAKind.ContainsKey(Categories.ones) &&
            shouldBeOnesFoursAndFourOfAKind.ContainsKey(Categories.fours) &&
            shouldBeOnesFoursAndFourOfAKind.ContainsKey(Categories.four_of_a_kind) &&
            shouldBeOnesFoursAndFourOfAKind.All(item => item.Value == 4) &&
            shouldBeOnesFoursAndFourOfAKind.Count == 3
        );
    }
}
