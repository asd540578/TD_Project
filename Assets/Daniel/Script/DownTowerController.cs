using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TDTK;

public class DownTowerController : MonoBehaviour {

	public List<UnitCreep> m_UnitCreep;
	public UnitTower m_UnitTower;
	public bool ison = false;
	public float m_MaxHP;
	public int index;

	// Use this for initialization
	void Start () {
		//m_MaxHP = m_UnitTower.statsList [0].hp;
		InvokeRepeating("CheckAttack",1,0.2f);
	}

	void OnTriggerEnter(Collider other) {

		if (m_UnitCreep.Count == 0) {
			m_UnitCreep.Add (other.GetComponent<UnitCreep> ());
		} else {
			for (int a = 0; a < m_UnitCreep.Count; a++) {
				if(other.GetComponent<UnitCreep>().transform == m_UnitCreep[a]){
					index = 1;
				}
			}
		}
			
		if (index == 0) {
			m_UnitCreep.Add (other.GetComponent<UnitCreep> ());
		}
			
	}

	void OnTriggerExit(Collider other) {

		m_UnitCreep.Remove (other.GetComponent<UnitCreep>());
	}

	public void CheckAttack(){


		for (int a = 0; a < m_UnitCreep.Count; a++) {

			if (m_UnitCreep [a].hp <= 0 || m_UnitCreep [a].path == null) {
				m_UnitCreep.Remove (m_UnitCreep [a]);
			} else {
				if (m_UnitCreep [a].attackTarget != null) {
					if (m_UnitCreep [a].attackTarget.transform == this.transform) {
						m_UnitCreep [a].stopToAttackCooldown = 1;
					}
				}
			}



		}

	}


}
