using UnityEngine;
using UnityEngine.InputSystem;

public class Playercontroller : MonoBehaviour
{
    [SerializeField] private float jumpForce = 5.0f;   //ジャンプ力
    [SerializeField] private float moveSpeed = 5.0f;　 //移動速度
    [SerializeField] private float turnSpeed = 100f;   // 回転速度
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

        //InPut Actionを登録
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
        // Inputアクションを有効化
        controls.Enable();
    }

    private void OnDisable()
    {
        // Inputアクションを無効化
        controls.Disable();
    }

    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        // Rigidbodyに上方向の力を加える
        if (rb != null)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
           
        }
    }

    private void FixedUpdate()
    {

        // 前後左右への移動を処理
        if (rb != null)
        {
            // 左右回転（A/D）
            float turn = moveInput.x * turnSpeed * Time.fixedDeltaTime;
            transform.Rotate(0, turn, 0); // Y軸で回転

            // 前後移動（W/S）
            Vector3 move = transform.forward * moveInput.y * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + move);
        }
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        // Moveアクションの値を取得
        moveInput = context.ReadValue<Vector2>();

        float speed = Mathf.Abs(moveInput.y);
       
    }

    private void OnSprintPerformed(InputAction.CallbackContext context)
    {

    }
    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        // Moveの入力が無くなったら移動を止める
        moveInput = Vector2.zero;
       
    }

    private void OnAttackPerformed(InputAction.CallbackContext context)
    {
        if (attackPrefab != null && firePoint != null)
        {
            // 弾を生成（位置と向きをfirePointから）
             Instantiate(attackPrefab, firePoint.position, firePoint.rotation);
        }
    }
}
