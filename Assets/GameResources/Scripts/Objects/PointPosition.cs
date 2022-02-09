using UnityEngine;

public class PointPosition
{
    private int width = 0;
    private int height = 0;

    public int Width
    {
        get
        {
            return width;
        }
    }

    public int Height
    {
        get
        {
            return height;
        }
    }

    public PointPosition(int width, int height)
    {
        this.width = width;
        this.height = height;
    }
}