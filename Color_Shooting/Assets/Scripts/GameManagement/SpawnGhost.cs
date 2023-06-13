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
    public GameObject ghost;
    public int ran;
    private int maxGhost = 30;
    public int fullGhost;
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
                Instantiate(ghost, ghostFac[ran].transform.position, ghostFac[ran].transform.rotation);
                fullGhost++;
                Debug.Log("spawn!");
                yield return new WaitForSeconds(10f);
            }
        }
    }
}
