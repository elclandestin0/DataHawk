﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class PlayerController : MonoBehaviour
{

    // Action to spawn hawk
    public SteamVR_Action_Boolean spawnHawk;
    public SteamVR_Action_Boolean getHawk;
    public SteamVR_Action_Boolean interactUI;
    public SteamVR_Action_Vector2 steerMotorcycle;
    public SteamVR_Action_Single accelerateMotorcycle;
    public SteamVR_Action_Single brakeMotorcycle;

    // Sound when hawk is spawned
    public GameObject SpawnObjectSound;
    public GameObject dataHawk;
    public GameObject Motorcycle;
    public GameObject GameController;
    public GameObject menuSound;

    GameObject VRCamera;

    public bool hawkFind;
    bool gettingHawk;

    float time = 0.0f;
    float timeToMove = 3.0f;
    // Use this for initialization
    void Start()
    {
        GameController = GameObject.FindGameObjectWithTag("GameController");
        VRCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    private void FixedUpdate()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            //var leftTriggerDown = interactUI.GetStateDown(SteamVR_Input_Sources.LeftHand);
            //if (leftTriggerDown)
            //{
            //    menuSound.GetComponent<AudioSource>().Play();
            //    GameController.GetComponent<GameController>().mainMenu = true;
            //}
            //var leftXDown = steerMotorcycle.GetStateDown(SteamVR_Input_Sources.LeftHand);
            //if (leftXDown)
            //{
            //    Motorcycle.GetComponent<Motorcycle>().steerMotorcycle = true;
            //    Debug.Log("Steering!");
            //}
            float accelerateValue = 0.0f;
            accelerateValue = accelerateMotorcycle.GetAxis(SteamVR_Input_Sources.LeftHand);
            if (accelerateValue > 0.1f)
            {
                Debug.Log(accelerateValue);
                Motorcycle.GetComponent<Motorcycle>().acceleration = accelerateValue * 20.0f;
                Motorcycle.GetComponent<Motorcycle>().accelerateMotorcycle = true;
            }
            else
            {
                Motorcycle.GetComponent<Motorcycle>().acceleration = accelerateValue * 20.0f;
                Motorcycle.GetComponent<Motorcycle>().accelerateMotorcycle = false;
            }

            var leftAxis = steerMotorcycle.GetAxis(SteamVR_Input_Sources.LeftHand).x;
            //var cameraAngle = VRCamera.transform.eulerAngles.z;
            // TODO: MAKE SURE THIS MOTHER FUCKER FITS
            if (leftAxis >= 0.1f || leftAxis <= -0.1f)
            {
                var steer = leftAxis * 2.0f;
                Motorcycle.GetComponent<Motorcycle>().steerLocation = steer;
                Motorcycle.GetComponent<Motorcycle>().steerMotorcycle = true;
            }

            else
            {
                Motorcycle.GetComponent<Motorcycle>().steerMotorcycle = false;
            }
            //else if (cameraAngle >= 270.0f && cameraAngle <= 347.5f)
            //{
            //    var steer = ((360.0f - cameraAngle)/ 90.0f);
            //    Debug.Log(steer);
            //    Motorcycle.GetComponent<Motorcycle>().steerLocation = steer * 0.5f;
            //    Motorcycle.GetComponent<Motorcycle>().steerMotorcycle = true;
            //} 
            //else
            //{
            //    Motorcycle.GetComponent<Motorcycle>().steerLocation = 0.0f;
            //    Motorcycle.GetComponent<Motorcycle>().steerMotorcycle = false;
            //}

            var brakeValue = brakeMotorcycle.GetAxis(SteamVR_Input_Sources.RightHand);
            if (brakeValue > 0.1f)
            {
                Motorcycle.GetComponent<Motorcycle>().brakeMotorcycle = true;

                Motorcycle.GetComponent<Motorcycle>().brakeDeceleration = brakeValue * 20.0f;
            }
        }
        else
        {
            //var leftXDown = steerMotorcycle.GetStateDown(SteamVR_Input_Sources.LeftHand);
            //var rightXDown = steerMotorcycle.GetStateDown(SteamVR_Input_Sources.RightHand);
            var leftGripDown = spawnHawk.GetStateDown(SteamVR_Input_Sources.LeftHand);
            var leftTriggerDown = getHawk.GetStateDown(SteamVR_Input_Sources.LeftHand);
            var leftTriggerUp = getHawk.GetStateUp(SteamVR_Input_Sources.LeftHand);

            var rightGripDown = spawnHawk.GetStateDown(SteamVR_Input_Sources.RightHand);
            var rightTriggerDown = getHawk.GetStateDown(SteamVR_Input_Sources.RightHand);
            var rightTriggerUp = getHawk.GetStateUp(SteamVR_Input_Sources.RightHand);

            if (leftTriggerDown)
            {
                dataHawk.GetComponent<Hawk>().gettingHawk = true;
                dataHawk.GetComponent<Hawk>().leftTriggerPressed = true;
            }
            if (rightTriggerDown)
            {
                dataHawk.GetComponent<Hawk>().gettingHawk = true;
                dataHawk.GetComponent<Hawk>().rightTriggerPressed = true;
            }

            if (leftGripDown || rightGripDown)
            {
                dataHawk.GetComponent<Hawk>().grabbingHawk = true;
            }
            else
            {
                dataHawk.GetComponent<Hawk>().grabbingHawk = false;
            }
        }

    }

    void Update()
    {
        if (hawkFind)
        {
            dataHawk = GameObject.FindGameObjectWithTag("Hawk");
            hawkFind = false;
        }
    }
}