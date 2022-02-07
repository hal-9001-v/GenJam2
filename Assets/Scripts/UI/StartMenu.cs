using UnityEngine;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Button _playButton;

    LevelLoader _levelLoader;

    private void Awake()
    {
        _levelLoader = FindObjectOfType<LevelLoader>();

        _playButton.onClick.AddListener(LoadPlayLevel);
    }

    void LoadPlayLevel()
    {
        _levelLoader.LoadLevel(_levelLoader.FirstGameLevel);
    }
}
