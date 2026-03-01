def Sedgvik(arr):
    comparisons = 0
    swaps = 0
    n = len(arr)
    gaps = []
    k = 0
    gap = 1 
    
    while gap < n:
        gaps.append(gap)
        k += 1
        if k % 2 == 0:
            gap = 9 * (2 ** k) - 9 * (2 ** (k // 2)) + 1
        else:
            gap = 8 * (2 ** k) - 6 * (2 ** ((k + 1) // 2)) + 1

    gaps.reverse()
    
    for h in gaps:
        for i in range(h, n):
            temp = arr[i]
            j = i
            while j >= h and arr[j-h] > temp:
                comparisons += 1
                arr[j] = arr[j-h]
                j -= h
            if j >= h and arr[j-h] <= temp: comparisons += 1
            arr[j] = temp
            swaps += 1
            
    return comparisons, swaps

def Buble_sort(arr):
    comparisons = 0
    swaps = 0
    n = len(arr)
    for i in range(n):
        for j in range(0, n-i-1):
            comparisons += 1
            if arr[j] > arr[j+1]:
                arr[j], arr[j+1] = arr[j+1], arr[j]
                swaps += 1
    return comparisons, swaps

def Buble_sort_optimized(arr):
    comparisons = 0
    swaps = 0
    n = len(arr)
    for i in range(n):
        swapped = False
        for j in range(0, n-i-1):
            comparisons += 1
            if arr[j] > arr[j+1]:
                arr[j], arr[j+1] = arr[j+1], arr[j]
                swaps += 1
                swapped = True
        if not swapped: break
    return comparisons, swaps

def run_tests():

    test_cases = [
        {"name": "Порожній масив", "input": [], "expected": []},
        {"name": "Один елемент", "input": [42], "expected": [42]},
        {"name": "Впорядкований", "input": [1, 2, 3, 4, 5], "expected": [1, 2, 3, 4, 5]},
        {"name": "Зворотний", "input": [5, 4, 3, 2, 1], "expected": [1, 2, 3, 4, 5]},
        {"name": "Випадковий", "input": [64, 34, 25, 12, 22, 11, 90], "expected": [11, 12, 22, 25, 34, 64, 90]},
        {"name": "Дублікати", "input": [3, 1, 4, 1, 5, 9, 2, 6, 5, 3], "expected": [1, 1, 2, 3, 3, 4, 5, 5, 6, 9]},
        {"name": "Усі рівні", "input": [7, 7, 7, 7, 7], "expected": [7, 7, 7, 7, 7]},
    ]

    algorithms = [
        ("Bubble Sort", Buble_sort),
        ("Optimized Bubble", Buble_sort_optimized),
        ("Sedgewick Shell", Sedgvik)
    ]

    print(f"{'Назва тесту':<18} | {'Алгоритм':<20} | {'Результат'}")
    print("-" * 55)

    all_passed = True

    for test in test_cases:
        for alg_name, func in algorithms:
            # Робимо копію масиву, бо алгоритми сортують його in-place (змінюють оригінал)
            arr_copy = test["input"].copy()
            
            # Запускаємо сортування (нас зараз не цікавлять повернені порівняння та перестановки)
            func(arr_copy)
            
            # Перевіряємо, чи збігається відсортований масив з очікуваним
            if arr_copy == test["expected"]:
                result = "PASSED"
            else:
                result = f"FAILED (Отримано: {arr_copy})"
                all_passed = False
                
            print(f"{test['name']:<18} | {alg_name:<20} | {result}")
        print("-" * 55)

    print("\nЗАГАЛЬНИЙ РЕЗУЛЬТАТ:")
    if all_passed:
        print("Усі алгоритми успішно пройшли всі тести! Можете сміливо писати це у звіт.")
    else:
        print("Знайдено помилки у сортуванні.")

if __name__ == "__main__":
    run_tests()