using System;
using Avalonia.Controls;
using CafeDirect.Context;
using CafeDirect.Models;
using Menu = CafeDirect.Models.Menu;

namespace CafeDirect.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public string Greeting => Convert.ToString(Test())+"IDCON";

    private int Test()
    {
        DataBaseContext db = new DataBaseContext();
        Menu menu1 = new Menu { Name = "Egor" , Price = 600 };
        Menu menu2 = new Menu { Name = "Marie" , Price = 700 };
        db.Menus.AddRange(menu1,menu2);
        db.SaveChangesAsync();
        return 101;
    }
}