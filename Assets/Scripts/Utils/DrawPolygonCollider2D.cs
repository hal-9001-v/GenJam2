using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent (typeof(PolygonCollider2D))]
public class DrawPolygonCollider2D : MonoBehaviour
{
    [SerializeField] private GameObject linePrefab;
    LineRenderer lineRenderer;
    List<LineRenderer> lineRendererList;
    PolygonCollider2D polygonCollider2D;
    int polycount;
    void Start()
    {
        lineRendererList = new List<LineRenderer>();
        polygonCollider2D = GetComponent<PolygonCollider2D>();
        polycount = polygonCollider2D.pathCount;
       
        for (int i = 0; i < polycount; i++){
        lineRenderer = Instantiate(linePrefab).GetComponent<LineRenderer>();
        lineRenderer.transform.SetParent(transform);
        lineRenderer.transform.localPosition = Vector3.zero;
        lineRendererList.Add(lineRenderer);
        }

    }

    void Update()
    {
        HiliteCollider();
    }

    void HiliteCollider()
    {

  
            for(int i = 0; i < polycount; i++) {
            
                var pointsI = polygonCollider2D.GetPath(i);
                Vector3[] positions = new Vector3[pointsI.Length];

                for(int j = 0; j < pointsI.Length; j++)
                {
                    positions[j] = transform.TransformPoint(pointsI[j]);
                }

                lineRendererList[i].positionCount = pointsI.Length;
               lineRendererList[i].SetPositions(positions);
            }
        

     
           
        
    }
}
