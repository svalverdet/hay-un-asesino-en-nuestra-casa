using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalStateAsesino : GenericState{
	
	#region SINGLETON
	private static GlobalStateAsesino INSTANCE;
	
	public static GlobalStateAsesino GetInstance(){
		if(INSTANCE == null){
			INSTANCE = new GlobalStateAsesino();
		}
		return INSTANCE;
	}
	#endregion
	
	override public void Enter(Personaje personaje){
		
	}
	
	override public void Execute(Personaje personaje){
		
		Asesino a = (Asesino) personaje;
		if(a.TienePis() 
			&& (personaje.GetFSM().GetCurrentState() != EstarEnElWCAsesino.GetInstance()))
		{
			personaje.println("Ay que me hago pis!!");
			personaje.GetFSM().ChangeState(EstarEnElWCAsesino.GetInstance());
		}
			
	}
	override public void Exit(Personaje personaje){
	}
}