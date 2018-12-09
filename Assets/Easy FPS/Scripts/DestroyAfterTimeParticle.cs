using System.Collections;
using UnityEngine;

public class DestroyAfterTimeParticle : MonoBehaviour {
	[Tooltip("Time to destroy")]
	public float timeToDestroy = 5f;
	/*
	* Destroys gameobject after its created on scene.
	* This is used for particles and flashes.
	*/
	void Start () {
        //transform.position = new Vector3(0, 0, 0);
		Destroy (gameObject, timeToDestroy);
	}

}
