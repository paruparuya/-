using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] GameObject player;
    [Header("マウス感度")]
    [SerializeField] private float mouseSensitivity = 3.0f; // ← インスペクターで調整可能に

    private float yaw = 0f;   // 水平回転角
    private float pitch = 0f; // 垂直回転角

    [Header("カメラ距離")]
    [SerializeField] private float normalDistance = 5.0f;
    [SerializeField] private float zoomSpeed = 5f;

    private float currentDistance;

    [Header("ズーム距離と位置調整")]
    [SerializeField] private bool useCustomZoomOffset = true;
    [SerializeField] GameObject zoomCamera;

    void Start()
    {
        currentDistance = normalDistance;
    }


    void Update()
    {
        // マウス移動量（毎フレームの相対移動）
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensitivity;

        // 回転角を更新（pitchは上下、yawは左右）
        yaw += mouseX;
        pitch -= mouseY;
        // ピッチ角度を制限（上を向きすぎたり下を向きすぎないように）
        pitch = Mathf.Clamp(pitch, -30f, 60f);

        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);

        // ズーム中かどうか
        bool isZooming = Input.GetMouseButton(1);

        // オフセット計算
        Vector3 offset;
        Vector3 targetPosition;

        if (isZooming && useCustomZoomOffset && zoomCamera != null)
        {
            // ズーム用の空オブジェクトの位置に移動
            targetPosition = zoomCamera.transform.position;
        }
        else
        {
            // 通常の回転＋距離に基づいたオフセット
            float targetDistance = normalDistance;
            currentDistance = Mathf.Lerp(currentDistance, targetDistance, Time.deltaTime * zoomSpeed);
            Vector3 offsetVec = rotation * new Vector3(0, 0, -currentDistance);
            targetPosition = player.transform.position + offsetVec;
        }

        // カメラ位置を更新
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * zoomSpeed);
        if (isZooming)
        {
            Vector3 lookTarget = player.transform.position + player.transform.forward * 10f;
            transform.LookAt(lookTarget);
        }
        else
        {
            transform.LookAt(player.transform.position);
        }
    }
}