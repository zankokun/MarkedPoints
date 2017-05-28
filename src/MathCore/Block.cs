using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathCore
{
    public class Block
    {
        public List<AxisRange> AxisRanges { get; private set; }
        public IPoint Point { get; private set; }

        public Block(List<AxisRange> axisRanges, IPoint point)
        {
            AxisRanges = axisRanges;
            Point = point;
        }

        public bool Equals(Block other)
        {
            for (int i = 0; i < AxisRanges.Count; ++i)
            {
                if (AxisRanges[i].First != other.AxisRanges[i].First ||
                   AxisRanges[i].Second != other.AxisRanges[i].Second)
                    return false;
            }
            return true;
        }
    }
}
