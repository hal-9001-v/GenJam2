using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawingObject: MonoBehaviour
{
    [Header("References")]
    [SerializeField] DrawLayer[] _layers;

    Queue<DrawLayer> _layerQueue;
    DrawLayer _currentLayer;
    DrawLayer _lastLayer;

    Action _endAction;
    private void Awake()
    {
        _layerQueue = new Queue<DrawLayer>();

        foreach (var layer in _layers)
        {
            layer.changeableSprite.WipeOut();
            layer.brush.enabled = false;
        }

    }

    public void StartDraw(Action endAction)
    {
        _endAction = endAction;

        _layerQueue.Clear();

        foreach (var layer in _layers)
        {
            layer.changeableSprite.WipeOut();
        }

        foreach (var layer in _layers)
        {
            _layerQueue.Enqueue(layer);
        }

        DrawNextLayer();
    }

    public void DrawNextLayer()
    {
        if (_layerQueue.Count != 0)
        {
            if (_lastLayer != null)
            {
                _lastLayer.brush.enabled = false;
            }

            _lastLayer = _currentLayer;
            _currentLayer = _layerQueue.Dequeue();

            _currentLayer.brush.enabled = true;
            _currentLayer.follower.StartPath(_currentLayer.paths, DrawNextLayer);
            _currentLayer.brush.SetTarget(_currentLayer.changeableSprite);

        }
        else
        {
            if (_endAction != null)
            {
                _endAction.Invoke();
            }
        }

    }

    [Serializable]
    class DrawLayer
    {
        public LineRenderer[] paths;
        public ChangeableSprite changeableSprite;

        public PathFollower follower;
        public SpriteBrush brush;
    }
}


