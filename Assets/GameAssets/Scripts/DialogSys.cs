using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class DialogSys : MonoBehaviour
{
    public string[] lines;
    public Text nameField;
    public string name;
    public Image image;
    public Text dialoguetext;
    public int index;
    [SerializeField] private float speedtext = 0.05f;

    [Header("End Button Settings")]
    [SerializeField] private Button endButton; // Кнопка после диалога
    [SerializeField] private string nextSceneName = "Scene2";

    void Start()
    {
        dialoguetext.text = string.Empty;
        index = 0;
        StartCoroutine(TypeLine());
        nameField.text = name;

        if (endButton != null)
        {
            endButton.gameObject.SetActive(false);
            endButton.onClick.AddListener(LoadNextScene);
        }
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            dialoguetext.text += c;
            yield return new WaitForSeconds(speedtext);
        }
    }

    public void SkipNextClick()
    {
        if (dialoguetext.text == lines[index])
        {
            NextLine();
        }
        else
        {
            StopAllCoroutines();
            dialoguetext.text = lines[index];
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
        }
    }

    private void LoadNextScene()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
            SceneManager.LoadScene(nextSceneName);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            SkipNextClick();
    }
}