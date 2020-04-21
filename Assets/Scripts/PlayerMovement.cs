using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float turnSpeed = 20f;
    public TextMeshProUGUI totalMonedas;
    public int contador = 0;
    public bool hasKey = false;
    public RawImage keyImage;
    public AudioSource coinAudio;
    public AudioSource keyAudio;
    private float jumpForce = 5;
    private float gravityModifier = 10;
    private bool isOnGround = true;
    Vector3 m_Movement;
    Animator m_Animator;
    Quaternion m_Rotation = Quaternion.identity;
    Rigidbody m_Rigidbody;
    AudioSource m_AudioSource;

    // Start is called before the first frame update
    void Start()
    {
        
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
        Physics.gravity = new Vector3(0, -1.0F, 0);
        Physics.gravity *= gravityModifier;
        m_AudioSource = GetComponent<AudioSource>();
        keyImage.enabled = false;
    }

    //usamos el fixedUpdate para que con el bucle de física evitar conflictos entre la física y la animación.
    void FixedUpdate()
    {

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        m_Movement.Set(horizontal, 0f, vertical);
        //lo normalizamos debido a que si no el pj se moveria mas rapido en diagonal que recto
        m_Movement.Normalize();

        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;
        m_Animator.SetBool("IsWalking", isWalking);

        if (isWalking)
        {
            if (!m_AudioSource.isPlaying)
            {
                m_AudioSource.Play();
            }
        }
        else
        {
            m_AudioSource.Stop();
        }

        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
        m_Rotation = Quaternion.LookRotation(desiredForward);

        if (Input.GetKeyDown(KeyCode.Space)&& isOnGround)
        {
            m_Rigidbody.AddForce(Vector3.up * jumpForce,ForceMode.Impulse);
            isOnGround = false;

        }


    }

    void OnAnimatorMove()
    {
        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude);
        m_Rigidbody.MoveRotation(m_Rotation);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Moneda"))
        {
            coinAudio.Play();
            contador++;
            totalMonedas.text = "Monedas: " + contador + "/5";
            Debug.Log(contador);
            Destroy(other.gameObject);
        }
        if (other.CompareTag("Key"))
        {
            keyAudio.Play();
            hasKey = true;
            keyImage.enabled = true;
            Destroy(other.gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
    }
}
