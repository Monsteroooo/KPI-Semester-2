def Sedgvik(arr):
    n = len(arr)
    gaps = []
    k = 0
    while True:
        if k % 2 == 0:  # k четное
            gap = 9 * (2 ** k) - 9 * (2 ** (k // 2)) + 1
        else:  # k нечетное
            gap = 8 * (2 ** k) - 6 * (2 ** ((k + 1) // 2)) + 1
        
        if gap > n:
            break
        gaps.append(gap)
        k += 1
    
    gaps.reverse()
    
    for h in gaps:
        for i in range(h, n):
            temp = arr[i]
            j = i
            while j >= h and arr[j-h] > temp:
                arr[j] = arr[j-h]
                j -= h
            arr[j] = temp
    return arr

def main():
    arr = [9,8,7,6,5,4,3,2,1]
    print(Sedgvik(arr))

if __name__ == "__main__":
    main()