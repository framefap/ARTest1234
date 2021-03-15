using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{

    private Button button;
    public GameObject ARPrefab;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(SelectObject);
    }

    void SelectObject()
    {
        DataHandler.Instance.ARObject = ARPrefab;
    }
}
