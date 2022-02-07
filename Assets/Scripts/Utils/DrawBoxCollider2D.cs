using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))]

public class DrawBoxCollider2D : MonoBehaviour
{
    [SerializeField] GameObject LinePrefab;
    [SerializeField] Color LineColor;

    private LineRenderer _line;
    private BoxCollider2D _boxCol;

    void Start()
    {
        _line = Instantiate(LinePrefab).GetComponent<LineRenderer>();
        _line.startColor = LineColor;
        _line.endColor = LineColor;
        _line.transform.SetParent(transform);
        _line.transform.localPosition = Vector3.zero;
        _boxCol = GetComponent<BoxCollider2D>();
       
    }

    void Update()
    {
        HilightCollider();
    }

    void HilightCollider()
    {
        Vector3[] pos = new Vector3[4];
        pos[0] = transform.TransformPoint( new Vector3(_boxCol.size.x / 2.0f, _boxCol.size.y / 2.0f,0));
        pos[1] = transform.TransformPoint(new Vector3(-_boxCol.size.x / 2.0f, _boxCol.size.y / 2.0f,0));
        pos[2] = transform.TransformPoint(new Vector3(-_boxCol.size.x / 2.0f, -_boxCol.size.y / 2.0f,0));
        pos[3] = transform.TransformPoint(new Vector3(_boxCol.size.x / 2.0f, -_boxCol.size.y / 2.0f,0));
        _line.SetPositions(pos);
    }
}