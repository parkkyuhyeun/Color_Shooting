using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnGhost : MonoBehaviour
{
    private static SpawnGhost instance;
    public static SpawnGhost Instance
    {
        get
        {
            if (instance == null) instance = FindObjectOfType<SpawnGhost>();
            return instance;
        }
    }

    public GameObject[] ghostFac;
    public GameObject[] ghost;
    public int ran;
    public int indexRan;
    public int fullGhost;
    private int maxGhost = 30;
    private void Start()
    {
        StartCoroutine(SpawningGhost());
    }
    IEnumerator SpawningGhost()
    {
        while (true)
        {
            if(fullGhost < maxGhost)
            {
                ran = Random.Range(0, 29);
                indexRan = Random.Range(0, 3);
                Instantiate(ghost[indexRan], ghostFac[ran].transform.position, ghostFac[ran].transform.rotation);
                fullGhost++;
                Debug.Log("spawn!");
                yield return new WaitForSeconds(10f);
            }
        }
    }
}
