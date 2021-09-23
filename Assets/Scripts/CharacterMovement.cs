using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private CharacterController controller;
    [SerializeField]
    private float turnSmoothTime = 0.1f;
    [SerializeField]
    private float gravity = -9.82f;
    [SerializeField]
    private float speed = 8f;

    private Vector3 desiredPosition;
    private float turnSmoothRate;
    private Vector3 gravVelocity;
    private string stopAnimationTag;
    private Vector3 looksAt;

    // --- Interface --- //

    public void MoveToPosition(Vector3 desiredPosition) {
        this.desiredPosition = desiredPosition;
        AlignTo(desiredPosition);
    }

    public void StopForAnimation(string animationTag) {
        desiredPosition = transform.position;
        stopAnimationTag = animationTag;
    }

    // Align the character so that it looks along the provided vector
    public void AlignTo(Vector3 v) {
        looksAt = new Vector3(v.x, transform.position.y, v.z);
    }

    // --- //

    // Start is called before the first frame update
    void Start()
    {
        desiredPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsTag(stopAnimationTag)) {
            Vector3 direction = (desiredPosition - transform.position).normalized;

            if (Util.xzMagnitude(direction) >= 0.1f) {
                // Running animation
                animator.SetFloat("Speed", Util.xzMagnitude(direction * speed));
                controller.Move(direction * speed * Time.deltaTime);
            }
            else {
                animator.SetFloat("Speed", 0);
            }
        }
        // Player rotation
        Vector3 lookDirection = (looksAt - transform.position).normalized;
        float targetAngle = Mathf.Atan2(lookDirection.x, lookDirection.z) * Mathf.Rad2Deg;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothRate, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
        // Gravity
        gravVelocity.y += gravity * Time.deltaTime;

        if (controller.isGrounded) {
            gravVelocity.Set(0, 0, 0);
        }

        controller.Move(gravVelocity * Time.deltaTime);
    }
}
