using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class PlayerHealthBar : MonoBehaviour
{

    RawImage healthBarRawImage;
    PlayerHealth playerHealth;

    // Use this for initialization
    void Start()
    {
        playerHealth = FindObjectOfType<Player>().GetComponent<PlayerHealth>();
        healthBarRawImage = GetComponent<RawImage>();
    }

    // Update is called once per frame
    void Update()
    {
        float xValue = -(playerHealth.healthAsPercentage / 2f) - 0.5f;
        healthBarRawImage.uvRect = new Rect(xValue, 0f, 0.5f, 1f);
    }
}
