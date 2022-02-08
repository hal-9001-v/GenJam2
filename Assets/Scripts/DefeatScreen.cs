using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class DefeatScreen : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Button _retryButton;

    CanvasGroup _canvasGroup;
    
    public Action onRetryCallback;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        Hide();

        _retryButton.onClick.AddListener(Retry);
    }
    
    public void Display()
    {
        _canvasGroup.alpha = 1;
        _canvasGroup.blocksRaycasts = true;

    }

    public void Hide()
    {
        _canvasGroup.alpha = 0;
        _canvasGroup.blocksRaycasts = false;
    }

    public void Retry()
    {
        Hide();

        if (onRetryCallback != null)
        {
            onRetryCallback.Invoke();
        }

    }
}