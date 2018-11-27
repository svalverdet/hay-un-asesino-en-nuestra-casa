using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Adulto : Personaje {

	private int mSed;
	private int mVejiga;
	private int mAburrimiento;
	private int LIMITE_ABURRIMIENTO = 15;
	private int LIMITE_SED = 9;
	private int LIMITE_VEJIGA = 18;


    override protected void Start(){
		
        base.Start();
		
        // Variables del padre
        mLastTimeUpdated = 0.0f;
		mIntervalToUpdate = 2.0f;
		agent = GetComponent<NavMeshAgent>();
		ChangeLocation(Sala.Location.Casa);
		
		// Variables propias
		mSed = 5;
		mVejiga = 2;
		mAburrimiento = 5;
		
		mFSM = new FSM(this);
		mFSM.SetCurrentState(EstarEnCasa.GetInstance());
		mFSM.SetPreviousState(EstarEnCasa.GetInstance());
		mFSM.SetGlobalState(GlobalStateAdulto.GetInstance());
		mFSM.GetCurrentState().Enter(this);
	}
	
	override public void UpdatePersonaje(){
		
		// No se actualiza de manera constante
		if(Sala.timer-mLastTimeUpdated > mIntervalToUpdate){
			mLastTimeUpdated = Sala.timer;
			mFSM.Update();
		}
		
		// Se muestra el texto
		Vector3 labelPos = Camera.main.WorldToScreenPoint(this.transform.position);
		mLabel.transform.position = labelPos;
	}
	
	
	// Métodos
	
	public int GetSed(){ return mSed;}
	public int GetVejiga(){ return mVejiga;}
	public int GetAburrimiento(){ return mAburrimiento;}
	
	public bool TieneSed(){ return mSed >= LIMITE_SED;}
	public bool TienePis(){ return mVejiga >= LIMITE_VEJIGA;}
	public bool EstaAburrido(){ return mAburrimiento >= LIMITE_ABURRIMIENTO;}
	
	public void IncrementarSed(){ int n = GetRandom(1,4); mSed += n;}
	public void IncrementarVejiga(){ mVejiga+=2;}
	public void IncrementarAburrimiento(){ mAburrimiento+=2;}
	
	public void EfectosDelBar(){ mSed = 0;}
	public void EfectosDeLaBronca(){ mAburrimiento -= 3; mSed += 2;}
	public void EfectosDelWC(){ mSed += 1; mVejiga = 0;}
	
}
