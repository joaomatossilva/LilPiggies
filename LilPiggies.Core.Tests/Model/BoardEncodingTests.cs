namespace LilPiggies.Core.Tests.Model;

using Core.Model;

public class BoardEncodingTests
{
    [Fact]
    public void AsEncoded_Board3_IsSame()
    {
        string expected = "AaE";
        var board = new Board(Guid.NewGuid(), 1, 3);

        board.Blocks[0] = new Block(BlockType.Start, Rotation.None);
        board.Blocks[1] = new Block(BlockType.Straight, Rotation.None);
        board.Blocks[2] = new Block(BlockType.House, Rotation.None);

        var encoded = board.AsEncoded();

        Assert.Equal(expected, encoded);
    }

    [Fact]
    public void AsEncoded_Board9_IsSame()
    {
        string expected = "A==a==E==";
        var board = new Board(Guid.NewGuid(), 3, 3);

        board.Blocks[0] = new Block(BlockType.Start, Rotation.None);
        board.Blocks[3] = new Block(BlockType.Straight, Rotation.None);
        board.Blocks[6] = new Block(BlockType.House, Rotation.None);

        var encoded = board.AsEncoded();

        Assert.Equal(expected, encoded);
    }

    [Fact]
    public void AsEncoded_BoardEmpty_IsSame()
    {
        string expected = "=========";
        var board = new Board(Guid.NewGuid(), 3, 3);

        var encoded = board.AsEncoded();

        Assert.Equal(expected, encoded);
    }
}