using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    Joystick joyStick;
    [SerializeField]
    float speed;
    [SerializeField]
    float rotateSpeed;

    Animator animator;

    Vector3 moveDirection = Vector3.zero;

    public static PlayerController Instance;

    public GameObject Slot;

    void Start()
    {
        Instance = this;
        animator = GetComponent<Animator>();
        Slot = transform.GetChild(3).gameObject;
    }

    //Summary: Karakterin hareket iþlevi burada bulunmaktadýr.
    private void Update()
    {
        moveDirection = new Vector3(-joyStick.Horizontal, 0.0f, -joyStick.Vertical);

        if (!moveDirection.Equals(Vector3.zero))
        {
            moveDirection.Normalize();
            transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveDirection, Vector3.up), rotateSpeed * Time.deltaTime);
            //transform.rotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            animator.SetBool("IsRunning", true);
        }
        else
        {
            animator.SetBool("IsRunning", false);
        }

    }

   


}
