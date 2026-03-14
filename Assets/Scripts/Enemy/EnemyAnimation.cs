using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private string runStateName = "Run";

    void Start()
    {
        // Get the Animator component attached to this GameObject
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // 1. Attack (Trigger)
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            anim.SetTrigger("attack");
        }

        // 2. Run (Bool) - Toggles Running ON, Idle OFF
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetAnimationState(running: true, idle: false);
        }

        // 3. Idle (Bool) - Toggles Idle ON, Running OFF
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetAnimationState(running: false, idle: true);
        }

        // 4. Stop - Turns both Running and Idle OFF
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SetAnimationState(running: false, idle: false);
        }

        KeepRunLooping();
    }

    // Helper method to keep the code clean
    private void SetAnimationState(bool running, bool idle)
    {
        anim.SetBool("isRunning", running);
        anim.SetBool("isIdle", idle);
    }

    private void KeepRunLooping()
    {
        if (!anim.GetBool("isRunning"))
        {
            return;
        }

        AnimatorStateInfo currentState = anim.GetCurrentAnimatorStateInfo(0);
        if (currentState.IsName(runStateName) && currentState.normalizedTime >= 1f)
        {
            anim.Play(runStateName, 0, 0f);
        }
    }
}