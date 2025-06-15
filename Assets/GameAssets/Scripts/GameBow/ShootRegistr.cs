using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int shotsFired = 0;
    public int hitsCount = 0;
    public const int MaxShots = 6;
    public const int WinCondition = 3;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RegisterShot()
    {
        shotsFired++;
        if (shotsFired >= MaxShots && hitsCount < WinCondition)
        {
            Defeat();
        }
    }

    public void RegisterHit()
    {
        hitsCount++;
        if (hitsCount >= WinCondition)
        {
            Victory();
        }
    }

    private void Victory()
    {
        Debug.Log("Pobeda!");
        SceneManager.LoadScene("VictoryScene");
    }

    private void Defeat()
    {
        Debug.Log("Porajenie!");
        SceneManager.LoadScene("DefeatScene");
    }

    public void ResetGame()
    {
        shotsFired = 0;
        hitsCount = 0;
    }
}