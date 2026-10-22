using UnityEngine;
using UnityEngine.InputSystem;


public class CameraController : MonoBehaviour
{
    private float pitch = 0f; // 현재 카메라가 위아래로 얼마나 회전했는지 기억하는 값
    private float yaw = 0f; // 좌우 회전값저장용

    public float mouseSensitivityX = 0.1f;
    public float mouseSensitivityY = 0.05f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pitch = transform.localEulerAngles.x;
        yaw = transform.localEulerAngles.y;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current.leftButton.isPressed)
        {
            return;
        }

        Vector2 mouseDelta = Mouse.current.delta.ReadValue();

        // 마우스의 좌우 이동량
        float mouseX = mouseDelta.x * mouseSensitivityX;
        float mouseY = mouseDelta.y * mouseSensitivityY;

        yaw += mouseX;

        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, -80f, 80f); // pitch를 -80~80사이로 제한.

        transform.localRotation = Quaternion.Euler(pitch, yaw, 0f);
        //카메라의 회전을 X = pitch, Y = 현재 Y각도 + 마우스 이동량, Z = 0으로 설정한다.
    }
}
