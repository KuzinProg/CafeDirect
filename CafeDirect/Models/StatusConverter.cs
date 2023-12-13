using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace CafeDirect.Models;

public class StatusConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is string)
        {
            string role = (string)value;
            if (role == "active") return "Работает";
            if (role == "fired") return "Уволен";
        }

        return value as string;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}