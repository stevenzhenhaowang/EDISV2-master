using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    /// <summary>
    /// This enum supports bitwise operation
    /// </summary>
    [Flags]
    public enum ASXIndexTypes
    {
        Asx200=1,
        Asx50=2,
        Asx500=4,
        Asx30=8
    }
}
