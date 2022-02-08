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
    [SerializeField] Sprite improvingSprite;
    [SerializeField] DrawingBlobs improvePaintSpawnableWall;
    [SerializeField] DrawingBlobs improvePaintSpawnableSoldier;

    SpriteRenderer cursorSpriteRenderer;
    RaycastHit2D hit;
    bool improving = false;
    bool building = false;
    float improvedElapsedCircles = 0;
    string previousImprovingRulerName;
    private void Awake()
    {
        ink = FindObjectOfType<InkHandler>();
        rulerType = FindObjectOfType<RulerButtons>();
        cursorSpriteRenderer = GetComponent<SpriteRenderer>();

    }
    private void Start()
    {
        spriteRendererPreview.enabled = false;
        previousImprovingRulerName = "Background";
    }
    private void Update()
    {
        HandleInput();
    }


    private void HandleInput()
    {

        //Pointer over UI
        if (EventSystem.current.IsPointerOverGameObject()) return;

        //Holding button
        if (Input.GetMouseButton(0))
        {
            OnHoldButton();
        }
        //Released
        if (Input.GetMouseButtonUp(0))
        {
            OnRelease();
        }

    }


    private void OnHoldButton()
    {


        if(building) spawnRotation = HandleSpawnableRotation();
        hit = CalculateHit();
        RulerType type = rulerType.GetRulerType();
        if (hit.collider != null)
        {
            //If hit defense or drawing area Improve them, dont build 
            switch (hit.collider.tag)
            {

                case "WallDefense":
                case "WallDrawing":
                case "SoldierDefense":
                case "SoldierDrawing":
                    //rub to improve
                    ImproveDefense(type);
                break;

                default:
                    if(improving == false)   SpawnPreview(type);
                break;
            }
        }

    }
    private void ImproveDefense(RulerType type)
    {

        if (CheckInk(1)) return;
        //if previous is now -> keep on improving else reset timer
        BaseDefense baseDefense = hit.collider.gameObject.GetComponent<BaseDefense>();
        if (baseDefense == null) baseDefense = hit.collider.gameObject.GetComponentInParent<BaseDefense>();
        if (baseDefense == null)
        {
            cursorSpriteRenderer.sprite = rulerType.GetSpritedRuler(rulerType.GetRulerType());
            CancelImprove();
            return;
        }
        string hitname = baseDefense.name;
        if(!building){ 
            improving = true;
            //spawn circles
            SpawnBlobs(baseDefense.GetRulerType());
            //if releases 
            if (!previousImprovingRulerName.Equals(hitname))
            {
                cursorSpriteRenderer.sprite = rulerType.GetSpritedRuler(rulerType.GetRulerType());
                //despawn circles
                CancelImprove();
            }
            else
            {
                cursorSpriteRenderer.sprite = improvingSprite;
                //circles++
                if (improvedElapsedCircles*Time.deltaTime > rulerType.improveCircles*100*Time.deltaTime)
                {
                    SpriteRenderer sr = baseDefense.GetComponentInChildren<SpriteRenderer>();
                    ImproveRuler(hit.collider.gameObject, sr, baseDefense);
                    improvedElapsedCircles = 0f;
                    ErasePaint();
                }
            
        }
        }   

        previousImprovingRulerName = hitname;
        

    }

    List<DrawingBlobs> blobs = new List<DrawingBlobs>();

    private void SpawnBlobs(RulerType type){
        
        for(int i = 0; i< rulerType.improveCircles; i++){
            
            var randomRotation = Random.rotation;
            var randomPosition = transform.position + new Vector3(Random.Range(0,2), Random.Range(0,2),0);
            DrawingBlobs blob;
            if(type == RulerType.Type1)  blob = Instantiate(improvePaintSpawnableWall,  randomPosition, randomRotation);
            else  blob = Instantiate(improvePaintSpawnableSoldier,  randomPosition, randomRotation);
            blob.gameObject.transform.parent = rulerType.transform;
            blobs.Add(blob);
            improvedElapsedCircles++;
        }

    }   

    private void ErasePaint(){
        
        foreach(DrawingBlobs b in blobs){
        
            if(b!=null) Destroy(b.gameObject);

        }

    }

    private void ImproveRuler(GameObject rulerGO, SpriteRenderer sr, BaseDefense bd)
    {

        sr.sprite = rulerType.GetImproveSprite(bd.GetRulerType());
        Animator anim = bd.GetAnimator();
        if(anim){

            anim.SetTrigger("IsImproved");

        }
        
        ink.SubInk(rulerType.improveCost);
        FindObjectOfType<AudioManager>().Play("Improve");
        bd.GetComponent<Health>().ModifyMaxHealth(rulerType.improveHealth) ;
        improving = false;
        //cambiar el prefab a otro que mole más
    }



    private void SpawnPreview(RulerType type)
    {

        if(improving == true) return;
        //if enough ink. spawn sprite preview
        //then change cursor to rotation img
        //then unparent preview
        //then change preview sprite to current type sprite
        //then handle defense preview rotation
        if (CheckInk(0)) return;
        building = true;
        spriteRendererPreview.enabled = true;
        cursorSpriteRenderer.sprite = rotationSprite;
        spriteRendererPreview.gameObject.transform.parent = null;
        spriteRendererPreview.sprite = rulerType.GetSpritedRuler(type);

    }  

    private bool CheckInk(int i){
        bool b = false;
        if(i == 0) b = ink.GetInk() < rulerType.spawnCost;
        if(i == 1) b = ink.GetInk() < rulerType.improveCost;

        if(b) FindObjectOfType<AudioManager>().Play("InkError");

        return b;
    }

    private Quaternion HandleSpawnableRotation()
    {

        //Rotate cursor and preview in relation to cursor
        float rotationSpeed = 2000f;

        spriteRendererPreview.gameObject.transform.Rotate(0, 0,
        Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime, Space.World);

        transform.Rotate(0, 0, Input.GetAxis("Mouse X") * rotationSpeed
        * Time.deltaTime, Space.World);
        var mousePosition = FindObjectOfType<FollowCursor>().cursorPos;

        //Other Rotation
        /* spriteRendererPreview.gameObject.transform.rotation = Quaternion.Euler(0,0, 
         -Mathf.Atan2(mousePosition.x, mousePosition.y)*Mathf.Rad2Deg);
         transform.rotation = Quaternion.Euler(0,0, 
         -Mathf.Atan2(mousePosition.x, mousePosition.y)*Mathf.Rad2Deg);*/



        return spriteRendererPreview.gameObject.transform.rotation;

    }
    
    private void CancelImprove(){
        improvedElapsedCircles = 0f;
        improving = false;
        ErasePaint();
    }   

    RaycastHit2D CalculateHit(){
            
        var ray = Physics2D.Raycast(FindObjectOfType<Camera>().ScreenToWorldPoint(Input.mousePosition), Vector3.forward, 1);
        
        return ray;
    }

    private void OnRelease()
    {   
        CancelImprove();
        building = false;
        //hide preview, then change cursor sprite to current type (from rotating sprite)
        spriteRendererPreview.enabled = false;
        cursorSpriteRenderer.sprite = rulerType.GetSpritedRuler(rulerType.GetRulerType());
          
     
        RulerType type = rulerType.GetRulerType();
        hit = CalculateHit();
        if (hit.collider != null)
        {

            //If hit defense or drawing area Improve them, dont build 
            switch (hit.collider.tag)
            {

                case "WallDefense":
                case "WallDrawing":
                case "SoldierDefense":
                case "SoldierDrawing":
                    return;

                case "Background":
                case "Drawing":
                    if(improving == false) 
                    SpawnRuler();
                break;
            }
        }

        //reset preview
        spriteRendererPreview.gameObject.transform.position = this.transform.position;
        this.transform.rotation = Quaternion.Euler(Vector3.zero);
        spriteRendererPreview.gameObject.transform.rotation = this.transform.rotation;
        spriteRendererPreview.gameObject.transform.parent = this.transform;
        
     
    }


    private void SpawnRuler()
    {
        //if can spawn -> spawn
        if(CheckInk(0)) return;
        ink.SubInk(rulerType.spawnCost);
        rulerType.SpawnRuler(rulerType.GetRulerType(),
        spriteRendererPreview.gameObject.transform.position, spawnRotation);

    }





}
