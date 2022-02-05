using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    [Header("References")]
    [SerializeField] DrawingObject _centerDraw;

    private void Start()
    {
        _centerDraw.StartDraw(NextLevel);
    }

    void NextLevel()
    {
        Debug.Log("End!");
    }
}
