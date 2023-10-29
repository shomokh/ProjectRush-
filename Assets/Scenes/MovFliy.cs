using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovFliy : MonoBehaviour
{
    public float speed = 12.5f;
    public float flySeed = 1f;
    public float drag = 6;

    public Rigidbody rb;

    public Vector3 rot;

    public float percentage;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rot = transform.eulerAngles;
    }

    private void Update()
    {
        

        // Rotate the player
        //x
        rot.x += 20 * Input.GetAxis("Vertical") * Time.deltaTime;
        rot.x = Mathf.Clamp(rot.x, -45 , 45);
        //y
        rot.y += 20 * Input.GetAxis("Horizontal") * Time.deltaTime;
        //z
        rot.z = -5 * Input.GetAxis("Horizontal");
        rot.z = Mathf.Clamp(rot.z, -5, 5);
        transform.rotation = Quaternion.Euler(rot);

        percentage = rot.x / 45;
        //Drag: Fast(4), Slow(6)
        float mod_drag = (percentage * -4) / drag;
        //Debug.Log("mod drag :" +mod_drag);
        //Debug.Log("precentage :" + mod_drag);
        //Debug.Log("rot x" + rot.x);
        flySeed = -rot.x * flySeed;
        // Speed: Fast(13.8), Slow(12.5)
        float mod_speed = percentage * (13.8f - speed) + speed;

        rb.drag = mod_drag;
        Debug.Log("original speed" + rb.velocity);
        //Vector3 localV = transform.InverseTransformDirection(rb.velocity);
        Vector3 localV = new Vector3(0f, 0f, 0f);
        Debug.Log("transformed speed" + localV);
        localV.z = mod_speed;
        localV.y = flySeed;
         //localV.y = mod_speed;
         Vector3 lastSpeed = transform.TransformDirection(localV);
        Debug.Log("lastSpeed" + lastSpeed);
        rb.velocity = lastSpeed;
    }
}
