using UnityEngine;
using System.Collections;

public class Scene1_Main : MonoBehaviour {
	
	void Start () {
	
	}

	void Update () {
	
	}

	public void OnButtonStart(){
		if(Application.platform == RuntimePlatform.Android){
			Application.LoadLevel("3_PlayerSelect");
		}else{
			Application.LoadLevel("2_MyRoom");
		}
	}

	public void OnButtonExit(){
		Application.Quit();
	}
}
