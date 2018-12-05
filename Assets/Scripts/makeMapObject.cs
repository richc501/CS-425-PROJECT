using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class makeMapObject : MonoBehaviour {

    public RectTransform image;

	// Use this for initialization
	void Start () {
        MiniMapController.RegisterMapObject(this.gameObject, image);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnDestroy()
    {
        MiniMapController.RemoveMapObject(this.gameObject);
    }
}
