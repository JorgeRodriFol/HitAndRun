using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCloudsScript : MonoBehaviour
{
    public float TimeSpawn;
    public GameObject Clouds1;
    public GameObject Clouds2;
    public GameObject Clouds3;
    GameObject[] CloudsModels = new GameObject[3];  // Crear un arreglo de GameObjects con capacidad para 3 elementos
    private Vector3 SpawnPosition;


    // Start is called before the first frame update
    void Start()
    {
        TimeSpawn = Time.time;
        CloudsModels[0] = Clouds1;  // Asignar un objeto al primer elemento
        CloudsModels[1] = Clouds2;
        CloudsModels[1] = Clouds3;// Asignar un objeto al segundo elemento
        SpawnPosition.x = 11.0f;
        SpawnPosition.y = 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        int ModelId = UnityEngine.Random.Range(0, 3);
        if (Time.time == TimeSpawn + 1.0f)
        {
            GameObject Cloud = Instantiate(CloudsModels[ModelId], SpawnPosition, Quaternion.identity);
        }
    }
}
