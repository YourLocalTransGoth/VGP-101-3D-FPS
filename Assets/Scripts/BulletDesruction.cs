using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDesctrucion : MonoBehaviour

{
    public float bulletSpan = 5;
    public PlayerMovement player;

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.Find("Player").GetComponent<PlayerMovement>();
        Destroy(gameObject, bulletSpan);
    }

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.SetActive(false);
        Debug.Log("It was trigger");
        Destroy(gameObject);
        player.numOfEnDest++;
    }

 


    // Update is called once per frame
    void Update()
    {
        
    }
}