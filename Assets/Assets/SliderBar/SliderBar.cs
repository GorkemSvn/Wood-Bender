using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class SliderBar : MonoBehaviour {

    [SerializeField]private float delay=0.2f;
    [SerializeField]private float FillRate;
    [SerializeField]private Color color;
    [SerializeField]private Image slider;
    Coroutine c;

    public void SetFillRateImmidietly(float rate)
    {
        rate = Mathf.Clamp(rate, 0, 1);
        slider.fillAmount = rate;
        FillRate = rate;
    }

    public void Setfillrate(float rate)
    {
        rate = Mathf.Clamp(rate, 0, 1);

        if (c == null)
            c = StartCoroutine(LerpProces(rate));
        else
        {
            StopCoroutine(c);
            c = StartCoroutine(LerpProces(rate));
        }
    }
    
    IEnumerator LerpProces(float f)
    {
        float startRate = slider.fillAmount;

        for (float t = 0; t < delay; t+=Time.fixedDeltaTime)
        {
            slider.fillAmount = Mathf.Lerp(startRate, f, t / delay);
            yield return new WaitForFixedUpdate();
        }
        slider.fillAmount = f;
    }

    private void OnValidate()
    {
        FillRate = Mathf.Clamp(FillRate, 0, 1);
        slider.fillAmount = FillRate;
        slider.color = color;
    }
}
