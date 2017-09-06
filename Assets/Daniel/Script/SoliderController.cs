using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TDTK;

public class SoliderController : MonoBehaviour {
	public TowerSoliderController m_TowerSoliderController;
	public UnitTower m_UnitTower;
	public float m_cooldown;
	public int m_BlackMax,m_Checkindex;
	public List<Transform> m_BlackCreep;
	public UnitCreep m_UnitCreep;
	public BoxCollider m_BoxCollider;
	public Animator m_Animator;

	void Start () {
		//InvokeRepeating ("CheckBlackCrrrpList", 0.1f, 0.1f);
		m_BoxCollider.center = new Vector3 ( transform.localPosition.x * -2, 0, transform.localPosition.z * -2);
		StartCoroutine( StartAnimator ());
		InvokeRepeating ("CheckSolide", 1, 0.1f);
	}

	public void CheckBlackCrrrpList(){
		
		int index = 0;
		if (m_BlackCreep.Count > 0) {
			for (int a = 0; a < m_BlackCreep.Count; a++) {
				m_UnitCreep = m_BlackCreep [a].gameObject.GetComponent<UnitCreep> ();
				if (m_UnitCreep != null && m_UnitCreep.attackTarget != null) {
					if (m_BlackCreep [a].transform == m_UnitCreep.attackTarget.transform) {
						index = 1;
					}
					if (index != 1) {
						m_BlackCreep.Remove (m_UnitCreep.transform);
					}

					index = 0;
				}
				if (m_UnitCreep.hp <= 0 || m_UnitCreep.path == null) {
					m_BlackCreep.Remove (m_UnitCreep.transform);
				}
			}
		}
	}

	public void CheckCreepList(Collider other){
		for (int a = 0; a < m_TowerSoliderController.m_CheckSoliderList.Count; a++) {
			if (other.transform == m_TowerSoliderController.m_CheckSoliderList [a].transform) {
				m_Checkindex = 1;
			}
		}

		if(m_Checkindex == 0){
			if (m_BlackCreep.Count < m_BlackMax) {
				m_UnitCreep = other.gameObject.GetComponent<UnitCreep> ();
				m_BlackCreep.Add (other.transform);
				m_TowerSoliderController.m_CheckSoliderList.Add (other.transform);
				m_UnitCreep.stopToAttack = true;
				m_UnitCreep.attackLimitPerStop = 1;
				m_UnitCreep.stopToAttackCooldown = 0.1f;
			}
		}
		m_Checkindex = 0;

	}

	public void CheckSolide()
	{


		if (m_UnitTower.attackTarget != null) {
			m_Animator.SetTrigger ("attack");
		} else {
			m_Animator.SetTrigger ("idle");
		}

	}

	IEnumerator StartAnimator(){
		yield return new WaitForSeconds(Random.Range(0.1f,1.0f));
		m_Animator.enabled = true;
	}

	void OnTriggerEnter(Collider other) {
		CheckCreepList (other);
	}
}
