﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using DrectSoft.Library.EmrEditor.Src.Document;
/// <summary>
/// 使属性值显示为空白，已经用于图片，颜色
/// </summary>
internal class BlankConverter : TypeConverter
{
    public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
    {
        return true;
    }
    public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
    {
        if (value is string[])
        {
            return "共" + (value as string[]).Length + "项";
        }
        if (value is List<ZYSelectableElementItem>)
        {
            return "共" + (value as List<ZYSelectableElementItem>).Count + "项";
        }
        if (value is bool)
        {
            if ((bool)value)
                return "是";
            else
                return "否";
        }
        if (value is Color)
        {

        }

        return "请设置";
    }

}