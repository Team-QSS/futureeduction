using UnityEngine;

public class UiManager : MonoBehaviour
{
    public static bool CanEdit;
    private void Start()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        CanEdit = true;
    }
    
    public void OnSettings()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(false);
        CanEdit = false;
    }

    public void ExitSettings()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(true);
        CanEdit = true;
    }

    public void OnHover()
    {
        CanEdit = false;
    }

    public void ExitHover()
    {
        CanEdit = true;
    }
}
