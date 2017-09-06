using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TDTK;
using System;

public class BuildButtonUIController : MonoBehaviour {

	public UIBuildButton m_UIBuildButton;
	public TowerManager m_TowerManager;
	public InputControl m_InputControl;
	public SelectInfo sele;
	public int m_Buttonindex;
	public int maxButtonindex;
	public GameObject butObj;
	public UITooltip m_UITooltip;
	public UnitTower  m_UnitTower,m_UnitTowerSolider;

	// Use this for initialization
	void Start () {
		m_UIBuildButton = GameObject.Find ("UIBuildButton").GetComponent<UIBuildButton>();
		Invoke ("GetButtonindex", 0.5f);
	}

	public void GetButtonindex(){
		maxButtonindex = m_UIBuildButton.buildButtons.Count - 1;
		//m_UIBuildButton.gameObject.SetActive (false);
	}

	public void OnHoverBuildButton(int  index){
		m_UIBuildButton.OnHoverBuildButton (m_UIBuildButton.buildButtons[index].rootObj.gameObject);
		butObj = m_UIBuildButton.buildButtons [index].rootObj.gameObject;
	}

	/*
	void Update (){

		if (Input.GetKeyUp (KeyCode.Q)){
			if (m_Buttonindex - 1 < 0) {
				m_Buttonindex = maxButtonindex;
			} else {
				m_Buttonindex -= 1;
			}
			OnHoverBuildButton (m_Buttonindex);
			m_UIBuildButton.buildButtons [m_Buttonindex].button.enabled = false;
		}else if(Input.GetKeyDown(KeyCode.Q)){
			m_UIBuildButton.OnExitBuildButton (m_UIBuildButton.buildButtons [m_Buttonindex].rootObj.gameObject);
			m_UIBuildButton.buildButtons [m_Buttonindex].button.enabled = true;
		}else if (Input.GetKeyUp (KeyCode.E)){
			m_UIBuildButton.OnBuildButton (m_UIBuildButton.buildButtons[m_Buttonindex].rootObj.gameObject,m_Buttonindex);
		}else if (Input.GetKeyUp (KeyCode.R)){
			m_UIBuildButton.buildButtons [m_Buttonindex].button.enabled = true;
			m_UIBuildButton.OnExitBuildButton (m_UIBuildButton.buildButtons [m_Buttonindex].rootObj.gameObject);
			m_Buttonindex = maxButtonindex;
			m_UIBuildButton.gameObject.SetActive (false);
		}

	}
	*/
	public void OnBuildButton(int index){

		if (index == 1) {
			m_Buttonindex = 2;
			m_UIBuildButton.OnExitBuildButton (m_UIBuildButton.buildButtons[2].rootObj.gameObject);
			m_UIBuildButton.buildButtons [m_Buttonindex].button.enabled = true;
			TowerManager.CreateDragNDropTower (m_TowerManager.buildableList[2]);
		} else {
			m_Buttonindex = 0;
			m_UIBuildButton.OnExitBuildButton (m_UIBuildButton.buildButtons [0].rootObj.gameObject);
			m_UIBuildButton.buildButtons [m_Buttonindex].button.enabled = true;
			TowerManager.CreateDragNDropTower (m_TowerManager.buildableList[0]);
		}



		//m_UIBuildButton.OnBuildButton (m_UIBuildButton.buildButtons[m_Buttonindex].rootObj.gameObject,m_Buttonindex);

		//UITooltip.Show (m_TowerManager.buildableList [m_Buttonindex], new Vector3 (0, 0, 0), m_Buttonindex, new Vector3 (0, 0, 0));
		//UITooltip.ShowUpgrade(m_TowerManager.buildableList [m_Buttonindex],m_Buttonindex,new Vector3 (0, 0, 0),m_Buttonindex, new Vector3 (0, 0, 0));
		//UITooltip.ShowSell (m_TowerManager.buildableList [m_Buttonindex], new Vector3 (0, 0, 0), m_Buttonindex, new Vector3 (0, 0, 0));
	}

	public void OnBuildButton2(int index){
		if (index != 1) {
			m_UIBuildButton.OnExitBuildButton (m_UIBuildButton.buildButtons [1].rootObj.gameObject);
			m_UIBuildButton.buildButtons [m_Buttonindex].button.enabled = true;
			TowerManager.CreateDragNDropTower (m_TowerManager.buildableList [1]);
		} else {
			m_UIBuildButton.OnExitBuildButton (m_UIBuildButton.buildButtons [3].rootObj.gameObject);
			m_UIBuildButton.buildButtons [m_Buttonindex].button.enabled = true;
			TowerManager.CreateDragNDropTower (m_TowerManager.buildableList [3]);
		}
	}
		

	public void GetUpgradeTowerInformation(){
		m_UnitTower = m_TowerManager.dndTower;
		Debug.Log ("塔的icon"+ m_UnitTower.icon.ToString());
		Debug.Log ("塔的名稱"+ m_UnitTower.unitName);
		Debug.Log ("階級icon{再看看怎抓這個值}");
		Debug.Log ("塔的升級選項（再看要怎抓值）"+m_UnitTower.upgradeTowerList[0].name);
		Debug.Log ("各塔的升級花費"+ m_UnitTower.statsList[0].cost[0].ToString());
		Debug.Log ("塔的血量"+ m_UnitTower.hp.ToString()+"/塔的最大值ＨＰ");
		Debug.Log("防禦力");
		Debug.Log ("塔的攻擊力"+ m_UnitTower.statsList[0].damageMin.ToString());
		Debug.Log ("塔的公速（多久打一下）"+ m_UnitTower.statsList[0].cooldown.ToString());
		Debug.Log ("塔的範圍"+ m_UnitTower.statsList[0].attackRange.ToString());
		Debug.Log ("格擋職");
	}

	public void GatCreatTowerInfotmation(){
		m_UnitTower = m_TowerManager.dndTower;
		Debug.Log ("塔的名稱"+ m_UnitTower.unitName);
		Debug.Log ("塔的icon"+ m_UnitTower.icon.ToString());
		Debug.Log ("塔的花費"+ m_UnitTower.statsList[0].cost[0].ToString());
		Debug.Log ("塔的血量"+ m_UnitTower.hp.ToString());
		Debug.Log ("塔的公速（多久打一下）"+ m_UnitTower.statsList[0].cooldown.ToString());
	}


	public void OnExitBuildButton(int index){
		
		if (index == 0) {
			m_UIBuildButton.buildButtons [index].button.enabled = true;
			m_UIBuildButton.OnExitBuildButton (m_UIBuildButton.buildButtons [index].rootObj.gameObject);
			m_Buttonindex = maxButtonindex;
			//m_UIBuildButton.gameObject.SetActive (false);
			Debug.Log (m_UIBuildButton.buildButtons [index].rootObj.gameObject);
		} else {
			m_UIBuildButton.buildButtons [index].button.enabled = true;
			m_UIBuildButton.OnExitBuildButton (m_UIBuildButton.buildButtons [index+1].rootObj.gameObject);
			m_Buttonindex = maxButtonindex;
			//m_UIBuildButton.gameObject.SetActive (false);
			Debug.Log (m_UIBuildButton.buildButtons [index+1].rootObj.gameObject);
		}
	}

	public void CheckTowerManagerMouseClick(){
		m_TowerManager.CheckMouseClick = true;
	}

	public void AddTower(){
		TowerManager.AddTower (m_TowerManager.dndTower,TowerManager.CreatePlatformForTower(m_TowerManager.dndTower,TowerManager.GetGridSize()), 0);
	}
		
}
