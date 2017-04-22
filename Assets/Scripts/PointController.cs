using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointController : MonoBehaviour {

    private int index;

	public void SetIndex(int idx)
    {
        index = idx;
    }

    public int GetIndex()
    {
        return index;
    }
}
