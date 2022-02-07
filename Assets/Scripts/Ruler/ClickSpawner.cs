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
    [SerializeField] Sprite improvePaintSpawnable;

    SpriteRenderer cursorSpriteRenderer;
    RaycastHit2D hit;
    float improvedElapsedCircles;
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
        previousImprovingRulerName = "null";
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


        hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.forward, Mathf.Infinity);
        if (hit.collider != null)
        {
            Debug.Log("Holding mouse on: " + hit.collider.name);

            //If hit defense or drawing area Improve them, dont build 
            RulerType type = rulerType.GetRulerType();
            switch (hit.collider.tag)
            {

                case "WallDefense":
                case "WallDrawing":
                case "SoldierDefense":
                case "SoldierDrawing":
                    //rub to improve
                    ImproveDefense(type);
                    break;
                //if hit drawing or background, build
                case "Drawing":
                case "Background":
                    SpawnPreview(type);
                    break;

            }
        }



    }
    private void ImproveDefense(RulerType type)
    {

        if (ink.GetInk() < rulerType.improveCost) return;
        //if previous is now -> keep on improving else reset timer
        BaseDefense baseDefense = hit.collider.gameObject.GetComponent<BaseDefense>();
        if (baseDefense == null) baseDefense = hit.collider.gameObject.GetComponentInParent<BaseDefense>();
        if (baseDefense == null)
        {
            cursorSpriteRenderer.sprite = rulerType.GetSpritedRuler(rulerType.GetRulerType());
            improvedElapsedCircles = 0;
            return;
        }
        string hitname = baseDefense.name;
        //spawn circles

        //if releases 
        if (!previousImprovingRulerName.Equals(hitname))
        {
            cursorSpriteRenderer.sprite = rulerType.GetSpritedRuler(rulerType.GetRulerType());
            //despawn circles
            improvedElapsedCircles = 0f;
        }
        else
        {
            cursorSpriteRenderer.sprite = improvingSprite;
            //circles++
            if (improvedElapsedCircles > rulerType.improveCircles)
            {
                SpriteRenderer sr = baseDefense.GetComponentInChildren<SpriteRenderer>();
                ImproveRuler(hit.collider.gameObject, sr, baseDefense);
                improvedElapsedCircles = 0f;

            }

        }   

        previousImprovingRulerName = hitname;

    }

  

    private void ImproveRuler(GameObject rulerGO, SpriteRenderer sr, BaseDefense bd)
    {

        Debug.Log("Improved" + rulerGO.name);
        sr.sprite = rulerType.improveSprite;
        ink.SubInk(rulerType.improveCost);
        bd.ImproveHealth(rulerType.improveHealth);
        //cambiar el prefab a otro que mole más
    }



    private void SpawnPreview(RulerType type)
    {
        //if enough ink. spawn sprite preview
        //then change cursor to rotation img
        //then unparent preview
        //then change preview sprite to current type sprite
        //then handle defense preview rotation
        if (ink.GetInk() < rulerType.spawnCost) return;
        spriteRendererPreview.enabled = true;
        cursorSpriteRenderer.sprite = rotationSprite;
        spriteRendererPreview.gameObject.transform.parent = null;
        spriteRendererPreview.sprite = rulerType.GetSpritedRuler(type);
        spawnRotation = HandleSpawnableRotation();

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
    
    private void OnRelease()
    {

        improvedElapsedCircles = 0f;
        //hide preview, then change cursor sprite to current type (from rotating sprite)
        spriteRendererPreview.enabled = false;

        cursorSpriteRenderer.sprite = rulerType.GetSpritedRuler(rulerType.GetRulerType());
        //Debug.Log(hit.collider.tag);

        hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.forward, Mathf.Infinity);
        //if drawing of background -> spawn 
        if (hit.collider != null)
        {
            switch (hit.collider.tag)
            {
                case "Drawing":
                case "Background":
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
        if (ink.GetInk() < rulerType.spawnCost) return;
        ink.SubInk(rulerType.spawnCost);
        rulerType.SpawnRuler(rulerType.GetRulerType(),
        spriteRendererPreview.gameObject.transform.position, spawnRotation);

    }





}
