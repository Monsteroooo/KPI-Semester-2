using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ClassDemo.Tests;

internal static class TestHelper
{
    public static Type GetRectangleType()
    {
        var type = Type.GetType("ClassDemo.Rectangle" + ", Class");

        AssertFailIfNull(type, "Class 'Rectangle'");

        return type;
    }

    public static object GetRectangleInstance(params object[] args)
    {
        var type = GetRectangleType();

        var rectangle = Activator.CreateInstance(type, args);

        AssertFailIfNull(rectangle, "Class 'Rectangle'");

        return rectangle;
    }

    public static Type GetArrayRectanglesType()
    {
        var type = Type.GetType("ClassDemo.ArrayRectangles" + ", Class");

        AssertFailIfNull(type, "Class 'ArrayRectangles'");

        return type;
    }

    public static object GetArrayRectanglesInstance(params object[] args)
    {
        var type = GetArrayRectanglesType();

        var rectangle = Activator.CreateInstance(type, args);

        AssertFailIfNull(rectangle, "Class 'ArrayRectangles'");

        return rectangle;
    }

    public static void AssertFailIfNull(object obj, string message)
    {
        if (obj == null)
        {
            Assert.Fail($"{message} doesn't exist");
        }
    }
}
