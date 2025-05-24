using UnityEngine;
using UnityEngine.InputSystem;

public class Playercontroller : MonoBehaviour
{
    [SerializeField] private float jumpForce = 5.0f;   //�W�����v��
    [SerializeField] private float moveSpeed = 5.0f;�@ //�ړ����x
    [SerializeField] private float turnSpeed = 100f;   // ��]���x
    private Vector2 moveInput;

    [SerializeField] private GameObject attackPrefab;
    [SerializeField] private Transform firePoint;


    private Rigidbody rb;
    private MyControls controls;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        controls = new MyControls();

        //InPut Action��o�^
        controls.Player.Jump.performed += OnJumpPerformed;
        controls.Player.Move.performed += OnMovePerformed;
        controls.Player.Move.canceled += OnMoveCanceled;
        controls.Player.Move.performed += OnSprintPerformed;
        controls.Player.Attack.performed += OnAttackPerformed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        // Input�A�N�V������L����
        controls.Enable();
    }

    private void OnDisable()
    {
        // Input�A�N�V�����𖳌���
        controls.Disable();
    }

    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        // Rigidbody�ɏ�����̗͂�������
        if (rb != null)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
           
        }
    }

    private void FixedUpdate()
    {

        // �O�㍶�E�ւ̈ړ�������
        if (rb != null)
        {
            // ���E��]�iA/D�j
            float turn = moveInput.x * turnSpeed * Time.fixedDeltaTime;
            transform.Rotate(0, turn, 0); // Y���ŉ�]

            // �O��ړ��iW/S�j
            Vector3 move = transform.forward * moveInput.y * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + move);
        }
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        // Move�A�N�V�����̒l���擾
        moveInput = context.ReadValue<Vector2>();

        float speed = Mathf.Abs(moveInput.y);
       
    }

    private void OnSprintPerformed(InputAction.CallbackContext context)
    {

    }
    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        // Move�̓��͂������Ȃ�����ړ����~�߂�
        moveInput = Vector2.zero;
       
    }

    private void OnAttackPerformed(InputAction.CallbackContext context)
    {
        if (attackPrefab != null && firePoint != null)
        {
            // �e�𐶐��i�ʒu�ƌ�����firePoint����j
             Instantiate(attackPrefab, firePoint.position, firePoint.rotation);
        }
    }
}
