using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    private LineRenderer lr;
    private List<Vector3> points;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
        points = new List<Vector3>();
    }

    public void SetUpLine(int i, Vector3 end)
    {
        lr.SetPosition(i, end);
    }
    public void AddLine(Vector3 start, Vector3 end)
    {
        lr.positionCount++;
        points.Add(start);
        points.Add(end);

    }
    public void RemoveLine(Vector3 start, Vector3 end)
    {
        lr.positionCount--;
        points.Remove(start);
        points.Remove(end);

    }




}
