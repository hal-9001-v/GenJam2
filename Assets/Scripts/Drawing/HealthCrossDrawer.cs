using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class HealthCrossDrawer : MonoBehaviour
{
    [Header("References")]
    [SerializeField] DrawingObject _drawingObject;
    [Header("Settings")]
    [SerializeField] [Range(1, 10)] int _crossesPerHit = 1;
    public int layerCount { get; private set; }

    public int maxCount { get { return _drawingObject.layerCount; } }

    private void Awake()
    {
        var health = GetComponent<Health>();

        health.deadAction += AddCross;
        health.hurtAction += AddCross;
    }

    public void AddCross()
    {
        for (int i = 0; i < _crossesPerHit; i++)
        {
            if (layerCount < maxCount)
            {
                _drawingObject.StartDrawingIDLayer(layerCount, null);
            }
            layerCount++;
        }

    }

    public void Restore()
    {
        layerCount = 0;

        _drawingObject.Restore();
    }




}
