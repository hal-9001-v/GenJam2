using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawingbBlobs : MonoBehaviour
{
    PolygonCollider2D _polyCol;


    private void Awake() {
        _polyCol = GetComponent<PolygonCollider2D>();
        gameObject.tag = "Paint";
     }


     private void Update() {
         
     }
}
