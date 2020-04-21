using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Slider healthBar;
    public TextMeshProUGUI lifeText;
    public TextMeshProUGUI totalMonedas;
    public RawImage keyImage;
    public AudioSource coinAudio;
    public AudioSource keyAudio;
    public bool hasKey = false;
    public int contador = 0;
    public void UpdateHealthBar(bool isGhost)
    {
        if (!isGhost)
        {
            healthBar.value -= 0.25f;
            lifeText.text = healthBar.value * 100 + "%";
        }
        else if (isGhost)
        {
            healthBar.value -= 0.5f;
            lifeText.text = healthBar.value * 100 + "%";
        }
    }
    public void updateCollectibles(bool isKey,Collider other)
    {
        if (!isKey)
        {
            coinAudio.Play();
            contador++;
            totalMonedas.text = "Monedas: " + contador + "/5";
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
}
