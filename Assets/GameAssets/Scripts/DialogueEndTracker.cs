using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class DialogueEndTracker : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private DialogSys_old dialogueSystem;
    [SerializeField] private Button endButton;
    [SerializeField] private Image flashImage; // Добавляем компонент Image для вспышки

    [Header("Settings")]
    [SerializeField] private string nextSceneName = "NextScene";
    [SerializeField] private float flashDuration = 1.0f; // Длительность вспышки
    [SerializeField] private Color flashColor = Color.white; // Цвет вспышки

    private void Start()
    {
        // Настройка вспышки
        if (flashImage != null)
        {
            flashImage.gameObject.SetActive(false);
            flashImage.color = new Color(flashColor.r, flashColor.g, flashColor.b, 0);
        }

        if (dialogueSystem == null)
        {
            Debug.LogError("DialogueSystem not assigned!");
            return;
        }

        if (endButton != null)
        {
            endButton.gameObject.SetActive(false);
            endButton.onClick.AddListener(StartFlashSequence);
        }
        else
        {
            Debug.LogError("End Button not assigned!");
        }

        StartCoroutine(WaitForDialogueEnd());
    }

    private IEnumerator WaitForDialogueEnd()
    {
        while (dialogueSystem.index < dialogueSystem.lines.Length - 1)
        {
            yield return null;
        }

        while (dialogueSystem.gameObject != null)
        {
            yield return null;
        }

        if (endButton != null)
        {
            endButton.gameObject.SetActive(true);
        }
    }

    private void StartFlashSequence()
    {
        StartCoroutine(FlashAndLoadScene());
    }

    private IEnumerator FlashAndLoadScene()
    {
        if (flashImage == null) yield break;

        // Активируем изображение вспышки
        flashImage.gameObject.SetActive(true);

        // Плавное нарастание вспышки
        float elapsedTime = 0f;
        while (elapsedTime < flashDuration / 2)
        {
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / (flashDuration / 2));
            flashImage.color = new Color(flashColor.r, flashColor.g, flashColor.b, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}