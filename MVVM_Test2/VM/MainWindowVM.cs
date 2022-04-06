using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using MVVM_Test2;

namespace MVVM_Test2;

/// <summary>
/// VM основного окна 
/// </summary>
public class MainWindowVM : BaseVM
{
    public MainWindowVM()
    {
        #region Команды

        CloseAppCmd = new ActionCommand(OnCloseAppCmdExecuted, CanCloseAppCmdExecuted);

        #endregion

        var dataPoints = new List<DataPoint>((int) (360 / 0.1));
        for (double x = 0; x < 360; x += 0.1)
        {
            const double TO_RADIUS = Math.PI / 180;
            var y = Math.Sin(x * TO_RADIUS);
            
            dataPoints.Add(new DataPoint(){XValue = x,YValue = y});
        }
        TestDataPoints = dataPoints;
    }

    #region Команды

    #region Команда закрытия окна

    /// <summary>
    /// Свойство команды закрытия окна (те сама команда)
    /// </summary>
    public ICommand CloseAppCmd { get; }


    /// <summary>
    /// Выполнение логики команды
    /// </summary>
    /// <param name="p"></param>
    void OnCloseAppCmdExecuted(object p)
    {
        Application.Current.Shutdown();
    }

    /// <summary>
    /// Доступность команды для выполнения
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    bool CanCloseAppCmdExecuted(object p)
    {
        //команда доступна для выполнения всегда, поэтому возварщаем труе 
        return true;
    }

    #endregion

    #endregion

    #region Свойства

    //создаем свойство и подцепляем к нему визуальный эл


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

    //если не планируется в будущем удалять или добавлять точки то можно ограничится IEnumerable 
    //если планируется то ObservableColection

    private IEnumerable<DataPoint> testDataPoints;

    /// <summary>
    /// Тестовый набор данных для визуализации графиков
    /// </summary>
    public IEnumerable<DataPoint> TestDataPoints
    {
        get => testDataPoints;
        set => Set(ref testDataPoints, value);
    }
    #endregion
}