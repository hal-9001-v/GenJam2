using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawingAreaRenamer : MonoBehaviour
{
    [SerializeField] GameObject myParent;

    private void Update() {
        gameObject.name = myParent.name;
    }
}
