using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TDTK;

public class TowerSoliderController : MonoBehaviour {
	public InputControl m_InputControl;
	public Vector3[] m_SetPosition;
	public Transform m_TowerParent;
	public Transform m_AllSoliderSample;
	public int index,m_BlackMax,m_CheckCreepIndex;
	public List<UnitTower> m_activeTowerList,m_ChangePositionList;
	public List<Transform> m_CheckSoliderList,m_CheckSoliderListForRecurrection;
	public List<bool> m_ReIsOn = new List<bool>{true,true,true};
	public float m_DelayTime = 2.0f;
	public int m_SoliderID;

	// Use this for initialization
	void Start () {
		m_ReIsOn.Add (true);
		m_ReIsOn.Add (true);
		m_ReIsOn.Add (true);
		m_InputControl = GameObject.Find ("MapEdit").GetComponent<InputControl> ();

	}


	public void ShowSolider(){
		for (int a = 0; a < 3; a++) {
			CallBullidButtonController (m_SoliderID);
		}
		m_InputControl.CheckTowerList ();
		m_activeTowerList.Add (this.gameObject.GetComponent<UnitTower>());
		int index = m_InputControl.m_activeTowerList.Count;
		for(int b = 0; b < 3; b++){
			
			m_ChangePositionList.Add (m_InputControl.m_activeTowerList[index - 1 - b]);
			Debug.Log (m_ChangePositionList [b].transform.position);
			m_ChangePositionList [b].transform.parent = m_TowerParent;
			m_ChangePositionList [b].transform.localPosition = m_SetPosition [b];
			m_ChangePositionList [b].gameObject.name = "Solider" + b.ToString ();
			BoxCollider m_BoxCollider = m_ChangePositionList [b].gameObject.GetComponent<BoxCollider> ();
			m_BoxCollider.center = new Vector3 (m_ChangePositionList [b].transform.localPosition.x * -2, 0, m_ChangePositionList [b].transform.localPosition.z * -2);
			SoliderController m_SoliderController = m_ChangePositionList [b].gameObject.GetComponent<SoliderController> ();
			m_SoliderController.m_TowerSoliderController = this;
			m_BlackMax += m_SoliderController.m_BlackMax;
			/*這裡是到時空降使用
			TweenPosition m_TweenPosition = m_ChangePositionList [b].gameObject.GetComponent<TweenPosition>();
			m_TweenPosition.enabled = true;
			m_TweenPosition.delay = m_DelayTime;
			m_TweenPosition.from = new Vector3 (m_SetPosition [b].x, 0, 5);
			m_TweenPosition.to = m_SetPosition [b];
			m_TweenPosition.PlayForward ();
			m_DelayTime += 0.3f;
			//*/
		}
		m_AllSoliderSample.gameObject.SetActive (false);
		InvokeRepeating ("CheckAllSolider", 1, 0.2f);
	}


	public void ResurrectionSolider(int ID){
		
		CallBullidButtonController (m_SoliderID);

		m_InputControl.CheckTowerList ();
		m_ChangePositionList[ID] = m_InputControl.m_activeTowerList[m_InputControl.m_activeTowerList.Count - 1];
		m_ChangePositionList [ID].transform.parent = m_TowerParent;
		m_ChangePositionList [ID].transform.localPosition = m_SetPosition [ID];
		m_ChangePositionList [ID].gameObject.name = "Solider" + ID.ToString ();
		SoliderController m_SoliderController = m_ChangePositionList [ID].gameObject.GetComponent<SoliderController> ();
		m_SoliderController.m_TowerSoliderController = this.transform.GetComponent<TowerSoliderController>();
		/*這裡是到時空降使用
		TweenPosition m_TweenPosition = m_ChangePositionList [b].gameObject.GetComponent<TweenPosition>();
		m_TweenPosition.enabled = true;
		m_TweenPosition.delay = m_DelayTime;
		m_TweenPosition.from = new Vector3 (m_SetPosition [b].x, 0, 5);
		m_TweenPosition.to = m_SetPosition [b];
		m_TweenPosition.PlayForward ();
		m_DelayTime += 0.3f;
		//*/
		m_ReIsOn [ID] = true;
	}

	public void CheckAllSolider(){
		m_CheckSoliderListForRecurrection.Clear ();
		for (int a = 0; a < m_ChangePositionList.Count; a++) {
			if (m_ChangePositionList [a] == null) {
				if (m_ReIsOn [a]) {
					StartCoroutine (ResurrectionSolideIsOn (a));
					m_ReIsOn [a] = false;
				}
			} else {
				for(int b = 0; b < m_ChangePositionList [a].GetComponent<SoliderController>().m_BlackCreep.Count;b++){
					m_CheckSoliderListForRecurrection.Add (m_ChangePositionList [a].GetComponent<SoliderController>().m_BlackCreep[b]);
				}
			}
		}

		m_CheckSoliderList = m_CheckSoliderListForRecurrection;

	}

	IEnumerator ResurrectionSolideIsOn(int ID)
	{
		
		yield return new WaitForSeconds(1);
		ResurrectionSolider (ID);

	}


	public void CallBullidButtonController(int index){
		m_InputControl.OnBuildSolider (index);
		m_InputControl.CreateTower (index);
	}

	//*
	void OnTriggerStay(Collider other) {
		if (other.GetComponent<UnitCreep> () != null) {
			if (other.GetComponent<UnitCreep> ().attackTarget != null) {
				if (other.GetComponent<UnitCreep> ().attackTarget.transform == this.transform) {
					for (int a = 0; a < m_ChangePositionList.Count; a++) {
						if (m_ChangePositionList [a] != null) {
							other.GetComponent<UnitCreep> ().attackTarget = m_ChangePositionList [a];
							return;
						} else {
							m_CheckCreepIndex += 1;
						}
					}

					if (m_CheckCreepIndex == 3) {
						other.GetComponent<UnitCreep> ().stopToAttackCooldown = 1;
						//other.GetComponent<UnitCreep> ().stopToAttack = false;
					} else {
						m_CheckCreepIndex = 0;
					}
				}
			}
		}

	}
	//*/

}
