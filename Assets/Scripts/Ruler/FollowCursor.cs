using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCursor : MonoBehaviour
{   
    public Vector2 cursorPos;
    private void Awake() {
        Cursor.visible = false;
    }
    
   private void Update() {
        cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
       transform.position = cursorPos;
   }
}
