using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Observer : MonoBehaviour
{
    // detectar el personaje del jugador
    public Transform player;
    public GameEnding gameEnding;
    private Rigidbody m_Rigidbody;
    public GameManager gameManager;
    
    bool m_IsPlayerInRange;

    private void Start()
    {
        m_Rigidbody = player.GetComponent<Rigidbody>();
        gameManager = gameManager.GetComponent<GameManager>();
    }
    void OnTriggerEnter(Collider other)
    {
        
        if (other.transform == player) 
        {
           m_IsPlayerInRange = true;
            Damage();
        }
        
    }
    void OnTriggerExit(Collider other)
    {
        if (other.transform == player)
        {
            m_IsPlayerInRange = false;
        }
    }
    void Update()
    {
       

        //solo si el personaje está en el radio de alcance.
        if (m_IsPlayerInRange)
        { 
            //Vector3.up es una combinación rápida que representa (0, 1, 0). 
            Vector3 direction = player.position - transform.position + Vector3.up;
            Ray ray = new Ray(transform.position, direction);
            RaycastHit raycastHit;
            //devovlera un booleano si algo ha chocado con el rayo
            if (Physics.Raycast(ray, out raycastHit))
            {
                if (raycastHit.collider.transform == player && gameManager.healthBar.value <= 0)
                {
                 gameEnding.CaughtPlayer();
                }
            }
        }
    }
    void Damage()
    {
        //el fin de hacer un nuevo vector y empujarlo hacia la direccion contraria
        //del jugador es hacer un knockback
        Vector3 pushDirection = transform.position - player.position;
        pushDirection = -pushDirection.normalized;
        m_Rigidbody.AddForce(pushDirection * 12, ForceMode.Impulse);
        if (gameObject.tag == "Gargola")
        {
            gameManager.UpdateHealthBar(false);
        }
        else if (gameObject.CompareTag("Fantasma"))
        {
            gameManager.UpdateHealthBar(true);
        }
    }    
}

