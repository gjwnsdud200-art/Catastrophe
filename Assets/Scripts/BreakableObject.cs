using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    [SerializeField] private GameObject brokenPrefab;
    [SerializeField] private float breakThreshold = 3f;
    [SerializeField] private float explosionForce = 3f;
    [SerializeField] private float explosionRadius = 2f;

    private Rigidbody rb;
    private bool isBroken = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isBroken)
        {
            return;
        }

        float impactSpeed = collision.relativeVelocity.magnitude;

        if (impactSpeed >= breakThreshold)
        {
            Break();
        }
    }

    private void Break()
    {
        isBroken = true;

        GameObject brokenObject = Instantiate(brokenPrefab, transform.position, Quaternion.identity);
        // 현재 컵과 같은 위치와 회전으로 파편 생성

        Rigidbody[] pieces = brokenObject.GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody piece in pieces)
        {
            if (rb != null)
            {
                // 원래 컵이 움직이던 속도를 파편에 전달
                piece.linearVelocity = rb.GetPointVelocity(piece.worldCenterOfMass);
                piece.angularVelocity = rb.angularVelocity;
            }

            piece.AddExplosionForce(explosionForce, transform.position, explosionRadius);
        }

        Destroy(gameObject);
    }




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
