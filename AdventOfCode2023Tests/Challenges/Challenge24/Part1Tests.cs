using AdventOfCode2023.Challenges.Challenge04;
using AdventOfCode2023.Domain;
using AdventOfCode2023.Domain.Geometries;
using AdventOfCode2023Tests.Extensions;
using AdventOfCode2023Tests.Inputs;
using FluentAssertions;

namespace AdventOfCode2023Tests.Challenges.Challenge04;

public class Part1Tests
{
    [Fact]
    public void Part1Test_Returns_2()
    {
        var input = TestInput.Create<IChallenge24Input, HailStone>("04_1.txt");
        
        var start = new NumberIndex(7, true);
        var end = new NumberIndex(27, true);
        var testArea = new Boundary(new(start, end), new(start, end));

        var result = Parts.Part1(input, testArea);

        result.Should().Be(2);
    }
    
    [Fact]
    public void BorrowedPart1Test_Returns_2()
    {
        var input = TestInput.Create<IChallenge24Input, HailStone>("04_1.txt");
        
        var start = new NumberIndex(7, true);
        var end = new NumberIndex(27, true);
        var testArea = new Boundary(new(start, end), new(start, end));

        var result = Parts.Part1Borrowed(input, testArea);

        result.Should().Be(2);
    }
    
    [Fact]
    public void MapIntersections_WithTestData_IsCorrect()
    {
        var input = TestInput.Create<IChallenge24Input, HailStone>("04_1.txt");
        var hsi = new Parser().Parse(input).Map(hs => new HailStoneIntersection(hs.ToHailStone2()));
        
        var start = new NumberIndex(7, true);
        var end = new NumberIndex(27, true);
        var testArea = new Boundary(new(start, end), new(start, end));

        var result = Parts.MapIntersections(hsi, testArea);

        (int a, int b, bool intersects, bool isFirst, bool inTestArea, double x, double y)[] expecteds = 
        {
            (0,1, true, true, true, 14.333, 15.333),
            (0,2, true, false, true, 11.667, 16.667),
            (0,3, true, false, false, 6.2, 19.4),
            (0,4, false, false, true, 0,0),
            (1,2, false, false, false, 0,0),
            (1,3, true, false, false, -6, -5),
            (1,4, false, false, false, 0, 0),
            (2,3, true, true, false, -2, 3),
            (3,4, false, false, false, 0, 0),
        };

        foreach (var expected in expecteds)
        {
            var a = result[expected.a];
            var b = result[expected.b];
            var intersectionInfo = a.IntersectsWith(b);

            if (expected.intersects)
            {
                intersectionInfo.ShouldBeSome(i =>
                {
                    if (expected.isFirst)
                    {
                        i.order.Should().Be(0);
                    }
                    else
                    {
                        if (expected.inTestArea)
                        {
                            i.order.Should().NotBe(0);
                        }
                    }

                    i.isInBounds.Should().Be(expected.inTestArea);

                    if ((expected.x, expected.y) != (0, 0))
                    {
                        i.intersectionInfo.Point.Should().Be(new PointD(expected.x, expected.y));
                    }
                });
            }
            else
            {
                intersectionInfo.ShouldBeNone();
            }
        }
    }
}