﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReyalpController : MonoBehaviour {

	public float speed;
	private bool isColliding;
	private float slowFactor = 4;
	private Rigidbody2D rb;
	public AudioClip damageSound_0;
	public AudioClip damageSound_1;
	public AudioClip winSound;

	void Start () {
		rb = GetComponent<Rigidbody2D> ();
	}

	void FixedUpdate () {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector2 movement = new Vector2 (-moveHorizontal/slowFactor, -moveVertical/slowFactor);

		rb.AddForce (movement * speed);
	}

	void NextLevel() {
		GameManager.instance.NextLevel ();
	}

	void PreviousLevel() {
		GameManager.instance.PreviousLevel ();
	}

	void EndGame() {
		GameManager.instance.EndGame ();
	}

	void Restart() {
		GameManager.instance.Reset ();
	}

	void Stop() {
		rb.velocity = Vector3.zero;
	}
		
	void OnTriggerEnter2D(Collider2D other) {
		if(isColliding) return;
		isColliding = true;
		if (other.gameObject.CompareTag ("PickUp")) {
			SoundManager.instance.PlaySingle (winSound);
			other.gameObject.SetActive (false);
			NextLevel ();
		} else if (other.gameObject.CompareTag ("Environment")) {
			SoundManager.instance.RandomizeSfx(damageSound_0, damageSound_1);
			PreviousLevel ();
		} else if (other.gameObject.CompareTag ("Win")) {
			SoundManager.instance.PlayEnding();
			EndGame ();
		}
	}

	void Update() {
		isColliding = false;
	}
}
