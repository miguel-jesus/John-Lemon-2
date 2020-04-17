using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class DoorOpen : MonoBehaviour
{
    public GameObject player;
    public GameObject secretDoor;

    Rigidbody rigidbody;
    PlayerMovement playerMovement;
     

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = secretDoor.GetComponent<Rigidbody>();
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.K) && playerMovement.hasKey)
        {
            rigidbody.mass = 1;
            playerMovement.keyImage.enabled = false;
        }
    }
}
