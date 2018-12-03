using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Roomba : Personaje {

    private List<GameObject> manchas;

    override protected void Start()
    {
        // Variables del padre
        base.Start();
		
        agent = GetComponent<NavMeshAgent>();
        ChangeLocation(Sala.Location.Cocina);

		mEstadoInicial = IdleRoomba.GetInstance();
        mFSM = new FSM(this);
        mFSM.SetCurrentState(mEstadoInicial);
        mFSM.SetPreviousState(mEstadoInicial);
        mFSM.GetCurrentState().Enter(this);
		
        // Variables propias
        manchas = new List<GameObject>();

    }

    override public void UpdatePersonaje()
    {
		mFSM.Update();
    }
	
	
	// Métodos

    public List<GameObject> GetManchas()
    {
        return manchas;
    }
    public void AddMancha(GameObject mancha)
    {
        manchas.Add(mancha);
    }
    public void LimpiarMancha(GameObject mancha)
    {
        GameObject.Destroy(mancha);
        manchas.Remove(mancha);
    }
}
