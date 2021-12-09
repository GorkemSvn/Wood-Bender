using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private GameObject barPrefab;
    [SerializeField] private int phases;
    [SerializeField] private int filled;
    [SerializeField] private float ratio;

    public int phasesCount { get; private set; }
    public int filledCounts { get; private set; }
    public int filratio { get; private set; }

    public void Start()
    {
        SetBars(phases);
        FillBars(0, 0);
    }

    public void SetBars(int phaseCount)
    {
        phasesCount = phaseCount;
        //delete current bars
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }

        //create bars as much as phase count
        RectTransform rt = GetComponent<RectTransform>();
        Vector3 gap = rt.rect.width * Vector3.right / phaseCount;
        Vector3 leftAnchor = transform.position - rt.rect.width * Vector3.right / 2 + gap / 2;
        Vector3 size = gap + rt.rect.height * Vector3.up;
        for (int i = 0; i < phaseCount; i++)
        {
            Vector3 pos = leftAnchor + gap * i;

            GameObject bar = Instantiate(barPrefab, pos, Quaternion.identity, transform);
            bar.GetComponent<RectTransform>().sizeDelta = size;
        }
    }
    public void FillBars(int fillCount,float activeRatio)
    {
        filled = fillCount;
        ratio = activeRatio;
        //fill as filledcount
        for (int i = 0; i < transform.childCount; i++)
        {
            SliderBar sb = transform.GetChild(i).GetComponent<SliderBar>();
            if (i < fillCount)
                sb.Setfillrate(1f);
            else if (i == fillCount)
                sb.Setfillrate(activeRatio);
            else
                sb.Setfillrate(0);
        }

    }
    /*
    public void OnValidate()
    {
        UnityEditor.EditorApplication.delayCall += () =>
        {
            if (gameObject)
            {
                SetBars(phases);
                FillBars(filled, ratio);
            }
        };
    }*/
}
