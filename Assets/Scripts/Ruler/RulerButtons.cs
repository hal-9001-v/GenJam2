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
    [SerializeField] private Sprite[] rulerImproveSprites;
    
    [SerializeField] private  SpriteRenderer cursorSprite;  
    [SerializeField] private RulerType currentType;

    [Header("Ruler Prefabs")]
    [SerializeField] GameObject[] rulerSpawnables;
    
    [SerializeField] GameObject arrowPrefab;
    
    [Header("Ruler Costs")]
    public int spawnCost;
    public int improveCost;
    public int improveCircles;
    public int improveHealth;
    public int improveDamage;
    
    public Sprite improveSprite;

    private int _id;
    private void Awake() {
        _id = 0;
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
        
        
        GameObject instance = Instantiate(rulerSpawnables[(int)rulerType], pos, rotation);
        instance.name =  _id.ToString();
        
        _id++;
        

        if(rulerType == RulerType.Type3) {
            var newRot = new Vector3(rotation.eulerAngles.x, rotation.eulerAngles.y, rotation.eulerAngles.z+109);
            Instantiate(arrowPrefab, pos, Quaternion.Euler(newRot));
            return;
        }

        
        
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
            improveCost = 20;
            improveCircles = 2;
            improveHealth = 3;
            improveSprite = rulerImproveSprites[0];
            
            break;

            case 1:
            spawnCost = 20;
            improveCost = 30;
            improveCircles = 3;
            improveHealth = 3;
            improveSprite = rulerImproveSprites[1];
            break;

            case 2:
            spawnCost = 15;
            improveCost = 200;
            break; 
            
            default:
            break;
        }

    }


    

}
