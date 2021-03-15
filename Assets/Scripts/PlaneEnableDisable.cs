using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;


[RequireComponent(typeof(ARRaycastManager))]
public class PlaneEnableDisable : MonoBehaviour
{
    [SerializeField]
    private Button ToggleButton;

    private ARRaycastManager aRRaycastManager;

    private ARPlaneManager aRPlaneManager;

    void Awake()
    {
        aRPlaneManager = GetComponent<ARPlaneManager>();

        //เริ่มมาให้ยังไม่detect plane
       // aRPlaneManager.enabled = false;


        if (ToggleButton != null)
        {
            ToggleButton.onClick.AddListener(ToggleARPlane);

        }
    }

     private void ToggleARPlane()
    {
        aRPlaneManager.enabled = !aRPlaneManager.enabled;

        foreach(var plane in aRPlaneManager.trackables)
        {
            plane.gameObject.SetActive(!aRPlaneManager);
        }
    }
}
