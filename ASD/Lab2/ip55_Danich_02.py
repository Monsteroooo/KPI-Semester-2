import sys
import os

def merge_and_count(arr, temp_arr, left, mid, right):
    i = left
    j = mid + 1
    k = left
    inv_count = 0

    while i <= mid and j <= right:
        if arr[i] <= arr[j]:
            temp_arr[k] = arr[i]
            i += 1
        else:
            temp_arr[k] = arr[j]
            inv_count += (mid - i + 1)
            j += 1
        k += 1

    while i <= mid:
        temp_arr[k] = arr[i]
        i += 1
        k += 1

    while j <= right:
        temp_arr[k] = arr[j]
        j += 1
        k += 1

    for loop_var in range(left, right + 1):
        arr[loop_var] = temp_arr[loop_var]

    return inv_count

def merge_sort_and_count(arr, temp_arr, left, right):
    inv_count = 0
    if left < right:
        mid = (left + right) // 2
        inv_count += merge_sort_and_count(arr, temp_arr, left, mid)
        inv_count += merge_sort_and_count(arr, temp_arr, mid + 1, right)
        inv_count += merge_and_count(arr, temp_arr, left, mid, right)
    return inv_count

def get_inversions(target_list, other_list):
    m = len(target_list)
    b = []
    
    for pos in range(1, m + 1):
        film_idx = target_list.index(pos)
        b.append(other_list[film_idx])
    
    temp = [0] * m
    return merge_sort_and_count(b, temp, 0, m - 1)

def main():
    if len(sys.argv) < 3:
        print("Використання: python program.py <вхідний_файл.txt> <X>")
        return

    input_filename = sys.argv[1]
    x_target = int(sys.argv[2]) 
    users = {}
    with open(input_filename, 'r') as f:
        lines = f.readlines()
        u, m = map(int, lines[0].strip().split())
        
        for line in lines[1:]:
            data = list(map(int, line.strip().split()))
            user_id = data[0]
            preferences = data[1:]
            users[user_id] = preferences

    results = []
    for user_id, preferences in users.items():
        if user_id != x_target:
            inv = get_inversions(users[x_target], preferences)
            results.append((user_id, inv))

    results.sort(key=lambda x: (x[1], x[0]))

    script_name = os.path.basename(sys.argv[0])
    name_without_ext = os.path.splitext(script_name)[0]
    output_filename = f"{name_without_ext}_output.txt"

    with open(output_filename, 'w') as f:
        f.write(f"{x_target}\n")
        for user_id, inv_count in results:
            f.write(f"{user_id} {inv_count}\n")

if __name__ == "__main__":
    main()