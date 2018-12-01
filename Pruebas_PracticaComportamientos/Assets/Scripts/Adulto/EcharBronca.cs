using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EcharBronca : GenericState {
	
	private static EcharBronca INSTANCE;
	
	public static EcharBronca GetInstance(){
		if(INSTANCE == null){
			INSTANCE = new EcharBronca();
		}
		return INSTANCE;
	}
	
	override public void Enter(Personaje personaje)
	{
		Adulto a = (Adulto) personaje;
		List<Personaje> ninos = personaje.GetController().GetPersonajesByType<Nino>();
		if(ninos.Count > 0 && ((Nino)ninos[0]).GetAdultoBronca()==null){
			Nino n = (Nino) ninos[0];
			n.SetAdultoBronca(a);
			n.GetFSM().ChangeState(RecibirBronca.GetInstance());
			
			a.SetNinoBronca(n);
			personaje.println(n.GetName()+ " ven aquí YA");
		}else{
			personaje.println("Arghh, no puedo echar bronca");
			personaje.GetFSM().ChangeState(personaje.GetFSM().GetPreviousState());
		}
		
	}
	
	override public void Execute(Personaje personaje){
		
		Adulto a = (Adulto) personaje;
		Nino n = a.GetNinoBronca();
		
		// Si el niño no está cerca, le sigue llamando
		if(n.GetLocation() == Sala.Location.Salon){
			
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
			
			// Si ya ha tenido suficiente, cambia de estado y deja ir al niño
			a.EfectosDeLaBronca();
			if(a.GetAburrimiento()<0)
			{
				personaje.GetFSM().ChangeState(personaje.GetFSM().GetPreviousState());
			}
		}else{
			personaje.mLabel.text = "NIÑOOOOOOOO!!!!";
		}
		
	}
	override public void Exit(Personaje personaje){
		Adulto a = (Adulto) personaje;
		Nino n = a.GetNinoBronca();
		
		if(n!= null)
		{
			int rnd = personaje.GetRandom(1,6);
			if(rnd<3)
				personaje.println("Qué poco nos respetan los niños...");
			else if(rnd>=3)
				personaje.println("En mis tiempos con una buena hostia se le quitaba la tontería.");
			
			n.GetFSM().ChangeState(n.GetFSM().GetPreviousState());
			a.SetNinoBronca(null);
		}
		
	}
}
