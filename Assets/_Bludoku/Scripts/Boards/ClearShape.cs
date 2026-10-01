using System;

namespace _Bludoku.Scripts.Boards
{
    [Flags]
    public enum ClearShape
    {
        None = 0,
        Row = 1 << 0,
        Column = 1 << 1,
        Box = 1 << 2
    }
}
