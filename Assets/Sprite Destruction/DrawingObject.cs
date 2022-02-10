using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawingObject : MonoBehaviour
{
    [Header("References")]
    [SerializeField] DrawLayer[] _layers;

    [Header("Settings")]
    [SerializeField] bool _atAwake;

    public int layerCount { get { return _layers.Length; } }

    Queue<DrawLayer> _layerQueue;
    DrawLayer _currentLayer;
    DrawLayer _lastLayer;

    Action _endAction;
    private void Awake()
    {
        _layerQueue = new Queue<DrawLayer>();

        foreach (var layer in _layers)
        {
            //layer.changeableSprite.WipeOut();
            layer.brush.enabled = false;
        }

        if (_atAwake)
        {
            StartDraw(null);
        }
    }

    public void StartDraw(Action endAction)
    {
        _endAction = endAction;

        _layerQueue.Clear();
        _lastLayer = null;

        Restore();

        foreach (var layer in _layers)
        {
            _layerQueue.Enqueue(layer);
        }

        DrawNextLayer();
    }

    public void StartDrawingIDLayer(int layerIndex, Action endAction)
    {
        if (layerIndex >= 0 && layerIndex < _layers.Length)
        {
            _layers[layerIndex].brush.enabled = true;
            _layers[layerIndex].brush.SetTarget(_layers[layerIndex].changeableSprite);
            _layers[layerIndex].follower.StartPath(_layers[layerIndex].paths, endAction);
        }
    }

    public void Restore()
    {
        foreach (var layer in _layers)
        {
            layer.brush.enabled = false;
            layer.follower.StopCompletely();
            layer.changeableSprite.WipeOut();
        }
    }

    public void DrawNextLayer()
    {
        if (_layerQueue.Count != 0)
        {
            if (_lastLayer != null && _lastLayer.brush)
            {
                _lastLayer.brush.enabled = false;
            }

            _lastLayer = _currentLayer;
            _currentLayer = _layerQueue.Dequeue();

            _currentLayer.brush.enabled = true;
            _currentLayer.brush.SetTarget(_currentLayer.changeableSprite);
            _currentLayer.follower.StartPath(_currentLayer.paths, DrawNextLayer);

        }
        else
        {
            if (_endAction != null)
            {
                _endAction.Invoke();
            }
        }

    }

    public void PauseDraw()
    {
        _currentLayer.follower.isPaused = true;
    }

    public void ResumeDraw()
    {
        _currentLayer.follower.isPaused = false;
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


