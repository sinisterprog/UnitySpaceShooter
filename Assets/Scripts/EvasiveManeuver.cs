using UnityEngine;
using System.Collections;

public class EvasiveManeuver : MonoBehaviour {

    public Vector2 startWait;
    public float dodge;
    public Vector2 maneuverTime;
    public Vector2 maneuverWait;
    public float smoothing;
    public float tilt = 2;

    private float currentSpeed;
    private float targetManeuver;
    private Rigidbody rb;
    public float speed = 2;

    void Start () {
        rb = GetComponent<Rigidbody>();

        GetComponent<Rigidbody>().velocity = transform.forward * speed;

        currentSpeed = rb.velocity.z;
        StartCoroutine(Evade());
	}

    IEnumerator Evade()
    {
        yield return new WaitForSeconds(Random.Range(startWait.x, startWait.y));
        while (true)
        {
            targetManeuver = Random.Range(1, dodge) * -Mathf.Sign(transform.position.x);
            yield return new WaitForSeconds(Random.Range(maneuverTime.x, maneuverTime.y));
            targetManeuver = 0f;
            yield return new WaitForSeconds(Random.Range(maneuverTime.x, maneuverTime.y));
        }
    }

	void FixedUpdate () {
        float newManeuver = Mathf.MoveTowards(rb.velocity.x, targetManeuver, Time.deltaTime * smoothing);
        rb.velocity = new Vector3(newManeuver, 0f, currentSpeed);
        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
	}
}
