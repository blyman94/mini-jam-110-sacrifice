using Unity.VisualScripting;
using UnityEngine;
public class GridObject
{
    private GridZ<GridObject> grid;
    private int x;
    private int z;

    private PlacedObject placedObject;

    public void SetPlacedObject(PlacedObject placedObject)
    {
        this.placedObject = placedObject;
        grid.triggerOnGridValueChanged(x,z);
    }

    public PlacedObject GetPlacedObject()
    {
        return placedObject;
    }

    public void ClearPlacedObject()
    {
        placedObject = null;
        grid.triggerOnGridValueChanged(x,z);
    }

    public bool CanBuild()
    {
        return placedObject == null;
    }
    public GridObject(GridZ<GridObject> grid, int x, int z)
    {
        this.grid = grid;
        this.x = x;
        this.z = z;
        
    }

    public override string ToString()
    {
        return x + ", " + z + "\n" + placedObject;
    }
}
