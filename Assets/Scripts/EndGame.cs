using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    [Header("References")]
    [SerializeField] DrawingObject _centerDraw;
    [SerializeField] Health _playerHealth;


    NextLevelScreen _nextLevelScreen;
    DefeatScreen _defeatScreen;

    public Action retryCallback { get { return _defeatScreen.onRetryCallback; } set { _defeatScreen.onRetryCallback = value; } }
    public Action endGameCallback;

    private void Awake()
    {
        _nextLevelScreen = FindObjectOfType<NextLevelScreen>();
        _defeatScreen = FindObjectOfType<DefeatScreen>();

    }

    private void Start()
    {
        _defeatScreen.onRetryCallback += StartGame;
        _playerHealth.deadAction += Defeat;

        StartGame();
    }

    void StartGame()
    {
        _centerDraw.StartDraw(NextLevel);
    }

    void NextLevel()
    {
        if (endGameCallback != null)
        {
            endGameCallback.Invoke();
        }
        RemoveDefenses();

        _nextLevelScreen.Display();
    }

    void Defeat()
    {
        if (endGameCallback != null)
        {
            endGameCallback.Invoke();
        }
        RemoveDefenses();

        _centerDraw.PauseDraw();

        _defeatScreen.Display();
    }

    void RemoveDefenses()
    {
        foreach (var defense in FindObjectsOfType<BaseDefense>())
        {
            Destroy(defense.gameObject);
        }
    }
}
