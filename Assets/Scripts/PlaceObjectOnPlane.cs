using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class PlaceObjectOnPlane : MonoBehaviour
{

    /*[SerializeField]
    private GameObject PlaceablePrefab;*/

    private ARRaycastManager aRRaycastManager;
    private GameObject spawnedObject;

    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private void Awake()
    {
        aRRaycastManager = GetComponent<ARRaycastManager>();
    }

    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        /*if (Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }

        touchPosition = default;
        return false;*/

        if (Input.GetMouseButtonDown(0))
        {
            touchPosition = Input.mousePosition;
            return true;
        }

        touchPosition = default;
        return false;
    }

    private void Update()
    {
        if (!TryGetTouchPosition(out Vector2 touchPosition)) // ไม่มีการtouch screen ก็ไม่ต้องทำไร
        {
            return;
        }

        if(aRRaycastManager.Raycast(touchPosition,hits,TrackableType.PlaneWithinPolygon))
        {
            var hitPose = hits[0].pose;

            if(spawnedObject == null) //ถ้ายังไม่มี object วาง ให้สร้างตรงที่จิ้ม 
            {
                spawnedObject = Instantiate(DataHandler.Instance.ARObject, hitPose.position, hitPose.rotation);
            }
            else //แต่ถ้ามีแล้วให้ย้ายที่ตัวนั้นมาตรงที่จิ้ม --> ดังนั้นมีได้แค่object เดียว
            {
                spawnedObject.transform.position = hitPose.position;
                spawnedObject.transform.rotation = hitPose.rotation;
            }
        }
    }


}
