using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paseo : GenericState {
    private List<string> locations;
    private string actualLocation;

    #region SINGLETON
    private static Paseo INSTANCE;

    public static Paseo GetInstance()
    {
        if (INSTANCE == null)
        {
            INSTANCE = new Paseo();
        }
        return INSTANCE;
    }

    #endregion
    override public void Enter(Personaje personaje)
    {
        personaje.mLabel.text = "Voy a pasear";
        personaje.println("A pasear");
        locations = new List<string>() { "Casa", "Bar", "WC" };
        personaje.GoTo(GetRandomLocation());
    }

    override public void Execute(Personaje personaje)
    {
        if (personaje.PathComplete())
        {
            personaje.GetFSM().ChangeState(IdlePerro.GetInstance());
        }
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
