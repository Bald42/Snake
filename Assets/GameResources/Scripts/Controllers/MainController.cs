using UnityEngine;
using DataBase;
using System;

public class MainController : MonoBehaviour
{
    public Action OnUpdateEvent = null;
    public Action OnDestroyEvent = null;

    [SerializeField] private int width = 0;
    [SerializeField] private int height = 0;
    [SerializeField] private bool hasWall = false;
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

    public GameController GameController
    {
        get
        {
            return gameController;
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

    public bool HasWall
    {
        get
        {
            return hasWall;
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

    private void Update()
    {
        OnUpdateEvent?.Invoke();
    }

    private void OnDestroy()
    {
        OnDestroyEvent?.Invoke();
    }
}