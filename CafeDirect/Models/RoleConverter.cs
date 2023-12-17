using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace CafeDirect.Models;

public class RoleConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is string)
        {
            string role = (string)value;
            if (role == "admin") return "Администратор";
            if (role == "cook") return "Повар";
            if (role == "waiter") return "Официант";
        }

        return value as string;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is string)
        {
            string role = (string)value;
            if (role == "Администратор") return "admin";
            if (role == "Повар") return "cook";
            if (role == "Официант") return "waiter";
        }

        return value as string;
    }
}