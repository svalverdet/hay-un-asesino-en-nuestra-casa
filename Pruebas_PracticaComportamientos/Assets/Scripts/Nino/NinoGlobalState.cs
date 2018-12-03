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
	
	override public void Execute(Personaje personaje)
	{
		Nino a = (Nino) personaje;
        if (a.GetFSM().GetCurrentState() != Huir.GetInstance()
            && a.GetAsesino() != null)
        {
            a.GetFSM().ChangeState(Huir.GetInstance());
        }
        /*else if(a.GetFSM().GetCurrentState() != NinoEstarEnElWC.GetInstance()  
			&&  a.GetFSM().GetCurrentState() != RecibirBronca.GetInstance()
            && a.GetFSM().GetCurrentState() != Huir.GetInstance()
            &&  a.TienePis())
		{
			a.GetFSM().ChangeState(NinoEstarEnElWC.GetInstance());
		}*/
		else if(a.TieneVejigaAlLimite())
		{
			a.VaciarVejiga();
			personaje.println("Me cagao encima");
			if(personaje.GetLocation() != Sala.Location.Bar) personaje.Mancha(0);
		}	
	}
	override public void Exit(Personaje personaje){
	}
}
