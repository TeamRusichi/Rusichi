using UnityEngine;
using UnityEngine.SceneManagement;

public class Suvorov : MonoBehaviour, IQuestObject
{
    public void OnPlayerApproach()
    {
        SceneManager.LoadScene("Choice");
    }
}
