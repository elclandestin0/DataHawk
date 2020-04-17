using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve;
using Valve.VR;

public class GameController : MonoBehaviour {

	public SteamVR_LoadLevel loadLevel;
	public bool goalHit = false;
	public bool mainMenu = false;
	public GameObject PlayerController;
    public GameObject Motorcycle;
    public GameObject Player;

	bool lastLevel;
	bool otherLevel;
	bool firstLevel;

	void Start () {
		PlayerController = GameObject.FindGameObjectWithTag ("PlayerController");
		PlayerController.GetComponent<PlayerController> ().hawkFind = true;
        if (loadLevel.levelName == "MainMenu") {
            lastLevel = true;
        } else if (loadLevel.levelName == "2") {
            Debug.Log(loadLevel.levelName);
            firstLevel = true;
            Player = GameObject.FindGameObjectWithTag("Player");
            Motorcycle = GameObject.FindGameObjectWithTag("Motorcycle");
            Player.transform.position = Motorcycle.transform.position;
            Debug.Log(Player.transform);
            Debug.Log(Motorcycle.transform);
        } else {
            otherLevel = true;
        }
	}

	void Update () {
		if (goalHit && firstLevel) {
			GameObject.FindGameObjectWithTag ("Hawk").SetActive (false);
			loadLevel.Trigger();
			firstLevel = false;
		}

		if (goalHit && otherLevel) {
			GameObject.FindGameObjectWithTag ("Teleport_Target_One").SetActive (false);
			GameObject.FindGameObjectWithTag ("Teleport_Target_Two").SetActive (false);
			GameObject.FindGameObjectWithTag ("Hawk").SetActive (false);
			loadLevel.Trigger ();
			otherLevel = false;
		}

		if (goalHit && lastLevel) {
			GameObject.FindGameObjectWithTag ("Hawk").SetActive (false);
			loadLevel.Trigger();
			StartCoroutine (DisablePlayer ());
			lastLevel = false;
		}

		if (mainMenu) {
			loadLevel.Trigger ();
			mainMenu = false;
		}

	}

	IEnumerator DisablePlayer () {
		yield return new WaitForSeconds (0.5f);
		GameObject.FindGameObjectWithTag ("Player").SetActive (false);
	}
}