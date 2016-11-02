using UnityEngine;

public class DestroyAsteroid : MonoBehaviour
{
    public GameObject explosion;
    public GameObject child, powerUp;
    public int size = 3;
    public int scoreValue;
    private GameController gameController;

    void Start()
       
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent <GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find GameController");
        }
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Boundary")
        {
            return;
        }
        if (other.tag == "Player")
        {
            return;
        }
        Instantiate(explosion, transform.position, transform.rotation);
      //  gameController.AddScore(scoreValue);

        if (size <=1)
        {
            return;
        }

        int x = Random.Range(0, 10);
        if (x <= 4)
        {
            Vector3 newLocation = new Vector3(transform.position.x , 0, transform.position.z);
            GameObject debris = (GameObject)Instantiate(child, newLocation, transform.rotation);
            debris.transform.localScale -= new Vector3(0.5f, 0.5f, 0.5f);
            debris.GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-4, 4), 0, Random.Range(2, 4));
            debris.GetComponent<DestroyAsteroid>().size = 2;   
        }

       else  if (Random.Range(0, 10) == 2) //Spawning powerups on top of asteroids is annoying
        {
          GameObject gb = (GameObject)  Instantiate(powerUp, transform.position, transform.rotation);
            gb.transform.forward = gameObject.transform.forward;
            Vector3 movement = new Vector3(0, 0, 3);
            gb.GetComponent<Rigidbody>().AddForce(movement);
        }
        Destroy(other.gameObject);
        Destroy(gameObject);
    }
}
