using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class SpriteLanguageChangeable : LanguageChangeable
{
    [Header("Settings")]

   
    [SerializeField] Sprite _englishText;
  
   
    [SerializeField] Sprite _spanishText;



    Image _textImage;

    void Awake()
    {
        _textImage = GetComponent<Image>();
    }

    private void Start()
    {

    }

    public override void ChangeLanguage(Language language)
    {
        switch (language)
        {
            case Language.English:
                _textImage.sprite = _englishText;

             
                break;
            case Language.Spanish:
                _textImage.sprite = _spanishText;

                
                break;
            default:
                break;
        }


    }

    [ContextMenu("Set Text from editor")]
    void SetText()
    {
        if (!_textImage) _textImage = GetComponent<Image>();

        ChangeLanguage(Language.English);
    }
}