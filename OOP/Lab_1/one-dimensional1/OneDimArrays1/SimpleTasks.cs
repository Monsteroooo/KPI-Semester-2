namespace OneDimArrays1
{
    public static class SimpleTasks
    {
        public static int Task1(int[] array, int a)
        {
            int result = 0;

            foreach (int num in array)
            {
                if (num > a)
                {
                    result += num;
                }
            }

            return result;
         }

        public static int Task2(int[] array)
        {
            int result = 0;

            foreach (int num in array)
            {
                if (num < 0)
                {
                    result += num;
                }
            }

            return result;
         }

        public static int Task3(int[] array)
        {
            int result = 0;

            for (int i = 0; i < array.Length; i++)
            {
                if (i % 2 == 0 && array[i] % 2 == 0)
                {
                    result += array[i];
                }
            }

            return result;
        }
    }
}
