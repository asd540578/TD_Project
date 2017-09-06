using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMap : MonoBehaviour {

	public Transform m_platforms;
	public Transform m_freatPlatformsTag;
	public int m_mapX;
	public int m_mapY;
	public float m_mapPostionXStar;
	public float m_mapPostionX;
	public float m_mapPostionY;
	public float m_mapPostionAddX;
	public float m_mapPostionAddY;
	public string[] m_mapString;
	private string[] m_textMapString;
	public string m_text = "0";
	public Transform m_instantiata;

	// Use this for initialization
	void Start () {
		//Test ();
		CreatePlatforms ();
	}

	//設定地圖，１＝可以搭建的位置，０＝不可搭建的地方
	public void Test(){
		m_mapString [0] = "0,0,0,1,1,1,1,0,0,0,0,0,0";
		m_mapString [1] = "0,1,0,1,1,1,0,1,1,0,0,0,0";
		m_mapString [2] = "0,1,0,1,1,1,1,1,1,1,1,1,0";
		m_mapString [3] = "0,1,1,1,1,1,1,1,1,1,1,1,1";
		m_mapString [4] = "0,1,1,1,1,1,1,1,0,1,1,1,0";
		m_mapString [5] = "0,1,1,0,1,1,1,1,1,1,1,1,1";
		m_mapString [6] = "0,1,1,0,0,1,1,1,0,1,1,0,1";

	}

	//執行建造地圖
	public void CreatePlatforms(){
		for(int a = 0; a < m_mapY; a++){
			m_text = m_mapString [a];
			m_textMapString = m_text.Split ("," [0]);
			for (int b = 0; b < m_mapX; b++) {
				if (int.Parse (m_textMapString [b]) == 1) {
					m_instantiata = Instantiate (m_platforms, new Vector3 (0, 0, 0), Quaternion.identity);
					m_instantiata.parent = m_freatPlatformsTag.transform;
					m_instantiata.localPosition = new Vector3 (m_mapPostionX, 0, m_mapPostionY);
					m_instantiata.localEulerAngles = new Vector3 (90, 0, 0);
				}
				m_mapPostionX += m_mapPostionAddX;
			}
			m_mapPostionX = m_mapPostionXStar;
			m_mapPostionY -= m_mapPostionAddY;
		}

	}
}