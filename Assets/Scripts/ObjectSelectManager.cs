using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObjectSelectManager : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown dropDown;
    [SerializeField] private DrawLine _drawLine;
    [SerializeField] public GameObject[] animObjects;
    public int _editingTemp;
    enum ObjectName
    {
        Default,
        Jump,
        Walk1,
        Walk2,
        Walk3
    }
    private void Start()
    {
        _editingTemp = (int)ObjectName.Default;
        _drawLine = GetComponent<DrawLine>();
        foreach (var o in animObjects)
        {
            DontDestroyOnLoad(o);
        }
    }

    public void DropDownChange()
    {
        _drawLine.SaveAction();
        int index = dropDown.value;
        animObjects[index].transform.position = new Vector3(0, 0);
        animObjects[_editingTemp].transform.position = new Vector3(0, 0, -10);
        _editingTemp = index;
    }
}
