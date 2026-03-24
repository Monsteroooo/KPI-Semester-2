import sys
import os

sys.setrecursionlimit(200000)

def get_data_type(arr):
    if len(arr) <= 1:
        return "random"
    
    ascending = True
    descending = True
    
    for i in range(len(arr) - 1):
        if arr[i] > arr[i+1]:
            ascending = False
        if arr[i] < arr[i+1]:
            descending = False
        if not ascending and not descending:
            return "random"
            
    if ascending:
        return "best"
    if descending:
        return "worst"
    return "random"

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

    if arr:
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

    if arr:
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

    if arr:
        qs(0, len(arr) - 1)
    return comp[0]

def main():
    if len(sys.argv) < 2:
        return
        
    input_filename = sys.argv[1]
    
    with open(input_filename, 'r') as f:
        lines = f.read().split()
        
    if not lines:
        return
        
    n = int(lines[0])
    arr = [int(x) for x in lines[1:n+1]]
    
    data_type = get_data_type(arr)
    
    arr1 = arr.copy()
    arr2 = arr.copy()
    arr3 = arr.copy()
    
    comp1 = Quick_sort(arr1)
    comp2 = Quick_Sort_Mediany(arr2)
    comp3 = Quick_sort_3_pivots(arr3)
    
    script_name = os.path.basename(sys.argv[0])
    name_without_ext = os.path.splitext(script_name)[0]
    output_filename = f"{name_without_ext}_output.txt"
    
    with open(output_filename, 'w') as f:
        f.write(f"{comp1} {comp2} {comp3} {data_type}\n")

if __name__ == "__main__":
    main()