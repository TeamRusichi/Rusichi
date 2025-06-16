using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;

public class DialogSys_old : MonoBehaviour
{
    public string[] lines;
    public Text nameField;
    public string name;
    public Image image;
    public Text dialoguetext;
    public int index;
    [SerializeField] private float speedtext = 0.05f;

    [Header("End Button Settings")]
    [SerializeField] private Button endButton;
    [SerializeField] private string nextSceneName = "Scene2";

    public event Action OnDialogueEnd;

    void Start()
    {
        dialoguetext.text = string.Empty;
        index = 0;
        StartCoroutine(TypeLine());
        nameField.text = name;

        if (endButton != null)
        {
            endButton.gameObject.SetActive(false);
            endButton.onClick.AddListener(() => SceneManager.LoadScene(nextSceneName));
        }
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            dialoguetext.text += c;
            yield return new WaitForSeconds(speedtext);
        }

        // ��������� ����� ������� ����� ������ ������
        if (IsDialogueEnded())
        {
            
            Debug.Log("Dialogue.End");
            OnDialogueEnd?.Invoke();
        }
    }

    public void SkipNextClick()
    {
        if (IsDialogueEnded())
        {
            endButton.gameObject.SetActive(true);
            return;
        }

        if (dialoguetext.text == lines[index])
        {
            NextLine();
        }
        else
        {
            StopAllCoroutines();
            dialoguetext.text = lines[index];

            // ���� ��� ��������� ������
            if (IsDialogueEnded())
            {
                OnDialogueEnd?.Invoke();
            }
        }
    }

    public void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            dialoguetext.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            if (endButton != null)
                endButton.gameObject.SetActive(true);
            OnDialogueEnd?.Invoke();
        }
    }

    public bool IsDialogueEnded()
    {
        Debug.Log($"{index} >= {lines.Length}-1 && {dialoguetext.text}=={lines[index]}");
        Debug.Log($"{index >= lines.Length - 1 && dialoguetext.text == lines[index]}");
        return index >= lines.Length - 1 && dialoguetext.text == lines[index];
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            SkipNextClick();
    }
}