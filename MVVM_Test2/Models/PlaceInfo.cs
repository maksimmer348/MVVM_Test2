using System.Collections.Generic;
using System.Drawing;

namespace MVVM_Test2;

public class PlaceInfo
{
    public string Name { get; set; }
    public Point Location { get; set; }
    
    public IEnumerable<ConfirmCount> CountCaseSicks { get; set; }
}