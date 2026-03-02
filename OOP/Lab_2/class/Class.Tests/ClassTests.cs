using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ClassDemo;
using NUnit.Framework;

namespace ClassDemo.Tests;

[TestFixture]
public class ClassTests
{
    public const string NameSpase = "ClassDemo";

    // винесено з атрибуту, бо там не працював GetType
    private static readonly object[] AddRectangleTestSource =
    {
        new object[]
        {
            "ArrayRectangles",
            "AddRectangle",
            typeof(bool),
            new Type[] { Type.GetType($"{ClassTests.NameSpase}.Rectangle, Class") },
        },
    };

    [TestCase("Rectangle")]
    [TestCase("ArrayRectangles")]
    public void ClassExists(string className)
    {
        // цей шит просить повторно вказати назву збірки після повної назви класу
        var type = Type.GetType($"{NameSpase}.{className}" + ", Class");

        if (type == null && !type.IsPublic)
        {
            Assert.Fail($"Class '{className}' doesn't exist.");
        }
    }

    [TestCase("Rectangle", typeof(double), typeof(double))]
    [TestCase("Rectangle", typeof(double))]
    [TestCase("Rectangle")]
    [TestCase("ArrayRectangles", typeof(int))]
    [TestCase("ArrayRectangles", typeof(int))]
    public void ClassHasPublicConstructor(string className, params Type[] parameters)
    {
        var type = Type.GetType($"{NameSpase}.{className}" + ", Class");

        if (type == null)
        {
            Assert.Fail($"Class '{className}' doesn't exist.");
        }

        var constructor = type.GetConstructor(parameters);

        if (constructor == null && !constructor.IsPublic)
        {
            Assert.Fail($"Class '{className}' doesn't have public constructor.");
        }
    }

    [TestCase("Rectangle", "sideA")]
    [TestCase("Rectangle", "sideB")]
    [TestCase("ArrayRectangles", "rectangleArray")]
    public void ClassHasPrivateField(string className, string fieldName)
    {
        var type = Type.GetType($"{NameSpase}.{className}" + ", Class");

        if (type == null)
        {
            Assert.Fail($"Class '{className}' doesn't exist.");
        }

        var field = type.GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);

        if (field == null && !field.IsPrivate)
        {
            Assert.Fail($"Class '{className}' doesn't have private field with name '{fieldName}'.");
        }
    }

    [TestCase("Rectangle", "GetSideA", typeof(double))]
    [TestCase("Rectangle", "GetSideB", typeof(double))]
    [TestCase("Rectangle", "Area", typeof(double))]
    [TestCase("Rectangle", "Perimeter", typeof(double))]
    [TestCase("Rectangle", "IsSquare", typeof(bool))]
    [TestCase("Rectangle", "ReplaceSides", typeof(void))]
    [TestCase("ArrayRectangles", "NumberMaxArea", typeof(int))]
    [TestCase("ArrayRectangles", "NumberMinPerimeter", typeof(int))]
    [TestCase("ArrayRectangles", "NumberSquare", typeof(int))]
    [TestCaseSource(nameof(AddRectangleTestSource))]
    public void ClassHasPublicMethodWithCorrectSignature(string className, string methodName, Type returnType, params Type[] parameters)
    {
        var type = Type.GetType($"{NameSpase}.{className}" + ", Class");

        if (type == null)
        {
            Assert.Fail($"Class '{className}' doesn't exist.");
        }

        var method = type.GetMethod(methodName, parameters);

        if (method == null && !method.IsPublic && method.ReturnType != returnType)
        {
            Assert.Fail($"Class '{className}' doesn't have method with name '{methodName}' that returns {returnType}.");
        }
    }

    [TestCase(5, 5)]
    [TestCase(2.28, 1234)]
    [TestCase(13.37, 14.89)]
    public void RectangleGetSideAWithConstructorThatTakes2ParametersReturnsCorrectValue(double sideA, double sideB)
    {
        var expectedSideA = sideA;
        var type = Type.GetType($"{NameSpase}.Rectangle" + ", Class");

        if (type == null)
        {
            Assert.Fail("Class 'Rectangle' doesn't exist.");
        }

        var rectangle = Activator.CreateInstance(type, sideA, sideB);

        var getSideA = type.GetMethod("GetSideA");
        TestHelper.AssertFailIfNull(getSideA, "Method 'GetSideA'");

        if (expectedSideA != (double)getSideA.Invoke(rectangle, null))
        {
            Assert.Fail("Method 'GetSideA' in rectangle that has constructor with 2 arguments works incorrectly.");
        }
    }

    [TestCase(5, 5)]
    [TestCase(2.28, 1234)]
    [TestCase(13.37, 14.89)]
    public void RectangleGetSideBWithConstructorThatTakes2ParametersReturnsCorrectValue(double sideA, double sideB)
    {
        var expectedSideB = sideB;
        var type = Type.GetType($"{NameSpase}.Rectangle" + ", Class");

        if (type == null)
        {
            Assert.Fail("Class 'Rectangle' doesn't exist.");
        }

        var rectangle = Activator.CreateInstance(type, sideA, sideB);

        var getSideB = type.GetMethod("GetSideB");
        TestHelper.AssertFailIfNull(getSideB, "Method 'GetSideB'");

        if (expectedSideB != (double)getSideB.Invoke(rectangle, null))
        {
            Assert.Fail("Method 'GetSideB' in rectangle that has constructor with 2 arguments works incorrectly.");
        }
    }

    [TestCase(5, 5)]
    [TestCase(2.28, 5)]
    [TestCase(13.37, 5)]
    public void RectangleGetSideAWithConstructorThatTakes1ParameterReturnsCorrectValue(double sideA, double sideB)
    {
        var expectedSideA = sideA;

        var type = Type.GetType($"{NameSpase}.Rectangle" + ", Class");

        if (type == null)
        {
            Assert.Fail("Class 'Rectangle' doesn't exist.");
        }

        var rectangle = Activator.CreateInstance(type, sideA);

        var getSideA = type.GetMethod("GetSideA");
        TestHelper.AssertFailIfNull(getSideA, "Method 'GetSideA'");

        if (expectedSideA != (double)getSideA.Invoke(rectangle, null))
        {
            Assert.Fail("Method 'GetSideA' in rectangle that has constructor with 1 argument works incorrectly.");
        }
    }

    [TestCase(5, 5)]
    [TestCase(2.28, 5)]
    [TestCase(13.37, 5)]
    public void RectangleGetSideBWithConstructorThatTakes1ParameterReturnsCorrectValue(double sideA, double sideB)
    {
        var expectedSideB = sideB;

        var type = Type.GetType($"{NameSpase}.Rectangle" + ", Class");

        if (type == null)
        {
            Assert.Fail("Class 'Rectangle' doesn't exist.");
        }

        var rectangle = Activator.CreateInstance(type, sideA);

        var getSideB = type.GetMethod("GetSideB");
        TestHelper.AssertFailIfNull(getSideB, "Method 'GetSideB'");

        if (expectedSideB != (double)getSideB.Invoke(rectangle, null))
        {
            Assert.Fail("Method 'GetSideB' in rectangle that has constructor with 1 argument works incorrectly.");
        }
    }

    [TestCase(4, 3)]
    public void RectangleGetSideAWithConstructorThatTakes0ParametersReturnsCorrectValue(double sideA, double sideB)
    {
        var expectedSideA = sideA;

        var type = Type.GetType($"{NameSpase}.Rectangle" + ", Class");

        if (type == null)
        {
            Assert.Fail("Class 'Rectangle' doesn't exist.");
        }

        var rectangle = Activator.CreateInstance(type);

        var getSideA = type.GetMethod("GetSideA");
        TestHelper.AssertFailIfNull(getSideA, "Method 'GetSideA'");

        if (expectedSideA != (double)getSideA.Invoke(rectangle, null))
        {
            Assert.Fail("Method 'GetSideA' in rectangle that has constructor with 0 arguments works incorrectly.");
        }
    }

    [TestCase(4, 3)]
    public void RectanglegetSideBWithConstructorThatTakes0ParametersReturnsCorrectValue(double sideA, double sideB)
    {
        var expectedSideB = sideB;

        var type = Type.GetType($"{NameSpase}.Rectangle" + ", Class");

        if (type == null)
        {
            Assert.Fail("Class 'Rectangle' doesn't exist.");
        }

        var rectangle = Activator.CreateInstance(type);

        var getSideB = type.GetMethod("GetSideB");
        TestHelper.AssertFailIfNull(getSideB, "Method 'GetSideB'");

        if (expectedSideB != (double)getSideB.Invoke(rectangle, null))
        {
            Assert.Fail("Method 'GetSideB' in rectangle that has constructor with 0 arguments works incorrectly.");
        }
    }

    [TestCase(5, 5, 25)]
    [TestCase(2, 2, 4)]
    [TestCase(0.5, 0.5, 0.25)]
    public void RectangleareaReturnsCorrectValue(double sideA, double sideB, double result)
    {
        var type = Type.GetType($"{NameSpase}.Rectangle" + ", Class");

        if (type == null)
        {
            Assert.Fail("Class 'Rectangle' doesn't exist.");
        }

        var rectangle = Activator.CreateInstance(type, sideA, sideB);

        var area = type.GetMethod("Area");
        TestHelper.AssertFailIfNull(area, "Method 'Area'");

        if (result != (double)area.Invoke(rectangle, null))
        {
            Assert.Fail("Method 'Area' in rectangle works incorrectly.");
        }
    }

    [TestCase(5, 5)]
    [TestCase(1, 2)]
    [TestCase(1, 1)]
    public void RectangleperimeterReturnsCorrectValue(double sideA, double sideB)
    {
        var type = Type.GetType($"{NameSpase}.Rectangle" + ", Class");

        if (type == null)
        {
            Assert.Fail("Class 'Rectangle' doesn't exists.");
        }

        var result = (sideA + sideB) * 2;

        var rectangle = Activator.CreateInstance(type, sideA, sideB);

        var perimeter = type.GetMethod("Perimeter");
        TestHelper.AssertFailIfNull(perimeter, "Method 'Perimeter'");

        if (result != (double)perimeter.Invoke(rectangle, null))
        {
            Assert.Fail("Method 'Perimeter' in rectangle works incorrectly.");
        }
    }

    [TestCase(5, 5, true)]
    [TestCase(2, 2, true)]
    [TestCase(2, 28, false)]
    [TestCase(14, 88, false)]
    public void RectangleisSquareReturnsCorrectValue(double sideA, double sideB, bool result)
    {
        var type = Type.GetType($"{NameSpase}.Rectangle" + ", Class");

        if (type == null)
        {
            Assert.Fail("Class 'Rectangle' doesn't exists.");
        }

        var rectangle = Activator.CreateInstance(type, sideA, sideB);

        var isSquare = type.GetMethod("IsSquare");
        TestHelper.AssertFailIfNull(isSquare, "Method 'IsSquare'");

        if (result != (bool)isSquare.Invoke(rectangle, null))
        {
            Assert.Fail("Method 'IsSquare' in rectangle works incorrectly.");
        }
    }

    [TestCase(1, 5)]
    [TestCase(1, 2)]
    [TestCase(2, 3)]
    public void RectangleReplaceSidesSwitchSides(double sideA, double sideB)
    {
        var type = Type.GetType($"{NameSpase}.Rectangle" + ", Class");

        if (type == null)
        {
            Assert.Fail("Class 'Rectangle' doesn't exist.");
        }

        var rectangle = Activator.CreateInstance(type, sideA, sideB);

        var replaceSides = type.GetMethod("ReplaceSides");
        TestHelper.AssertFailIfNull(replaceSides, "Method 'ReplaceSides'");

        replaceSides.Invoke(rectangle, null);

        var rectangleSideA = type.GetField("sideA", BindingFlags.NonPublic | BindingFlags.Instance);
        var rectangleSideB = type.GetField("sideB", BindingFlags.NonPublic | BindingFlags.Instance);
        if (rectangleSideA != null && rectangleSideA.IsPrivate && rectangleSideB != null &&
            rectangleSideB.IsPrivate && sideA != (double)rectangleSideB.GetValue(rectangle) &&
            sideB != (double)rectangleSideA.GetValue(rectangle))
        {
            Assert.Fail("Method 'ReplaceSides' in rectangle works incorrectly.");
        }
    }

    [TestCase(2)]
    [TestCase(4)]
    [TestCase(10)]
    public void ArrayRectanglesConstructorThatTakesIntAssignsCorrectValueToField(int length)
    {
        var arrRectType = TestHelper.GetArrayRectanglesType();

        var arrayRectangles = TestHelper.GetArrayRectanglesInstance(length);

        var arrayInfo = arrRectType.GetField("rectangleArray", BindingFlags.NonPublic | BindingFlags.Instance);
        TestHelper.AssertFailIfNull(arrayInfo, "Field 'rectangleArray'");

        var objArray = (Array)arrayInfo.GetValue(arrayRectangles);

        if (objArray.Length != length)
        {
            Assert.Fail("Constructor in 'ArrayRectangles' works incorrectly.");
        }
    }

    [Test]
    public void ArrayRectanglesConstructorThatTakesEnumerableOrArrayAssignsCorrectValueToField()
    {
        var arrayRectanglesType = TestHelper.GetArrayRectanglesType();
        var rectangleType = TestHelper.GetRectangleType();

        var arrayType = Array.CreateInstance(rectangleType, 1).GetType();
        var enumerableType = typeof(IEnumerable<>).MakeGenericType(rectangleType);

        var enumerableConstructor = arrayRectanglesType.GetConstructor(new Type[] { enumerableType });
        var arrayConstructor = arrayRectanglesType.GetConstructor(new Type[] { arrayType });

        var arrayInfo = arrayRectanglesType.GetField("rectangleArray", BindingFlags.NonPublic | BindingFlags.Instance);
        TestHelper.AssertFailIfNull(arrayInfo, "Field 'rectangleArray'");

        object arrayRectangles;
        object rectanglesCollection;
        if (arrayConstructor != null)
        {
            var array = Array.CreateInstance(rectangleType, 10);
            arrayRectangles = arrayConstructor.Invoke(new object[] { array });
            rectanglesCollection = array;
        }
        else if (enumerableConstructor != null)
        {
            var enumerable = Activator.CreateInstance(typeof(List<>).MakeGenericType(rectangleType));
            arrayRectangles = enumerableConstructor.Invoke(new object[] { enumerable });
            rectanglesCollection = enumerable;
        }
        else
        {
            Assert.Fail("There is no constructor in 'ArrayRectangles' that takes IEnumerable or array of rectangles");
            return;
        }

        var arrayField = (Array)arrayInfo.GetValue(arrayRectangles);
        var rectanglesCollectionAsArray = (Array)rectanglesCollection;

        for (int i = 0; i < arrayField.Length; i++)
        {
            if (arrayField.GetValue(i) != rectanglesCollectionAsArray.GetValue(i))
            {
                Assert.Fail("Constructor in 'ArrayRectangles' works incorrectly.");
            }
        }
    }

    [TestCase(1)]
    [TestCase(5)]
    [TestCase(7)]
    [TestCase(0)]
    public void ArrayRectanglesAddRectangleAddsRectangleOnFirstFreePlace(int addCount)
    {
        var arrRectType = TestHelper.GetArrayRectanglesType();
        var rectangleType = TestHelper.GetRectangleType();

        var arrayRectangles = TestHelper.GetArrayRectanglesInstance(10);
        var rectangle = TestHelper.GetRectangleInstance();

        var arrayInfo = arrRectType.GetField("rectangleArray", BindingFlags.NonPublic | BindingFlags.Instance);
        TestHelper.AssertFailIfNull(arrayInfo, "Field 'rectangleArray'");

        var method = arrRectType.GetMethod("AddRectangle");
        TestHelper.AssertFailIfNull(method, "Method 'AddRectangle'");

        for (int i = 0; i < addCount; i++)
        {
            method.Invoke(arrayRectangles, new object[] { rectangle });
        }

        var objArray = (Array)arrayInfo.GetValue(arrayRectangles);
        var array = Array.CreateInstance(rectangleType, 10);
        Array.Copy(objArray, array, 10);

        for (int i = 0; i < array.Length; i++)
        {
            if ((i < addCount && array.GetValue(i) == null)
                || (i >= addCount && array.GetValue(i) != null))
            {
                Assert.Fail("Method 'AddRectangle' in ArrayRectangles works incorrectly.");
            }
        }
    }

    [Test]
    public void ArrayRectanglesAddRectangleReturnsTrueIfArrayHasFreePlace()
    {
        var arrRectType = TestHelper.GetArrayRectanglesType();

        var arrayRectangles = TestHelper.GetArrayRectanglesInstance(10);
        var rectangle = TestHelper.GetRectangleInstance();

        var method = arrRectType.GetMethod("AddRectangle");
        TestHelper.AssertFailIfNull(method, "Method 'AddRectangle'");

        var hasFreePlace = (bool)method.Invoke(arrayRectangles, new object[] { rectangle });

        if (!hasFreePlace)
        {
            Assert.Fail("Method 'AddRectangle' in ArrayRectangles works incorrectly.");
        }
    }

    [Test]
    public void ArrayRectanglesAddRectangleReturnsFalseIfArrayHasNotFreePlace()
    {
        var arrRectType = TestHelper.GetArrayRectanglesType();

        var arrayRectangles = TestHelper.GetArrayRectanglesInstance(0);
        var rectangle = TestHelper.GetRectangleInstance();

        var method = arrRectType.GetMethod("AddRectangle");
        TestHelper.AssertFailIfNull(method, "Method 'AddRectangle'");

        var hasFreePlace = (bool)method.Invoke(arrayRectangles, new object[] { rectangle });

        if (hasFreePlace)
        {
            Assert.Fail("Method 'AddRectangle' in ArrayRectangles works incorrectly.");
        }
    }

    [Test]
    public void ArrayRectanglesNumberMaxareaReturnsCorrectValue()
    {
        var arrRectType = TestHelper.GetArrayRectanglesType();

        var addRectangle = arrRectType.GetMethod("AddRectangle");
        TestHelper.AssertFailIfNull(addRectangle, "Method 'AddRectangle'");

        var rectangle1 = TestHelper.GetRectangleInstance(2, 2);
        var rectangle2 = TestHelper.GetRectangleInstance(10, 8);
        var rectangle3 = TestHelper.GetRectangleInstance(5, 5);

        var rectangleArray = arrRectType.GetField("rectangleArray", BindingFlags.NonPublic | BindingFlags.Instance);
        TestHelper.AssertFailIfNull(rectangleArray, "Field 'rectangleArray'");

        var arrayRectangles = TestHelper.GetArrayRectanglesInstance(3);
        addRectangle.Invoke(arrayRectangles, new object[] { rectangle1 });
        addRectangle.Invoke(arrayRectangles, new object[] { rectangle2 });
        addRectangle.Invoke(arrayRectangles, new object[] { rectangle3 });

        var method = arrRectType.GetMethod("NumberMaxArea");
        TestHelper.AssertFailIfNull(method, "Method 'NumberMaxArea'");

        var result = (int)method.Invoke(arrayRectangles, null);

        if (result != 1)
        {
            Assert.Fail("Method 'NumberMaxArea' in ArrayRectangles works incorrectly.");
        }
    }

    [Test]
    public void ArrayRectanglesNumberMinperimeterReturnsCorrectValue()
    {
        var arrRectType = TestHelper.GetArrayRectanglesType();

        var addRectangle = arrRectType.GetMethod("AddRectangle");
        TestHelper.AssertFailIfNull(addRectangle, "Method 'AddRectangle'");

        var rectangle1 = TestHelper.GetRectangleInstance(5, 5);
        var rectangle2 = TestHelper.GetRectangleInstance(10, 8);
        var rectangle3 = TestHelper.GetRectangleInstance(2, 3);

        var rectangleArray = arrRectType.GetField("rectangleArray", BindingFlags.NonPublic | BindingFlags.Instance);
        TestHelper.AssertFailIfNull(rectangleArray, "Field 'rectangleArray'");

        var arrayRectangles = TestHelper.GetArrayRectanglesInstance(3);
        addRectangle.Invoke(arrayRectangles, new object[] { rectangle1 });
        addRectangle.Invoke(arrayRectangles, new object[] { rectangle2 });
        addRectangle.Invoke(arrayRectangles, new object[] { rectangle3 });

        var method = arrRectType.GetMethod("NumberMinPerimeter");
        TestHelper.AssertFailIfNull(method, "Method 'NumberMinPerimeter'");

        var result = (int)method.Invoke(arrayRectangles, null);

        if (result != 2)
        {
            Assert.Fail("Method 'NumberMinPerimeter' in ArrayRectangles works incorrectly.");
        }
    }

    [Test]
    public void ArrayRectanglesNumberSquareReturnsCorrectValue()
    {
        var arrRectType = TestHelper.GetArrayRectanglesType();

        var addRectangle = arrRectType.GetMethod("AddRectangle");
        TestHelper.AssertFailIfNull(addRectangle, "Method 'AddRectangle'");

        var rectangle1 = TestHelper.GetRectangleInstance(2, 3);
        var rectangle2 = TestHelper.GetRectangleInstance(10, 8);
        var rectangle3 = TestHelper.GetRectangleInstance(5, 5);
        var rectangle4 = TestHelper.GetRectangleInstance(8, 8);
        var rectangle5 = TestHelper.GetRectangleInstance(4, 4);
        var rectangle6 = TestHelper.GetRectangleInstance(5, 5);

        var arrayRectangles = TestHelper.GetArrayRectanglesInstance(6);
        addRectangle.Invoke(arrayRectangles, new object[] { rectangle1 });
        addRectangle.Invoke(arrayRectangles, new object[] { rectangle2 });
        addRectangle.Invoke(arrayRectangles, new object[] { rectangle3 });
        addRectangle.Invoke(arrayRectangles, new object[] { rectangle4 });
        addRectangle.Invoke(arrayRectangles, new object[] { rectangle5 });
        addRectangle.Invoke(arrayRectangles, new object[] { rectangle6 });

        var method = arrRectType.GetMethod("NumberSquare");
        TestHelper.AssertFailIfNull(method, "Method 'NumberSquare'");

        var result = (int)method.Invoke(arrayRectangles, null);

        if (result != 4)
        {
            Assert.Fail("Method 'NumberSquare' in ArrayRectangles works incorrectly.");
        }
    }
}