# Classes Demo

Develop **Rectangle** and **ArrayRectangles** with predefined functionality.

Implement **Rectangle** class with the following members:

- Two private fields of type `double`: **sideA** and **sideB** (sides A and B of the rectangle).
- Constructor with two parameters **a** and **b** of type `double` (set rectangle sides).
- Constructor with one parameter **a** of type `double` (side A; side B is 5).
- Parameterless constructor (side A = 4, side B = 3).
- Method **GetSideA** — returns side A.
- Method **GetSideB** — returns side B.
- Method **Area** — calculates and returns the area.
- Method **Perimeter** — calculates and returns the perimeter.
- Method **IsSquare** — returns `true` if the rectangle is a square, otherwise `false`.
- Method **ReplaceSides** — swaps sides A and B.


Implement class **ArrayRectangles** with the following members:

- Private field **rectangleArray** — array of **Rectangle**.
- Constructor **ArrayRectangles(int n)** — creates an array of length **n** (empty slots are `null`).
- Constructor **ArrayRectangles(Rectangle[] rectangles)** — takes an array of **Rectangle** (uses it as the internal array).
- Method **AddRectangle(Rectangle rectangle)** — adds a rectangle into the first free slot (`null`); returns `true` if added, `false` if there is no free space.
- Method **NumberMaxArea()** — returns the index of the rectangle with the maximum area (zero-based).
- Method **NumberMinPerimeter()** — returns the index of the rectangle with the minimum perimeter (zero-based).
- Method **NumberSquare()** — returns the count of squares in the array (rectangles where **IsSquare()** is `true`).
