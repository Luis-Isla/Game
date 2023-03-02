using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
   
    public float speedMovement = 3f;
    public float speedRotation = 200.0f;
    private Animator anim;
    public float x, y;

    void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        Movement();
    }

    void Movement()
    {
       x = Input.GetAxis("Horizontal");
       y = Input.GetAxis("Vertical");
        //Vector3 movJugador = new Vector3(hor, 0, ver);
        //transform.Translate(movJugador * speedMovement * Time.deltaTime);

        transform.Rotate(0, x * Time.deltaTime * speedRotation, 0);
        transform.Translate(0,0,y*Time.deltaTime * speedMovement);

        anim.SetFloat("VelX", x);
        anim.SetFloat("VelY", y);
    }

}
