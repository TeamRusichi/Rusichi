using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Choice : MonoBehaviour
{
    public Button Proriv;
    public Button Obhod;

    private void Start()
    {
        // �������� �� �������
        DialogSys_old dialogueSystem = FindObjectOfType<DialogSys_old>();
        if (dialogueSystem != null)
        {
            dialogueSystem.OnDialogueEnd += HandleDialogueEnd;
        }

        // ��������� ������
        MakeButtonInvisible(Proriv);
        MakeButtonInvisible(Obhod);

        // ��������� ����������� �������
        Proriv.onClick.AddListener(() => LoadScene("ProrivScene"));
        Obhod.onClick.AddListener(() => LoadScene("ObhodScene"));
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
        // ����� ���������� �� ������� ��� ����������� �������
        DialogSys_old dialogueSystem = FindObjectOfType<DialogSys_old>();
        if (dialogueSystem != null)
        {
            dialogueSystem.OnDialogueEnd -= HandleDialogueEnd;
        }
    }

    // ���������� ������� ��������� �������
    private void HandleDialogueEnd()
    {
        MakeButtonVisible(Proriv);
        MakeButtonVisible(Obhod);
    }

    public void MakeButtonInvisible(Button button)
    {
        if (button == null) return;

        Image buttonImage = button.GetComponent<Image>();
        if (buttonImage != null)
        {
            buttonImage.color = new Color(1, 1, 1, 0);
            buttonImage.raycastTarget = false;
        }

        TextMeshProUGUI text = button.GetComponentInChildren<TextMeshProUGUI>();
        if (text != null)
        {
            text.color = new Color(1,1,1,0);
            text.raycastTarget = false;
        }
    }

    public void MakeButtonVisible(Button button)
    {
        if (button == null) return;

        Image buttonImage = button.GetComponent<Image>();
        if (buttonImage != null)
        {
            buttonImage.color = new Color(1, 1, 1, 1);
            buttonImage.raycastTarget = true;
        }

        TextMeshProUGUI text = button.GetComponentInChildren<TextMeshProUGUI>();
        if (text != null)
        {
            text.color = new Color(0, 0, 0);
            text.raycastTarget = true;
        }
    }
}