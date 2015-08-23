using UnityEngine;
using System.Collections;

public class GameMain : MonoBehaviour {


	private string id = "xxx";
	public GameObject targetController;
	private PhotonInit photonInit;
	public bool findTarget = false;
	public bool ready = false;
	
	void Awake(){
		DontDestroyOnLoad(this);
	}

	void Start () {
		photonInit = transform.GetComponent<PhotonInit>();
		targetController = gameObject;
	}
	
	void Update () {

	}

	public void SetController(GameObject controller){
		findTarget = true;
		targetController = controller;
	}



	public GameObject GetMyController(){
		return targetController;
	}

	public string GetID(){
		return id;
	}

	public void SetID(string id){
		this.id = id;
	}


}
