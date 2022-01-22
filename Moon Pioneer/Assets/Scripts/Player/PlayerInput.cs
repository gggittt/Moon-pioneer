using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerInput : MonoBehaviour
{
    [SerializeField] private Joystick _joystick;
    [SerializeField] private float _moveSpeed = 10f;

    private Rigidbody _rigidbody;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 velocity;
#if UNITY_ANDROID
        velocity = new Vector3(
            _joystick.Horizontal,
            0,
            _joystick.Vertical
        );

#endif
#if UNITY_EDITOR || UNITY_STANDALONE
        velocity = new Vector3(
            Input.GetAxis("Horizontal"),
            0,
            Input.GetAxis("Vertical")
        );
#endif

        if (velocity != Vector3.zero)
        {
            _rigidbody.velocity = velocity * _moveSpeed;
            
            //transform.rotation = Quaternion.LookRotation(-velocity);
        }
        //todo скрипт бракейса или у др возьми


        /*
        if (velocity.x != 0)
            Debug.Log($"<color=cyan> velocity.x  </color>");
        if (velocity.z != 0)
            Debug.Log($"<color=cyan> velocity.z  </color>");
        */

        //if (velocity.x != 0 && velocity.y != 0)
        //   _rigidbody.MovePosition(velocity * _moveSpeed);
    }
}