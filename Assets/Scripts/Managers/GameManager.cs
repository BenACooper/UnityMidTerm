using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private LevelManager[] levels;

    public static GameManager instance;

    private GameState currentState;
    private LevelManager currentLevel;
    private int currentLevelIndex = 0;

    public enum GameState
    {
        Briefing,
        LevelStart,
        LevelIn,
        LevelEnd,
        GameOver,
        GameEnd
    }

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(instance);
            return;
        }

        instance = this;
    }

    private void Start()
    {
        if(levels.Length > 0)
        {
            ChangeState(GameState.Briefing, levels[currentLevelIndex]);
        }
    }

    public void ChangeState(GameState state, LevelManager level)
    {
        currentState = state;
        currentLevel = level;

        switch (currentState)
        {
            case GameState.Briefing:
                StartBriefing();
                break;
            case GameState.LevelStart:
                InitiateLevel();
                break;
            case GameState.LevelIn:
                RunLevel();
                break;
            case GameState.LevelEnd:
                CompleteLevel();
                break;
            case GameState.GameOver:
                GameOver();
                break;
            case GameState.GameEnd:
                GameEnd();
                break;
            default:
                break;
        }
    }
private void StartBriefing()
    {
        Debug.Log("Briefing started...");
        ChangeState(GameState.LevelStart, currentLevel);
    }

    private void InitiateLevel()
    {
        Debug.Log("Level started...");
        currentLevel.StartLevel();
        ChangeState(GameState.LevelIn, currentLevel);
    }

    private void RunLevel()
    {
        Debug.Log("Level running: " + currentLevel.gameObject.name);
    }
    
    private void CompleteLevel()
    {
        Debug.Log("Level ended...");
        //Go to the next level.
        if (currentLevelIndex + 1 < levels.Length)
        {
            ChangeState(GameState.LevelStart, levels[++currentLevelIndex]);
        }
        else
        {
            ChangeState(GameState.GameEnd, currentLevel); // Game end if no more levels
        }
    }

    private void GameOver()
    {
        Debug.Log("Game over, you lose!");
    }

    private void GameEnd()
    {
        Debug.Log("Game end, you win!");
    }

    public void RestartLevel()
    {
        // Reloads the currently active scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
