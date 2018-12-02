using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Perro : Personaje {

    override protected void Start()
    {
		// Variables del padre
        base.Start();
        
        agent = GetComponent<NavMeshAgent>();
        ChangeLocation(Sala.Location.Entrada);

        mFSM = new FSM(this);
        mFSM.SetCurrentState(IdlePerro.GetInstance());
        mFSM.SetPreviousState(IdlePerro.GetInstance());
        mFSM.SetGlobalState(PerroGlobalState.GetInstance());
        mFSM.GetCurrentState().Enter(this);

        List<Personaje> asesinos = GetController().GetPersonajesByType<Asesino>();
        mPersonajesDeInteres.AddRange(asesinos);

        // Variables propias
        // ...
    }

    override public void UpdatePersonaje()
    {
		mFSM.Update();
    }

    override public void UpdatePercepcion()
    {
        base.UpdatePercepcion();
        if (personajeOido != null &&
            mFSM.GetCurrentState() == IdlePerro.GetInstance())
            transform.LookAt(new Vector3(personajeOido.transform.position.x, transform.position.y, personajeOido.transform.position.z));

        if (personajeVisto != null && personajeVisto.GetComponent<Asesino>() != null)
        {
            asesino = (Asesino)personajeVisto;
            transform.LookAt(new Vector3(asesino.transform.position.x, transform.position.y, asesino.transform.position.z));
        }
    }
}
