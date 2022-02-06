using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    private int id = 0;
    private ActionController actionController = null;

    public void Init(int id)
    {
        this.id = id;
        Spawner.SpawnGameObjectWithMonoBehaviour<SnakeController>(MainController.Instance.gameObject, $"SnakeController {id}");
#if UNITY_EDITOR
        actionController = new EditorActionController();
#else
        // TODOOO пока нет других контроллеров
        actionController = new EditorActionController();
#endif
        actionController.Init();
    }
}