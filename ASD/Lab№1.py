import random
import sys
import time
import matplotlib.pyplot as plt

#ФУНКЦІЇ
def generate_data(n, gen_type="random"):
    if gen_type == "best":
        return list(range(n))
    elif gen_type == "worst":
        return list(range(n, 0, -1))
    else:
        return [random.randint(1, 1000000) for _ in range(n)]

def Sedgvik(arr):
    comparisons = 0
    swaps = 0
    n = len(arr)
    gaps = []
    k = 0
    while True:
        if k % 2 == 0:
            gap = 9 * (2 ** k) - 9 * (2 ** (k // 2)) + 1
        else:
            gap = 8 * (2 ** k) - 6 * (2 ** ((k + 1) // 2)) + 1
        if gap >= n: break
        gaps.append(gap)
        k += 1
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

#ГОЛОВНА ЧАСТИНА
def main():
    sizes = [10, 100, 1000, 5000, 10000, 20000]
    # Список усіх типів, які потрібно перевірити
    all_types = ["random", "worst", "best"] 
    
    algorithms = [
        ("Bubble Sort", Buble_sort),
        ("Optimized Bubble", Buble_sort_optimized),
        ("Sedgewick Shell", Sedgvik)
    ]

    #ГОЛОВНИЙ ЦИКЛ ПО ТИПАХ ДАНИХ
    for current_type in all_types:
        
        # Очищаємо результати перед новим типом
        results = {name: [] for name, _ in algorithms}
        # Виводимо заголовок для поточного типу даних
        print(f"\n{'='*70}")
        print(f" ТИП ДАННИХ: {current_type.upper()} ".center(70, "="))
        print(f"{'Size':<10} | {'Algorithm':<20} | {'Comparisons':<15} | {'Swaps':<15}")
        print("-" * 70)
        # Генеруємо тести та вимірюємо алгоритми для кожного розміру
        for n in sizes:
            data = generate_data(n, current_type)
            # Вимірюємо кожен алгоритм для поточного набору даних
            for name, func in algorithms:
                # Пропускаємо повільний "пузирковий" для великих даних
                # У BEST випадку Bubble швидкий і його можна не пропускати,
                # але для WORST і RANDOM на 20k він може зависнути.
                if n > 10000 and "Bubble" in name and current_type != "best":
                    results[name].append(None)
                    print(f"{n:<10} | {name:<20} | {'SKIPPED':<15} | {'-'}")
                    continue
                # Копіюємо дані, щоб кожен алгоритм працював з однаковим набором
                arr_copy = data.copy()
                comp, swp = func(arr_copy)
                # Зберігаємо результати для графіка
                results[name].append(comp)
                
                print(f"{n:<10} | {name:<20} | {comp:<15,} | {swp:<15,}")
            
            print("-" * 70)

        #ПОБУДОВА ГРАФІКА ДЛЯ ПОТОЧНОГО ТИПУ
        print(f"\n>> Побудова графіка для {current_type.upper()}...")
        print(">> Закрийте вікно з графіками щоб продовжити!")
        # Побудова площини для графіків
        plt.figure(figsize=(10, 6))
        colors = {'Bubble Sort': 'red', 'Optimized Bubble': 'orange', 'Sedgewick Shell': 'blue'}
        
        for name, data_points in results.items():
            # Проста (звичайна ітерація)
            valid_sizes = []  #Створюємо порожні списки 
            valid_data = []
            #Проходимося по парах (розмір, дані)
            for s, d in zip(sizes, data_points):
                #Перевіряємо умову
                if d is not None:
                    #Додаємо в списки
                    valid_sizes.append(s)
                    valid_data.append(d)
            
            if valid_data:
                plt.plot(valid_sizes, valid_data, marker='o', color=colors.get(name), label=f"Exp: {name}")

        # Малюємо теоретичні криві для порівняння
        if current_type != "best":
            theory_n2 = [0.2 * (x**2) for x in sizes] 
            plt.plot(sizes, theory_n2, 'k--', alpha=0.4, label="Theory: O(n^2)")
            
            theory_shell = [1.5 * (x**(4/3)) for x in sizes]
            plt.plot(sizes, theory_shell, 'g--', alpha=0.6, label="Theory: O(n^(4/3))")

        plt.title(f"Порівняння (Comparisons) - {current_type.upper()}")
        plt.xlabel("Size (N)")
        plt.ylabel("Comparisons")
        plt.legend()
        plt.grid(True, which="both", ls="-", alpha=0.5)
        plt.xscale('log')
        plt.yscale('log')
        
        # Програма зупиниться тут, поки ти не закриєш вікно
        plt.show()

if __name__ == "__main__":
    sys.setrecursionlimit(100000)
    main()