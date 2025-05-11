using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootIK : MonoBehaviour
{
    [SerializeField]
    private Vector3 offset = new Vector3(0, 0.1f, 0);
    [SerializeField]
    private float rayRange = 1f;
    [SerializeField]
    private string fieldLayerName = "Field";

    private Animator animator;

    private Transform _transform;
    public new Transform transform
    {
        get
        {
            if (_transform == null)
            {
                _transform = gameObject.transform;
            }
            return _transform;
        }
    }


    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnAnimatorIK()
    {
        //�E��
        var ray = new Ray(animator.GetIKPosition(AvatarIKGoal.RightFoot), -transform.up);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, rayRange, LayerMask.GetMask(fieldLayerName)))
        {
            Quaternion rightRotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
            //weight�͂Ƃ肠����1�ŌŒ肵�Ă����i0f:���̃A�j���[�V����,1f:IK�����S�ɔ��f�j
            animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1);
            animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, 1);
            animator.SetIKPosition(AvatarIKGoal.RightFoot, hit.point + offset);
            animator.SetIKRotation(AvatarIKGoal.RightFoot, rightRotation);
        }

        //����
        ray = new Ray(animator.GetIKPosition(AvatarIKGoal.LeftFoot), -transform.up);
        if (Physics.Raycast(ray, out hit, rayRange, LayerMask.GetMask(fieldLayerName)))
        {
            Quaternion leftRotaion = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;

            animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 1);
            animator.SetIKPosition(AvatarIKGoal.LeftFoot, hit.point + offset);
            animator.SetIKRotation(AvatarIKGoal.LeftFoot, leftRotaion);
        }
    }
}



