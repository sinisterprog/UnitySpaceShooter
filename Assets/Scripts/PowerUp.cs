using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour
{
    public FamiliarController familiarController;   
    void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            GetComponent<AudioSource>().Play();

        }
       
    }
}
