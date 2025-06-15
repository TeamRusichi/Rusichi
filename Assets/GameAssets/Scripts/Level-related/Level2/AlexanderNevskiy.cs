using UnityEngine;
using UnityEngine.SceneManagement;

public class AlexanderNevskiy : MonoBehaviour, IQuestObject
{
    public void OnPlayerApproach()
    {
        Debug.Log("Nevskiy interaction fired");
        SceneManager.LoadScene("Dragndrop minigame");
    }
}
