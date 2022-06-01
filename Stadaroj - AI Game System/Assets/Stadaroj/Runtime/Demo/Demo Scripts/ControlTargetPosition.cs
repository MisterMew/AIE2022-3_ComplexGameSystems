using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlTargetPosition : MonoBehaviour
{
    /* Variables */
    [SerializeField] private float speed = 1F;

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Jump");
        float z = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(x, y, z);
        transform.Translate(movement * speed * Time.deltaTime);
    }
}
