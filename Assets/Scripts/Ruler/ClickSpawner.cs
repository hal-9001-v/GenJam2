using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class ClickSpawner : MonoBehaviour
{

    InkHandler ink;
    RulerButtons rulerType;

    private void Awake() {
        ink = FindObjectOfType<InkHandler>();
        rulerType = FindObjectOfType<RulerButtons>();
    }
    private void Update() {
        HandleInput();
    }
    

    private void HandleInput(){

        if(EventSystem.current.IsPointerOverGameObject()) return;
        if(!Input.GetMouseButtonDown(0)) return;
        if(ink.GetInk() < rulerType.spawnCost) return; 
        
        ink.SubInk(rulerType.spawnCost);
        rulerType.SpawnRuler(rulerType.GetRulerType());
    
    }

    
}
