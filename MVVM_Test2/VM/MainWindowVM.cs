using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
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

        ChangeTab = new ActionCommand(OnChangeTabExecuted, CanChangeTabExecuted);

        #endregion

        #region График

        var dataPoints = new List<DataPoint>((int) (360 / 0.1));
        for (double x = 0; x < 360; x += 0.1)
        {
            const double TO_RADIUS = Math.PI / 180;
            var y = Math.Sin(x * TO_RADIUS);

            dataPoints.Add(new DataPoint() {XValue = x, YValue = y});
        }

        // var dataPoints = new List<DataPoint>();
        //
        //     
        // dataPoints.Add(new DataPoint(){XValue = 1,YValue = 2});
        // dataPoints.Add(new DataPoint(){XValue = 19,YValue = 20});

        TestDataPoints = dataPoints;

        #endregion

        var studentIndex = 1;
        var students = Enumerable.Range(1, 10).Select(i => new Student()
        {
            Name = $"Name {studentIndex}",
            Surname = $"Surname {studentIndex}",
            Patronymic = $"Patronymic {studentIndex++}",
            Birthday = DateTime.Now,
            Rating = 0,
            Description = $"Описание {studentIndex}"
        });

        //если добавлять по одной группе в ObservableCollection то это будет долго на каждую группу он будет 
        //вызыывать у себя систему событий что будет сильно тормозить у всей системв работу

        //созазим ппречислне целых чисел от 1 до 20 (Enumerable.Range(1, 20)), и дальше проведем преобразование
        //возьмеме это число из текущего перечисления и на его основе создади групппу
        var groups = Enumerable.Range(1, 20).Select(i => new Group()
        {
            Name = $"Group {i}",
            Students = new ObservableCollection<Student>(students)
        });

        //дальше разместим колллекцию в observable тем самым оно создаство гораздо быстрее
        Groups = new ObservableCollection<Group>(groups);


        var dataList = new List<object>();

        //создаем разнордный набор данных
        var group = Groups[1];

        dataList.Add("Hello");
        dataList.Add(12);
        dataList.Add(group);
        dataList.Add(group.Students[0]);

        this.CompositeCollection = dataList.ToArray();
       
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

    public ICommand ChangeTab { get; }

    void OnChangeTabExecuted(object p)
    {
        if (p == null)
        {
            return;
        }

        SelectedPageIndex += Convert.ToInt32(p);
    }

    bool CanChangeTabExecuted(object p) => SelectedPageIndex >= 0;

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


    /// <summary>
    /// Сатус Ползунка
    /// </summary>
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

    #region Точки графика

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

    private int selectedPageIndex = 2;

    /// <summary>
    /// ВЫбранная вкладка
    /// </summary>
    public int SelectedPageIndex
    {
        get => selectedPageIndex;

        set
        {
            if (value < 0)
            {
                value = 3;
            }

            if (value > 3)
            {
                value = 0;
            }

            Set(ref selectedPageIndex, value);
        }
    }


    #region Группы и студенты

    /// <summary>
    /// Список групп
    /// </summary>
    public ObservableCollection<Group> Groups { get; set; }

    #region Выбранная группа

    private Group selecGroup;

    /// <summary>
    /// ВЫбранная группа
    /// </summary>
    public Group SelectGroup
    {
        get => selecGroup;
        set => Set(ref selecGroup, value);
    }

    #endregion

    #endregion

    #region Разнородныый набор данных

    //создадим список любых эл
    public object[] CompositeCollection { get; }

    
    private object selectedCompositeValue;

    /// <summary>
    /// выбранный разнорднный элемент
    /// </summary>
    public object SelectedCompositeValue
    {
        get => selectedCompositeValue;
        set => Set(ref selectedCompositeValue, value);
    }

    #endregion

    #endregion
}