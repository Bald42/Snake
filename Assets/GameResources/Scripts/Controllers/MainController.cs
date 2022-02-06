using UnityEngine;
using DataBase;

public class MainController : MonoBehaviour
{
    [SerializeField] private int width = 0;
    [SerializeField] private int height = 0;
    [SerializeField] private SceneLinks sceneLinks = null;

    private Spawner spawner = null;

    private static MainController instance = null;

    private GameController gameController = new GameController();

    public static MainController Instance
    {
        get
        {
            return instance;
        }
    }

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

    public Spawner Spawner
    {
        get
        {
            return spawner;
        }
    }

    public SceneLinks SceneLinks
    {
        get
        {
            return sceneLinks;
        }
    }

    private void Awake()
    {
        DataBases.Init();
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        instance = this;
        InitSpawner();
        gameController.Init();
    }

    private void InitSpawner()
    {
        spawner = new Spawner();
        spawner.Init(gameObject);
    }
}