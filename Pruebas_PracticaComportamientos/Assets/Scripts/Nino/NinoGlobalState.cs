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
		if(a.GetFSM().GetCurrentState() != NinoEstarEnElWC.GetInstance()  
			&&  a.GetFSM().GetCurrentState() != RecibirBronca.GetInstance()  
			&&  a.TienePis())
		{
			a.GetFSM().ChangeState(NinoEstarEnElWC.GetInstance());
		}else if(a.GetVejiga() > 20 && !a.GetPisEncima()){
			a.SetPisEncima(true);
			personaje.mLabel.text = "Me cagao encima";
		}
			
			
	}
	override public void Exit(Personaje personaje){
	}
}
