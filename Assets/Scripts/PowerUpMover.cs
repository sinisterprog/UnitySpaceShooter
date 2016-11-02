using UnityEngine;
using System.Collections;

public class PowerUpMover : MonoBehaviour
{

    public float thrust;
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        transform.forward = new Vector3(0.0f, 0.0f, -1.0f);
    }
    void Update()
    {
        rb.AddForce(transform.forward * thrust);
    }
 
}
