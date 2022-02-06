using UnityEngine;
using DataBase;

public class Spawner : MonoBehaviour
{
    public void Init(GameObject parent)
    {
        GameObject emptyGameObject = Instantiate(new GameObject(), parent.transform);
        emptyGameObject.name = "Spawner";
        emptyGameObject.AddComponent<Spawner>();
    }

    public Point SpawnPoints(int width, int height)
    {
        Point point = Instantiate(DataBases.GameResourcesWrapper.Point,
                                  new Vector3(width, 0f, height),
                                  Quaternion.identity,
                                  MainController.Instance.SceneLinks.Location);
        point.name = point.name.ReplaceClone($" ({width},{height})");
        return point;
    }
}