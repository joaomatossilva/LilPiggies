namespace LilPiggies.Core.Model;

using System.Text;

public class Board
{
    public Guid Id { get; set; }
    public Block[] Blocks { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }

    public Board(Guid id, int width, int height)
    {
        this.Id = id;
        this.Width = width;
        this.Height = height;
        this.Blocks = new Block[width * height];

    }

    public Board(Guid id, int width, int height, string encodedBlocks)
        : this(id, width, height)
    {
        LoadBlocksFromEncoded(encodedBlocks);
    }

    private void LoadBlocksFromEncoded(string encoded)
    {
        if (encoded.Length != this.Blocks.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(encoded),
                "The length of encoded is not compatible with the blocks size");
        }

        for (var index = 0; index < encoded.Length; index++)
        {
            var @char = encoded[index];
            var block = Block.Parse(@char);
            this.Blocks[index] = block;
        }
    }

    public string AsEncoded()
    {
        var sb = new StringBuilder(this.Blocks.Length);
        foreach (var block in Blocks)
        {
            sb.Append(Block.Encode(block));
        }

        return sb.ToString();
    }

    public Block GetBlockAt(int x, int y)
    {
        if (x < 0 || x > Width)
            throw new ArgumentOutOfRangeException(nameof(x));
        if (y < 0 || y > Height)
            throw new ArgumentOutOfRangeException(nameof(y));

        var index = y * Width + x;
        if (index >= Blocks.Length)
        {
            throw new IndexOutOfRangeException("Computed index is out of bounds");
        }

        return Blocks[index];
    }

    public override int GetHashCode()
    {
        var code = HashCode.Combine(Width, Height);
        foreach (var block in Blocks)
        {
            code = HashCode.Combine(code, block is null ? 24 : block.GetHashCode());
        }

        return code;
    }
}