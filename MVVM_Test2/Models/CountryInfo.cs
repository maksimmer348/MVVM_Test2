using System.Collections.Generic;

namespace MVVM_Test2;

public class CountryInfo : PlaceInfo
{
    public IEnumerable<Province> Provinces { get; set; }
}