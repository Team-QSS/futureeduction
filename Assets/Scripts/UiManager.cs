using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public static bool canEdit;
    // Start is called before the first frame update
    void Start()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        canEdit = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSettings()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(false);
        canEdit = false;
    }

    public void ExitSettings()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(true);
        canEdit = true;
    }

    public void OnHover()
    {
        canEdit = false;
    }

    public void ExitHover()
    {
        canEdit = true;
    }
}
