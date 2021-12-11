using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Animator))]
public class InverseKinematics : MonoBehaviour
{
    [SerializeField] Transform RightHandIK;
    [SerializeField] Transform LeftHandIK;
    [SerializeField] Transform RightFootIK;
    [SerializeField] Transform LeftFootIK;

    [SerializeField] Transform RightHand;
    [SerializeField] Transform LeftHand;
    [SerializeField] Transform RightFoot;
    [SerializeField] Transform LeftFoot;

    private Animator animator;

    private bool rightHandActive;
    private bool leftHandActive;
    private bool rightFootActive;
    private bool leftFootActive;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        RightHandIK.SetParent(null);
        LeftHandIK.SetParent(null);
        RightFootIK.SetParent(null);
        LeftFootIK.SetParent(null);
    }

    public LimbController GetRightHandController()
    {
        return new LimbController(RightHandIK, RightHand,0,this);
    }
    public LimbController GetLeftHandController()
    {
        return new LimbController(LeftHandIK, LeftHand,1,this);
    }
    public LimbController GetRightFootController()
    {
        return new LimbController(RightFootIK, RightFoot,2,this);
    }
    public LimbController GetLeftFootController()
    {
        return new LimbController(LeftFootIK, LeftFoot,3,this);
    }

    public Transform GetRightHand()
    {
        return RightHand;
    }
    public Transform GetLeftHand()
    {
        return LeftHand;
    }
    public Transform GetRightFoot()
    {
        return RightFoot;
    }
    public Transform GetLeftFoot()
    {
        return LeftFoot;
    }

    public Transform GetRightHandIK()
    {
        return RightHandIK;
    }
    public Transform GetLeftHandIK()
    {
        return LeftHandIK;
    }
    public Transform GetRightFootIK()
    {
        return RightFootIK;
    }
    public Transform GetLeftFootIK()
    {
        return LeftFootIK;
    }

    public void SetRightHand(bool setActive)
    {
        rightHandActive = setActive;
    }
    public void SetLeftHand(bool setActive)
    {
        leftHandActive = setActive;
    }
    public void SetRightFoot( bool setActive)
    {
        rightFootActive = setActive;
    }
    public void SetLeftFoot(bool setActive)
    {
        leftFootActive = setActive;
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (rightHandActive)
        {
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
            animator.SetIKPosition(AvatarIKGoal.RightHand, RightHandIK.position);
            if(RightHand.position!=RightHandIK.position)
                animator.SetIKRotation(AvatarIKGoal.RightHand, Quaternion.LookRotation(RightHandIK.position - RightHand.position));
            // RightHandIK.position = Vector3.Lerp(RightHandIK.position, RightHand.position, Time.fixedDeltaTime * restrictorFactor);
        }
        else
        {
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
            animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
        }

        if (leftHandActive)
        {
            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
            animator.SetIKPosition(AvatarIKGoal.LeftHand, LeftHandIK.position);
            if (LeftHand.position != LeftHandIK.position)
                animator.SetIKRotation(AvatarIKGoal.LeftHand, Quaternion.LookRotation(LeftHandIK.position - LeftHand.position));
            // LeftHandIK.position = Vector3.Lerp(LeftHandIK.position, LeftHand.position, Time.fixedDeltaTime * restrictorFactor);
        }
        else
        {
            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0);
        }

        if (rightFootActive)
        {
            animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1);
            animator.SetIKPosition(AvatarIKGoal.RightFoot, RightFootIK.position);
            if (RightFoot.position != RightFootIK.position)
                animator.SetIKRotation(AvatarIKGoal.RightFoot, Quaternion.LookRotation(RightFootIK.position - RightFoot.position));
            // RightFootIK.position = Vector3.Lerp(RightFootIK.position, RightFoot.position, Time.fixedDeltaTime * restrictorFactor);
        }
        else
        {
            animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 0);
            animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, 0);
        }

        if (leftFootActive)
        {
            animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1);
            animator.SetIKPosition(AvatarIKGoal.LeftFoot, LeftFootIK.position);
            if (LeftFootIK.position != LeftFoot.position)
                animator.SetIKRotation(AvatarIKGoal.LeftFoot, Quaternion.LookRotation(LeftFootIK.position - LeftFoot.position));
            // LeftFootIK.position = Vector3.Lerp(LeftFootIK.position, LeftFoot.position, Time.fixedDeltaTime * restrictorFactor);
        }
        else
        {
            animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 0);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 0);
        }
    }

    public struct LimbController
    {
        public Transform ikTargetTransform { get; private set; }
        public Transform meshTransform { get; private set; }

        private int limbCode;
        private InverseKinematics ikBase;

        public LimbController(Transform ikTarget,Transform limb,int code,InverseKinematics iKBase)
        {
            ikTargetTransform = ikTarget;
            meshTransform = limb;
            limbCode = code;
            ikBase = iKBase;
        }

        public void SetPosition(Vector3 targetPos)
        {
            ikTargetTransform.position = targetPos;
        }

        public void SetActive(bool active)
        {
            if (active)            
                ikTargetTransform.position = meshTransform.position;

            if (limbCode == 0)
                ikBase.SetRightHand(active);
            else if (limbCode == 1)
                ikBase.SetLeftHand(active);
            else if (limbCode == 2)
                ikBase.SetRightFoot(active);
            else if (limbCode == 3)
                ikBase.SetLeftFoot(active);
        }
    }
}