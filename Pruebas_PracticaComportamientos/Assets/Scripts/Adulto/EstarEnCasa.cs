using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstarEnCasa : GenericState {

	#region SINGLETON
	private static EstarEnCasa INSTANCE;
	
	public static EstarEnCasa GetInstance(){
		if(INSTANCE == null){
			INSTANCE = new EstarEnCasa();
		}
		return INSTANCE;
	}
	#endregion
	
	override public void Enter(Personaje personaje){
		personaje.GoTo(Sala.Location.Salon);
	}
	
	override public void Execute(Personaje personaje){
		
		
		if(personaje.PathComplete())
		{
			if(personaje.TienePis() || personaje.EstaAburrido())
			{
				GenericSmartObject obj = Sala.CheckBestSmartObjectForMe(personaje);
				if(obj!=null){
					if(obj.tipo == GenericSmartObject.Tipo.Cerveza)
					{
						float rnd = Random.value * 100;
						if(rnd<70){
							personaje.GetSmartObjectInUse().inUse = false;
							personaje.SetSmartObjectInUse(null);
							personaje.GetFSM().ChangeState(EcharBronca.GetInstance());
						}else{
							personaje.GetFSM().ChangeState(EstarEnElBar.GetInstance());
						}
						
					}
					else if(obj.tipo == GenericSmartObject.Tipo.Vater)
					{
						personaje.GetFSM().ChangeState(EstarEnElWC.GetInstance());
					}
				}
			}
		}
		
	}
	override public void Exit(Personaje personaje){
	}
}
