using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToChoice : MonoBehaviour
{
 

    private void Start()
    {
        DialogSys_old dialogueSystem = FindObjectOfType<DialogSys_old>();
        if (dialogueSystem != null)
        {
            dialogueSystem.OnDialogueEnd += HandleDialogueEnd;
        }
    }

    private void LoadScene(string sceneName)
    {
        Debug.Log(sceneName);
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    private void OnDestroy()
    {
        
        DialogSys_old dialogueSystem = FindObjectOfType<DialogSys_old>();
        if (dialogueSystem != null)
        {
            dialogueSystem.OnDialogueEnd -= HandleDialogueEnd;
        }
    }
    private void HandleDialogueEnd()
    {
        LoadScene("Choice");
    }
}
