using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AudioController9000 : MonoBehaviour
{
    [Header("UI Elements")]
    public Button gearButton;
    public Slider voiceSlider;
    public Slider soundSlider;
    public Image voiceImage;
    public Image soundImage;
    public Canvas settingsCanvas;
    public Button continueButton;
    public Button exitButton;

    [Header("Audio Sources")]
    public AudioSource[] voiceSources;
    public AudioSource[] soundSources;

    [Header("Settings")]
    public float defaultVoiceVolume = 0.75f;
    public float defaultSoundVolume = 0.75f;

    private bool isSettingsVisible = false;
    private const string VOICE_VOLUME_KEY = "VoiceVolume";
    private const string SOUND_VOLUME_KEY = "SoundVolume";

    void Start()
    {
        // Инициализация кнопок
        gearButton.onClick.AddListener(ToggleSettings);
        continueButton.onClick.AddListener(ContinueGame);
        exitButton.onClick.AddListener(ExitGame);

        // Настройка слайдеров
        voiceSlider.onValueChanged.AddListener(delegate { UpdateVoiceVolume(); });
        soundSlider.onValueChanged.AddListener(delegate { UpdateSoundVolume(); });

        // Загрузка сохраненных настроек громкости
        voiceSlider.value = PlayerPrefs.GetFloat(VOICE_VOLUME_KEY, defaultVoiceVolume);
        soundSlider.value = PlayerPrefs.GetFloat(SOUND_VOLUME_KEY, defaultSoundVolume);

        // Применение начальных настроек громкости
        UpdateVoiceVolume();
        UpdateSoundVolume();

        // Скрываем панель настроек при старте
        HideSettings();
    }

    public void ToggleSettings()
    {
        if (isSettingsVisible)
        {
            HideSettings();
        }
        else
        {
            ShowSettings();
        }
    }

    public void ShowSettings()
    {
        settingsCanvas.gameObject.SetActive(true);
        isSettingsVisible = true;
        Time.timeScale = 0f;
    }

    public void HideSettings()
    {
        settingsCanvas.gameObject.SetActive(false);
        isSettingsVisible = false;
        Time.timeScale = 1f; // Возобновление игры
    }

    public void ContinueGame()
    {
        HideSettings();
    }

    public void ExitGame()
    {
        // Сохраняем настройки перед выходом
        PlayerPrefs.Save();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void UpdateVoiceVolume()
    {
        float volume = voiceSlider.value;

        foreach (AudioSource source in voiceSources)
        {
            if (source != null)
            {
                source.volume = volume;
            }
        }

        PlayerPrefs.SetFloat(VOICE_VOLUME_KEY, volume);
    }

    public void UpdateSoundVolume()
    {
        float volume = soundSlider.value;

        foreach (AudioSource source in soundSources)
        {
            if (source != null)
            {
                source.volume = volume;
            }
        }

        PlayerPrefs.SetFloat(SOUND_VOLUME_KEY, volume);
    }

    public void MuteAllSounds()
    {
        voiceSlider.value = 0;
        soundSlider.value = 0;
        UpdateVoiceVolume();
        UpdateSoundVolume();
    }

    public void UnmuteAllSounds()
    {
        voiceSlider.value = defaultVoiceVolume;
        soundSlider.value = defaultSoundVolume;
        UpdateVoiceVolume();
        UpdateSoundVolume();
    }

    void OnApplicationQuit()
    {
        PlayerPrefs.Save();
    }
}