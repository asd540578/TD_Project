using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using TDTK;


namespace TDTK {
	public class InputControl : MonoBehaviour {
		public BuildPlatform m_BuildPlatform;
		public BuildButtonUIController m_BuildButtonUIController;
		public SelectPlatfromController m_SelectPlatfromController;
		public RscManager m_RscManager;
		public TowerManager m_TowerManager;
		private Transform m_selectTransform;
		public bool InputControlIsOn,SelectTowerUI,CheckIndexIsOn ;
		private List<int> m_CreatIndexArray,expectedList;
		public int m_index;
 		private RaycastHit hit;
		public Transform m_AllTower ;
		public List<UnitTower> m_activeTowerList;
		public TowerSoliderController m_TSC;
		public float m_addMoneyTime = 1.0f;
		public int m_RewardMoney;
		public int m_MaxRewardTime;
		public int m_RewardMax;
		public string m_RewardTimeShow;
		private int m_RewardTime;
		private int m_RewardIndex;

		// Use this for initialization
		void Start () {
			m_RewardIndex = 0;
			m_RewardTime = m_MaxRewardTime;
			InputControlIsOn = true;
			SelectTowerUI = false;
			CheckIndexIsOn = false;


			InvokeRepeating ("AddCash", m_addMoneyTime, m_addMoneyTime);

			InvokeRepeating ("CheckTime", 1, 1);
		}

		public void CheckTime(){
			m_RewardTime -= 1;
			TimeSpan timeSpan = TimeSpan.FromSeconds(m_RewardTime);
			m_RewardTimeShow = string.Format ("{0:D2}:{1:D2}",timeSpan.Minutes, timeSpan.Seconds);
			Debug.Log (m_RewardTimeShow);
			if(m_RewardTime == 0){
				AddRewardCash();
				m_RewardTime = m_MaxRewardTime;
			}
		}

		public void AddCash(){
			//自動增加資源的程式，取當前的值然後＋上一個值在更新
			int money;
			money = m_RscManager.rscList [0];
			m_RscManager.rscList = new List<int>{money + 1,1};
			//更新目前前導核心（包含ＵＩ）
			TDTK.OnRscChanged (m_RscManager.rscList);
		}

		public void AddRewardCash(){
			if (m_RewardIndex + 1 > m_RewardMax) {
				return;
			} else {
				m_RewardIndex += 1;
				//自動增加資源的程式，取當前的值然後＋上一個值在更新
				int money;
				money = m_RscManager.rscList [0];
				m_RscManager.rscList = new List<int>{ money + m_RewardMoney, 1};
				//更新目前前導核心（包含ＵＩ）
				TDTK.OnRscChanged (m_RscManager.rscList);
			}
		}

		public void CheckIndex(){
			//初始化List大小
			m_CreatIndexArray = new List<int>(m_BuildPlatform.unavailablePrefabIDList.Count);
			//先建立一個新的List
			for (int a = 0; a < m_BuildPlatform.unavailablePrefabIDList.Count + 1; a++) {
				m_CreatIndexArray.Add (a);
			}
			//比較兩個list的差異（Ａ有Ｂ沒有）
			expectedList = m_CreatIndexArray.Except (m_BuildPlatform.unavailablePrefabIDList).ToList();
			//取出沒有得值
			m_index = expectedList [0];
			//轉換成程式用的由０開始
			m_BuildButtonUIController.m_Buttonindex = m_index - expectedList [0];
			m_CreatIndexArray.Clear ();
		}

		public void CheckTowerList(){
			m_activeTowerList = m_TowerManager.activeTowerList.Except (m_activeTowerList).ToList ();
			//m_activeTowerList3 = m_activeTowerList2.Except (m_activeTowerList).ToList ();
		}
			
			
		// Update is called once per frame
		void Update () {

			if (InputControlIsOn) {
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				if (Physics.Raycast (ray, out hit)) {
					if (hit.collider != null) {

						if (m_SelectPlatfromController == null) {
							m_SelectPlatfromController = hit.collider.gameObject.GetComponent<SelectPlatfromController> ();
							m_selectTransform = hit.collider.transform;
							if (m_SelectPlatfromController != null && !SelectTowerUI)
								m_SelectPlatfromController.ShowUI ();
						} else {
							if (m_selectTransform != hit.collider.transform) {
								m_SelectPlatfromController.ClossUI();
								m_SelectPlatfromController = hit.collider.gameObject.GetComponent<SelectPlatfromController> ();
								m_selectTransform = hit.collider.transform;
								if(m_SelectPlatfromController != null && !SelectTowerUI)
									m_SelectPlatfromController.ShowUI ();
							}
						}


						if (Input.GetMouseButtonUp(0)) {
							if (hit.collider.gameObject != null) {
								if (hit.collider.gameObject.GetComponent<BuildPlatform> ()) {
									if (!CheckIndexIsOn) {
										
										OnBuildTower (m_SelectPlatfromController.m_index);
									}
								} else if(!hit.collider.gameObject.GetComponent<BuildPlatform> ()){
									if (CheckIndexIsOn) {
										CreateTower(m_BuildButtonUIController.m_Buttonindex);
										CheckIndexIsOn = false;
									}
								} 
							} else {
							
							}

						} else if (Input.GetMouseButtonDown (1)) {
							//m_BuildButtonUIController.OnBuildButton2 ();
							SelectTowerUI = false;
							m_BuildButtonUIController.CheckTowerManagerMouseClick ();
							//m_BuildButtonUIController.OnExitBuildButton (m_SelectPlatfromController.m_index);
							CheckIndexIsOn = false;
						}
					}
				}
			}
		}

		public void OnBuildTower(int index){
			
			m_BuildPlatform = hit.collider.gameObject.GetComponent<BuildPlatform> ();
			//CheckIndex ();
			m_BuildButtonUIController.OnBuildButton (index);
			SelectTowerUI = true;
			m_BuildButtonUIController.OnExitBuildButton (index);
			CheckIndexIsOn = true;
		}

		public void OnBuildSolider(int index){
			m_BuildPlatform = hit.collider.gameObject.GetComponent<BuildPlatform> ();
			m_BuildButtonUIController.m_Buttonindex = index;
			m_BuildButtonUIController.OnBuildButton2 (m_BuildButtonUIController.m_Buttonindex);
			SelectTowerUI = true;
			m_BuildButtonUIController.OnExitBuildButton (m_BuildButtonUIController.m_Buttonindex);
			CheckIndexIsOn = true;
		}
		public void CreateTower(int index){
		    Test ();
			CheckIndexIsOn = false;
		}

		public void CreateSolider(){
			m_TSC = m_AllTower.gameObject.GetComponent<TowerSoliderController> ();
		}

		public void CreateButton(){
			m_BuildButtonUIController.AddTower ();
			m_BuildButtonUIController.OnExitBuildButton (m_BuildButtonUIController.m_Buttonindex);
			SelectTowerUI = false;
		}
		private static SelectInfo sInfo;
		public void Test(){
			if(m_TowerManager.dndTower != null){
				
				if(RscManager.HasSufficientRsc(m_TowerManager.dndTower.GetCost())){
					
					RscManager.SpendRsc(m_TowerManager.dndTower.GetCost());
					Debug.Log ("buy");
					m_TSC = m_TowerManager.dndTower.gameObject.GetComponent<TowerSoliderController>();
					SelectControl.ClearUnit();
					TowerManager.AddTower (m_TowerManager.dndTower, TowerManager.CreatePlatformForTower (m_TowerManager.dndTower, TowerManager.GetGridSize ()), 0);
					m_TowerManager.dndTower.Build();	m_TowerManager.dndTower=null;	//TowerManager.dndCooldown=Time.time;
					if(m_TSC != null)
						m_TSC.ShowSolider ();
				}
				else{
					GameControl.InvalidAction("Insufficient Resources");
					m_TowerManager._ExitDragNDropPhase();
				}
			}

		}
	}
}
