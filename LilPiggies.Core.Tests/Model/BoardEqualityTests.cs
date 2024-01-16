namespace LilPiggies.Core.Tests.Model;

using Core.Model;

public class BoardEqualityTests
{
    [Theory]
    [InlineData("6b3be956-6a19-4a2c-a29b-1f4b0dc76bbb", "8a376ea9-ef0a-4195-9d2f-1688791fc11d", 3, 4)]
    public void GetHashCode_SameComposition_AreEqual(string id1, string id2, int width, int height)
    {
        var board1 = new Board(new Guid(id1), width, height);
        var board2 = new Board(new Guid(id2), width, height);

        Assert.Equal(board1.GetHashCode(), board2.GetHashCode());
    }

    [Theory]
    [InlineData("6b3be956-6a19-4a2c-a29b-1f4b0dc76bbb", 3, 4)]
    public void GetHashCode_SameCompositionButDifferentWidth_AreNotEqual(string id, int width, int height)
    {
        var board1 = new Board(new Guid(id), width, height);
        var board2 = new Board(new Guid(id), width + 1, height);

        Assert.NotEqual(board1.GetHashCode(), board2.GetHashCode());
    }

    [Theory]
    [InlineData("6b3be956-6a19-4a2c-a29b-1f4b0dc76bbb", 3, 4)]
    public void GetHashCode_SameCompositionButDifferentHeight_AreNotEqual(string id, int width, int height)
    {
        var board1 = new Board(new Guid(id), width, height);
        var board2 = new Board(new Guid(id), width, height +1);

        Assert.NotEqual(board1.GetHashCode(), board2.GetHashCode());
    }
}