using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    [Header("UI Buttons")]
    public Button beginButton;
    public Button settingsButton;
    public Button quitButton;

    [Header("Scene Names")]
    public string gameSceneName = "GameScene";
    public string settingsSceneName = "SettingsScene";

    void Start()
    {
        beginButton.onClick.AddListener(BeginAdventure);
        settingsButton.onClick.AddListener(OpenSettings);
        quitButton.onClick.AddListener(ExitGame);
    }

    public void BeginAdventure()
    {
        SceneManager.LoadScene("PrologueScene");

    }

    public void OpenSettings()
    {
       
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("GAME CLSOED OK?");
    }
}
