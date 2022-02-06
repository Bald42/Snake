using UnityEngine;

public class Location
{
    private Point[,] points = null;

    public void Init()
    {
        GenerateLocation();
    }

    private void GenerateLocation()
    {
        GeneratePointPositionLocation();
    }

    private void GeneratePointPositionLocation()
    {
        points = new Point[MainController.Instance.Width, MainController.Instance.Height];

        for (int width = 0; width < MainController.Instance.Width; width++)
        {
            for (int height = 0; height < MainController.Instance.Height; height++)
            {
                points[width, height] = SpawnPoints(width, height);
            }
        }
    }

    private Point SpawnPoints(int width, int height)
    {
        Point point = MainController.Instance.Spawner.SpawnPoints(width, height);
        point.Init();
        return point;
    }
}