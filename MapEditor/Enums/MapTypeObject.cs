using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEditor.Enums
{
    enum MapTypeObject
    {
        Empty = 0,
        Transition = 1,
        Enemy = 2,
        Item = 3,
        MoveableWall = 4,
        Checkpoint= 5,
        EndLocation = 6
        
    }
}
