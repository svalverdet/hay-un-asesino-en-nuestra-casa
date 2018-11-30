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
        personaje.GoTo(Sala.GetRandomRoomPositionExcept(personaje.GetLocation()));
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
            else if (personaje.GetLocation() == Sala.Location.Bar)
            {
                msg = "Ya estoy borracho iuuuupi";
            }
            else if (personaje.GetLocation() == Sala.Location.WC)
            {
                msg = "No se para que vengo aquí si tengo pañales";
            }
			
            personaje.GoTo(Sala.GetRandomRoomPositionExcept(personaje.GetLocation()));
            personaje.println(msg);
        }
		
        if(Random.value * 100 < 5)
            personaje.GetFSM().ChangeState(Sentarse.GetInstance());

    }
	
    override public void Exit(Personaje personaje)
    {
    }
	

}
