using UnityEngine;
using System.Collections;

public class Scene1_Main : MonoBehaviour {
	
	void Start () {
	
	}

	void Update () {
	
	}

	public void OnButtonStart(){
		Application.LoadLevel("2_MyRoom");
	}

	public void OnButtonExit(){
		Application.Quit();
	}
}
