import random
import time
import numpy as np
import matplotlib.pyplot as plt

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

def main():
    sizes = [int(100 * (10 ** (3 * i / 48))) for i in range(49)]

    RUNS = 5 
    MAX_BUBBLE_SIZE = 5000
    
    all_types = ["best", "worst", "random"]
    algorithms = [
        ("Bubble Sort", Buble_sort),
        ("Optimized Bubble", Buble_sort_optimized),
        ("Sedgewick Shell", Sedgvik)
    ]

    all_results = {t: {name: {'comp': [], 'swap': [], 'time': [], 'valid_sizes': []} for name, _ in algorithms} for t in all_types}

    for current_type in all_types:
        print(f"\n{'='*90}")
        print(f" ТИП ДАНИХ: {current_type.upper()} ".center(90, "="))
        print(f"{'Size':<10} | {'Algorithm':<20} | {'Avg Comp':<15} | {'Avg Swaps':<15} | {'Avg Time (ns)':<20}")
        print("-" * 90)

        for n in sizes:
            run_stats = {name: {'comp': 0, 'swap': 0, 'time': 0} for name, _ in algorithms}
            
            skip_bubble = n > MAX_BUBBLE_SIZE

            for _ in range(RUNS):
                data = generate_data(n, current_type)
                
                for name, func in algorithms:
                    if "Bubble" in name and skip_bubble:
                        continue
                    
                    arr_copy = data.copy()
                    
                    start_time = time.perf_counter_ns()
                    comp, swp = func(arr_copy)
                    end_time = time.perf_counter_ns()
                    
                    run_stats[name]['comp'] += comp
                    run_stats[name]['swap'] += swp
                    run_stats[name]['time'] += (end_time - start_time)

            
            for name, func in algorithms:
                if "Bubble" in name and skip_bubble:
                    
                    print(f"{n:<10} | {name:<20} | {'SKIPPED':<15} | {'SKIPPED':<15} | {'SKIPPED':<20}")
                    continue

                avg_comp = run_stats[name]['comp'] // RUNS
                avg_swp = run_stats[name]['swap'] // RUNS
                avg_time = run_stats[name]['time'] // RUNS

                all_results[current_type][name]['comp'].append(avg_comp)
                all_results[current_type][name]['swap'].append(avg_swp)
                all_results[current_type][name]['time'].append(avg_time)
                all_results[current_type][name]['valid_sizes'].append(n)
                
                print(f"{n:<10} | {name:<20} | {avg_comp:<15,} | {avg_swp:<15,} | {avg_time:<20,}")
            print("-" * 90)

        print(f"\nКоефіцієнти апроксимації для {current_type.upper()}:")
        print(f"{'Algorithm':<20} | {'Metric':<10} | {'a (n^2)':<25} | {'b (n)':<25} | {'c':<25}")
        for name in [alg[0] for alg in algorithms]:
            x = all_results[current_type][name]['valid_sizes']
            if not x: continue
            
            y_comp = all_results[current_type][name]['comp']
            coef_comp = np.polyfit(x, y_comp, 2)
            print(f"{name:<20} | {'Comp':<10} | {coef_comp[0]:<25.5e} | {coef_comp[1]:<25.5e} | {coef_comp[2]:<25.5e}")
            
            y_swp = all_results[current_type][name]['swap']
            coef_swp = np.polyfit(x, y_swp, 2)
            print(f"{name:<20} | {'Swaps':<10} | {coef_swp[0]:<25.5e} | {coef_swp[1]:<25.5e} | {coef_swp[2]:<25.5e}")

        colors = {'Bubble Sort': 'red', 'Optimized Bubble': 'orange', 'Sedgewick Shell': 'blue'}

        # 1. Графік порівнянь
        plt.figure(figsize=(10, 6))
        for name in [alg[0] for alg in algorithms]:
            x = all_results[current_type][name]['valid_sizes']
            if not x: continue
            y_comp = all_results[current_type][name]['comp']
            plt.plot(x, y_comp, marker='.', color=colors[name], label=name)
        
        plt.title(f"Кількість порівнянь - Тип: {current_type.upper()}", fontsize=14)
        plt.xlabel("Розмір масиву (n)", fontsize=12)
        plt.ylabel("Порівняння", fontsize=12)
        plt.xscale('log')
        plt.yscale('log')
        plt.grid(True, which="both", ls="--", alpha=0.5)
        plt.legend(fontsize=12)
        plt.tight_layout()
        plt.show()

        # 2. Графік перестановок
        plt.figure(figsize=(10, 6))
        for name in [alg[0] for alg in algorithms]:
            x = all_results[current_type][name]['valid_sizes']
            if not x: continue
            y_swp = all_results[current_type][name]['swap']
            plt.plot(x, y_swp, marker='.', color=colors[name], label=name)
            
        plt.title(f"Кількість перестановок - Тип: {current_type.upper()}", fontsize=14)
        plt.xlabel("Розмір масиву (n)", fontsize=12)
        plt.ylabel("Перестановки", fontsize=12)
        plt.xscale('log')
        plt.yscale('log')
        plt.grid(True, which="both", ls="--", alpha=0.5)
        plt.legend(fontsize=12)
        plt.tight_layout()
        plt.show()

        # 3. Графік часу виконання
        plt.figure(figsize=(10, 6))
        for name in [alg[0] for alg in algorithms]:
            x = all_results[current_type][name]['valid_sizes']
            if not x: continue
            y_time = all_results[current_type][name]['time']
            plt.plot(x, y_time, marker='.', color=colors[name], label=name)
            
        plt.title(f"Час виконання - Тип: {current_type.upper()}", fontsize=14)
        plt.xlabel("Розмір масиву (n)", fontsize=12)
        plt.ylabel("Час (наносекунди)", fontsize=12)
        plt.xscale('log')
        plt.yscale('log')
        plt.grid(True, which="both", ls="--", alpha=0.5)
        plt.legend(fontsize=12)
        plt.tight_layout()
        plt.show()

if __name__ == "__main__":
    main()