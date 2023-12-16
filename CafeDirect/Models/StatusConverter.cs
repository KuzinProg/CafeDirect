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
            string status = (string)value;
            if (status == "active") return "Работает";
            if (status == "fired") return "Уволен";
            if (status == "new") return "Новый";
            if (status == "preparing") return "Готовится";
            if (status == "paid") return "Оплачен";
            if (status == "canceled") return "Отменён";
        }

        return value as string;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return null;
    }
}