using System;

public class Grid<T>
{
    private readonly T[] cells;
    public int Width { get; }
    public int Height { get; }

    public Grid(int width, int height)
    {
        Width = width;
        Height = height;
        cells = new T[Width * Height];
    }

    public T GetCell(int x, int y)
    {
        if (x < 0 || x > Width - 1)
            throw new IndexOutOfRangeException("x is not in width");
        if (y < 0 || y > Height - 1)
            throw new IndexOutOfRangeException("y is not in height");

        return cells[Width * y + x];
    }

    public void SetCell(int x, int y, T cell)
    {
        if (x < 0 || x > Width - 1)
            throw new IndexOutOfRangeException("x is not in range of width");
        if (y < 0 || y > Height - 1)
            throw new IndexOutOfRangeException("y is not in range of height");

        cells[Width * y + x] = cell;
    }

    public void ForEach(Action<int, int> action)
    {
        int column, row = 0;
        for (int i = 0; i < cells.Length; i++)
        {
            column = i % Width;
            action(column, row);
            if (column == Width - 1)
                row++;
        }
    }
}
