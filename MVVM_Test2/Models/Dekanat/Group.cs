using System.Collections.Generic;

namespace MVVM_Test2;

public class Group
{
    public string Name { get; set; }

    //ICollection тут для свободы выобра те ту может быть списко может быыть массив может быть
    //observabeColection
    public ICollection<Student> Students { get; set; }
}