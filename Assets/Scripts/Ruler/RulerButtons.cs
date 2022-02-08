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
            FindObjectOfType<AudioManager>().Play("MenuBop");
            SetRulerType((int)RulerType.Type1);
        });

         rulers[(int)RulerType.Type2].onClick.AddListener(() =>
        {
            //Ruler2
            FindObjectOfType<AudioManager>().Play("MenuBop");
            SetRulerType((int)RulerType.Type2);
        });

         rulers[(int)RulerType.Type3].onClick.AddListener(() =>
        {
            //Ruler3
            FindObjectOfType<AudioManager>().Play("MenuBop");
            SetRulerType((int)RulerType.Type3);
            
        });

    }
    
    private void Start() {
        SetRulerType((int)RulerType.Type1);
    }
    public void SpawnRuler(RulerType rulerType, Vector2 pos, Quaternion rotation){
        
        switch(rulerType){
            case RulerType.Type1:
                FindObjectOfType<AudioManager>().Play("RockSpawn");
            break;
            case RulerType.Type2:
                FindObjectOfType<AudioManager>().Play("SoldierSpawn");
            break;
            case RulerType.Type3:
                FindObjectOfType<AudioManager>().Play("BowSpawn");
            break;
            

        }
        
        GameObject instance = Instantiate(rulerSpawnables[(int)rulerType], pos, rotation);
        instance.name =  _id.ToString();
        
        _id++;
        

        if(rulerType == RulerType.Type3) {
            var newRot = new Vector3(rotation.eulerAngles.x, rotation.eulerAngles.y, rotation.eulerAngles.z+109);
            StartCoroutine(SpawnArrow( pos, newRot));
            return;
        }

        
        
    }

    
    IEnumerator SpawnArrow(Vector3 pos,Vector3 newRot){

        yield return new WaitForSeconds(0.125f*3f);
        FindObjectOfType<AudioManager>().Play("BowShoot");
        Instantiate(arrowPrefab, pos, Quaternion.Euler(newRot));

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
            spawnCost = 25;
            improveCost = 40;
            improveCircles = 3;
            improveHealth = 5;
            
            break;

            case 1:
            spawnCost = 15;
            improveCost = 30;
            improveCircles = 3;
            improveHealth = 3;
            break;

            case 2:
            spawnCost = 15;
            improveCost = 200;
            break; 
            
            default:
            break;
        }

    }   

    public Sprite GetImproveSprite(RulerType type) {

        switch(type){
            case RulerType.Type1:
            improveSprite = rulerImproveSprites[0];
            break;
            case RulerType.Type2:
            improveSprite = rulerImproveSprites[1];
            break;

        }

        return improveSprite;

    }


    

}
