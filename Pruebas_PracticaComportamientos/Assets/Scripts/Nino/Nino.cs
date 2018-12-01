using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AI;

public class Nino : Personaje {

	private Adulto mAdultoBronca;
	
	override protected  void Start()
	{
		// Variables del padre
		base.Start();
		
		agent = GetComponent<NavMeshAgent>();
		ChangeLocation(Sala.Location.HabitacionNino);
		
		mVejiga = 0;
		ALERTA_VEJIGA = 100;
		MAX_LIMITE_VEJIGA = 250;
		
		mFSM = new FSM(this);
		mFSM.SetCurrentState(Jugando.GetInstance());
		mFSM.SetPreviousState(Jugando.GetInstance());
		mFSM.SetGlobalState(NinoGlobalState.GetInstance());
		mFSM.GetCurrentState().Enter(this);
		
		// Variables propias
		// ...
		
	}
	
	override public void UpdatePersonaje()
	{
		mFSM.Update();
		IncrementarVejiga();
	}
	
	
	// Métodos
	
	public void EfectosDelWC(){ this.mVejiga-=5;}
	
	public void IncrementarVejiga(){ mVejiga+=1;}
	
	public Adulto GetAdultoBronca(){ return this.mAdultoBronca;}
	public void SetAdultoBronca(Adulto a){ this.mAdultoBronca = a;}
	
	
	
}