using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Valve.VR.InteractionSystem
{
    [RequireComponent(typeof(Interactable))]
    public class Motorcycle : MonoBehaviour
    {
        public GameObject player;
        GameObject VRCamera;
        public bool getMotorcycle = false;
        public bool steerMotorcycle = false;
        public bool accelerateMotorcycle = false;
        public bool brakeMotorcycle = false;

        public bool leftGripPressed;

        public float time = 0.0f;
        float timeToMove = 1.5f;
        public float steerLocation = 0.0f;
        public float speed = 0.0f;
        public float maxSpeed = 120.0f; //This is the maximum speed that the object will achieve
        public float acceleration = 0.0f; //How fast will object reach a maximum speed
        float deceleration = 3.0f; //How fast will object reach a speed of 0
        public float brakeDeceleration = 0.0f;
        // Use this for initialization
        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            VRCamera = GameObject.FindGameObjectWithTag("MainCamera");
        }

        // Update is called once per frame
        void Update()
        {
            if (getMotorcycle == true)
            {
                GetMotorcycle();
            }

            if (accelerateMotorcycle == true)
            {
                AccelerateMotorcycle();
            }
            else
            {
                DeccelerateMotorcycle();
            }

            if (steerMotorcycle == true)
            {
                SteerMotorcycle();
            }

            if (brakeMotorcycle == true)
            {
                BrakeMotorcycle();
            }
        }

        void GetMotorcycle()
        {
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            Quaternion finalCycleAngle = Quaternion.Euler(0, 180, 0);
            time += Time.deltaTime;
            float lerpTime = time / timeToMove;
            transform.position = Vector3.Lerp(transform.position, player.transform.position, lerpTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, finalCycleAngle, lerpTime);
            // play sound
            if (lerpTime >= 1.0f)
            {
                gameObject.GetComponent<Rigidbody>().isKinematic = false;
                getMotorcycle = false;
                leftGripPressed = false;
                time = 0.0f;
            }
        }

        void AccelerateMotorcycle()
        {
            Debug.Log("accelerating");
            if (speed <= maxSpeed)
            {
                speed = speed + acceleration * Time.deltaTime;
                transform.position += -transform.forward * Time.deltaTime * speed;
            }
            else
            {
                speed = maxSpeed;
                transform.position += -transform.forward * Time.deltaTime * speed;
            }
        }

        void SteerMotorcycle()
        {
            float angle = transform.localEulerAngles.z;
            angle = (angle > 180) ? angle - 360 : angle;
            float finalAngle = Mathf.Floor(angle);
            Debug.Log(finalAngle);
            if (finalAngle >= -90.0f)
            {
                transform.RotateAround(transform.position, transform.forward, Time.deltaTime * steerLocation * 90f);
            } else if (finalAngle <= 90.0f)
            {
                transform.RotateAround(transform.position, transform.forward, Time.deltaTime * steerLocation * 90f);
            }
        }

        void DeccelerateMotorcycle()
        {
            if (speed >= 0.0f)
            {
                speed = speed - deceleration * Time.deltaTime;
                transform.position += -transform.forward * Time.deltaTime * speed;
            }
        }

        void BrakeMotorcycle()
        {
            if (speed >= 0.0f)
            {
                speed = speed - brakeDeceleration * Time.deltaTime;
                transform.position += -transform.forward * Time.deltaTime * speed;
            }
        }
    }
}
