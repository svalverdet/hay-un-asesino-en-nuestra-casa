using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Adulto : Personaje {

	private int mSed;
	private int mVejiga;
	private int LIMITE_SED = 6;
	private int LIMITE_VEJIGA = 14;
	
       
	void Start(){
		
		// Variables del padre
		mLastTimeUpdated = 0.0f;
		mIntervalToUpdate = 2.0f;
		agent = GetComponent<NavMeshAgent>();
		ChangeLocation(Sala.Location.Casa);
		
		// Variables propias
		mSed = 5;
		mVejiga = 2;
		
		mFSM = new FSM(this);
		mFSM.SetCurrentState(EstarEnCasa.GetInstance());
		mFSM.SetPreviousState(EstarEnCasa.GetInstance());
		mFSM.SetGlobalState(GlobalState.GetInstance());
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
	
	public int GetSed(){ return mSed;}
	public int GetVejiga(){ return mVejiga;}
	
	public bool TieneSed(){ return mSed >= LIMITE_SED;}
	public bool TienePis(){ return mVejiga >= LIMITE_VEJIGA;}
	public void IncrementarSed(){ int n = GetRandom(1,3); mSed += n;}
	public void EfectosDelBar(){ mSed = 0;}
	
	public void EfectosDelWC(){ mSed += 1; mVejiga = 0;}
	
}
