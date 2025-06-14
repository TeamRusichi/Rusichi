using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToChoice : MonoBehaviour
{
 

    private void Start()
    {
        DialogSys dialogueSystem = FindObjectOfType<DialogSys>();
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
        
        DialogSys dialogueSystem = FindObjectOfType<DialogSys>();
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
