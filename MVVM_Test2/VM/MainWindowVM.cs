namespace MVVM_Test2;

/// <summary>
/// VM основного окна 
/// </summary>
public class MainWindowVM : BaseVM
{
    //создаем свойство и подцепляем к нему визуальный эл

    #region Заголовок окна

    private string title;


    /// <summary>
    /// Заголовок окна
    /// </summary>
    public string Title
    {
        get => title;
        // set
        // {
        //     //если значение свойста Title не меняется то мы ничего не делаем
        //     if (Equals((title, value))) return;
        //     title = value;
        //     OnPropertyChanged();
        // }

        //тк в MainVM уже есть конструкция Set сокращзаем вышенаписаное до:
        set => Set(ref title, value);
    }

private int status;

public int Status
{
    get => status;
    set
    {
        Set(ref status, value);
        if (status > 10) Title = "Не готов";
        else Title = "Готов";
    }
}
    #endregion

  
}