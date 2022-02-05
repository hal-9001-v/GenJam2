using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    [Header("References")]
    [SerializeField] DrawingObject _centerDraw;
    [SerializeField] Health _playerHealth;

    private void Start()
    {
        _centerDraw.StartDraw(NextLevel);

        _playerHealth.deadAction += Defeat;
    }

    void NextLevel()
    {
        Debug.Log("End!");
    }

    void Defeat()
    {

    }
}
