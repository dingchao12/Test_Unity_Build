using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TsetSorter : MonoBehaviour {

	// Use this for initialization
	void Start () {
        selectionsorter();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void selectionsorter()
    {
        int min;
        List<int> temp = new List<int>() { 1,3,0,5,2,7};
        for (int i = 0; i < temp.Count - 1; i++)
        {
            min = i;
            for (int j = i + 1; j < temp.Count; j++)
            {
                if (temp[j] < temp[min])
                    min = j;
            }
            int t = temp[min];
            temp[min] = temp[i];
            temp[i] = t;
        }
        string tempstr="";
        for (int i = 0; i < temp.Count; i++)
        {
            tempstr += temp[i] + ",";
            Debug.Log(tempstr);
        }
    }
}
