using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

	public List<Personaje> personajes;
    public Asesino asesino;
	// Última vez que se actualizó el estado
	float mLastTimeUpdated = 0.0f;
	
	// Tiempo que puede pasar entre actualizaciones del estado
	float mIntervalToUpdate = 0.25f;
	
	int size;
	
	void Start () {
		Sala.timer = 0.0f;
		Sala.SetList();
	}
	
	void Update () {
		Sala.timer += Time.deltaTime;
		//float seconds = Sala.timer % 60;
		
		size = personajes.Count;
			
		// Actualizar textos
		for(int i=0; i<size; i++)
		{
			personajes[i].UpdateTextoPersonaje();
		}
		
		// Actualizar FSM y percepciones
		if (Sala.timer - mLastTimeUpdated > mIntervalToUpdate)
		{
			mLastTimeUpdated = Sala.timer;
			for(int i=0; i<size; i++)
			{
				personajes[i].UpdatePercepcion();
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
	
	public void PersonajeMuerto(Personaje p)
	{
		personajes.Remove(p);
		int size = personajes.Count;
		for(int i=0; i<size; i++)
		{
			personajes[i].GetPersonajesDeInteres().Remove(p);
		}
		GameObject.Destroy(p);
	}

    public void GenerarAsesino()
    {
        if (GetPersonajesByType<Asesino>().Count == 0)
        {
            Asesino a = Instantiate(asesino);
            personajes.Add(a);
            foreach (Personaje p in personajes)
            {
                if (p.GetComponent<Roomba>() == null) p.AddInteraccionAsesino();
            }
        }
    }
}
