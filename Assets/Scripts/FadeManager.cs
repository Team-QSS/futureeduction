using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{
    public static FadeManager Instance;
    private Image spriteRenderer;

    private void Awake()
    {
        Instance = this;
        spriteRenderer = GetComponent<Image>();
    }

    public void FadeIn()
    {
        StartCoroutine("FadeInIE");
    }

    public void FadeOut()
    {
        StartCoroutine("FadeOutIE");
    }

    IEnumerator FadeOutIE()
    {
        float elapsedTime = 0;
        while (elapsedTime < 1)
        {
            yield return null;
            spriteRenderer.color = new Color(0, 0, 0, elapsedTime);
            elapsedTime += Time.deltaTime;
        }
        spriteRenderer.color = new Color(0, 0, 0, 1);
    }

    IEnumerator FadeInIE()
    {
        gameObject.SetActive(true);
        float elapsedTime = 0;
        while (elapsedTime < 1)
        {
            yield return null;
            spriteRenderer.color = new Color(0, 0, 0, 1-elapsedTime);
            elapsedTime += Time.deltaTime;
        }
        spriteRenderer.color = new Color(0, 0, 0, 0);
    }
}
