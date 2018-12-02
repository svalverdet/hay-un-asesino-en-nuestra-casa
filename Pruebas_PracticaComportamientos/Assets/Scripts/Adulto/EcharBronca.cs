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
        int index = Random.Range(0, ninos.Count - 1);
        if (ninos.Count > 0 && ((Nino)ninos[index]).GetAdultoBronca() == null
            && ((Nino)ninos[index]).GetFSM().GetCurrentState() != Huir.GetInstance()) {
            Nino n = (Nino)ninos[index];
            n.SetAdultoBronca(a);
            n.GetFSM().ChangeState(RecibirBronca.GetInstance());

            a.SetNinoBronca(n);
            personaje.println(n.GetName() + " ven aquí YA");
		}else{
			personaje.println("Arghh, no puedo echar bronca");
			personaje.GetFSM().ChangeState(personaje.GetFSM().GetPreviousState());
		}
		
	}
	
	override public void Execute(Personaje personaje){
		
		Adulto a = (Adulto) personaje;
		Nino n = a.GetNinoBronca();
        if (n != null)
        {
            a.transform.LookAt(new Vector3(n.transform.position.x, a.transform.position.y, n.transform.position.z));
            // Si el niño no está cerca, le sigue llamando
            if (a.GetNinoBronca() == a.GetNino())
            {

                int num = personaje.GetRandom(1, 4);
                string msg;
                if (num == 1)
                {
                    msg = "dsa2hiu1j ·$%dnal $$&&d sakAAAAAAA";
                }
                else if (num == 2)
                {
                    msg = "Verás, cariñito, mi niño bonito...";
                }
                else
                {
                    msg = "FUISTE UN ACCIDENTE";
                }
                personaje.println(msg);

                // Si ya ha tenido suficiente, cambia de estado y deja ir al niño
                a.EfectosDeLaBronca();
                if (a.GetAburrimiento() < 0)
                {
                    personaje.GetFSM().RevertToPreviousState();
                }
            }
            else
            {
                personaje.println("NIÑOOOOOOOO!!!!");
            }
        }
        else
        {
            a.GetFSM().RevertToPreviousState();
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
			
			n.GetFSM().RevertToPreviousState();
            a.SetNinoBronca(null);
		}
		
	}
}
