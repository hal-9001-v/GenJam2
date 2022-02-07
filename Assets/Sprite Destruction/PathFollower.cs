using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollower : MonoBehaviour
{
    [Header("References")]
    [SerializeField] LineRenderer[] _paths;

    [Header("settings")]
    [SerializeField] [Range(0.1f, 20)] float _speed = 5;

    public bool isPaused;

    private void Awake()
    {
        if (_paths != null && _paths.Length != 0)
        {
            StartPath(_paths);
        }
    }

    public void StartPath(LineRenderer path)
    {
        StartPath(new LineRenderer[] { path });

        isPaused = false;
    }

    public void StartPath(LineRenderer[] paths)
    {
        StartPath(paths, null);
    }
    public void StartPath(LineRenderer path, Action endOfPathAction)
    {
        StartPath(new LineRenderer[] { path }, endOfPathAction);
    }

    public void StartPath(LineRenderer[] paths, Action endOfPathAction)
    {
        _paths = paths;

        StopAllCoroutines();

        isPaused = false;
        StartCoroutine(FollowPath(endOfPathAction));
    }

    IEnumerator FollowPath(Action endOfPathAction)
    {
        foreach (var path in _paths)
        {

            transform.position = path.transform.TransformPoint(path.GetPosition(0));
            for (int i = 1; i < path.positionCount; i++)
            {
                Vector3 targetPoint = path.transform.TransformPoint(path.GetPosition(i));
                Vector3 startingPoint = transform.position;

                float elapsedTime = 0;
                float time = Vector2.Distance(targetPoint, startingPoint) / _speed;

                while (elapsedTime < time)
                {
                    if (!isPaused)
                    {
                        elapsedTime += Time.deltaTime;

                        transform.position = Vector2.Lerp(startingPoint, targetPoint, elapsedTime / time);
                    }

                    yield return null;
                }

                transform.position = targetPoint;
            }
        }

        if (endOfPathAction != null)
        {
            endOfPathAction.Invoke();
        }


    }

    /// <summary>
    /// Stop this follower from path. It cant be resumed.
    /// </summary>
    public void StopCompletely()
    {
        StopAllCoroutines();
    }

}
