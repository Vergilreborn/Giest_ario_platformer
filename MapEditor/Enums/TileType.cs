using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEditor.Enums
{
    enum TileType
    {
        Block = 0,
        None = 1,  //No collision (Allowed for hidden secrets!)
        Death = 2,
        Damage = 3,//Damages the player
        Water = 4, //Different animation and physics
        Ice = 5   //less friction
        
    }
}
