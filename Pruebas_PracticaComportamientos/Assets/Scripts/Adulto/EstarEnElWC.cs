using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstarEnElWC : GenericState{

	private static EstarEnElWC INSTANCE;
	
	public static EstarEnElWC GetInstance(){
		if(INSTANCE == null){
			INSTANCE = new EstarEnElWC();
		}
		return INSTANCE;
	}
	
	override public void Enter(Personaje personaje){
		personaje.GoTo(Sala.Location.WC);
	}
	
	override public void Execute(Personaje personaje){
		
		if(personaje.PathComplete() && personaje.GetLocation() == Sala.Location.WC)
		{
			Adulto a = (Adulto) personaje;
			a.EfectosDelWC();
			if(personaje.GetVejiga()<0)
			{
				personaje.VaciarVejiga();
				personaje.println("Ahh...Feels good man");
				personaje.GetFSM().ChangeState(EstarEnCasa.GetInstance());
			}
		}
	}
	override public void Exit(Personaje personaje){
	}
}