using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

using UnityEngine.Audio;

public class SettingsMenuManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] AudioMixer _audioMixer;

    [Header("Sliders")]

    [SerializeField] private Slider _SFX;
    [SerializeField] private Slider _master;
    [SerializeField] private Slider _music;

    [Header("Buttons")]
    [SerializeField] private Button _menu;



    [Header("Languages")]
    [SerializeField] Button _englishButton;
    [SerializeField] Button _spanishButton;

    [Header("Settings Title References")]
    [SerializeField] Sprite _englishTitle;
    [SerializeField] Sprite _spanishTitle;
    [SerializeField] Image _titleRenderer;

    
    const string MasterAudioKey = "Master";
    const string MusicAudioKey = "Music";
    const string SFXAudioKey = "SFX";

    
    LanguageContext _languageContext;

    const float MaxAudioValue = 1f;
    const float MinAudioValue = 0.001f;

    public static SettingsMenuManager instance;
    private void Awake() {

        //Singleton
            if(instance == null) {

                instance =this;
                DontDestroyOnLoad(this.gameObject);
                
            } else Destroy(this.gameObject);
        //Find language context
            _languageContext = FindObjectOfType<LanguageContext>();

        //Eng button
        _englishButton.onClick.AddListener(() =>
            {
                SetLanguage(Language.English);
            });

        //castillian button
            _spanishButton.onClick.AddListener(() =>
            {
                SetLanguage(Language.Spanish);
            });

            SetSliders();
    }

      public void SetSliders()
    {
        SetSlider(_master, SetMasterVolume, MasterAudioKey);
        SetSlider(_music, SetMusicVolume, MusicAudioKey);
        SetSlider(_SFX, SetSFXVolume, SFXAudioKey);
    }

    void SetSlider(Slider slider, UnityAction<float> onValueChanged, string groupKey)
    {
        slider.onValueChanged.AddListener(onValueChanged);

        slider.minValue = MinAudioValue;
        slider.maxValue = MaxAudioValue;
        float value;
        _audioMixer.GetFloat(groupKey, out value);
         value = Mathf.Pow(10,value/20);
        slider.value = value;
    }
      public void SetMasterVolume(float newVolume)
    {
        _audioMixer.SetFloat(MasterAudioKey, Mathf.Log10(newVolume)*20);
    }

    public void SetMusicVolume(float newVolume)
    {
        _audioMixer.SetFloat(MusicAudioKey, Mathf.Log10(newVolume)*20);
    }

    public void SetSFXVolume(float newVolume)
    {
        _audioMixer.SetFloat(SFXAudioKey, Mathf.Log10(newVolume)*20);
    }

   public void SetLanguage(Language language)
    {
        switch (language)
        {
            case Language.English:
                Debug.Log("English!");
                _languageContext.ChangeLanguage(Language.English);
                _titleRenderer.sprite = _englishTitle;
                break;
            case Language.Spanish:
                Debug.Log("Spanish!");
                _languageContext.ChangeLanguage(Language.Spanish);
                _titleRenderer.sprite = _spanishTitle;
                break;
        }
    }

 
    
}