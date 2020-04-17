using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    // detectar el personaje del jugador
    public Transform player;
    public GameEnding gameEnding;

    bool m_IsPlayerInRange;
    void OnTriggerEnter(Collider other)
    {
        if (other.transform == player)
        {
            m_IsPlayerInRange = true;
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
                if (raycastHit.collider.transform == player)
                {
                    gameEnding.CaughtPlayer();
                }
            }
        }
    }
}
