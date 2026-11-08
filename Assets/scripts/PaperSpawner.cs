using UnityEngine;
using System.Collections.Generic; 
public class PaperSpawner : MonoBehaviour
{

    public Cabinet1[] cabinets;
    public Paper[] papers; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        List<Cabinet1> available = new List<Cabinet1>(cabinets); 

        foreach (Paper paper in papers)
        {
            Debug.Log("Paper object: " + paper.name);
            int index = Random.Range(0, available.Count);
            available[index].paper = paper;
            available.RemoveAt(index); 
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
