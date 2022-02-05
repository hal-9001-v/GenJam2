using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class ClickSpawner : MonoBehaviour
{

    InkHandler ink;
    RulerButtons rulerType;
    Quaternion spawnRotation;
    [SerializeField] SpriteRenderer spriteRendererPreview;
    [SerializeField] Sprite rotationSprite;
    SpriteRenderer cursorSpriteRenderer;
    private void Awake() {
        ink = FindObjectOfType<InkHandler>();
        rulerType = FindObjectOfType<RulerButtons>();
        cursorSpriteRenderer = GetComponent<SpriteRenderer>();
        
    }
    private void Start() {
        spriteRendererPreview.enabled = false;
        
    }
    private void Update() {
        HandleInput();
    }
    

    private void HandleInput(){

        if(EventSystem.current.IsPointerOverGameObject()) return;
        if(Input.GetMouseButton(0)){
            OnHoldButton();
        }
        if(Input.GetMouseButtonUp(0)) {
            OnRelease();
        }   

    }

  
    private void OnHoldButton(){

        if(ink.GetInk() < rulerType.spawnCost) return; 
        spriteRendererPreview.enabled = true;
        cursorSpriteRenderer.sprite = rotationSprite;
        spriteRendererPreview.gameObject.transform.parent = null;
        spriteRendererPreview.sprite = rulerType.GetSpritedRuler(rulerType.GetRulerType());
        spawnRotation = HandleSpawnableRotation();

        
    }

    private void OnRelease(){
        if(ink.GetInk() < rulerType.spawnCost) return; 
        spriteRendererPreview.enabled = false;
        cursorSpriteRenderer.sprite = rulerType.GetSpritedRuler(rulerType.GetRulerType());
        ink.SubInk(rulerType.spawnCost);
        rulerType.SpawnRuler(rulerType.GetRulerType(), 
        spriteRendererPreview.gameObject.transform.position ,spawnRotation);
        
        spriteRendererPreview.gameObject.transform.position = this.transform.position;
        this.transform.rotation = Quaternion.Euler(Vector3.zero);
        spriteRendererPreview.gameObject.transform.rotation = this.transform.rotation;
        spriteRendererPreview.gameObject.transform.parent = this.transform;
    }

      private Quaternion HandleSpawnableRotation(){
        float rotationSpeed = 2000f;

        spriteRendererPreview.gameObject.transform.Rotate(0,0, 
        Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime, Space.World);

        transform.Rotate(0,0, Input.GetAxis("Mouse X") * rotationSpeed 
        * Time.deltaTime, Space.World);
        var mousePosition = FindObjectOfType<FollowCursor>().cursorPos;
        
        //Other Rotation
       /* spriteRendererPreview.gameObject.transform.rotation = Quaternion.Euler(0,0, 
        -Mathf.Atan2(mousePosition.x, mousePosition.y)*Mathf.Rad2Deg);
        transform.rotation = Quaternion.Euler(0,0, 
        -Mathf.Atan2(mousePosition.x, mousePosition.y)*Mathf.Rad2Deg);*/



        return spriteRendererPreview.gameObject.transform.rotation;

    }

    
}
