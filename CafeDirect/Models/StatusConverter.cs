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
            if (status == "ready") return "Готов";
        }

        return value as string;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is string)
        {
            string status = (string)value;
            if (status == "Работает") return "active";
            if (status == "Уволен") return "fired";
            if (status == "Новый") return "new";
            if (status == "Готовится") return "preparing";
            if (status == "Оплачен") return "paid";
            if (status == "Отменён") return "canceled";
            if (status == "Готов") return "ready";
        }

        return value as string;
    }
}