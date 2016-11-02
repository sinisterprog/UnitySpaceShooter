using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
    public float xMax, xMin, zMax, zMin;
}

public class PlayerController : MonoBehaviour {
    private Rigidbody rb;
    public float speed;
    public Boundary boundary;
    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;
    private float nextFire;
    private bool dying;
    public FamiliarController familiarController;
    public GameObject playerExplosion;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        boundary.xMax = 6;
        boundary.xMin = -6;
        boundary.zMax = 6;
        boundary.zMin = -3;
        speed = 6;
        fireRate = 0.25f;
        dying = false;
    }

    public void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire) {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            familiarController.fire();
            GetComponent<AudioSource>().Play();
        } 
    }

    // Update is called once per frame
    void FixedUpdate () {
        if (dying)
        {
            return;
        }
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement =new Vector3 (moveHorizontal, 0.0f, moveVertical);
        rb.velocity = movement * speed;
        rb.position = new Vector3
        (
            Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
            0.0f,
            Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
        );
        if (moveHorizontal != 0 || moveVertical != 0)
        {
            familiarController.updatePosition();
        }
        rb.rotation = Quaternion.Euler(0.0f, 0.0f, -rb.velocity.x * 6);
	}
    void OnTriggerEnter(Collider other)
    {
        
        if (dying == true)
        {
            return;
        }
        if (other.tag == "PowerUp")
        {
            Destroy(other.gameObject);
            familiarController.addFamiliar();
        }
        else if (other.tag == "Asteroid" || other.tag == "Enemy")
        {

            dying = true;
            rb = GetComponent<Rigidbody>();
            rb.angularVelocity = Random.insideUnitSphere * 4;
            rb.velocity = rb.velocity / 2;

            StartCoroutine(die(1));
        }
    }
    IEnumerator die(float time)
    {
        yield return new WaitForSeconds(time);
        Instantiate(playerExplosion, transform.position, transform.rotation);
        Destroy(gameObject);
        dying = false;
        yield return null;
    }
}
