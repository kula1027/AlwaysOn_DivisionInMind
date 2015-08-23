using UnityEngine;
using System.Collections;

public class _ShipRenderer : MonoBehaviour {

	// Use this for initialization
	public bool engine = false;
	public Transform fire;

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetEngine(bool on){
		if(on){
			if(!engine){
				Transform newFire = (Transform)Instantiate(fire, transform.position, transform.rotation);
				newFire.parent = transform;
				engine = true;
			}
		}else{
			if(engine){
				Destroy(transform.GetChild(0).gameObject);
				engine = false;
			}
		}
	}
}
