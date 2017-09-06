using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TDTK;

public class GetUnitCreepInformation : MonoBehaviour {

	public UnitCreep m_UnitCreep;

	// Use this for initialization
	void Start () {
		Debug.Log (m_UnitCreep.statsList [0].cooldown);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
