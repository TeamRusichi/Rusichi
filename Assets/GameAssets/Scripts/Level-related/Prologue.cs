using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource), typeof(Image))]
public class Prologue : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    [SerializeField] private float fadeSpeed = 0.5f; // Скорость увеличения альфа-канала
    
    private AudioSource _audioSource;
    private Image _image;
    private float _currentAlpha = 0f;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _image = GetComponent<Image>();
        _audioSource.playOnAwake = false;
        
        // Начальные настройки Image
        _image.color = new Color(1, 1, 1, 0); // Полностью прозрачный
    }

    private void OnEnable()
    {
        _currentAlpha = 0f;
        _image.color = new Color(1, 1, 1, 0);
        
        if (_audioSource.clip != null)
        {
            _audioSource.Play();
            Invoke(nameof(LoadNextScene), _audioSource.clip.length);
        }
        else
        {
            Debug.LogError("AudioClip не назначен!");
            LoadNextScene();
        }
    }

    private void Update()
    {
        if (_currentAlpha < 1f)
        {
            _currentAlpha += fadeSpeed * Time.deltaTime;
            _image.color = new Color(1, 1, 1, _currentAlpha);
        }
    }

    private void LoadNextScene()
    {
        if (!string.IsNullOrEmpty(sceneToLoad))
            SceneManager.LoadScene(sceneToLoad);
        else
            Debug.LogError("Сцена не указана!");
    }
}
