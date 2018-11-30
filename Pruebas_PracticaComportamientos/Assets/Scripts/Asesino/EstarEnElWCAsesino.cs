using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstarEnElWCAsesino : GenericState{

	private static EstarEnElWCAsesino INSTANCE;
	
	public static EstarEnElWCAsesino GetInstance(){
		if(INSTANCE == null){
			INSTANCE = new EstarEnElWCAsesino();
		}
		return INSTANCE;
	}
	
	override public void Enter(Personaje personaje){
		personaje.println("PASO QUE ME HAGO PIS");
		personaje.GoTo(Sala.Location.WC);
	}
	
	override public void Execute(Personaje personaje){
		
		if(personaje.PathComplete() && personaje.GetLocation() == Sala.Location.WC)
		{
			Asesino a = (Asesino) personaje;
			a.EfectosDelWC();
			if(personaje.GetVejiga()<0)
			{
				personaje.VaciarVejiga();
				personaje.GetFSM().ChangeState(personaje.GetFSM().GetPreviousState());
			}
		}
	}
	override public void Exit(Personaje personaje){
		personaje.println("Jo, cómo me estaba meando");
	}
}