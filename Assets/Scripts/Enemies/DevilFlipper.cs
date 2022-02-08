using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevilFlipper : MonoBehaviour
{
    [SerializeField] Transform devil;
    float originalX;

    private void Awake()
    {
        originalX = devil.transform.localScale.x;
    }

    public void RightFlip()
    {
        devil.transform.localScale = new Vector3(originalX, devil.localScale.y, devil.localScale.z);

    }

    public void InverseFlip()
    {

        devil.transform.localScale = new Vector3(-originalX, devil.localScale.y, devil.localScale.z);
    }


}
