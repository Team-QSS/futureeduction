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
        int index = dropDown.value;
        AnimHolder.Instance.animations[index].transform.position = new Vector3(0, 0);
        _editingTemp = index;
        _drawLine = GetComponent<DrawLine>();
    }

    public void DropDownChange()
    {
        _drawLine.SaveAction();
        int index = dropDown.value;
        AnimHolder.Instance.animations[index].transform.position = new Vector3(0, 0);
        AnimHolder.Instance.animations[_editingTemp].transform.position = new Vector3(0, 0, -10);
        _editingTemp = index;
    }
}
