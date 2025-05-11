using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] GameObject player;
    [Header("�}�E�X���x")]
    [SerializeField] private float mouseSensitivity = 3.0f; // �� �C���X�y�N�^�[�Œ����\��

    private float yaw = 0f;   // ������]�p
    private float pitch = 0f; // ������]�p

    [Header("�J��������")]
    [SerializeField] private float normalDistance = 5.0f;
    [SerializeField] private float zoomSpeed = 5f;

    private float currentDistance;

    [Header("�Y�[�������ƈʒu����")]
    [SerializeField] private bool useCustomZoomOffset = true;
    [SerializeField] GameObject zoomCamera;

    void Start()
    {
        currentDistance = normalDistance;
    }


    void Update()
    {
        // �}�E�X�ړ��ʁi���t���[���̑��Έړ��j
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensitivity;

        // ��]�p���X�V�ipitch�͏㉺�Ayaw�͍��E�j
        yaw += mouseX;
        pitch -= mouseY;
        // �s�b�`�p�x�𐧌��i��������������艺�����������Ȃ��悤�Ɂj
        pitch = Mathf.Clamp(pitch, -30f, 60f);

        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);

        // �Y�[�������ǂ���
        bool isZooming = Input.GetMouseButton(1);

        // �I�t�Z�b�g�v�Z
        Vector3 offset;
        Vector3 targetPosition;

        if (isZooming && useCustomZoomOffset && zoomCamera != null)
        {
            // �Y�[���p�̋�I�u�W�F�N�g�̈ʒu�Ɉړ�
            targetPosition = zoomCamera.transform.position;
        }
        else
        {
            // �ʏ�̉�]�{�����Ɋ�Â����I�t�Z�b�g
            float targetDistance = normalDistance;
            currentDistance = Mathf.Lerp(currentDistance, targetDistance, Time.deltaTime * zoomSpeed);
            Vector3 offsetVec = rotation * new Vector3(0, 0, -currentDistance);
            targetPosition = player.transform.position + offsetVec;
        }

        // �J�����ʒu���X�V
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