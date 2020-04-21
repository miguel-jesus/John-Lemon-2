using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class DoorOpen : MonoBehaviour
{
    public GameObject player;
    public GameObject secretDoor;
    public AudioSource doorAudio;

    Rigidbody rigidbody;
    PlayerMovement playerMovement;
    bool isOpen = false;

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
            if(isOpen == false)
            {
                doorAudio.Play();
                isOpen = true;
            }
            rigidbody.mass = 1;
            playerMovement.keyImage.enabled = false;
        }
    }
}
