using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class AnimHolder : MonoBehaviour
{
    public List<GameObject> animations = null;
    public static AnimHolder Instance;
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        Instance = this;
        foreach (var o in animations)
        {
            DontDestroyOnLoad(o);
        }
    }

    public void Reset()
    {
        animations = new List<GameObject>();
    }

    public void InGameSizing()
    {
        foreach (var o in animations)
        {
            o.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        }
    }

    public void DrawSizing()
    {
        foreach (var o in animations)
        {
            o.transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
