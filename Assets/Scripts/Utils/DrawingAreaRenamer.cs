using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawingAreaRenamer : MonoBehaviour
{
    [SerializeField] GameObject myParent;

    private void Start() {
        gameObject.name = myParent.name;
    }
}
