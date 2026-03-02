using NUnit.Framework;

namespace ClassesDemo.Tests;

internal static class TestHelper
{
    public static Type GetRectangleType()
    {
        var type = Type.GetType("ClassesDemo.Rectangle, ClassesDemo");

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
        var type = Type.GetType("ClassesDemo.ArrayRectangles, ClassesDemo");

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

    public static void AssertFailIfNull(object? obj, string message)
    {
        if (obj == null)
        {
            Assert.Fail($"{message} doesn't exist");
        }
    }
}
