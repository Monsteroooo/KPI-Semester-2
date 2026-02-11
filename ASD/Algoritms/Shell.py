"""
Сортування Шелла (Shell Sort)

Реалізація класичного алгоритму Шелла та версій з різними послідовностями кроків (Кнут, Седжвік)
"""


def shell_sort_classic(arr):
    """Класичне сортування Шелла з послідовністю n/2, n/4, n/8, ..., 1.

    Часова складність: O(n²) у гіршому випадку
    Просторова складність: O(1)
    """
    n = len(arr)
    h = n // 2  # Початковий крок (gap)

    while h > 0:
        # Сортування елементів з кроком h
        for i in range(h, n):
            temp = arr[i]
            j = i

            # Порівняння елементів на відстані h
            while j >= h and arr[j - h] > temp:
                arr[j] = arr[j - h]
                j -= h

            arr[j] = temp

        h //= 2  # Зменшуємо крок у 2 рази

    return arr


def shell_sort_knuth(arr):
    """Сортування Шелла з послідовністю Кнута: h = 3*h + 1.

    Часова складність: O(n^(3/2)) у гіршому випадку
    Просторова складність: O(1)
    """
    n = len(arr)

    # Обчислюємо максимальний h для даного розміру масиву
    h = 1
    while h <= n // 3:
        h = 3 * h + 1

    # h = 1, 4, 13, 40, 121, 364, 1093, ...

    while h > 0:
        # Сортування елементів з кроком h
        for i in range(h, n):
            temp = arr[i]
            j = i

            while j >= h and arr[j - h] > temp:
                arr[j] = arr[j - h]
                j -= h

            arr[j] = temp

        h //= 3

    return arr


def shell_sort_sedgewick(arr):
    """Сортування Шелла з послідовністю Седжвіка.

    Послідовність Седжвіка обирається так, щоб зменшити кількість порівнянь
    і уникнути «поганих» інтерлівів між суміжними кроками.
    """
    n = len(arr)

    # Генеруємо послідовність Седжвіка
    gaps = []
    k = 0
    while True:
        if k % 2 == 0:  # k парне
            gap = 9 * (2 ** k) - 9 * (2 ** (k // 2)) + 1
        else:  # k непарне
            gap = 8 * (2 ** k) - 6 * (2 ** ((k + 1) // 2)) + 1

        if gap > n:
            break
        gaps.append(gap)
        k += 1

    # Йдемо по послідовності у зворотному порядку (від більших до менших)
    gaps.reverse()

    for h in gaps:
        # Сортування елементів з кроком h
        for i in range(h, n):
            temp = arr[i]
            j = i

            while j >= h and arr[j - h] > temp:
                arr[j] = arr[j - h]
                j -= h

            arr[j] = temp

    return arr


def shell_sort_sedgewick_alt(arr):
    """Альтернативна (простiша) версія послідовності Седжвіка.

    Використовує попередньо обчислені значення послідовності для практичних розмірів.
    """
    n = len(arr)

    # Попередньо обчислені значення послідовності Седжвіка
    sedgewick_sequence = [1, 5, 19, 41, 109, 209, 505, 929, 2161, 3905, 8929, 16001, 36289]

    # Знаходимо початкові значення gap для нашого розміру масиву
    gaps = [g for g in sedgewick_sequence if g < n]
    gaps.reverse()  # Йдемо від більших до менших

    for h in gaps:
        # Сортування елементів з кроком h
        for i in range(h, n):
            temp = arr[i]
            j = i

            while j >= h and arr[j - h] > temp:
                arr[j] = arr[j - h]
                j -= h

            arr[j] = temp

    return arr


# ============================================================================
# ПОЯСНЕННЯ ВІДМІН
# ============================================================================

"""
Класичне сортування Шелла vs Седжвік:

1. Класичне (h = n/2, n/4, n/8, ..., 1):
   - Проста логіка, але в гіршому випадку O(n²)

2. Послідовність Кнута (h = 3*h + 1):
   - Краща у багатьох випадках, дає приблизно O(n^(3/2))

3. Послідовність Седжвіка:
   - Складніша формула, але значно краща на практиці (приблизно O(n^(4/3)))

Усі версії використовують однаковий базовий алгоритм (сортування вставками з кроком h),
але вибір послідовності кроків впливає на кількість порівнянь і практичну швидкодію.
"""


if __name__ == "__main__":
    # Приклад використання
    test_arrays = [
        [64, 34, 25, 12, 22, 11, 90],
        [5, 2, 8, 1, 9, 3, 7, 4, 6],
        list(range(20, 0, -1)),  # Відсортований у зворотному порядку
    ]

    print("=" * 70)
    print("СРАВНЕНИЕ МЕТОДОВ СОРТИРОВАНИЯ ШЕЛЛА")
    print("=" * 70)

    for idx, arr in enumerate(test_arrays, 1):
        print(f"\nТест {idx}: {arr}")

        # Класичне
        result1 = shell_sort_classic(arr.copy())
        print(f"Классическое:        {result1}")

        # Кнута
        result2 = shell_sort_knuth(arr.copy())
        print(f"Кнута:               {result2}")

        # Седжвік (формула)
        result3 = shell_sort_sedgewick(arr.copy())
        print(f"Седжвик (формула):   {result3}")

        # Седжвік (альтернативна)
        result4 = shell_sort_sedgewick_alt(arr.copy())
        print(f"Седжвик (таблица):   {result4}")

        # Перевірка
        assert result1 == result2 == result3 == result4
        print("✓ Все методы дают одинаковый результат")

    # Продуктивність
    print("\n" + "=" * 70)
    print("СРАВНЕНИЕ ПРОИЗВОДИТЕЛЬНОСТИ")
    print("=" * 70)

    import time
    import random

    sizes = [100, 1000, 10000]

    for size in sizes:
        arr = [random.randint(1, 10000) for _ in range(size)]
        print(f"\nМассив размером {size} элементов:")

        # Класичне
        start = time.time()
        shell_sort_classic(arr.copy())
        classic_time = time.time() - start
        print(f"  Классическое:      {classic_time*1000:.4f} мс")

        # Кнута
        start = time.time()
        shell_sort_knuth(arr.copy())
        knuth_time = time.time() - start
        print(f"  Кнута:             {knuth_time*1000:.4f} мс")

        # Седжвік
        start = time.time()
        shell_sort_sedgewick(arr.copy())
        sedgewick_time = time.time() - start
        print(f"  Седжвик:           {sedgewick_time*1000:.4f} мс")
