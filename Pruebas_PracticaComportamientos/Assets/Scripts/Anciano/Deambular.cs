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
        personaje.mLabel.text = "Voy a deambular";
        personaje.GoTo(GetRandomLocation(new string[] { "Casa", "Bar", "WC" }));
    }

    override public void Execute(Personaje personaje)
    {
        string msg;
        if (personaje.PathComplete())
        {
            if (personaje.GetLocation() == Sala.Location.Casa)
            {
                personaje.GoTo(GetRandomLocation(new string[] { "Bar", "WC" }));
                msg = "Me piro de casa";
                personaje.println(msg);
                personaje.mLabel.text = msg;
            }
            else if (personaje.GetLocation() == Sala.Location.Bar)
            {
                personaje.GoTo(GetRandomLocation(new string[] { "Casa", "WC" }));
                msg = "Ya estoy borracho iuuuupi";
                personaje.println(msg);
                personaje.mLabel.text = msg;
            }
            else if (personaje.GetLocation() == Sala.Location.WC)
            {
                personaje.GoTo(GetRandomLocation(new string[] { "Casa", "Bar" }));
                msg = "No se para que vengo aquí si tengo pañales";
                personaje.println(msg);
                personaje.mLabel.text = msg;
            }
        }
        if(Random.value * 100 < 5)
            personaje.GetFSM().ChangeState(Sentarse.GetInstance());

    }
    override public void Exit(Personaje personaje)
    {
    }
    private string GetRandomLocation(string[] locations)
    {
        return locations[Mathf.FloorToInt(Random.value * locations.Length)];
    }
}
