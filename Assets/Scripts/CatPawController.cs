using UnityEngine;
using UnityEngine.InputSystem;


public class CatPawController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float pawHeight = 0.5f;
    public LayerMask pawSurfaceLayer;

    private Rigidbody rb;
    private Vector3 targetPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();//현재 마우스의 화면상 위치가져오기

        Ray ray = Camera.main.ScreenPointToRay(mousePosition);

        RaycastHit hit; // Ray가 부딪힌 결과 정보가들어간다.

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, pawSurfaceLayer))  // 이 Ray를 3D 공간으로 쏴보고, 무언가에 맞았다면 그 정보를 hit에 넣어줘
        {
            targetPosition = hit.point;
            targetPosition.y += pawHeight;

            Debug.Log("Ray 충돌 : " + hit.collider.gameObject.name);

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
        Vector3 newPosition = Vector3.MoveTowards(rb.position, targetPosition, moveSpeed * Time.fixedDeltaTime);

        rb.MovePosition(newPosition);
    }
}
