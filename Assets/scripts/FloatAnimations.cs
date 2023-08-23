using UnityEngine;

public class FloatAnimations : MonoBehaviour
{
    private Animator animator;

    Fishing fishing;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        fishing = Fishing.instance;

        fishing.onFloatActionCallBack += ChangeAnimationState;
    }


    void ChangeAnimationState(string newState)
    {
        animator.Play(newState);
    }
}
