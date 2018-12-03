using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deambular : GenericState{

    #region SINGLETON
    private static Deambular INSTANCE;

    public static Deambular GetInstance()
    {
        if (INSTANCE == null)
        {
            INSTANCE = new Deambular();
        }
        return INSTANCE;
    }

    #endregion
    override public void Enter(Personaje personaje)
    {
        personaje.println("Voy a deambular");
        personaje.GoTo(Sala.GetRandomRoomPositionExcept(new Sala.Location[] { personaje.GetLocation(), Sala.Location.Bar }));
    }

    override public void Execute(Personaje personaje)
    {
        if (personaje.PathComplete())
        {
            string msg = "";
            if (personaje.GetLocation() == Sala.Location.Salon)
            {
                msg = "Me piro de casa";
            }
            else if (personaje.GetLocation() == Sala.Location.Entrada)
            {
                msg = "Estoy en la entrada de la casa y la salida de la vida";
            }
            else if (personaje.GetLocation() == Sala.Location.WC || personaje.GetLocation() == Sala.Location.WC2)
            {
                msg = "No se para que vengo aquí si tengo pañales";
            }
            else if (personaje.GetLocation() == Sala.Location.Cocina)
            {
                msg = "Voy a pelar unas pipas";
            }
            else if (personaje.GetLocation() == Sala.Location.HabitacionNino)
            {
                msg = "Ven niño que tengo aqui un caramelo";
            }
            personaje.GoTo(Sala.GetRandomRoomPositionExcept(new Sala.Location[] { personaje.GetLocation(), Sala.Location.Bar }));
            personaje.println(msg);
        }
		
        if(personaje.EstaAburrido()){
			GenericSmartObject obj = Sala.CheckBestSmartObjectForMe(personaje);
			if(obj!=null)
				personaje.GetFSM().ChangeState(Sentarse.GetInstance());
		}

    }
	
    override public void Exit(Personaje personaje)
    {
    }
	

}
