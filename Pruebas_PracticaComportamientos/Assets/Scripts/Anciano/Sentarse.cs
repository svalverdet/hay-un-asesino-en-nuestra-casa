using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sentarse : GenericState {

    private bool sit;

    #region SINGLETON
    private static Sentarse INSTANCE;

    public static Sentarse GetInstance()
    {
        if (INSTANCE == null)
        {
            INSTANCE = new Sentarse();
        }
        return INSTANCE;
    }

    #endregion
    override public void Enter(Personaje personaje)
    {
        personaje.mLabel.text = "Voy a sentarme";
        personaje.GoTo(GetSeatLocation(personaje));
        sit = false;
    }

    override public void Execute(Personaje personaje)
    {
        if (personaje.PathComplete() && !sit)
        {
            string msg = "Me siento";
            personaje.println(msg);
            personaje.mLabel.text = msg;
            sit = true;
        }else if (sit && Random.value * 100 < 10)
        {
            personaje.GetFSM().ChangeState(Deambular.GetInstance());
        }
    }
    override public void Exit(Personaje personaje)
    {
    }
    //Devuelve el asiento más cercano
    private Vector3 GetSeatLocation(Personaje personaje)
    {
        ArrayList seats = ((Anciano)personaje).GetSeats();
        float distance = Mathf.Infinity;
        float distanceToCharacter = Mathf.Infinity;
        Vector3 actualPos = new Vector3(0.0f, 0.0f, 0.0f);
        foreach (GameObject c in seats)
        {
            distanceToCharacter = (personaje.transform.position - c.transform.position).magnitude;
            if (distance > distanceToCharacter)
            {
                distance = distanceToCharacter;
                actualPos = c.transform.position;
            }
        }
        return actualPos;
    }
}
