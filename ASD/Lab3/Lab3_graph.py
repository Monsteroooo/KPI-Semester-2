import random
import sys
import numpy as np
import matplotlib.pyplot as plt

sys.setrecursionlimit(200000)

def generate_data(n, gen_type="random"):
    if gen_type == "best":
        return list(range(n))
    elif gen_type == "worst":
        return list(range(n, 0, -1))
    else:
        return [random.randint(1, 1000000) for _ in range(n)]

def Quick_sort(arr):
    comp = [0]

    def partition(p, r):
        x = arr[r]
        i = p - 1
        for j in range(p, r):
            comp[0] += 1
            if arr[j] <= x:
                i += 1
                arr[i], arr[j] = arr[j], arr[i]
        arr[i + 1], arr[r] = arr[r], arr[i + 1]
        return i + 1

    def qs(p, r):
        if p < r:
            q = partition(p, r)
            qs(p, q - 1)
            qs(q + 1, r)

    qs(0, len(arr) - 1)
    return comp[0]

def Quick_Sort_Mediany(arr):
    comp = [0]

    def sort_small(p, r):
        length = r - p + 1
        if length == 2:
            comp[0] += 1
            if arr[p] > arr[r]:
                arr[p], arr[r] = arr[r], arr[p]
        elif length == 3:
            comp[0] += 1
            if arr[p] > arr[p+1]:
                arr[p], arr[p+1] = arr[p+1], arr[p]
            comp[0] += 1
            if arr[p+1] > arr[r]:
                arr[p+1], arr[r] = arr[r], arr[p+1]
            comp[0] += 1
            if arr[p] > arr[p+1]:
                arr[p], arr[p+1] = arr[p+1], arr[p]

    def get_median_idx(p, r):
        mid = (p + r) // 2
        a, b, c = arr[p], arr[mid], arr[r]
        if (a - b) * (c - a) >= 0: return p
        elif (b - a) * (c - b) >= 0: return mid
        else: return r

    def partition(p, r):
        med_idx = get_median_idx(p, r)
        arr[med_idx], arr[r] = arr[r], arr[med_idx]
        x = arr[r]
        i = p - 1
        for j in range(p, r):
            comp[0] += 1
            if arr[j] <= x:
                i += 1
                arr[i], arr[j] = arr[j], arr[i]
        arr[i + 1], arr[r] = arr[r], arr[i + 1]
        return i + 1

    def qs(p, r):
        if r - p + 1 <= 3:
            sort_small(p, r)
        elif p < r:
            q = partition(p, r)
            qs(p, q - 1)
            qs(q + 1, r)

    qs(0, len(arr) - 1)
    return comp[0]

def Quick_sort_3_pivots(arr):
    comp = [0]

    def sort_small(p, r):
        length = r - p + 1
        if length == 2:
            comp[0] += 1
            if arr[p] > arr[r]:
                arr[p], arr[r] = arr[r], arr[p]
        elif length == 3:
            comp[0] += 1
            if arr[p] > arr[p+1]:
                arr[p], arr[p+1] = arr[p+1], arr[p]
            comp[0] += 1
            if arr[p+1] > arr[r]:
                arr[p+1], arr[r] = arr[r], arr[p+1]
            comp[0] += 1
            if arr[p] > arr[p+1]:
                arr[p], arr[p+1] = arr[p+1], arr[p]

    def partition3(p, r):
        if arr[p] > arr[p+1]: arr[p], arr[p+1] = arr[p+1], arr[p]
        if arr[p+1] > arr[r]: arr[p+1], arr[r] = arr[r], arr[p+1]
        if arr[p] > arr[p+1]: arr[p], arr[p+1] = arr[p+1], arr[p]

        q1, q2, q3 = arr[p], arr[p+1], arr[r]
        
        a = p + 2
        b = p + 2
        c = r - 1
        d = r - 1

        while b <= c:
            comp[0] += 1
            if arr[b] < q2:
                comp[0] += 1
                if arr[b] < q1:
                    arr[a], arr[b] = arr[b], arr[a]
                    a += 1
                b += 1
            else:
                comp[0] += 1
                if arr[b] > q2:
                    while arr[c] > q2 and b < c:
                        comp[0] += 1
                        if arr[c] > q3:
                            arr[c], arr[d] = arr[d], arr[c]
                            d -= 1
                        c -= 1
                    comp[0] += 1 
                    arr[b], arr[c] = arr[c], arr[b]
                    comp[0] += 1
                    if arr[b] < q1:
                        arr[a], arr[b] = arr[b], arr[a]
                        a += 1
                    c -= 1
                b += 1
        
        a -= 1; b -= 1; c += 1; d += 1
        arr[p+1], arr[a] = arr[a], arr[p+1]
        arr[a], arr[b] = arr[b], arr[a]; a -= 1
        arr[p], arr[a] = arr[a], arr[p]
        arr[r], arr[d] = arr[d], arr[r]
        arr[d], arr[c] = arr[c], arr[d]

        return a, b, c

    def qs(p, r):
        if r - p + 1 <= 3:
            sort_small(p, r)
        elif p < r:
            p1, p2, p3 = partition3(p, r)
            qs(p, p1 - 1)
            qs(p1 + 1, p2 - 1)
            qs(p2 + 1, p3 - 1)
            qs(p3 + 1, r)

    qs(0, len(arr) - 1)
    return comp[0]

def main():
    base_sizes = [int(100 * (10 ** (3 * i / 48))) for i in range(49)]
    sizes = [s for s in base_sizes if s <= 10000]
    
    for s in range(15000, 35000, 5000):
        sizes.append(s)
        
    # Змінено верхню межу з 100001 на 50001
    for s in range(40000, 50001, 10000):
        sizes.append(s)

    sizes = sorted(list(set(sizes)))
    
    RUNS = 3 
    
    all_types = ["best", "worst", "random"]
    algorithms = [
        ("Quick Sort 1", Quick_sort),
        ("Quick Sort 2 (Med)", Quick_Sort_Mediany),
        ("Quick Sort 3 (Pivots)", Quick_sort_3_pivots)
    ]

    all_results = {t: {name: {'comp': [], 'valid_sizes': []} for name, _ in algorithms} for t in all_types}

    for current_type in all_types:
        print(f"\n{'='*60}")
        print(f" ТИП ДАНИХ: {current_type.upper()} ".center(60, "="))
        print(f"{'Size':<10} | {'Algorithm':<22} | {'Avg Comp':<15}")
        print("-" * 60)

        for n in sizes:
            run_stats = {name: {'comp': 0} for name, _ in algorithms}
            
            for _ in range(RUNS):
                data = generate_data(n, current_type)
                
                for name, func in algorithms:
                    arr_copy = data.copy()
                    
                    try:
                        comp = func(arr_copy)
                    except RecursionError:
                        comp = -1
                    
                    if comp != -1:
                        run_stats[name]['comp'] += comp

            for name, func in algorithms:
                if run_stats[name]['comp'] == 0 and RUNS > 0:
                    print(f"{n:<10} | {name:<22} | {'RecursionError':<15}")
                    continue
                    
                avg_comp = run_stats[name]['comp'] // RUNS

                all_results[current_type][name]['comp'].append(avg_comp)
                all_results[current_type][name]['valid_sizes'].append(n)
                
                print(f"{n:<10} | {name:<22} | {avg_comp:<15,}")
            print("-" * 60)

        colors = {'Quick Sort 1': 'red', 'Quick Sort 2 (Med)': 'orange', 'Quick Sort 3 (Pivots)': 'blue'}

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

if __name__ == "__main__":
    main()