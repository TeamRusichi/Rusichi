using UnityEngine;
using UnityEngine.SceneManagement;

public class IlyaMuromets : MonoBehaviour, IQuestObject
{
    public void OnPlayerApproach()
    {
        SceneManager.LoadScene("GameBow");
    }
}
