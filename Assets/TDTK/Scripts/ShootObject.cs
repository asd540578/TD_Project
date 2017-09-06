﻿using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace TDTK {
	
	
	public class ShootObject : MonoBehaviour {
		
		public enum _Type{ Projectile, Beam, Effect, /*Missile,*/ }
		public _Type type;
		
		[Header("Projectile")]
		public float speed=10;
		public float elevation=2;		//how elevated the shoot trajectory is
		public float falloffRange=1;	//below this range, the elevation will gradually decrease, try set to match the max range of the tower
		
		private float eta=1;				//estimated time to hit target, used to adjust offsetPos during runtime
		private float effElevation=1;		//actual elevation used in runtime, recalculated based on falloffRange
		
		//offset to the targetPos for the SO to aim for, adjust in runtime to create a trajectory
		//it's always (0, value, 0), and value is consistently droping as the SO approach the target, making the SO aim above the target and drops overtime
		private Vector3 offsetPos=Vector3.zero;	
		
		[Space(5)] public List<TrailRenderer> trailList=new List<TrailRenderer>();
		
		
		//[Header("Missile")]
		//public float maxDeviation=1;
		//private Vector3 missileOffset=Vector3.zero;	
		
		
		[Header("Beam")]
		public List<LineRenderer> lines=new List<LineRenderer>();
		public float beamDuration=0.5f;
		public float startWidth=0.25f;
		private List<Vector3> linePos=new List<Vector3>{ Vector3.zero, Vector3.zero };
		private Vector3 tgtPos;
		
		
		[Header("Effect")]
		public float effectDuration=0.5f;
		
		
		[Header("Visual and Audio")]
		public VisualObject effectShoot=new VisualObject();
		public VisualObject effectHit=new VisualObject();
		
		public AudioClip shootSound;
		public AudioClip hitSound;
		
		
		[Header("Runtime Attribute (For Debugging)")]
		public Unit tgtUnit;
		public float tgtRadius=0;
		public Vector3 targetPos;
		
		public AttackInfo attackInfo;
		
		public float shootTime;
		public Transform shootPoint;
		
		private bool shot=false;
		private bool hit=false;
		
		protected GameObject thisObj;	//public GameObject GetObj(){ return thisObj; }
		protected Transform thisT;		//public Transform GetT(){ return thisT; }
		public Vector3 GetPos(){ return thisT!=null ? thisT.position : transform.position ; }
		public Quaternion GetRot(){ return thisT!=null ? thisT.rotation : transform.rotation ; }
		
		public void Awake(){
			thisT=transform;
			thisObj=gameObject;
			
			if(type==_Type.Beam){
				for(int i=0; i<lines.Count; i++){
					if(lines[i]==null){ lines.RemoveAt(i); i-=1; }
				}
				if(lines.Count==0) Debug.LogWarning("Beam type shoot-object hasn't been assigned any LineRenderer");
			}
		}
		
		
		void OnEnable(){
			if(trailList==null) return;
			for(int i=0; i<trailList.Count; i++) trailList[i].Clear();
		}
		
		
		//called by Unit to fire the shoot-object, all initial calculation for a shot goes here
		public void InitShoot(AttackInfo aInfo, Transform shootP=null){
			shootPoint=shootP;
			attackInfo=aInfo;
			InitShoot(aInfo.tgtUnit);
		}
		public void InitShoot(Unit tUnit){
			tgtUnit=tUnit;
			tgtRadius=tgtUnit.GetRadius();
			targetPos=tgtUnit.GetTargetPoint();
			shot=true;	hit=false; shootTime=Time.time;
			
			if(type==_Type.Projectile){
				//estimate the time taken to reach the target (roughly) and calculate the effective elevation based on falloffRange
				float dist=Vector3.Distance(GetPos(), targetPos);
				eta=dist/speed;
				effElevation=elevation*Mathf.Clamp((dist-(falloffRange*.5f))/falloffRange, 0, 1);
			}
			else if(type==_Type.Beam){
				if(shootPoint!=null) thisT.parent=shootPoint;
			}
			else if(type==_Type.Effect){
				thisT.LookAt(targetPos);
			}
			/*
			else if(type==_Type.Missile){
				float dist=Vector3.Distance(GetPos(), targetPos);
				eta=dist/speed;
				
				if(shootPoint!=null) thisT.rotation=shootPoint.rotation;
				
				float rotX=maxDeviation;//Random.Range(0, maxDeviation);
				float rotY=Random.value>.5f ? -maxDeviation : maxDeviation;	//Random.Range(-maxDeviation, maxDeviation);
				missileOffset=thisT.rotation*Quaternion.Euler(-rotX, rotY, 0)*Vector3.forward*dist*0.65f;
			}
			*/
			
			effectShoot.Spawn(GetPos(), GetRot());
			AudioManager.PlaySound(shootSound);
		}
		
		
		private void UpdateTargetPos(){
			if(tgtUnit!=null) targetPos=tgtUnit.GetTargetPoint();
			else tgtRadius=0.1f;
		}
		
		void OnDrawGizmos(){
			Gizmos.DrawLine(GetPos(), targetPos);
		}
		
		void Update(){
			if(!shot || hit) return;
			
			UpdateTargetPos();
			
			if(type==_Type.Projectile){
				//calculate the offset position based on the shoot time and eta
				float t=Mathf.Min((Time.time-shootTime)/eta, 1);
				offsetPos=new Vector3(0, 1-(t), 0)*effElevation;
				
				Vector3 dir=(targetPos+offsetPos-GetPos()).normalized;
				thisT.LookAt(GetPos()+dir);
				
				float dist=Vector3.Distance(targetPos+offsetPos, GetPos());
				
				if(dist>tgtRadius) thisT.Translate(dir*Mathf.Min(Time.deltaTime*speed, dist), Space.World);
				else Hit(GetPos());
			}
			else if(type==_Type.Beam){
				float durRemain=Mathf.Clamp(beamDuration-(Time.time-shootTime), 0, beamDuration);
				
				if(durRemain<=0) Hit(ModifyTargetPosWithTgtRadius());
				else{
					for(int i=0; i<lines.Count; i++){
						linePos[0]=GetPos(); linePos[1]=ModifyTargetPosWithTgtRadius();
						lines[i].SetPositions(linePos.ToArray());
						lines[i].widthMultiplier = Mathf.Lerp(startWidth, 0, 1-durRemain/beamDuration);
						//lines[i].startWidth=Mathf.Lerp(startWidth, 0, 1-durRemain/beamDuration);
						//lines[i].endWidth=Mathf.Lerp(startWidth, 0, 1-durRemain/beamDuration);
					}
				}
			}
			else if(type==_Type.Effect){
				if(Time.time-shootTime>effectDuration){
					Hit(ModifyTargetPosWithTgtRadius());
				}
			}
			/*
			else if(type==_Type.Missile){
				float t=Mathf.Min((Time.time-shootTime)/eta, 1);
				Vector3 offset=Vector3.Slerp(missileOffset, Vector3.zero, t);
				
				thisT.LookAt(targetPos+offset);
				
				float dist=Vector3.Distance(targetPos+offset, GetPos());
				
				if(dist>tgtRadius) thisT.Translate(Vector3.forward*Mathf.Min(Time.deltaTime*speed, dist), Space.Self);
				else Hit(GetPos());
			}
			*/
		}
		
		void Hit(Vector3 hitPos){
			if(hit) return;
			hit=true;
			
			effectHit.Spawn(hitPos, Quaternion.identity);
			AudioManager.PlaySound(hitSound);
			
			if(attackInfo!=null && tgtUnit!=null) tgtUnit.ApplyAttack(attackInfo);
			
			ObjectPoolManager.Unspawn(thisObj);
		}
		
		
		Vector3 ModifyTargetPosWithTgtRadius(){
			Vector3 dir=(GetPos()-targetPos).normalized;
			return targetPos+dir*tgtRadius;
		}
		
		
		public float GetElevationAngle(Vector3 sPos, Vector3 tPos){
			if(type!=_Type.Projectile) return 0;
			
			float dist=Vector3.Distance(sPos, tPos);
			float elev=elevation*Mathf.Clamp((dist-(falloffRange*.5f))/falloffRange, 0, 1);
			return -Mathf.Atan(elev/dist)*Mathf.Rad2Deg;
		}
		
	}
	
}