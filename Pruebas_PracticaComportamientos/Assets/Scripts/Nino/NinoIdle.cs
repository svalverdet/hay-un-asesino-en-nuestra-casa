using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinoIdle : GenericState{

	private static NinoIdle INSTANCE;
	
	public static NinoIdle GetInstance(){
		if(INSTANCE == null){
			INSTANCE = new NinoIdle();
		}
		return INSTANCE;
	}
	
	override public void Enter(Personaje personaje){
		if(personaje.GetLocation() != Sala.Location.HabitacionNino){
			personaje.GoTo(Sala.Location.HabitacionNino);
		}
	}
	
	override public void Execute(Personaje personaje){
		if(personaje.PathComplete())
		{
			if(personaje.EstaAburrido() || personaje.TienePis())
			{
				GenericSmartObject obj = Sala.CheckBestSmartObjectForMe(personaje);
				if(obj!=null){
					if(obj.tipo == GenericSmartObject.Tipo.Juguete)
					{
						personaje.GetFSM().ChangeState(Jugando.GetInstance());
					}
					else if(obj.tipo == GenericSmartObject.Tipo.Vater)
					{
						personaje.GetFSM().ChangeState(NinoEstarEnElWC.GetInstance());
					}
				}
			}
		}
	}
	
	override public void Exit(Personaje personaje){
	}
}
