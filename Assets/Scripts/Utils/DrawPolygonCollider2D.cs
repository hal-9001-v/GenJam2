using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent (typeof(PolygonCollider2D))]
public class DrawPolygonCollider2D : MonoBehaviour
{
    [SerializeField]  GameObject LinePrefab;
    [SerializeField] Color LineColor;
    private LineRenderer _line;
    private List<LineRenderer> _lineList;
    private PolygonCollider2D _polygonCol;
    private int polycount;
    void Start()
    {
        
        _lineList = new List<LineRenderer>();
        _polygonCol = GetComponent<PolygonCollider2D>();
        polycount = _polygonCol.pathCount;
       
        for (int i = 0; i < polycount; i++){
            _line = Instantiate(LinePrefab).GetComponent<LineRenderer>();
            _line.startColor = LineColor;
            _line.endColor = LineColor;
            _line.transform.SetParent(transform);
            _line.transform.localPosition = Vector3.zero;
            _lineList.Add(_line);
        }
    }

    void Update()
    {
        HilightCollider();
    }

    void HilightCollider()
    {
            for(int i = 0; i < polycount; i++) {
            
                var pointsI = _polygonCol.GetPath(i);
                Vector3[] positions = new Vector3[pointsI.Length];

                for(int j = 0; j < pointsI.Length; j++)
                {
                    positions[j] = transform.TransformPoint(pointsI[j]);
                }

                _lineList[i].positionCount = pointsI.Length;
               _lineList[i].SetPositions(positions);
            }  
        
    }
}
