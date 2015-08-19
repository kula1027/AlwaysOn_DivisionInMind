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
		//InitMyController();
		Exit();
	}

	private void InitMyController(){
		if(photonInit.GetReady()){
			if(Application.platform != RuntimePlatform.Android){
				if(id != "xxx"){
					if(targetController == gameObject){
						for(int i = 0 ; i < transform.childCount ; i ++){
							if(id == transform.GetChild(i).GetComponent<PhotonView>().owner.name){
								targetController = transform.GetChild(i).gameObject;
								findTarget = true;
								break;
							}
						}
					}
				}
				if(findTarget){
					if(targetController == null){
						findTarget = false;
					}
				}
			}
		}
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

	private void Exit(){
		if(Input.GetKey(KeyCode.Escape)){
			Application.Quit();
		}
	}
}
