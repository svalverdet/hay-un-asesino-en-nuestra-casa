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
        if (a.GetHealth() < 5)
        {
            personaje.GetFSM().ChangeState(AdultoHuir.GetInstance());
        }
        else if (a.GetAsesino() != null
           )
        {
            personaje.GetFSM().ChangeState(Golpear.GetInstance());
        }
        else if (a.TienePis()
           && (personaje.GetFSM().GetCurrentState() != EstarEnElWC.GetInstance())
           && (personaje.GetFSM().GetCurrentState() != EstarEnElBar.GetInstance())
           && (personaje.GetFSM().GetCurrentState() != EcharBronca.GetInstance())
           && (personaje.GetFSM().GetCurrentState() != AtenderPerro.GetInstance())
           && (personaje.GetFSM().GetCurrentState() != Golpear.GetInstance())
           && (personaje.GetFSM().GetCurrentState() != Huir.GetInstance())
           )

        {
            personaje.println("Ay que me hago pis!!");
            personaje.GetFSM().ChangeState(EstarEnElWC.GetInstance());
        }
			
	}
	override public void Exit(Personaje personaje){
	}
}