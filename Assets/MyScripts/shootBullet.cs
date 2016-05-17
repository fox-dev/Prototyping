using UnityEngine;
using System.Collections;

public class shootBullet : MonoBehaviour {

    public float range = 10f;
    public float damage = 5;
    public float maxWidth = 17f;
    public float startWidth = 0.025f;
    public float growingWidth = 0.025f;

    public float startWidth2 = 17f;
    public float growingWidth2 = 17f;

    Ray shootRay;
    RaycastHit shootHit;
    int shootableMask;
    LineRenderer gunLine;

	// Use this for initialization
	void Awake () {
        shootableMask = LayerMask.GetMask("Shootable");
        gunLine = GetComponent<LineRenderer>();

        Vector2 currentPos = new Vector2(transform.position.x, transform.position.y);
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePos - currentPos;

        direction.Normalize();

        shootRay.origin = transform.position;
        shootRay.direction = direction;
        gunLine.SetPosition(0, transform.position);

        if(Physics.Raycast(shootRay, out shootHit, range, shootableMask))
        {
            //hit an enemy goes here
            gunLine.SetPosition(1, shootHit.point);
            //gunLine.SetPosition(1, Input.mousePosition);
        }
        else
        {
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range); 
            //Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
          
           
            //mousePos = new Vector3(mousePos.x, mousePos.y, 0);
            //gunLine.SetPosition(1, mousePos);
        }
	
	}
	
	// Update is called once per frame
	void Update () {

        if(growingWidth <= maxWidth - 1f)
        {
            growingWidth = Mathf.Lerp(growingWidth, maxWidth, Time.deltaTime * 5f);
            gunLine.SetWidth(startWidth, growingWidth);
        }
        else if(growingWidth >= maxWidth - 1f)
        {
            growingWidth2 = Mathf.Lerp(growingWidth2, 0, Time.deltaTime * 2f);
            gunLine.SetWidth(startWidth, growingWidth2);
        }
        

        

       


	}
}
