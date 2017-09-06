using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteController : MonoBehaviour {

	public float m_waitTime;
	public float m_ChangeSpeed;
	public float m_alphaIndex;
	public SpriteRenderer m_Sprite;
	public Material m_Material;
	public bool m_Change;

	// Use this for initialization
	void Start () {
		Debug.Log (m_Material.color.a);
		InvokeRepeating ("ChangeSpriteAlpha", 0.1f, 1f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void ChangeSpriteAlpha(){

		if (m_Change) {
			if (m_alphaIndex - m_ChangeSpeed > 0) {
				m_Sprite.material.color = new Color (1f, 1f, 1f, m_alphaIndex - m_ChangeSpeed);
			} else {
				m_Change = false;
			}
		} else {
			if (m_alphaIndex + m_ChangeSpeed < 1) {
				m_Sprite.material.color = new Color (1f, 1f, 1f, m_alphaIndex + m_ChangeSpeed);
			} else {
				m_Change = true;
			}
		}
	}
}
