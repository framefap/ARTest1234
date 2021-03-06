using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class PlaceARObjectWithIndicator : MonoBehaviour
{
    [SerializeField]
    private GameObject ARObject;

    [SerializeField]
    private GameObject PlacementIndicator;

    private ARRaycastManager aRRaycastManager;
    private GameObject spawnedObject;

    private Pose PlacementPose;
    private bool isPlacementPoseValid = false;

    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private void Awake()
    {
        aRRaycastManager = GetComponent<ARRaycastManager>();
    }

    private void Update()
    {
        if (isPlacementPoseValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            PlaceObject();
        }

        UpdatePlacementPose();
        UpdatePlacementIndicator();
    }

    private void UpdatePlacementIndicator() //อัพเดทตำแหน่ง placementindicator ตามplacementpose
    {
        if (isPlacementPoseValid) //ถ้าไม่มี object ก็ขึ้นรูป indicator ไป
        {
            PlacementIndicator.SetActive(true);
            PlacementIndicator.transform.SetPositionAndRotation(PlacementPose.position, PlacementPose.rotation);
        }
        else //ถ้ามี object แล้วก็inactivate indicator ซะ
        {
            PlacementIndicator.SetActive(false);
        }
    }

    private void UpdatePlacementPose() //อัพเดท placementpose ให้ตามกล้องจับ เพื่อไปอัพเดทตำแหน่ง placementindicator
    {
        var Screencenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hitPose = hits[0].pose;
        aRRaycastManager.Raycast(Screencenter, hits, TrackableType.PlaneWithinPolygon);

        isPlacementPoseValid = hits.Count > 0;

        if (isPlacementPoseValid)
        {
            PlacementPose = hitPose;

            var cameraForward = Camera.current.transform.forward;
            var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
            PlacementPose.rotation = Quaternion.LookRotation(cameraBearing);

        }

    }

    private void PlaceObject()
    {
        Instantiate(ARObject, PlacementPose.position, PlacementPose.rotation);
    }
}
