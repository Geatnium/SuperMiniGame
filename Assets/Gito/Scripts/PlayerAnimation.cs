using UnityEngine;
using DG.Tweening;

public class PlayerAnimation : MonoBehaviour
{
    private CharacterController controller;
    private Animator animator;

    [SerializeField] private float jumpForce = 10.0f;
    [SerializeField] private float gravity = 10.0f;

    private float moveY = 0.0f;

    public bool isGround { get; private set; }

    private float _animationSpeed = 1.0f;
    public float animationSpeed
    {
        private get { return _animationSpeed; }
        set { _animationSpeed = 1.0f * value; }
    }

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        isGround = true;
    }

    private void Update()
    {
        isGround = controller.isGrounded;
        if (!isGround)
        {
            moveY -= gravity * animationSpeed * animationSpeed * Time.deltaTime;
        }
        animator.speed = animationSpeed * 0.6f;
        animator.SetFloat("VelocityY", controller.velocity.y);
        animator.SetBool("Ground", isGround);

        controller.Move(Vector3.up * moveY * Time.deltaTime);
    }

    public void DoJump()
    {
        if (!isGround)
        {
            return;
        }
        animator.SetTrigger("Jump");
        moveY = jumpForce * animationSpeed;
    }

    public void DoDeath()
    {
        animator.SetTrigger("Death");
        transform.DOMove(new Vector3(-10.0f, 10.0f, 0.0f), 1.0f);
        transform.DORotate(new Vector3(-180.0f, 90.0f, 0.0f), 1.0f);
    }
}
