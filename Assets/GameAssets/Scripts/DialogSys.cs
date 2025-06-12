using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DialogSys : MonoBehaviour
{
    public string[] lines;
    public string name;
    public Image image;
    public Text dialoguetext;
    public int index;
    [SerializeField] private float speedtext = 0.05f;

    void Start()
    {
        dialoguetext.text = string.Empty;
        index = 0;
        StartCoroutine(TypeLine());
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
            gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SkipNextClick();
        }
    }
}