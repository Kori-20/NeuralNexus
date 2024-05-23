using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager thisInstance;
    public static GameManager Instance => thisInstance;

    private float initialTimeFlow = 1f;
    private float currentTimeFlow = 1f;

    private bool bIsPaused = false;

    private void Awake()
    {
        if (thisInstance == null)
        {
            thisInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RestartLevel()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
        ResumeTimeDefault();
    }

    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    public void PauseTime()
    {
        Time.timeScale = 0;
        bIsPaused = true;
    }

    public void ResumeTimeDefault()
    {
        Time.timeScale = initialTimeFlow;
        bIsPaused = false;
    }

    public void ResumeTime()
    {
        Time.timeScale = currentTimeFlow;
        bIsPaused = false;
    }

    public void SetTime(float newTime)
    {
        Time.timeScale = newTime;
        currentTimeFlow = newTime;
    }

    public bool IsPaused()
    {
        return bIsPaused;
    }
}
