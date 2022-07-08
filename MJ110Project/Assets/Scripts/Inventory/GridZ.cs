using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridZ<TGridObject>
{

    public event EventHandler<OnGridValueChangedEventArgs> OnGridValueChanged;

    public class OnGridValueChangedEventArgs : EventArgs    
    {
        public int x;
        public int y;
    }
    private int width;
    private int height;

    private float cellSize; 
    
    private TGridObject[,] gridArray;
    
    private Vector3 originPoint;

    private TextMesh[,] debugTextArray;

    private bool showDebug;
    public GridZ(int width, int height, float cellSize, Vector3 originPoint, Func< GridZ<TGridObject>, int, int, TGridObject> createGridObject)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPoint = originPoint;

        gridArray = new TGridObject[width, height];

        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                gridArray[x, y] = createGridObject(this, x,y);
            }
        }

        showDebug = true;

        if(showDebug)
            debugTextArray = new TextMesh[width, height];

            for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {

                debugTextArray[x,y] = createWorldText(gridArray[x, y].ToString(), null, GetWorldPosition(x, y) + new Vector3(cellSize,0, cellSize) * 0.5f, 20,
                    Color.white, TextAnchor.MiddleCenter, TextAlignment.Center, 5);            
            Debug.DrawLine(GetWorldPosition(x,y), GetWorldPosition(x,y+1), Color.white, 100f);
            Debug.DrawLine(GetWorldPosition(x,y), GetWorldPosition(x+1,y), Color.white, 100f);

            }
        }
        Debug.DrawLine(GetWorldPosition(0,height), GetWorldPosition(width,height), Color.white, 100f);
        Debug.DrawLine(GetWorldPosition(width,0), GetWorldPosition(width,height), Color.white, 100f);


    }
    
    public Vector3 GetWorldPosition(int x, int y)
    {
        Debug.Log("Beta:" + originPoint.ToString());
        Debug.Log("ALPHA: " + (new Vector3(x,0,y)*cellSize + originPoint).ToString());
        return new Vector3(x,0,y)*cellSize + originPoint;
    }
    public TextMesh createWorldText(string text, Transform parent,
        Vector3 localPosition, int fontSize,
        Color color, TextAnchor textAnchor, TextAlignment textAlignment,
        int sortingOrder)
    {

        GameObject gameObject = new GameObject("World_Text", typeof(TextMesh));
        Transform transform = gameObject.transform;
        transform.SetParent(parent, false);
        transform.localPosition = localPosition;

        TextMesh textMesh = gameObject.GetComponent<TextMesh>();
        
        textMesh.anchor = textAnchor;
        textMesh.alignment = textAlignment;
        textMesh.text = text;
        textMesh.fontSize = fontSize;
        textMesh.color = color;
        textMesh.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;
        return textMesh;
    }

    public int GetHeight()
    {
        return height;
    }

    public int GetWidth()
    {
        return width;
    }

    public float getCellSize()
    {
        return cellSize;
    }

    public void SetGridObject(int x, int y, TGridObject value)
    {
        if(x <= width*cellSize && y*cellSize <= height && x > 0 && y > 0){
            gridArray[x,y] = value;
            if (OnGridValueChanged != null)
            {
                OnGridValueChanged(this, new OnGridValueChangedEventArgs{x = x, y = y
            })

            ;
            }
        }
    }

    public void triggerOnGridValueChanged(int x, int y)
    {
        if (OnGridValueChanged != null)
            OnGridValueChanged(this, new OnGridValueChangedEventArgs
            {
                x = x, y = y
            });
    }
    public void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPosition - originPoint).x / cellSize);
        y = Mathf.FloorToInt((worldPosition - originPoint).z / cellSize);
    }
    


    public void SetGridObject(Vector3 worldPosition, TGridObject value)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        SetGridObject(x,y,value);
    }

    public TGridObject GetGridObject(int x, int y)
    {
        if (x <= width && y <= height && x > 0 && y > 0)
        {
            return gridArray[x, y];
        }
        else
        {
            return default(TGridObject);
        }
    }
    
    public TGridObject GetGridObject(Vector3 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        Debug.Log(x + "," + y);
        return GetGridObject(x,y);
    }
}
