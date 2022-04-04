using System;
using System.Windows;
using MVVM_Test2;

namespace MVVM_Test2;

public class CloseAppCmd: BaseCommand
{
    //команда всегда доступна поэтому труе
    public override bool CanExecute(object? parameter) => true;

    public override void Execute(object? parameter) => Application.Current.Shutdown();
}