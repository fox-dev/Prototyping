using UnityEngine;
using System.Collections;

public class FireBullet : MonoBehaviour {

    public float timeBetweenBullets = 0.15f;
    public GameObject projectile;

    Vector3 mousePos;
    Vector2 mousePos2D;

    public float direction;

    float nextBullet; //when next can be fired

    GameObject temp;


    // Use this for initialization
    void Awake () {
        nextBullet = 0f;
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetAxisRaw("Fire1") > 0 && nextBullet < Time.time)
        {

            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos2D = new Vector2(mousePos.x, mousePos.y);
            
            Vector2 target = mousePos2D - new Vector2(transform.parent.transform.position.x, transform.parent.transform.position.y);
            float angle = Mathf.Atan2(target.y, target.x);
         
            nextBullet = Time.time + timeBetweenBullets;
            
            temp = Instantiate(projectile, transform.position, Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;

            temp.transform.parent = gameObject.transform.parent;

            

        
        }

        print(mousePos);

        //temp.GetComponent<LineRenderer>().SetPosition(0, temp.transform.parent.position);

    }
}
