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
			
	}
	override public void Exit(Personaje personaje){
	}
}