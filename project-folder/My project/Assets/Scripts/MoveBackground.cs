using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackground : MonoBehaviour {



	public float speed;
	private float x;
	public float destinationPoint;
	public float originalPoint;




	// Use this for initialization
	void Start () {
		//PontoOriginal = transform.position.x;
	}
	
	// Update is called once per frame
	void Update () {


		x = transform.position.x;
		x += speed * Time.deltaTime;
		transform.position = new Vector3 (x, transform.position.y, transform.position.z);



		if (x <= destinationPoint){

			Debug.Log ("hhhh");
			x = originalPoint;
			transform.position = new Vector3 (x, transform.position.y, transform.position.z);
		}


	}
}
