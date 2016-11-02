using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour
{
	public GameObject explosion;
	public GameObject playerExplosion;
	public int scoreValue = 20;
	private GameController gameController;

	void Start ()
	{
        Debug.Log("Starting + " + GetComponent<Collider>().tag);

        GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("GameController");
		if (gameControllerObject != null)
		{
			gameController = gameControllerObject.GetComponent <GameController>();
		}
		if (gameController == null)
		{
			Debug.Log ("Cannot find 'GameController' script");
		}
	}

    void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag( "Boundary" ) || other.CompareTag("Enemy")) 
        {
            return;
        }

        if (other.tag == "Bullet")
        {
            gameController.AddScore(scoreValue);
            GetComponent<AudioSource>().Play();
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        if (explosion != null)
		{
			Instantiate(explosion, transform.position, transform.rotation);

        }

        if (other.tag == "Player")
		{
			Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
			gameController.GameOver();
		}

	}

}