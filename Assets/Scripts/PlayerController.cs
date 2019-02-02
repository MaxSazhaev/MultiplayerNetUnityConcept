using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking; // <=========

public class PlayerController : NetworkBehaviour {


	public GameObject bulletPrefab;
	public Transform bulletSpawn;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!this.isLocalPlayer) {
			return;
		}
			float vMove = Input.GetAxis ("Vertical") * Time.deltaTime * 100;
			float hMove = Input.GetAxis ("Horizontal") * Time.deltaTime * 100;
			//var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
			//var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

			transform.Rotate (0, hMove, 0);
			transform.Translate (0, 0, vMove);

		if (Input.GetKeyDown(KeyCode.Space))
		{
			CmdFire();
		}

	}
	// This [Command] code is called on the Client …
	// … but it is run on the Server!
	[Command]
	void CmdFire()
	{
		// Create the Bullet from the Bullet Prefab
		var bullet = (GameObject)Instantiate(
			bulletPrefab,
			bulletSpawn.position,
			bulletSpawn.rotation);

		// Add velocity to the bullet
		bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 6;

		// Spawn the bullet on the Clients
		NetworkServer.Spawn(bullet);

		// Destroy the bullet after 2 seconds
		Destroy(bullet, 2.0f);        
	}

	public override void OnStartLocalPlayer()
	{
		GetComponent<MeshRenderer>().material.color = Color.blue;
	}
}
