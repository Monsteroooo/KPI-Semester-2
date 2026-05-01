import random
import time
import string
import numpy as np
import matplotlib.pyplot as plt

T = list(range(256))
random.seed(42)
random.shuffle(T)

class HashTable:
    def __init__(self, size, mode="random"):
        self.size = size
        self.table = [[] for _ in range(size)]
        self.mode = mode

    def _hash(self, key):
        if self.mode == "worst":
            return 0
        
        h = len(key) % 256
        for char in key:
            h = T[h ^ ord(char)]
        return h % self.size

    def insert(self, key, value):
        idx = self._hash(key)
        for item in self.table[idx]:
            if item[0] == key:
                item[1] = value
                return
        self.table[idx].append([key, value])

    def search(self, key):
        idx = self._hash(key)
        comparisons = 0
        for item in self.table[idx]:
            comparisons += 1
            if item[0] == key:
                return item[1], comparisons
        return None, comparisons

def generate_random_string(max_length=20):
    length = random.randint(5, max_length)
    return ''.join(random.choices(string.ascii_letters + string.digits, k=length))

def main():
    sizes_exp = [int(100 * (200 ** (i / 19))) for i in range(20)]
    sizes_lin = list(range(30000, 50001, 10000))
    sizes = sorted(list(set(sizes_exp + sizes_lin)))
    
    RUNS = 5 
    all_types = ["best", "worst", "random"]
    
    algorithms = ["Pearson Hash Search"]
    
    all_results = {t: {name: {'comp': [], 'time': [], 'valid_sizes': []} for name in algorithms} for t in all_types}

    for current_type in all_types:
        print(f"\n{'='*90}")
        print(f" СЦЕНАРІЙ ПОШУКУ: {current_type.upper()} ".center(90, "="))
        print(f"{'Size':<10} | {'Algorithm':<20} | {'Avg Comp':<15} | {'Avg Time (ns)':<20}")
        print("-" * 90)

        for n in sizes:
            run_stats = {name: {'comp': 0.0, 'time': 0.0} for name in algorithms}
            
            for _ in range(RUNS):
                ht = HashTable(int(n * 1.5), mode=current_type)
                keys = []
                
                for _ in range(n):
                    k = generate_random_string()
                    v = generate_random_string(50)
                    ht.insert(k, v)
                    keys.append(k)
                    
                search_keys = []
                if current_type == "best":
                    for bucket in ht.table:
                        if bucket:
                            search_keys.append(bucket[0][0])
                            if len(search_keys) >= max(1, n // 10): break
                elif current_type == "worst":
                    if ht.table[0]:
                        search_keys = [ht.table[0][-1][0]] * max(1, n // 10)
                else:
                    search_keys = random.sample(keys, max(1, n // 10))
                    
                if not search_keys: 
                    search_keys = keys[:1]

                total_comp = 0
                start_time = time.perf_counter_ns()
                for k in search_keys:
                    _, comp = ht.search(k)
                    total_comp += comp
                end_time = time.perf_counter_ns()
                
                run_stats["Pearson Hash Search"]['comp'] += total_comp / len(search_keys)
                run_stats["Pearson Hash Search"]['time'] += (end_time - start_time) / len(search_keys)

            name = "Pearson Hash Search"
            avg_comp = run_stats[name]['comp'] / RUNS
            avg_time = run_stats[name]['time'] / RUNS

            all_results[current_type][name]['comp'].append(avg_comp)
            all_results[current_type][name]['time'].append(avg_time)
            all_results[current_type][name]['valid_sizes'].append(n)
            
            print(f"{n:<10} | {name:<20} | {avg_comp:<15.2f} | {avg_time:<20.2f}")
        print("-" * 90)

        print(f"\nКоефіцієнти апроксимації для {current_type.upper()}:")
        print(f"{'Algorithm':<20} | {'Metric':<10} | {'a (n^2)':<25} | {'b (n)':<25} | {'c':<25}")
        
        x = all_results[current_type][name]['valid_sizes']
        y_comp = all_results[current_type][name]['comp']
        coef_comp = np.polyfit(x, y_comp, 2)
        print(f"{name:<20} | {'Comp':<10} | {coef_comp[0]:<25.5e} | {coef_comp[1]:<25.5e} | {coef_comp[2]:<25.5e}")
            
        y_time = all_results[current_type][name]['time']
        coef_time = np.polyfit(x, y_time, 2)
        print(f"{name:<20} | {'Time':<10} | {coef_time[0]:<25.5e} | {coef_time[1]:<25.5e} | {coef_time[2]:<25.5e}")

    colors = {'best': 'green', 'worst': 'red', 'random': 'blue'}

    plt.figure(figsize=(10, 6))
    for t in all_types:
        x = all_results[t]["Pearson Hash Search"]['valid_sizes']
        y_comp = all_results[t]["Pearson Hash Search"]['comp']
        plt.plot(x, y_comp, marker='o', color=colors[t], label=f'Pearson - {t.capitalize()}')
    
    plt.title("Кількість порівнянь - Хеш-таблиця (Пірсон)", fontsize=14)
    plt.xlabel("Розмір структури (n)", fontsize=12)
    plt.ylabel("Кількість порівнянь", fontsize=12)
    plt.xscale('log')
    plt.yscale('log')
    plt.grid(True, which="both", ls="--", alpha=0.5)
    plt.legend(fontsize=12)
    plt.tight_layout()
    plt.show()

    plt.figure(figsize=(10, 6))
    for t in all_types:
        x = all_results[t]["Pearson Hash Search"]['valid_sizes']
        y_time = all_results[t]["Pearson Hash Search"]['time']
        plt.plot(x, y_time, marker='o', color=colors[t], label=f'Pearson - {t.capitalize()}')
        
    plt.title("Час виконання пошуку - Хеш-таблиця (Пірсон)", fontsize=14)
    plt.xlabel("Розмір структури (n)", fontsize=12)
    plt.ylabel("Час (наносекунди)", fontsize=12)
    plt.xscale('log')
    plt.yscale('log')
    plt.grid(True, which="both", ls="--", alpha=0.5)
    plt.legend(fontsize=12)
    plt.tight_layout()
    plt.show()

if __name__ == "__main__":
    main()
