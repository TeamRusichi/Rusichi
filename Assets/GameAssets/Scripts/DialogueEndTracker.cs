using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections; // Не забываем эту директиву для IEnumerator

public class DialogueEndTracker : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private DialogSys dialogueSystem; // Ваш скрипт диалога
    [SerializeField] private Button endButton; // Кнопка (изначально скрыта в Unity Editor)

    [Header("Settings")]
    [SerializeField] private string nextSceneName = "NextScene";

    private void Start()
    {
        // Проверяем ссылки
        if (dialogueSystem == null)
        {
            Debug.LogError("DialogueSystem not assigned in DialogueEndTracker!");
            return;
        }

        if (endButton != null)
        {
            endButton.gameObject.SetActive(false); // Скрываем кнопку в начале
            endButton.onClick.AddListener(LoadNextScene);
        }
        else
        {
            Debug.LogError("End Button not assigned in DialogueEndTracker!");
        }

        // Запускаем корутину отслеживания конца диалога
        StartCoroutine(WaitForDialogueEnd());
    }

    // Исправлено: правильно написан IEnumerator
    private IEnumerator WaitForDialogueEnd()
    {
        // Ждем, пока индекс диалога не станет последним
        while (dialogueSystem.index < dialogueSystem.lines.Length - 1)
        {
            yield return null; // Ждем каждый кадр
        }

        // Дополнительная проверка: ждем, пока игрок не "прожимает" последнюю реплику
        while (dialogueSystem.gameObject != null)
        {
            yield return null;
        }

        // Активируем кнопку после уничтожения DialogSys
        if (endButton != null)
        {
            endButton.gameObject.SetActive(true);
            Debug.Log("Dialogue ended - button activated!");
        }
    }

    private void LoadNextScene()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogWarning("Next scene name is not set in DialogueEndTracker!");
        }
    }
}