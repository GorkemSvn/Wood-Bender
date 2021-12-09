using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class TutorialPanel : Panel
{
    public Text instruction;
    public Image hand;

    public Ease handPathEase;
    public List<Transform> handPath;

    protected override void OnAppearStart()
    {
        base.OnAppearStart();

        List<Vector3> path = new List<Vector3>();
        foreach (Transform t in handPath)
            path.Add(t.position);
        hand.transform.position = path[0];
        hand.transform.DOPath(path.ToArray(), path.Count).SetEase(handPathEase).SetLoops(-1);
    }

}