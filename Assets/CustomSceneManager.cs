using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomSceneManager : MonoBehaviour
{
    public void SetScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
