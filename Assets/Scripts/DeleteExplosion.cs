using UnityEngine;
using System.Collections;

public class DeleteExplosion : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(die(1));

    }

    IEnumerator die(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
        yield return null;
    }
}
