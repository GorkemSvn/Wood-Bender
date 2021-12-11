using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarverGameManager : GameManager
{
    [SerializeField] InverseKinematics ik;
    [SerializeField] Transform cylinder;

    InverseKinematics.LimbController rightHandC;

    protected override void Start()
    {
        base.Start();
        rightHandC = ik.GetRightHandController();
    }

    protected override void OnStart()
    {
        base.OnStart();
        StartCoroutine(HandControl());
    }

    IEnumerator HandControl()
    {
        Debug.Log("Hand Control Active");
        rightHandC.SetActive(true);
        Vector3 targetPos = cylinder.position + Vector3.up -2* Vector3.right;
        Vector3 lastPos = targetPos;
        rightHandC.SetPosition(targetPos);

        while (enabled)
        {
            if (ButtonJoystick.singleTon.swiping)
            {
                targetPos = Vector3.Lerp(targetPos, rightHandC.meshTransform.position, Time.fixedDeltaTime);
                targetPos.z = cylinder.position.z+0.1f;
                targetPos += (Vector3)ButtonJoystick.singleTon.deltaPos/30f;//*Time.deltaTime;
                lastPos = Vector3.Lerp(lastPos, targetPos, Time.deltaTime);
                rightHandC.SetPosition(lastPos);
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
