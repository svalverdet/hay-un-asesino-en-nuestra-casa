using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AI;

public class Nino : Personaje {

	private int mVejiga;
	private int LIMITE_VEJIGA = 14;
	private bool mPisEncima = false;
	
	override protected  void Start(){
		
		base.Start();
		
		// Variables del padre
		mLastTimeUpdated = 0.0f;
		mIntervalToUpdate = 2.0f;
		agent = GetComponent<NavMeshAgent>();
		ChangeLocation(Sala.Location.Casa);
		
		// Variables propias
		mVejiga = 2;
		
		mFSM = new FSM(this);
		mFSM.SetCurrentState(Jugando.GetInstance());
		mFSM.SetPreviousState(Jugando.GetInstance());
		mFSM.SetGlobalState(NinoGlobalState.GetInstance());
		mFSM.GetCurrentState().Enter(this);
	}
	
	override public void UpdatePersonaje(){
		// No se actualiza de manera constante
		if(Sala.timer-mLastTimeUpdated > mIntervalToUpdate){
			mLastTimeUpdated = Sala.timer;
			mFSM.Update();
			mVejiga+=2;
		}
		
		// Se muestra el texto
		Vector3 labelPos = Camera.main.WorldToScreenPoint(this.transform.position);
		mLabel.transform.position = labelPos;
		
	}
	
	// Métodos
	
	public int GetVejiga(){ return this.mVejiga;}
	public void SetVejiga(int num){ this.mVejiga = num;}
	public bool GetPisEncima(){ return this.mPisEncima;}
	public void SetPisEncima(bool pisEncima){ this.mPisEncima = pisEncima;}
	
	public bool TienePis(){ return this.mVejiga >= LIMITE_VEJIGA;}	
	public void EfectosDelWC(){ this.mVejiga-=5; this.mPisEncima = false;}
	
	
}