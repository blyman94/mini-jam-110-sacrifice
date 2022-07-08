using Unity.VisualScripting;
using UnityEngine;
public class GridObject
{
    private GridZ<GridObject> grid;
    private int x;
    private int z;
    public GridObject(GridZ<GridObject> grid, int x, int z)
    {
        this.grid = grid;
        this.x = x;
        this.z = z;
    }

    public override string ToString()
    {
        return x + ", " + z;
    }
}
