using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.UI.Image;

public class Player: MonoBehaviour
{
    [SerializeField] private float jumpForce = 5.0f;   //�W�����v��
    [SerializeField] private float moveSpeed = 5.0f;�@ //�ړ����x
    [SerializeField] private float turnSpeed = 100f;   // ��]���x


    private Rigidbody rb;
    private MyControls controls;
    private Animator animator;

    private Vector2 moveInput;

    [SerializeField] private float rayDistance = 2f; // ���C�̒���
    [SerializeField] private float rayHeight = 1.5f; //���C�̍���
    [SerializeField] private LayerMask interactableLayer; // �A�C�e���̃��C���[���w��
    private ItemPickupUI currentUI = null;

    private bool isPlayerControlEnabled = true;





    private void Awake()
    {
        //�R���|�[�l���g���擾
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        controls = new MyControls();

       //InPut Action��o�^
        controls.Player.Jump.performed += OnJumpPerformed;
        controls.Player.Move.performed += OnMovePerformed;
        controls.Player.Move.canceled += OnMoveCanceled;
        controls.Player.Interact.performed += OnInteractPerformed;
    }

    private void Update()
    {
        //���C�̐ݒ�
        Vector3 origin = transform.position + Vector3.up * rayHeight;�@//���C�̒��S���߁A�L�����̒��S���獂�����v�Z
        Vector3 direction = transform.forward;�@�@//���C�̌��������߂�A����͑O������

        if (Physics.Raycast(origin, direction, out RaycastHit hit, rayDistance, interactableLayer))�@
        {
            GameObject item = hit.collider.gameObject;

            // �A�C�e����UI������Ȃ�\��
            ItemPickupUI ui = item.GetComponent<ItemPickupUI>();
            if (ui != null)
            {
                if (currentUI != ui)
                {
                    if (currentUI != null) currentUI.ShowPickupUI(false); // �O��UI���\��
                    currentUI = ui;
                    currentUI.ShowPickupUI(true);
                }
            }
        }
        else
        {
            if (currentUI != null)
            {
                currentUI.ShowPickupUI(false);
                currentUI = null;
            }
        }
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
            animator.SetBool("Jump",true);
        }
    }

    private void OnInteractPerformed(InputAction.CallbackContext context)  //�C���^���N�g
    {
        if (currentUI != null)
        {
            GameObject itemObj = currentUI.gameObject;

            // WorldItem �X�N���v�g���擾
            WorldItem worldItem = itemObj.GetComponentInParent<WorldItem>(); 
            if (worldItem != null)
            {
                InventoryItem newItem = worldItem.CreateInventoryItem();
                InVentoryManeger.Instance.AddItem(newItem); //  �C���x���g���ɒǉ�
            }
            else
            {
                Debug.LogWarning("WorldItem ��������܂���ł���");
            }

            Destroy(currentUI.gameObject); // �A�C�e�����ƍ폜
            currentUI = null;
        }
    }

  
    private void FixedUpdate()
    {
        if (!isPlayerControlEnabled) return;  // ���얳���Ȃ珈�����X�L�b�v

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
        animator.SetFloat("Speed", speed);
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        // Move�̓��͂������Ȃ�����ړ����~�߂�
        moveInput = Vector2.zero;
        animator.SetFloat("Speed", 0f);
    }

    public void SetPlayerControl(bool enabled)
    {
        isPlayerControlEnabled = enabled;
    }
}