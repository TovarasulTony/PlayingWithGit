using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pusher : MonoBehaviour {

    public float time;
    public float speed;
    private bool tempBool = true;
    private float tempFloat = 0;

    private float initX;
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(tempFloat);
        tempFloat += Time.deltaTime;
        //Debug.Log(tempBool);
        if (tempBool == true && tempFloat > time*2)
        {//3.4
            tempBool = false;
            tempFloat -= time;
            tempFloat *= -1;
            //tempFloat = 0;
        }
        else if (tempBool == false && tempFloat > time*2)
        {
            tempBool = true;
            tempFloat -= time*2;
            tempFloat *= -1;
            //tempFloat = 0;
        }

        if (tempBool == true)
        {
            transform.Translate(Vector3.left * Time.deltaTime * 2*speed);
        }
        else
        {
            transform.Translate(Vector3.right * Time.deltaTime*2*speed);
        }
    }
}
