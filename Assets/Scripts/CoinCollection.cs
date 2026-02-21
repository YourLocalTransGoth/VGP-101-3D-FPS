using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollection : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Something is HIT");
        if (other.gameObject.tag == "Coin")
        {
            Debug.Log("Coin is hit");
            Destroy(other.gameObject);
        }
    }
}