using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinValue = 1;  // Value of the coin, default is 1 coin per collection

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider belongs to the player
        if (other.CompareTag("Player"))
        {
            // Add coins to the GameManager
            GameManager.instance.AddCoin(coinValue);

            // Destroy the coin object after it is collected
            Destroy(gameObject);
        }
    }
}
