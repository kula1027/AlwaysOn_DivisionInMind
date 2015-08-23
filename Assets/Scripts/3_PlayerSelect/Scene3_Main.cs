using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using UnityEngine.UI;

public class Scene3_Main : MonoBehaviour {

	public GameObject[] onlyAndriods;

	public GameObject controllerBox;

	public Transform controllerButton;
	public GameObject buttonCollect;

	private int numberOfControllers = 0;

	void Start(){
		if(Application.platform != RuntimePlatform.Android){
			onlyAndriods[0].SetActive(false);
			onlyAndriods[3].SetActive(false);
		}else{
			onlyAndriods[0].SetActive(true);
			onlyAndriods[3].SetActive(true);
		}
		onlyAndriods[1].SetActive(false);
		onlyAndriods[2].SetActive(false);
	}

	void Update(){
		Exit();
		if(numberOfControllers != controllerBox.transform.childCount){
			if(numberOfControllers < controllerBox.transform.childCount){
				CreateButton(numberOfControllers, controllerBox.transform.GetChild(numberOfControllers).GetComponent<PhotonView>().owner.name);
				numberOfControllers++;
			}else{
				int count = buttonCollect.transform.childCount;
				bool destroy = false;
				for(int i = 0 ; i < count ; i ++){
					if(destroy){

					}else{
						int jcount = controllerBox.transform.childCount;
						bool find = false;
						for(int j = 0 ; j < jcount ; j ++){
							if((controllerBox.transform.GetChild(j).GetComponent<PhotonView>().owner.name
							   == buttonCollect.transform.GetChild(i).GetChild(0).GetComponent<Text>().text || 
							    controllerBox.transform.GetChild(j).GetComponent<PhotonView>().owner.name
							    == buttonCollect.transform.GetChild(i).GetChild(0).GetComponent<Text>().text + "(find)" )){
								find = true;
								break;
							}
						}
						if(!find){
							Destroy(buttonCollect.transform.GetChild(i).gameObject);
							destroy = true;
						}
					}
				}
				numberOfControllers--;
			}
		}
	}

	public void OnButtonID(InputField input){
		if(GameObject.Find("GameMain").GetComponent<PhotonInit>().GetReady()){
			GameObject.Find("GameMain").GetComponent<GameMain>().SetID(input.text);
			GameObject.Find("GameMain").GetComponent<PhotonInit>().OnJoinRandomRoom();
			onlyAndriods[1].SetActive(true);
			onlyAndriods[3].SetActive(true);
		}
	}

	public void OnButtonSetting(){
		int count = GameObject.Find("GameMain").GetComponent<GameMain>().transform.childCount;
		bool find = false;
		for(int i = 0 ; i < count ; i ++){
			if(GameObject.Find("GameMain").transform.GetChild(i).GetComponent<PhotonView>().isMine){
				if(GameObject.Find("GameMain").transform.GetChild(i).GetComponent<Controller>()){
					GameObject.Find("GameMain").transform.GetChild(i).GetComponent<Controller>().SettingVecter();
					find = true;
				}
			}
		}
		if(!find){
			Application.Quit();
		}
		onlyAndriods[2].SetActive(true);
	}

	public void OnButtonResetting(){
		onlyAndriods[2].SetActive(false);
	}

	public void OnButtonIDForDeskTop(Button button){
		if(GameObject.Find("GameMain").GetComponent<PhotonInit>().GetReady()){
			//GameObject.Find("GameMain").GetComponent<GameMain>().SetID();
			GameObject.Find("GameMain").GetComponent<PhotonInit>().OnJoinRandomRoom();
		}
	}

	public void OnButtonReady(){
		if(Application.platform != RuntimePlatform.Android){
			if(GameObject.Find("GameMain").GetComponent<GameMain>().findTarget){
				GameObject.Find("GameMain").GetComponent<GameMain>().ready = true;
				transform.GetComponent<PhotonView>().RPC("GoNextScene", PhotonTargets.All, null);
				//GoNextScene();
			}
		}
	}

	public void CreateButton(int index, string id){
		if(Application.platform != RuntimePlatform.Android){
			Transform newButton = (Transform)Instantiate(controllerButton, controllerButton.position, Quaternion.identity);
			newButton.parent = buttonCollect.transform;
			RectTransform rect = newButton.GetComponent<RectTransform>();
			rect.anchorMin = new Vector2(0.1f, 0.1f + 0.1f*index);
			rect.anchorMax = new Vector2(0.3f, 0.1f*index + 0.2f);
			newButton.GetChild(0).GetComponent<Text>().text = id;
			newButton.GetComponent<Button>().onClick.AddListener(delegate (){this.OnButtonClick(id);});
		}
	}

	public void OnButtonClick(string id){
		int count = controllerBox.transform.childCount;
		for(int i = 0 ; i < count ; i ++){
			if(controllerBox.transform.GetChild(i).GetComponent<PhotonView>().owner.name == id){
				if(!controllerBox.transform.GetChild(i).GetComponent<Controller>().findOwner){
					controllerBox.transform.GetChild(i).GetComponent<Controller>().findOwner = true;
					transform.GetComponent<PhotonView>().RPC("SetControllerFind", PhotonTargets.All, id);
					GameObject.Find("GameMain").GetComponent<GameMain>().SetController(controllerBox.transform.GetChild(i).gameObject);
				}else{
					Debug.Log("already find player!!");
				}
			}
		}
	}

	private void Exit(){
		if(Input.GetKey(KeyCode.Escape)){
			Application.Quit();
		}
	}
	
	public void CreateController(){
		PhotonNetwork.Instantiate("3_PlayerSelect/Controller", new Vector3(0, 0, 0), Quaternion.identity, 0);
	}

	[PunRPC]
	void GoNextScene(){
		PhotonNetwork.isMessageQueueRunning = false;
		Application.LoadLevel("4_Racing");
	}

	[PunRPC]
	void SetControllerFind(string id){
		int count = controllerBox.transform.childCount;
		for(int i = 0 ; i < count ; i ++){
			if(controllerBox.transform.GetChild(i).GetComponent<PhotonView>().owner.name == id){
				controllerBox.transform.GetChild(i).GetComponent<Controller>().findOwner = true;
			}
		}

		int jcount = buttonCollect.transform.childCount;
		for(int j = 0 ; j < jcount ; j ++){
			if(buttonCollect.transform.GetChild(j).GetChild(0).GetComponent<Text>().text == id){
				buttonCollect.transform.GetChild(j).GetChild(0).GetComponent<Text>().text += "(find)";
			}
		}
	}
}
