
using System;

public enum Dir
{
    DOWN, LEFT, UP, RIGHT
    
}

public static class Extensions
{

    public static Dir getNextDir(Dir dir)
    {
        Dir[] Arr = (Dir[])Enum.GetValues(dir.GetType());
        int j = Array.IndexOf(Arr, dir) + 1;
        return (Arr.Length==j) ? Arr[0] : Arr[j];
    }
}
