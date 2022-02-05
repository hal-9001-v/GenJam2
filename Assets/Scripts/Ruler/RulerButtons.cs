using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum RulerType{

Type1,
Type2,
Type3,

}

public class RulerButtons : MonoBehaviour
{
    [Header("RulerComponents")]
    [SerializeField] private  Button[]  rulers;
    [SerializeField] private Sprite[] rulerSprites;
    [SerializeField] private  SpriteRenderer cursorSprite;  
    [SerializeField] private RulerType currentType;

    [Header("Ruler Prefabs")]
    [SerializeField] GameObject[] rulerSpawnables;
    
    public int spawnCost;
    private void Awake() {

          rulers[(int)RulerType.Type1].onClick.AddListener(() =>
        {
            //Ruler1
            SetRulerType((int)RulerType.Type1);
        });

         rulers[(int)RulerType.Type2].onClick.AddListener(() =>
        {
            //Ruler2
            SetRulerType((int)RulerType.Type2);
        });

         rulers[(int)RulerType.Type3].onClick.AddListener(() =>
        {
            //Ruler3
            SetRulerType((int)RulerType.Type3);
            
        });

    }

    private void Start() {
        SetRulerType((int)RulerType.Type1);
    }
    public void SpawnRuler(RulerType rulerType, Vector2 pos, Quaternion rotation){
        
        Instantiate(rulerSpawnables[(int)rulerType], pos, rotation);

    }
    public Sprite GetSpritedRuler(RulerType rulerType){
        
        return rulerSprites[(int)rulerType];
        
    }

    public RulerType GetRulerType(){
        return currentType;
    }

    public void SetRulerType(int rulerType){
        
        cursorSprite.sprite = rulerSprites[rulerType];
        currentType = (RulerType) rulerType;
        
        switch(rulerType){

            case 0:
            spawnCost = 10;
            break;

            case 1:
            spawnCost = 20;
            break;

            case 2:
            spawnCost = 30;
            break; 
            
            default:
            break;
        }

    }


    

}
