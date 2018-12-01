using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

	public List<Personaje> personajes;
	
	// Última vez que se actualizó el estado
	float mLastTimeUpdatedFSM = 0.0f;
	float mLastTimeUpdatedPercepcion = 0.0f;
	
	// Tiempo que puede pasar entre actualizaciones del estado
    float mIntervalToUpdateFSM = 2.0f;
	float mIntervalToUpdatePercepcion = 0.5f;
	
	void Start () {
		Sala.timer = 0.0f;
		Sala.SetList();
	}
	
	void Update () {
		Sala.timer += Time.deltaTime;
		//float seconds = Sala.timer % 60;
		
		int size;
		size = personajes.Count;
		
		// Actualizar textos
		for(int i=0; i<size; i++)
		{
			personajes[i].UpdateTextoPersonaje();
		}
		
		// Actualizar percepciones
		if (Sala.timer - mLastTimeUpdatedPercepcion > mIntervalToUpdatePercepcion)
		{
			mLastTimeUpdatedPercepcion = Sala.timer;
			for(int i=0; i<size; i++)
			{
				personajes[i].UpdatePercepcion();
			}
		}
		
		// Actualizar FSM
		if (Sala.timer - mLastTimeUpdatedFSM > mIntervalToUpdateFSM)
		{
			mLastTimeUpdatedFSM = Sala.timer;
			for(int i=0; i<size; i++)
			{
				personajes[i].UpdatePersonaje();
			}
		}
		
		
		
	}
	
	
	// Métodos

    public List<Personaje> GetPersonajesByType<T>()
    {
        List<Personaje> aux = new List<Personaje>();
        foreach (Personaje p in personajes)
        {
            if (p.GetComponent<T>() != null) aux.Add(p);
        }
        return aux;
    }
}
