using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarvestJump
{
    public interface ISmart
    {
        void HandleWaypoint(Direction direction);
        BoundingBox boundingBox { get; set; }
    }
}
