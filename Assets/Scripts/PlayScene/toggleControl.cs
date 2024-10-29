using System.Collections;
using System.Collections.Generic;
using PlayScene;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class toggleControl : MonoBehaviour
{
    public GameObject ui;
    private bool _isInAble;

    public void Click()
    {
        if (_isInAble)
        {
            _isInAble = false;
            ui.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
            _isInAble = true;
            ui.SetActive(true);
        }
    }
}
