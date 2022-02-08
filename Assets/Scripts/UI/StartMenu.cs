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
    [SerializeField] public CanvasGroup _settingsMenu;
  

    LevelLoader _levelLoader;

    private void Awake()
    {
        _levelLoader = FindObjectOfType<LevelLoader>();

        _playButton.onClick.AddListener(LoadPlayLevel);
        _exitButton.onClick.AddListener(Application.Quit);
        _tutorialButton.onClick.AddListener(ShowTutorial);
      

    }
    private void Start()
    {
        _settingsMenu = FindObjectOfType<SettingsMenuManager>().ThisGroup;

        _settingsButton.onClick.AddListener(ShowSettings);
    }
    void LoadPlayLevel()
    {
        FindObjectOfType<AudioManager>().Play("MenuBop");
        _levelLoader.LoadLevel(_levelLoader.FirstGameLevel);
    }

    void ShowTutorial()
    {
        _tutorialMenu.transform.DOLocalMove(Vector3.zero, 2);
        FindObjectOfType<AudioManager>().Play("MenuBop");
        _tutorialMenu.interactable = true;
        _mainMenu.interactable = false;
        _mainMenu.alpha = 0;
    }

    void ShowSettings()
    {
        FindObjectOfType<AudioManager>().Play("MenuBop");
        _settingsMenu.transform.DOLocalMove(Vector3.zero, 2);
        _settingsMenu.interactable = true;
        _mainMenu.interactable = false;
        _mainMenu.alpha = 0;
    }

    public void HideSettings()
    {
        FindObjectOfType<AudioManager>().Play("MenuBop");
        _settingsMenu.transform.DOLocalMove(new Vector3(-2000,0,0), 2);
        _mainMenu.interactable = true;
        _settingsMenu.interactable = false;
        _mainMenu.alpha = 1;
    }
   
    public void HideTutorial()
    {
        FindObjectOfType<AudioManager>().Play("MenuBop");
        _tutorialMenu.transform.DOLocalMove(new Vector3(2000,0,0), 2);
        _mainMenu.interactable = true;
        _tutorialMenu.interactable = false;
        _mainMenu.alpha = 1;
    }

}
