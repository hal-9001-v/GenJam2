using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevilFlipper : MonoBehaviour
{
    [SerializeField] Transform devil;
    float originalX;

    private void Awake()
    {
        originalX = transform.localScale.x;
    }

    public void RightFlip()
    {
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);

    }

    public void InverseFlip()
    {

        transform.localScale = new Vector3(-originalX, transform.localScale.y, transform.localScale.z);
    }


}
