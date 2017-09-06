using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TDTK;

namespace TDTK{

	public class TDGameControl : MonoBehaviour {

		public GameControl m_GameControl;
		public static string m_TD_life;
		public static string m_TD_Resource;
		public static string m_TD_AbilityRsc;
		public static string m_TD_PerkRsc;
		public static string m_TD_Wave;

		// Use this for initialization
		void Start () {
			
		}


		void OnEnable(){ 
			TDTK.onLifeChangedE += UpdateLifeDisplay;
			TDTK.onRscChangedE += UpdateResourceDisplay;
			TDTK.onAbilityRscChangedE += UpdateAbilityRscDisplay;
			TDTK.onPerkRscChangedE += UpdatePerkRscDisplay;
			TDTK.onNewWaveE += UpdateWaveDisplay;
		}
		void OnDisable(){ 
			TDTK.onLifeChangedE -= UpdateLifeDisplay;
			TDTK.onRscChangedE -= UpdateResourceDisplay;
			TDTK.onAbilityRscChangedE -= UpdateAbilityRscDisplay;
			TDTK.onPerkRscChangedE -= UpdatePerkRscDisplay;
			TDTK.onNewWaveE -= UpdateWaveDisplay;
		}

		//更新生命值
		//m_GameControl.life = 取得生命值（取生命值的另一個方法）
		void UpdateLifeDisplay(int life){
			Debug.Log (life);
			//if(!GameControl.CapLife()) lifeItem.lbMain.text=life.ToString("f0");
			//else lifeItem.lbMain.text=life.ToString("f0")+"/"+GameControl.GetLifeCap().ToString("f0");
		}

		//取值參考RcManager的Resource List
		void UpdateResourceDisplay(List<int> list){
			Debug.Log (list[0]); //目前取金錢的方法
			//for(int i=0; i<rscItemList.Count; i++) rscItemList[i].lbMain.text=list[i].ToString();
		}

		//技能能量
		void UpdateAbilityRscDisplay(int value){
			Debug.Log (value);
			//lbAbilityRsc.text=AbilityManager.GetRsc()+"/"+AbilityManager.GetRscCap();
			//sliderAbilityRsc.value=AbilityManager.GetRscRatio();
		}

		//更新星星數
		void UpdatePerkRscDisplay(int value){
			Debug.Log (value);
			//perkRscItem.lbMain.text=value.ToString();
		}


		//目前第幾波
		void UpdateWaveDisplay(int wave){
			//wave = 當前第幾波
			//SpawnManager.GetTotalWaveCount() = 總共有幾波
			Debug.Log (wave);
			//lbWave.text="wave-"+wave+(!SpawnManager.IsEndlessMode() ? "/"+SpawnManager.GetTotalWaveCount() : "" );
			//waveItem.lbMain.text=wave+(!SpawnManager.IsEndlessMode() ? "/"+SpawnManager.GetTotalWaveCount() : "" );
		}
	}
}
