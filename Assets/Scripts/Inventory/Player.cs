using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.UI.Image;

public class Player: MonoBehaviour
{
    [SerializeField] private float jumpForce = 5.0f;   //ジャンプ力
    [SerializeField] private float moveSpeed = 5.0f;　 //移動速度
    [SerializeField] private float turnSpeed = 100f;   // 回転速度


    private Rigidbody rb;
    private MyControls controls;
    private Animator animator;

    private Vector2 moveInput;

    [SerializeField] private float rayDistance = 2f; // レイの長さ
    [SerializeField] private float rayHeight = 1.5f; //レイの高さ
    [SerializeField] private LayerMask interactableLayer; // アイテムのレイヤーを指定
    private ItemPickupUI currentUI = null;

    private bool isPlayerControlEnabled = true;





    private void Awake()
    {
        //コンポーネントを取得
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        controls = new MyControls();

       //InPut Actionを登録
        controls.Player.Jump.performed += OnJumpPerformed;
        controls.Player.Move.performed += OnMovePerformed;
        controls.Player.Move.canceled += OnMoveCanceled;
        controls.Player.Interact.performed += OnInteractPerformed;
    }

    private void Update()
    {
        //レイの設定
        Vector3 origin = transform.position + Vector3.up * rayHeight;　//レイの中心決め、キャラの中心から高さを計算
        Vector3 direction = transform.forward;　　//レイの向きを決める、今回は前方向に

        if (Physics.Raycast(origin, direction, out RaycastHit hit, rayDistance, interactableLayer))　
        {
            GameObject item = hit.collider.gameObject;

            // アイテムにUIがあるなら表示
            ItemPickupUI ui = item.GetComponent<ItemPickupUI>();
            if (ui != null)
            {
                if (currentUI != ui)
                {
                    if (currentUI != null) currentUI.ShowPickupUI(false); // 前のUIを非表示
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
            animator.SetBool("Jump",true);
        }
    }

    private void OnInteractPerformed(InputAction.CallbackContext context)  //インタラクト
    {
        if (currentUI != null)
        {
            GameObject itemObj = currentUI.gameObject;

            // WorldItem スクリプトを取得
            WorldItem worldItem = itemObj.GetComponentInParent<WorldItem>(); 
            if (worldItem != null)
            {
                InventoryItem newItem = worldItem.CreateInventoryItem();
                InVentoryManeger.Instance.AddItem(newItem); //  インベントリに追加
            }
            else
            {
                Debug.LogWarning("WorldItem が見つかりませんでした");
            }

            Destroy(currentUI.gameObject); // アイテムごと削除
            currentUI = null;
        }
    }

  
    private void FixedUpdate()
    {
        if (!isPlayerControlEnabled) return;  // 操作無効なら処理をスキップ

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
        animator.SetFloat("Speed", speed);
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        // Moveの入力が無くなったら移動を止める
        moveInput = Vector2.zero;
        animator.SetFloat("Speed", 0f);
    }

    public void SetPlayerControl(bool enabled)
    {
        isPlayerControlEnabled = enabled;
    }
}