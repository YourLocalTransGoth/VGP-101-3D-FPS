using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSjhoot : MonoBehaviour
{
    public Transform BulletSpawnPoint;
    public GameObject BulletPrefab;

    public float BulletSpeed = 30.0f;
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var bullet = Instantiate(BulletPrefab, BulletSpawnPoint.position, BulletSpawnPoint.rotation); // makes a new bullet
            bullet.SetActive(true); // inactive bullet made active.
            bullet.GetComponent<Rigidbody>().velocity = Vector3.forward * BulletSpeed; // new Vector3 (0,0,30), gives bullet speed.
            Debug.Log("we are shooting");
            
        }
    }
}