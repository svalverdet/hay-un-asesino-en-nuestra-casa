using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinoGlobalState : GenericState {

	private static NinoGlobalState INSTANCE;
	
	public static NinoGlobalState GetInstance(){
		if(INSTANCE == null){
			INSTANCE = new NinoGlobalState();
		}
		return INSTANCE;
	}
	
	override public void Enter(Personaje personaje){
		
	}
	override public void Execute(Personaje personaje){
		Nino a = (Nino) personaje;
		if(a.TienePis()){
			a.println("Ayy que me hago pipiii");
			a.GetFSM().ChangeState(NinoEstarEnElWC.GetInstance());
		}
			
			
	}
	override public void Exit(Personaje personaje){
	}
}
