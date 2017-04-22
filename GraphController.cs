using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;
using UnityEngine;

public class GraphController : MonoBehaviour {

    public GameObject point;
    public Material[] colors;
    public string filePath;
    public int maxPoints = 100;
    public float scale = 1f;

	// Use this for initialization
	void Start () {
        GenerateGraph(filePath);
	}

    void GenerateGraph(string path)
    {
        StreamReader reader = new StreamReader(filePath, Encoding.Default);
        string line;
        int index = 0;

        do
        {
            line = reader.ReadLine();
            if (line != null)
            {
                float[] pos = Array.ConvertAll(line.Split(','), float.Parse);
                SpawnPoint(new Vector3(pos[0] * scale, pos[1] * scale, pos[2] * scale/3), (int) pos[3], index);
            }

            index++;
        }
        while (line != null && index < maxPoints);
        
        reader.Close();
    }

    void SpawnPoint(Vector3 pos, int color, int index)
    {
        GameObject newPoint = Instantiate(point, pos, Quaternion.identity, transform);
        MeshRenderer renderer = newPoint.GetComponent<MeshRenderer>();
        newPoint.GetComponent<PointController>().SetIndex(index);
        renderer.material = colors[color%10];
    }
}
