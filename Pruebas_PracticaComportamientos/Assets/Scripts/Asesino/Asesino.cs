using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Asesino : Personaje {
	
	private Personaje victima;

    override protected void Start()
	{
		// Variables del padre
        base.Start();
		
		agent = GetComponent<NavMeshAgent>();
		ChangeLocation(Sala.Location.Entrada);
		
		mVejiga = 0;
		ALERTA_VEJIGA = 20;
		
		mFSM = new FSM(this);
		mFSM.SetCurrentState(BuscarVictima.GetInstance());
		mFSM.SetPreviousState(BuscarVictima.GetInstance());
		mFSM.SetGlobalState(GlobalStateAsesino.GetInstance());
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
	
	public void IncrementarVejiga(){ mVejiga+=1;}
	
	public void EfectosDelWC(){mVejiga -= 5;}
	
	public Personaje GetVictima(){return victima;}
	public void SetVictima(Personaje p){ victima = p;}
	
}
