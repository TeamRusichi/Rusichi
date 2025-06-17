using System.Collections;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class QuestDialog : MonoBehaviour
{
    [Header("ExternalLinks")]
    [SerializeField] private TMP_Text dialogText;
    
    [Header("Dialogue main settings")]
    [SerializeField] private string[] lines;
    [SerializeField] private AudioClip[] clips;
    [SerializeField] private UnityEvent onDialogueEnd;

    [Header("Undertale Mode")]
    [Description("Make per-char noises on dialog appearance instead of voiceover")]
    [SerializeField] private bool perCharMode;
    [SerializeField] private AudioClip charClip;
    
    [Header("Extra settings")]
    [SerializeField] private float textSpeed = 0.06f;
    
    
    private int _index;
    private AudioSource _audioSource;
    
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.playOnAwake = false;
        
        // Checks if the amounts of voiceovers and dialog lines match.
        if (!perCharMode && lines.Length != clips.Length)
        {
            Debug.LogWarning($"The lines and voiceovers count in the dialog {gameObject.name} must be equal!");
        }
        
        dialogText.text = string.Empty;
        StartDialog();
    }

    void StartDialog()
    {
        _index = 0;
        PerCharacterLineSound();
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[_index])
        {
            dialogText.text += c;
            PerCharSound();
            yield return new WaitForSeconds(textSpeed);
        }
    }

    public void SkipText()
    {
        Debug.Log($"Skipping {gameObject.name}");
        if (dialogText.text == lines[_index]) NextLines();
        else
        {
            StopAllCoroutines();
            dialogText.text = lines[_index];
        }
    }

    void NextLines()
    {
        if (_index < lines.Length - 1)
        {
            _index++;
            PerCharacterLineSound();
            dialogText.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
            
            // calling the script that was bound to start after 
            // the dialog
            onDialogueEnd?.Invoke();
        }
    }

    void PerCharSound()
    {
        if (!perCharMode) return;
        
        _audioSource.PlayOneShot(charClip);
    }

    void PerCharacterLineSound()
    {
        if (perCharMode) return;
        
        _audioSource.Stop();
        if (_index >= lines.Length)
        {
            Debug.LogError("Couldn't find any sound for dialog line!");
        }
        _audioSource.PlayOneShot(clips[_index]);
    }
}
