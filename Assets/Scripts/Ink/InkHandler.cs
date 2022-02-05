using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InkHandler : MonoBehaviour
{
  private Slider _inkSlider;

    private float _totalInk;
    private float _minInk;
    
    [Header("InkValue")]
    [SerializeField] private float currentInk;
    

    private void Awake() {
        _totalInk = 100f;
        _minInk = 0f;
        currentInk = _totalInk;
        _inkSlider = GetComponent<Slider>();
        _inkSlider.maxValue = _totalInk;
        _inkSlider.minValue = _minInk;

    }

    private void Update() {
        UpdateInk();
        _inkSlider.value = currentInk;
    }

    private void UpdateInk(){   

        if(currentInk >= _totalInk) return;

        currentInk += (int) 10f * Time.deltaTime;
        
    }

    public void SubInk(int quantity){

        currentInk -= quantity;

    }

    public int GetInk(){
        
        return (int)currentInk;

    }

    [ContextMenu("RemoveInk")]

    public void RemoveInk(){

        currentInk = 0f;

    }
    
}
