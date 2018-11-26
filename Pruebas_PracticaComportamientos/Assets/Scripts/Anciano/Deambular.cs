using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deambular : GenericState{

    private List<string> locations;
    private string actualLocation;

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
        locations = new List<string>() { "Casa", "Bar", "WC" };
        personaje.GoTo(GetRandomLocation());
    }

    override public void Execute(Personaje personaje)
    {
        if (personaje.PathComplete())
        {
            string msg = "";
            if (personaje.GetLocation() == Sala.Location.Casa)
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
            personaje.GoTo(GetRandomLocation());
            personaje.println(msg);
            personaje.mLabel.text = msg;
        }
        if(Random.value * 100 < 5)
            personaje.GetFSM().ChangeState(Sentarse.GetInstance());

    }
    override public void Exit(Personaje personaje)
    {
    }
    private string GetRandomLocation()
    {
        int index = Mathf.FloorToInt(Random.value * locations.Count);
        string nextLocation = locations[index];
        locations.RemoveAt(index);
        locations.Add(actualLocation);
        actualLocation = nextLocation;
        return actualLocation;
    }

}
