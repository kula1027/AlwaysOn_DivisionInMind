using UnityEngine;
using System.Collections;

public class GameMain : MonoBehaviour {


	private string id = "xxx";
	private GameObject targetController;
	private PhotonInit photonInit;

	void Awake(){
		DontDestroyOnLoad(this);
	}

	void Start () {
		photonInit = GameObject.Find("PhotonInit").GetComponent<PhotonInit>();
		targetController = gameObject;
	}
	
	void Update () {
		InitMyController();
	}

	private void InitMyController(){
		if(photonInit.GetReady()){
			if(Application.platform != RuntimePlatform.Android){
				if(id != "xxx"){
					if(targetController == gameObject){
						for(int i = 0 ; i < transform.childCount ; i ++){
							if(id == transform.GetChild(i).GetComponent<Controller>().GetID()){
								targetController = transform.GetChild(i).gameObject;
								break;
							}
						}
					}
				}
			}
		}
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
