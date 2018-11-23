using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM {

	private Personaje mOwner; //agente que posee la instancia
	private GenericState mCurrentState; //estado actual
	private GenericState mPreviousState; //estado previo
	private GenericState mGlobalState; //llamado cada vez que la FSM se actualiza
	
	
	public FSM(Personaje owner){
		this.mOwner = owner;
		this.mCurrentState = null;
		this.mPreviousState = null;
		this.mGlobalState = null;
	}
	
	// Métodos para inicializar la FMS
	public void SetCurrentState(GenericState s){ this.mCurrentState = s;}
	public void SetPreviousState(GenericState s){ this.mPreviousState = s;}
	public void SetGlobalState(GenericState s){ this.mGlobalState = s;}
	
	// Se ejecutan los métodos de los estados
	public void Update(){
		if(this.mGlobalState != null) mGlobalState.Execute(this.mOwner);
		if(this.mCurrentState != null) mCurrentState.Execute(this.mOwner);
	}
	
	public void ChangeState(GenericState newState){
		if(newState != null){
			// Se guarda el último estado
			this.mPreviousState = this.mCurrentState;
			
			// Se ejecuta el método de salida del último estado
			this.mCurrentState.Exit(this.mOwner);
			
			// Se cambia al nuevo estado
			this.mCurrentState = newState;
			
			// Se llama al método de entrada del nuevo estado
			this.mCurrentState.Enter(this.mOwner);
		}
	}
	
	public void RevertToPreviousState(){
		ChangeState(this.mPreviousState);
	}
	
	// Comprobar si se encuentra en el estado 's'
	public bool isInState(GenericState s){
		//return (this.)
		return true;
	}
	
	public GenericState GetCurrentState(){ return this.mCurrentState;}
	public GenericState GetPreviousState(){ return this.mPreviousState;}
	public GenericState GetGlobalState(){ return this.mGlobalState;}
}
