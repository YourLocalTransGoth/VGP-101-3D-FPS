using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public float horizontalInput;
    public float verticalInput;
    public GameObject player;

    private Transform mytf;
    public float numOfEnDest = 0;
    public TextMeshProUGUI EnNum;



    // Start is called before the first frame update
    void Start()
    {
        mytf = player.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");


        mytf.Translate(Vector3.forward * horizontalInput * Time.deltaTime * 10);
        mytf.Translate(Vector3.right * verticalInput * Time.deltaTime * 10);

        EnNum.text = "Num of Enemy destroyed: " + numOfEnDest;

    }
}