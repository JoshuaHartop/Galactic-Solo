using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WaveTextScript : MonoBehaviour
{
    private TMP_Text text;
    public void textAppear()
    {
        StartCoroutine(FadeTextToFullAlpha(1f, GetComponent<TMP_Text>()));
    }

    public IEnumerator FadeTextToFullAlpha(float t, TMP_Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            
        }
        yield return StartCoroutine(fadeWait());

    }

    public IEnumerator fadeWait()
    {
        
        yield return StartCoroutine(FadeTextToZeroAlpha(1f, GetComponent<TMPro.TMP_Text>())); 
    }

    public IEnumerator FadeTextToZeroAlpha(float t, TMP_Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }

    public void setText(string textToSet)
    {
        text = GetComponent<TMP_Text>();
        text.text = textToSet;
    }
}
