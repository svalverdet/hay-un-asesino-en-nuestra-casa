using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

	public List<Personaje> personajes;
	
	// Última vez que se actualizó el estado
	float mLastTimeUpdated = 0.0f;
	
	// Tiempo que puede pasar entre actualizaciones del estado
    float mIntervalToUpdate = 2.0f;
	
	
	
	void Start () {
		Sala.timer = 0.0f;
		Sala.SetList();
	}
	
	void Update () {
		Sala.timer += Time.deltaTime;
		//float seconds = Sala.timer % 60;
		
		// Actualizar FSM
		int size;
		if (Sala.timer - mLastTimeUpdated > mIntervalToUpdate)
		{
			mLastTimeUpdated = Sala.timer;
			size = personajes.Count;
			for(int i=0; i<size; i++)
			{
				personajes[i].UpdatePersonaje();
			}
		}
		
		// Actualizar textos
		size = personajes.Count;
		for(int i=0; i<size; i++)
		{
			personajes[i].UpdateTextoPersonaje();
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
