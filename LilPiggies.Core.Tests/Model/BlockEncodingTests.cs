namespace LilPiggies.Core.Tests.Model;

using Core.Model;

public class BlockEncodingTests
{
    [Theory]
    [InlineData(BlockType.Start, Rotation.None, 'A')]
    [InlineData(BlockType.Curve, Rotation._90, 'n')]
    [InlineData(BlockType.Straight, Rotation._180, 'c')]
    [InlineData(BlockType.Start, Rotation._270, 'D')]
    public void Encode_Block_AreEqual(BlockType type, Rotation rotation, char encoded)
    {
        var block1 = new Block(type, rotation);

        Assert.Equal(encoded, Block.Encode(block1));
    }

    [Theory]
    [InlineData('A', BlockType.Start, Rotation.None)]
    [InlineData('n', BlockType.Curve, Rotation._90)]
    [InlineData('c', BlockType.Straight, Rotation._180)]
    [InlineData('D', BlockType.Start, Rotation._270)]
    public void Parse_Block_AreEqual(char encoded, BlockType type, Rotation rotation)
    {
        var block = Block.Parse(encoded);

        Assert.Equal(type, block.Type);
        Assert.Equal(rotation, block.Rotation);
    }
}