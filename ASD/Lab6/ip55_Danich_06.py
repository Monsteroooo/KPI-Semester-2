import sys

class Node:
    def __init__(self, val):
        self.val = val
        self.left = None
        self.right = None

def read_tree(tokens, index):
    if index[0] >= len(tokens):
        return None
    
    val = tokens[index[0]]
    index[0] += 1
    
    if val == 0:
        return None
        
    node = Node(val)
    node.left = read_tree(tokens, index)
    node.right = read_tree(tokens, index)
    return node

def get_inorder(node, vals):
    if not node:
        return
    get_inorder(node.left, vals)
    vals.append(node.val)
    get_inorder(node.right, vals)

def set_inorder(node, vals, index):
    if not node:
        return
    set_inorder(node.left, vals, index)
    node.val = vals[index[0]]
    index[0] += 1
    set_inorder(node.right, vals, index)

def find_paths(node, S, current_path, result):
    if not node:
        return
        
    current_path.append(node.val)
    
    current_sum = 0
    for i in range(len(current_path) - 1, -1, -1):
        current_sum += current_path[i]
        if current_sum == S:
            result.append(list(current_path[i:]))
            
    find_paths(node.left, S, current_path, result)
    find_paths(node.right, S, current_path, result)
    
    current_path.pop()

def main():
    if len(sys.argv) < 3:
        print(f"Використання: python {sys.argv[0]} <input_file> <S_value>")
        sys.exit(1)
        
    input_filename = sys.argv[1]
    try:
        S = int(sys.argv[2])
    except ValueError:
        print("Помилка: Значення S має бути цілим числом.")
        sys.exit(1)
        
    try:
        with open(input_filename, 'r') as f:
            content = f.read().split()
            tokens = [int(x) for x in content]
    except FileNotFoundError:
        print(f"Помилка: Не вдалося відкрити файл {input_filename}")
        sys.exit(1)
        
    index = [0]
    root = read_tree(tokens, index)
    
    tree_values = []
    get_inorder(root, tree_values)
    tree_values.sort()
    
    index = [0]
    set_inorder(root, tree_values, index)
    
    valid_paths = []
    find_paths(root, S, [], valid_paths)
    
    try:
        with open("output.txt", 'w') as f:
            for path in valid_paths:
                f.write(" ".join(map(str, path)) + "\n")
        print("Готово. Результати збережено у файл output.txt")
    except IOError:
        print("Помилка: Не вдалося записати у файл output.txt")
        sys.exit(1)

if __name__ == "__main__":
    main()
