﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    public float speed;
    public Text test;
    public Text countText;
    public Text winText;
    private bool b;
    private Rigidbody rb ;
    private int count;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winText.text = "";
        test.text = "";
    }

      void Update()
    {
        b = Input.GetKeyDown("space");
        if(Input.GetKeyDown("space"))
        {
            Debug.Log("SPACE WAS PRESSED");
        }
    }
    
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float force;

   
        Vector3 movement = new Vector3(moveHorizontal*speed, 0.0f, 0.0f);

        rb.AddForce(movement,ForceMode.Impulse);

        if (moveHorizontal > 0)
            test.text = "dreapta";
        if (moveHorizontal < 0)
            test.text = "stanga";
        if (moveHorizontal == 0)
            test.text = "nimic";

    }


    void Saritura(Collision other)
    {
        Vector3 jump = new Vector3(0.0f, 10, 0.0f);

        Debug.Log("In afara if-ului " + other.gameObject.tag + " " + Input.GetKeyDown("space"));

        if (b && other.gameObject.CompareTag("Ground"))
        {
            Debug.Log("In if " + other.gameObject.tag);
            rb.AddForce(jump, ForceMode.Impulse);
            test.text = "jump";
        }
    }
 
    void OnCollisionStay(Collision other)  
    {

        Saritura(other);
 
    }

    void OnCollisionEnter(Collision other)
    {

        Saritura(other);

    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }
    }


    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if(count>=15)
        {
            winText.text = "You Win";
        }
    }
}
