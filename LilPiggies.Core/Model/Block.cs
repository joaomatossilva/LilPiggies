namespace LilPiggies.Core.Model;

public class Block(BlockType type, Rotation rotation)
{
    public static readonly Block Empty = new Block(BlockType.Empty, Rotation.None);

    public BlockType Type { get; } = type;

    public Rotation Rotation { get; } = rotation;

    public override int GetHashCode()
    {
        return HashCode.Combine(Type, Rotation);
    }

    public static Block Parse(char c)
    {
        int startOffset = c - (int)BlockType.Start;
        int rotationOffset = startOffset % 4;

        var type = (BlockType)(c - rotationOffset);
        var rotation = (Rotation)rotationOffset;

        return new Block(type, rotation);
    }

    public static char Encode(Block? block)
    {
        block ??= Empty;
        return (char)((int)block.Type + (int)block.Rotation);
    }
}

public enum BlockType
{
    Empty = 61,
    Start = 65,
    House = 69,
    Obstacle = 73,
    Straight = 97,
    Junction = 101,
    Crossroad = 105,
    Curve = 109,
}

public enum Rotation
{
    None = 0,
    _90 = 1,
    _180 = 2,
    _270 = 3
}