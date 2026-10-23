using UnityEngine;
using UnityEngine.InputSystem;


public class CatPawController : MonoBehaviour
{
    public float moveForce = 10f;
    public float pawHeight = 0.5f;
    public float pawSensitivity = 0.01f;

    public LayerMask pawSurfaceLayer;

    public Transform pawRestPoint;

    private Vector2 virtualMousePosition;

    private Rigidbody rb;
    private Vector3 targetPosition;

    private bool isControllingPaw; // 마우스를 누르고 움직이게

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        targetPosition = rb.position;

        virtualMousePosition = new Vector2(Screen.width / 2f, Screen.height / 2f);
    }
    // Update is called once per frame
    void Update()
    {
        isControllingPaw = Mouse.current.leftButton.isPressed;

        if (!isControllingPaw)
        {
            return;
        }
        Vector2 mouseDelta = Mouse.current.delta.ReadValue(); // 방향 정보도 들어 있고 이동량 정보도 들어 있다
        virtualMousePosition += mouseDelta;
        virtualMousePosition.x = Mathf.Clamp(virtualMousePosition.x, 0f, Screen.width);
        virtualMousePosition.y = Mathf.Clamp(virtualMousePosition.y, 0f, Screen.height);
        // 가상 마우스가 화면을 벗어나지 않도록


        /*
        Vector3 pawMoveX = Camera.main.transform.right * mouseDelta.x * pawSensitivity;
        Vector3 pawMoveY = Camera.main.transform.up * mouseDelta.y * pawSensitivity;

        targetPosition += pawMoveX + pawMoveY;
        */

        Ray ray = Camera.main.ScreenPointToRay(virtualMousePosition);

        RaycastHit hit; // Ray가 부딪힌 결과 정보가들어간다.

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, pawSurfaceLayer))  // 이 Ray를 3D 공간으로 쏴보고, 무언가에 맞았다면 그 정보를 hit에 넣어줘
        {
            targetPosition = hit.point;
            targetPosition.y += pawHeight;

        }
        /*
         * ray
→ 어떤 광선을 쏠 것인가

out hit
→ 맞은 결과를 어디에 저장할 것인가

Mathf.Infinity
→ 얼마나 멀리 쏠 것인가
  = 무한대까지

pawSurfaceLayer
→ 어떤 Layer만 맞을 것인가
*/

    }

    void FixedUpdate() //물리 시간에 맞춰 Rigidbody를 움직이는 로직
    {
        if (!isControllingPaw)
        {
            targetPosition = pawRestPoint.position;
        }

        Vector3 direction = targetPosition - rb.position; //목표를 향하는 벡터
        // 마우스가 손에 가까우면 약한힘 멀리가면 강한힘이 작용하게

        rb.AddForce(direction * moveForce);
    }
}
