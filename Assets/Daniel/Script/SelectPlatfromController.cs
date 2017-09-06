using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectPlatfromController : MonoBehaviour {

	public Transform m_uiTransform;
	public int m_index;


	public void ShowUI(){
		m_uiTransform.gameObject.SetActive (true);
	}

	public void ClossUI(){
		m_uiTransform.gameObject.SetActive (false);
	}
}
