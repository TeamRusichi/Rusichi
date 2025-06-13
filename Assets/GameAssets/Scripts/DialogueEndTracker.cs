using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections; // �� �������� ��� ��������� ��� IEnumerator

public class DialogueEndTracker : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private DialogSys dialogueSystem; // ��� ������ �������
    [SerializeField] private Button endButton; // ������ (���������� ������ � Unity Editor)

    [Header("Settings")]
    [SerializeField] private string nextSceneName = "NextScene";

    private void Start()
    {
        // ��������� ������
        if (dialogueSystem == null)
        {
            Debug.LogError("DialogueSystem not assigned in DialogueEndTracker!");
            return;
        }

        if (endButton != null)
        {
            endButton.gameObject.SetActive(false); // �������� ������ � ������
            endButton.onClick.AddListener(LoadNextScene);
        }
        else
        {
            Debug.LogError("End Button not assigned in DialogueEndTracker!");
        }

        // ��������� �������� ������������ ����� �������
        StartCoroutine(WaitForDialogueEnd());
    }

    // ����������: ��������� ������� IEnumerator
    private IEnumerator WaitForDialogueEnd()
    {
        // ����, ���� ������ ������� �� ������ ���������
        while (dialogueSystem.index < dialogueSystem.lines.Length - 1)
        {
            yield return null; // ���� ������ ����
        }

        // �������������� ��������: ����, ���� ����� �� "���������" ��������� �������
        while (dialogueSystem.gameObject != null)
        {
            yield return null;
        }

        // ���������� ������ ����� ����������� DialogSys
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
            // TODO: Flashbang + delay
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogWarning("Next scene name is not set in DialogueEndTracker!");
        }
    }
}