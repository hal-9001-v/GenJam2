using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class PauseMenu : MonoBehaviour
{
    [Header("Buton References")]
    [SerializeField] Button PauseButton;
    [SerializeField] CanvasGroup PauseGroup;
    [SerializeField] Button ResumeButton;
    [SerializeField] Button QuitButton;
    [SerializeField] Button SettingsButton;
    [SerializeField] Button MenuButton;
    [SerializeField] CanvasGroup blockingGroup;

    [Header("Title Sprites")]
    [SerializeField] Image TitleRenderer;
    [SerializeField] Sprite EngTitle;
    [SerializeField] Sprite SpTitle;
    private LevelLoader _levelLoader;

    SettingsMenuManager settings;

    private void Awake()
    {
        _levelLoader = FindObjectOfType<LevelLoader>();

        PauseButton.onClick.AddListener(() =>
        {
            DisplayPauseMenu();
            FindObjectOfType<AudioManager>().Play("MenuBop");
            Cursor.visible = true;
            PauseGame();
        });
        ResumeButton.onClick.AddListener(() =>
        {
            ResumeGame();
            FindObjectOfType<AudioManager>().Play("MenuBop");
            Cursor.visible = false;
            HidePauseMenu();
            blockingGroup.blocksRaycasts=false;
            PauseButton.enabled = true;
        });
        QuitButton.onClick.AddListener(() =>
        {
            FindObjectOfType<AudioManager>().Play("MenuBop");
            Application.Quit();

        });
        SettingsButton.onClick.AddListener(() =>
        {
            DisplaySettings();
            FindObjectOfType<AudioManager>().Play("MenuBop");
        });
        MenuButton.onClick.AddListener(() =>
        {
            ReturnToMenu();
            FindObjectOfType<AudioManager>().Play("MenuBop");

        });
        

    }

    
    void PauseGame() {

        Time.timeScale = 0f;

    }

    void ResumeGame() {

        Time.timeScale = 1f;
    }

    void DisplayPauseMenu() {
        blockingGroup.blocksRaycasts = true;
        PauseGroup.transform.DOLocalMoveX( 0,2f).SetUpdate(true);
        PauseGroup.interactable = true;
        PauseGroup.blocksRaycasts = true;
        PauseButton.enabled = false;

    }

    void HidePauseMenu() {
        
        PauseGroup.transform.DOLocalMoveX(2000f, 2f).SetUpdate(true) ;
        PauseGroup.interactable = false;
        PauseGroup.blocksRaycasts = false;


    }

    void ReturnToMenu() {

        _levelLoader.LoadLevel(0);

    }

    void DisplaySettings() {
        settings = FindObjectOfType<SettingsMenuManager>();

        if (settings) {
            settings.ThisGroup.transform.DOLocalMoveX(0, 2f).SetUpdate(true);
            settings.ThisGroup.interactable= true;
            settings.ThisGroup.alpha= 1;
            HidePauseMenu();

        }
    }

    public void HideSettings() {

        settings = FindObjectOfType<SettingsMenuManager>();

        if (settings)
        {
            settings.ThisGroup.transform.DOLocalMoveX(-2000f, 2f).SetUpdate(true);
            settings.ThisGroup.interactable = false;
            DisplayPauseMenu();
            
        }
      


    }

    private void Start()
    {
        settings = FindObjectOfType<SettingsMenuManager>();
        settings.returnButton.onClick.AddListener(HideSettings);

    }
    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<LanguageContext>().currentLanguage == Language.English)
        {

            TitleRenderer.sprite = EngTitle;


        }
        else {

            TitleRenderer.sprite = SpTitle;    

        }
    }
}
        
