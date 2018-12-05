using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pointMoveToGoal : MonoBehaviour {
    public Transform goal;
    Vector3 initPos;
    private Collider2D collider2D;
    private GameObject shapeCollider;
    // Use this for initialization
	void Start () {
        initPos = goal.position;
        transform.position = new Vector3(0, 800, 0);
        shapeCollider = GameObject.FindGameObjectWithTag("Colider");
        collider2D = shapeCollider.GetComponent<BoxCollider2D>();
	}

    private void Update()
    {
        float moveSpeed = 10 * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, goal.position, moveSpeed);

        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, goal.position);
        if(hits.Length > 0)
        {
            for(int i=0; i<hits.Length; i++)
            {
                Debug.Log(hits[i].transform.name);
            }
        }
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        //Vector3 newPos = goal.position;
        //newPos.y = transform.position.y;
        //transform.position = newPos;

        transform.rotation = Quaternion.Euler(90f, goal.eulerAngles.y, 0f);
    }

    //void ClampIconColliderWise()
    //{
    //    sprRect.anchoredPosition = screenPos - rt.sizeDelta / 2f;
    //    Vector2 diff;
    //    diff = (rt.position - sprRect.position);
    //    RaycastHit2D[] hits = Physics2D.RaycastAll(sprRect.position, diff);
    //    if (hits.Length > 0)
    //    {
    //        for (int i = 0; i < hits.Length; i++)
    //        {
    //            if (hits[i].transform.name == mmc.shapeColliderGO.name)
    //            {
    //                //Debug.DrawLine (sprRect.position, rt.position);
    //                sprRect.position = hits[i].point;
    //                break;
    //            }
    //        }
    //    }

    //}

}
