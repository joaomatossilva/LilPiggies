namespace LilPiggies.Core.Tests.Model;

using Core.Model;

public class BlockEqualityTests
{
    [Theory]
    [InlineData(BlockType.Start, Rotation.None)]
    [InlineData(BlockType.Curve, Rotation._90)]
    [InlineData(BlockType.Straight, Rotation._180)]
    [InlineData(BlockType.Start, Rotation._270)]
    public void GetHashCode_SameComposition_AreEqual(BlockType type, Rotation rotation)
    {
        var block1 = new Block(type, rotation);
        var block2 = new Block(type, rotation);

        Assert.Equal(block1.GetHashCode(), block2.GetHashCode());
    }

    [Theory]
    [InlineData(BlockType.Start, Rotation.None, Rotation._90)]
    [InlineData(BlockType.Curve, Rotation._90, Rotation._180)]
    [InlineData(BlockType.Straight, Rotation._180, Rotation._270)]
    [InlineData(BlockType.Start, Rotation._270, Rotation.None)]
    public void GetHashCode_DifferentRotation_AreNotEqual(BlockType type, Rotation rotation1, Rotation rotation2)
    {
        var block1 = new Block(type, rotation1);
        var block2 = new Block(type, rotation2);

        Assert.NotEqual(block1.GetHashCode(), block2.GetHashCode());
    }

    [Theory]
    [InlineData(BlockType.Start, BlockType.Curve, Rotation._90)]
    [InlineData(BlockType.Curve, BlockType.Straight, Rotation._180)]
    [InlineData(BlockType.Straight, BlockType.Start, Rotation._270)]
    [InlineData(BlockType.Start, BlockType.Crossroad, Rotation.None)]
    public void GetHashCode_DifferentType_AreNotEqual(BlockType type1, BlockType type2, Rotation rotation)
    {
        var block1 = new Block(type1, rotation);
        var block2 = new Block(type2, rotation);

        Assert.NotEqual(block1.GetHashCode(), block2.GetHashCode());
    }
}