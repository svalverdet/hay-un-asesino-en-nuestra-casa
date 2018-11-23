using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalState : GenericState{
	
	#region SINGLETON
	private static GlobalState INSTANCE;
	
	public static GlobalState GetInstance(){
		if(INSTANCE == null){
			INSTANCE = new GlobalState();
		}
		return INSTANCE;
	}
	#endregion
	
	override public void Enter(Personaje personaje){
		
	}
	override public void Execute(Personaje personaje){
		Adulto a = (Adulto) personaje;
		if(a.TienePis() && (personaje.GetFSM().GetCurrentState() != EstarEnElWC.GetInstance())){
			personaje.println("Ay que me hago pis!!");
			personaje.mLabel.text = "Ay que me hago pis!!";
			personaje.GetFSM().ChangeState(EstarEnElWC.GetInstance());
		}
			
	}
	override public void Exit(Personaje personaje){
	}
}