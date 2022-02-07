using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StartMenu : MonoBehaviour
{
    [Header("ButtonReferences")]
    [SerializeField] Button _playButton;
    [SerializeField] Button _exitButton;
    [SerializeField] Button _settingsButton;
    [SerializeField] Button _tutorialButton;

    [Header("CanvasGroups")]
    [SerializeField] CanvasGroup _mainMenu;
    [SerializeField] CanvasGroup _tutorialMenu;
    [SerializeField] CanvasGroup _settingsMenu;
  

    LevelLoader _levelLoader;

    private void Awake()
    {
        _levelLoader = FindObjectOfType<LevelLoader>();

        _playButton.onClick.AddListener(LoadPlayLevel);
        _exitButton.onClick.AddListener(Application.Quit);
        _tutorialButton.onClick.AddListener(ShowTutorial);
        _settingsButton.onClick.AddListener(ShowSettings);

    }

    void LoadPlayLevel()
    {
        _levelLoader.LoadLevel(_levelLoader.FirstGameLevel);
    }

    void ShowTutorial()
    {
        _tutorialMenu.transform.DOLocalMove(Vector3.zero, 2);
        _mainMenu.interactable = false;
        _mainMenu.alpha = 0;
    }

    void ShowSettings()
    {
        _settingsMenu.transform.DOLocalMove(Vector3.zero, 2);
        _mainMenu.interactable = false;
        _mainMenu.alpha = 0;
    }

    void HideSettings()
    {
        _settingsMenu.transform.DOLocalMove(new Vector3(-2000,0,0), 2);
        _mainMenu.interactable = true;
        _settingsMenu.interactable = false;
        _mainMenu.alpha = 1;
    }
   
    void HideTutorial()
    {
        _settingsMenu.transform.DOLocalMove(new Vector3(2000,0,0), 2);
        _mainMenu.interactable = true;
        _tutorialMenu.interactable = false;
        _mainMenu.alpha = 1;
    }

}
