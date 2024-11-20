using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using System;

public class FurniturePlacementManager : MonoBehaviour
{
    public static FurniturePlacementManager Instance;

    // for create & destroy the object
    public GameObject delButton;
    public GameObject currentSpawnedObject;
    public GameObject spawnableFurniture;
    
    // AR componant variable
    public ARSession sessionManager;
    public ARRaycastManager raycastManager;
    public ARPlaneManager planeManager;

    // ray line variable
    private List<ARRaycastHit> raycastHits = new List<ARRaycastHit>();

    void Awake()
    {
        Instance = this;
        delButton.SetActive(true);
    }
    void Update()
    {
        if(Input.touchCount>0){
            if(Input.GetTouch(0).phase == TouchPhase.Began){
                bool collision = raycastManager.Raycast(Input.GetTouch(0).position, raycastHits, TrackableType.PlaneWithinPolygon);

                if(collision && isButtonPressed() == false && isSliderActive() == false){ // if button is pressed so the prefab not spawn
                   SpawnPrefab(); 
                }

                // this below logic for our ar look more realstic
                foreach (var plane in planeManager.trackables)
                {
                    plane.gameObject.SetActive(false); //plane detection stop
                }
                planeManager.enabled = false; // plane manager componant disable
            }
        } 

    }

    void SpawnPrefab(){
        GameObject cloneObject = Instantiate(spawnableFurniture);
        cloneObject.transform.position = raycastHits[0].pose.position;
        cloneObject.transform.rotation = raycastHits[0].pose.rotation;
        currentSpawnedObject = cloneObject;
        delButton.SetActive(true);
        Scale_Rotate_Slider.Instance.ScaleSlider.value = Scale_Rotate_Slider.Instance.scaleMinValue;
        Scale_Rotate_Slider.Instance.RotateSlider.value = Scale_Rotate_Slider.Instance.rotMinValue;
    }

    public bool isButtonPressed(){
        if(EventSystem.current.currentSelectedGameObject?.GetComponent<Button>() == null){
            return false;
        }
        else{
            return true;
        }
    }

    public bool isSliderActive(){
        if(EventSystem.current.currentSelectedGameObject?.GetComponent<Slider>() == null){
            return false;
        }
        else{
            return true;
        }
    }

    public void SwitchFurniture(GameObject furnitureObject){
        spawnableFurniture = furnitureObject;
    }

    public void DestroySpawnableObject(){
        Destroy(currentSpawnedObject, 0.2f);
        delButton.SetActive(false);
        Scale_Rotate_Slider.Instance.ScaleSlider.value = Scale_Rotate_Slider.Instance.scaleMinValue;
        Scale_Rotate_Slider.Instance.RotateSlider.value = Scale_Rotate_Slider.Instance.rotMinValue;
    }
}
