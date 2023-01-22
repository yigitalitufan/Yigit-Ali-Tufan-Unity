using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public VariableJoystick joystick;
    public Animator animCtrl;

    public float Speed = 5f;
    public float RotationSpeed = 10f;

    public float altitude;
    public EnterPlane plane;

    private void Start()
    {
        altitude = transform.position.y;
    }
    void Update()
    {
        if (joystick == null)
            return;

        if (animCtrl == null)
            return;


        Vector2 direction = joystick.Direction;

        Vector3 movementVector = new Vector3(direction.x, 0, direction.y);

        movementVector = movementVector * Time.deltaTime * Speed;

        transform.position += movementVector;
        //movementCache += movementVector;

        if (movementVector.magnitude != 0)
        {
            //transform.forward = movementVector;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(movementVector, Vector3.up), Time.deltaTime * RotationSpeed);
        }


        //bool isWalking = direction != Vector2.zero;
        bool isWalking = direction.magnitude > 0;

        animCtrl.SetBool("IsWalking", isWalking);

        animCtrl.SetFloat("SpeedValue", direction.magnitude);

        if (plane.InPlane)
        {
            transform.DOMoveY(30f, 10);
        }
    }

    //private void FixedUpdate()
    //{
    //    if (movementCache != Vector3.zero)
    //    {
    //        transform.position += movementCache;
    //        movementCache = Vector3.zero;  
    //    }

    //}

}
