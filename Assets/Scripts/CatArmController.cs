using UnityEngine;

public class CatArmController : MonoBehaviour
{
    public Transform armAnchor;
    public Transform pawJoint;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = pawJoint.position - armAnchor.position;

        float distance = direction.magnitude;

        transform.position = armAnchor.position + direction / 2f;

        transform.rotation = Quaternion.LookRotation(direction);

        Vector3 scale = transform.localScale;
        scale.z = distance;
        transform.localScale = scale;
      
    }
}
