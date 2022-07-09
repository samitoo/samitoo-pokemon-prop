using UnityEngine;
using System.Collections;

public class EdgeColliderRoomBounds : MonoBehaviour {

    Vector2[] colliderpoints;
    EdgeCollider2D bounds;
    //custom
    public bool customPoints = false, copyPoints = false;
    public Vector2[] customColliderPoints;// = new Vector2[5];    

	// Use this for initialization
	void Start () {
        bounds = gameObject.AddComponent<EdgeCollider2D>();
        colliderpoints = new Vector2[5];
        setRoomBounds();
        
	}
	
	// Update is called once per frame
	void Update () {
        //Copy component to save the items and update room layout
        if (copyPoints)
        {
            customColliderPoints = bounds.points;
        }
	}


    void setRoomBounds()
    {
        Vector2 roomSize = gameObject.GetComponent<SpriteRenderer>().sprite.bounds.size;
        bounds.offset = new Vector2((roomSize.x / -2f), (roomSize.y / -2f));


        //if custom
        if (customPoints)
        {
            bounds.points = customColliderPoints;
        }
        else
        {
            colliderpoints[0] = new Vector2(0, 0);
            colliderpoints[1] = new Vector2(roomSize.x, 0);
            colliderpoints[2] = new Vector2(roomSize.x, roomSize.y);
            colliderpoints[3] = new Vector2(0, roomSize.y);
            colliderpoints[4] = new Vector2(0, 0);
            bounds.points = colliderpoints;
        }
        
        

    }
}
