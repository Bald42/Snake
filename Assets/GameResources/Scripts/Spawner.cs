using UnityEngine;
using DataBase;

public class Spawner : MonoBehaviour
{
    public void Init(GameObject parent)
    {
        SpawnGameObjectWithMonoBehaviour<Spawner>(parent, "Spawner");
    }

    public static void SpawnGameObjectWithMonoBehaviour<T>(GameObject parent, string nameComponent) where T : Component
    {
        GameObject emptyGameObject = new GameObject();
        emptyGameObject.transform.SetParent(parent.transform);
        // TODOOO Непонял, как получить имя из T
        emptyGameObject.name = nameComponent;
        emptyGameObject.AddComponent<T>();
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