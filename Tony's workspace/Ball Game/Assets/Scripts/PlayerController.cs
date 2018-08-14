using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public float speed;
    public Text countText;
    public Text winText;
    public float jumper;

    private Rigidbody rb;
    private int count;

    void Start()
    {
        Debug.Log(Physics.gravity.x + " " + Physics.gravity.y + " " + Physics.gravity.z);
        //Physics.gravity = new Vector3(0, -500.0F, 0);
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winText.text = "";
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        if (!rb)
        {
            Debug.Log("naspa");
        }
        else
        {
            //Debug.Log("ok");
            rb.AddForce(movement * speed);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            ++count;
            SetCountText();
        }
        else if (other.gameObject.CompareTag("EndOfMap"))
        {
            transform.position = new Vector3(0.0f, 1.5f, 0.0f);
        }
        /*else if(other.gameObject.CompareTag("Wall_Test"))
        {
            Debug.Log("perete");
            float moveHorizontal = -Input.GetAxis("Horizontal");
            float moveVertical = -Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
            rb.AddForce(movement * speed / 2);
        }*/
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("perete333");
        if (collision.gameObject.CompareTag("Jumper"))
        {
            Debug.Log("perete222");
            Vector3 movement = new Vector3(0f, jumper, 0f);
            rb.AddForce(movement*10);
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if(count>=8)
        {
            winText.text = "Wait... Is this loss?";
        }
    }
}
