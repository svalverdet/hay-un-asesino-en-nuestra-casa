using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalStateAdulto : GenericState{
	
	#region SINGLETON
	private static GlobalStateAdulto INSTANCE;
	
	public static GlobalStateAdulto GetInstance(){
		if(INSTANCE == null){
			INSTANCE = new GlobalStateAdulto();
		}
		return INSTANCE;
	}
	#endregion
	
	override public void Enter(Personaje personaje){
		
	}
	
	override public void Execute(Personaje personaje){
		
		Adulto a = (Adulto) personaje;
        if (a.GetAsesino() != null)
        {
            personaje.GetFSM().ChangeState(Golpear.GetInstance());
        }
        
			
	}
	override public void Exit(Personaje personaje){
	}
}