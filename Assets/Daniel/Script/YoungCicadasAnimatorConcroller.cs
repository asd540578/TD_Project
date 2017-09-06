using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TDTK;

public class YoungCicadasAnimatorConcroller : MonoBehaviour {

	public UnitCreep m_UnitCreep;

	public Animator m_Animator;
	public Vector3 m_OldVector3;
	public string m_AnimationName;
	public float m_x = 0.05f;
	public float m_z = 0.2f;


	// Use this for initialization
	void Start () {
		m_OldVector3 = transform.localPosition;
		StartCoroutine (StartAnimator());
	}

	// Update is called once per frame
	void Update () {
		transform.rotation = Quaternion.Euler (new Vector3 (90,0, m_UnitCreep.transform.localPosition.y * -1));


		if (Input.GetKeyUp (KeyCode.G)){
			ChangeFacade ();
		}else if(Input.GetKeyDown(KeyCode.H)){
			ChangeBack ();
		}else if (Input.GetKeyUp (KeyCode.J)){
			ChangeDie ();
		}else if (Input.GetKeyUp (KeyCode.K)){
			ChangeWalk ();
		}else if (Input.GetKeyUp (KeyCode.L)){
			ChangeAttack ();
		}
	}

	public void ChangeFacade(){
		m_Animator.SetTrigger("facade");
	}

	public void ChangeBack(){
		m_Animator.SetTrigger("back");
	}

	public void ChangeDie(){
		m_Animator.SetTrigger("die");
	}

	public void ChangeWalk(){
		m_Animator.SetTrigger("walk");
	}

	public void ChangeAttack(){
		m_Animator.SetTrigger("attack");
	}


	IEnumerator StartAnimator(){
		//yield return new WaitForSeconds(0.2f);
		for(int a = 0; a < 5; a++)yield return new WaitForEndOfFrame();
		CheckAnimation ();

	}

	public void CheckAnimation(){

		if (m_UnitCreep.transform.localPosition == m_OldVector3) {
			if (m_AnimationName != "attack") {
				ChangeAttack ();
				m_AnimationName = "attack";
			}
		} else {
			if (m_UnitCreep.transform.position.z < m_OldVector3.z - m_z) {
				if (m_AnimationName != "Facade") {
					ChangeFacade ();
					m_AnimationName = "Facade";
					m_OldVector3 = m_UnitCreep.transform.position;
				}
			}
			if (m_UnitCreep.transform.position.z > m_OldVector3.z + m_z) {
				if (m_AnimationName != "Bacade") {
					ChangeBack ();
					m_AnimationName = "Bacade";
					m_OldVector3 = m_UnitCreep.transform.position;
				}

			}

			if (m_UnitCreep.transform.position.x > m_OldVector3.x + m_x) {
				if (m_AnimationName != "walk_L") {
					ChangeWalk ();
					m_AnimationName = "walk_L";
					if(transform.localScale.x > 0){
						transform.localScale = new Vector3 (transform.localScale.x * -1,transform.localScale.y,transform.localScale.z);
						m_OldVector3 = m_UnitCreep.transform.position;
					}
				}
			} 
			if (m_UnitCreep.transform.position.x < m_OldVector3.x - m_x) {
				if (m_AnimationName != "walk_R") {
					ChangeWalk ();
					m_AnimationName = "walk_R";
					if(transform.localScale.x < 0){
						transform.localScale = new Vector3 (transform.localScale.x * -1,transform.localScale.y,transform.localScale.z);
						m_OldVector3 = m_UnitCreep.transform.position;
					}
				}
			}
		} 

		StartCoroutine (StartAnimator());
	}
}

