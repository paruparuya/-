using UnityEngine;

public class HandIKController : MonoBehaviour
{
    public Animator animator; // �L������Animator
    public Transform rightHandTarget; // �����̃O���b�v����

    void OnAnimatorIK(int layerIndex)
    {
        if (rightHandTarget != null)
        {
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);
            animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1f);

            animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandTarget.position);
            animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandTarget.rotation);
        }
    }
}