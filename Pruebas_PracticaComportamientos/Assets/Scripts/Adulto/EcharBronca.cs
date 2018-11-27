using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EcharBronca : GenericState {

	private static EcharBronca INSTANCE;
	private Nino n;
	
	public static EcharBronca GetInstance(){
		if(INSTANCE == null){
			INSTANCE = new EcharBronca();
		}
		return INSTANCE;
	}
	
	override public void Enter(Personaje personaje){
		
		List<Personaje> ninos = personaje.GetController().GetPersonajesByType<Nino>();
		n = (Nino) ninos[0];
		personaje.mLabel.text = n.GetName()+ " ven aquí YA";
		personaje.println(n.GetName()+ " ven aquí YA");
		n.GetFSM().ChangeState(RecibirBronca.GetInstance());
		if(!personaje.IsAgentStopped()) personaje.Stop();
	}
	
	override public void Execute(Personaje personaje){
		// Si el niño no está cerca, le sigue llamando
		if(n.GetLocation() == Sala.Location.Casa){
			
			int num = personaje.GetRandom(1,4);
			string msg;
			if(num == 1){
				msg = "dsa2hiu1j ·$%dnal $$&&d sakAAAAAAA";
			}else if(num == 2){
				msg = "Verás, cariñito, mi niño bonito...";
			}else{
				msg = "FUISTE UN ACCIDENTE";
			}
			personaje.println(msg);
			personaje.mLabel.text = msg;
			
			// Si ya ha tenido suficiente, cambia de estado y deja ir al niño
			Adulto a = (Adulto) personaje;
			a.EfectosDeLaBronca();
			if(a.GetAburrimiento()<4){
				personaje.GetFSM().ChangeState(personaje.GetFSM().GetPreviousState());
			}
		}else{
			personaje.mLabel.text = "NIÑOOOOOOOO!!!!";
		}
		
	}
	override public void Exit(Personaje personaje){
		int num = personaje.GetRandom(1,6);
		if(num<3)
			personaje.mLabel.text = "Qué poco nos respetan los niños...";
		else if(num>=3)
			personaje.mLabel.text = "En mis tiempos con una buena hostia se le quitaba la tontería.";
		
		n.GetFSM().ChangeState(n.GetFSM().GetPreviousState());
	}
}
