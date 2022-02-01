using UnityEngine;

public class MainController : MonoBehaviour
{
    private GameController gameController = new GameController();

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        gameController.Init();
    }
}