using System.Linq;
using UnityEngine;

public class ObjectPoints
{
    private ObjectPoint[] objectPoints = null;

    private ObjectPoint[] Points
    {
        get
        {
            return objectPoints;
        }
    }

    public ObjectPoints(int count)
    {
        objectPoints = new ObjectPoint[count];
    }

    public void AddPoint(ObjectPoint objectPoint, int number)
    {
        objectPoints[number] = objectPoint;
    }
    
    public ObjectPoint GetPoint(PointPosition pointPosition)
    {
        return objectPoints.Where(x => x.PointPosition.Id == pointPosition.Id).FirstOrDefault();
    }
}