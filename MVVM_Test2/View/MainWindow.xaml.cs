using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MVVM_Test2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Фильтрует элменты если они пройдут все проверки то убираются из списка (те испольлзуется ленивая
        /// логика)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">сам элмент Item который подвергается фильтрации, на каждом момене работы
        /// CollectionViewSource - каждый раз вызвает это событие </param>
        private void GroupsCollection_OnFilter(object sender, FilterEventArgs e)
        {
            //если обьект не является группой то ничего не делаем
            if (!(e.Item is Group g)) return;
            // если в группе не задано имя или оно пустое то ничего не делаем
            if (g.Name is null || string.IsNullOrWhiteSpace(g.Name)) return;

            //вытаскиваем текст из Textbox GroupSearch
            var filterText = GroupSearch.Text;
            if (filterText.Length == 0) return;

            //если имя группы содержит искомый текст то ничего недлеаем (ленивая логика)
            if (g.Name.Contains(filterText, StringComparison.OrdinalIgnoreCase)) return;

            //если описание группы сущетвует (!= null) и при этом содержит искомый текст то ничего недлеаем (ленивая логика)
            if (g.Description != null && g.Description.Contains(GroupSearch.Text, StringComparison.OrdinalIgnoreCase)) return;

            //если эл прошел все проверки то он не проходит фильтр;
            e.Accepted = false;
        }

        /// <summary>
        /// Будет вызыватся каждый раз когда изменяется текст в текстбоксе в котром он записан в виде
        /// TextChanged="OnGroupsFilterTextChanged"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnGroupsFilterTextChanged(object sender, TextChangedEventArgs e)
        {
            //получаем TextBox из обьекта исчточника из sender
            var textBox = (TextBox) sender;
            
            // У поулченного TextBox вытаскиваем CollectionViewSource отвечающий за фильтарцию эл
            var collection = (CollectionViewSource)textBox.FindResource("GroupsCollection");
            
            // обновляем этот эл для отображения результатов
            collection.View.Refresh();
        }
    }
}