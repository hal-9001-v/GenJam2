using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class NextLevelScreen : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Button _nextLevelButton;

    CanvasGroup _canvasGroup;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();

        _nextLevelButton.onClick.AddListener(LoadNextScene);

        Hide();
    }

    public void Display()
    {
        _canvasGroup.alpha = 1;
        _canvasGroup.blocksRaycasts = true;
    }

    void Hide()
    {
        _canvasGroup.alpha = 0;
        _canvasGroup.blocksRaycasts = false;
    }

    void LoadNextScene()
    {
        Debug.Log("Next Scene!");
    }

}
