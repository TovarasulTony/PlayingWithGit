using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour {

    private int contor;

    public float speed;

    public float jumper;

    private bool semafor;

    private bool rejump;

    private Rigidbody rb;

    void Start()
    {
        contor = 3;
        rb = GetComponent<Rigidbody>();
        semafor = false;
        rejump = false;
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);

        Vector3 jump = new Vector3(0.0f, jumper * 100, 0.0f);

        if (Input.GetKeyUp("space"))
            rejump = true;
        if (semafor == true && Input.GetKeyDown("space") == true)
        {
            rejump = false;
            contor--;
            rb.AddForce(jump);
        }

        if (Input.GetKeyDown("space") == true && contor != 0 && rejump == true)
        {
            rejump = false;
            contor--;
            rb.AddForce(jump);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("on collision enter");
        semafor = true;
        contor = 3;
    }

    void OnCollisionExit(Collision collision)
    {
        Debug.Log("on collision stay");
        semafor = false;
    }
}
