using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class MoveController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float jumpHeight = 30f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;

    private CharacterController controller;
    private MyControls controls;
    private Vector2 moveInput;
    private Vector3 velocity;
    private bool isGrounded;

    private Animator animator;

    private float currentMoveX = 0f;
    private float currentMoveY = 0f;

    public float rayDistance = 3f; // ���m���鋗��
    public Color debugRayColor = Color.red;


    void Awake()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        controls = new MyControls();

        controls.Player.Jump.performed += OnJumpPerformed;
        controls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => moveInput = Vector2.zero;
    }

    private void OnEnable()
    {
        controls.Enable();
    }
    private void OnDisable()
    {
        controls.Disable();
    }

    void Update()
    {
        // �n�ʃ`�F�b�N�iSphere���g���Đڒn����j
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.3f, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // �n�ʂɋz��������
            animator.SetBool("Jump", false);
        }

        // �J������̈ړ�����
        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;
        camForward.y = 0;
        camRight.y = 0;
        camForward.Normalize();
        camRight.Normalize();

        Vector3 move = camForward * moveInput.y + camRight * moveInput.x;
        Vector3 finalMove = move * moveSpeed + new Vector3(0, velocity.y, 0);
        controller.Move(finalMove * Time.deltaTime);


        // �A�j���[�^�[����
        Vector3 localMove = transform.InverseTransformDirection(move);
        currentMoveX = Mathf.Lerp(currentMoveX, localMove.x, 0.1f);
        currentMoveY = Mathf.Lerp(currentMoveY, localMove.z, 0.1f);
        animator.SetFloat("MoveX", currentMoveX);
        animator.SetFloat("MoveY", currentMoveY);

        // �d��
        velocity.y += gravity * Time.deltaTime;
       

        // ��]�i�ړ����̂݁j
        if (move.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 0.2f);
        }
    }

    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        if (isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            animator.SetBool("Jump", true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Item")
        {
            
        }
    }

   

}
