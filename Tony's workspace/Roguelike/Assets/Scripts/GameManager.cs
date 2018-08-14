using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public float levelStartDelay = 2f;
    public float turnDelay = .1f;
    public static GameManager instance = null;
    public BoardManager boardScript;
    public int playerFoodPoints = 100;
    [HideInInspector] public bool playersTurn = true;


    private bool jeg =false;
    private Text levelText;
    private GameObject levelImage;
    private int level = 0;
    private List<Enemy> enemies;
    private bool enemiesMoving;
    private bool doingSetup;

    void Awake()
    {
        FindObjectOfType<Player>().enabled = true;
        Debug.Log("I AM AWAKE!");
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        enemies = new List<Enemy>();
        boardScript = GetComponent<BoardManager>();
        InitGame();
    }
    /*
    [RuntimeInitializeOnLoadMethod]
    static void OnRuntimeMethodLoad()
    {
        level++;

        InitGame();
    }*/
    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if (jeg == true)
        {
            level++;

            InitGame();
        }
        else
        {
            jeg = true;
        }
    }

    void OnDisable()
    {
        Debug.Log("");
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void OnEnable()
    {
        Debug.Log("ENABLE");
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }
    
    /*
    private void OnLevelWasLoaded(int index)
    {
        Debug.Log("WAS LOADED!");
        level++;

        InitGame();
    }*/
    void InitGame()
    {
        doingSetup = true;

        levelImage = GameObject.Find("LevelImage");
        levelText = GameObject.Find("LevelText").GetComponent<Text>();
        levelText.text = "Day " + level;
        levelImage.SetActive(true);
        Invoke("HideLevelImage", levelStartDelay);

        enemies.Clear();
        boardScript.SetupScene(level);
    }

    private void HideLevelImage()
    {
        levelImage.SetActive(false);
        doingSetup = false;
    }
    public void GameOver()
    {
        levelText.text = "After " + level + " days, you starved.";
        levelImage.SetActive(true);
        enabled = false;
    }

    IEnumerator MoveEnemies()
    {
        enemiesMoving = true;
        yield return new WaitForSeconds(turnDelay);
        if (enemies.Count == 0)
        {
            yield return new WaitForSeconds(turnDelay);
        }

        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].MoveEnemy();
            yield return new WaitForSeconds(enemies[i].moveTime);
        }

        playersTurn = true;
        enemiesMoving = false;
    }
    // Use this for initialization
    void Start()
    {
        Debug.Log("IS STARTED!!!");
    }

    // Update is called once per frame
    void Update()
    {
        if (playersTurn || enemiesMoving || doingSetup)
        {
            return;
        }

        StartCoroutine(MoveEnemies());

	}

    public void AddEnemyToList(Enemy script)
    {
        enemies.Add(script);
    }
}
