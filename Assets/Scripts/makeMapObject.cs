using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class makeMapObject : MonoBehaviour {

    public RectTransform image;

	// Use this for initialization
	void Start () {
        Collider[] hit = Physics.OverlapSphere(transform.position, 149);
        //if we didn't hit anything return true;
        foreach(Collider c in hit){
            if(!gameObject.name.Equals("Goal"))
            Destroy(c.gameObject);
        }
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
