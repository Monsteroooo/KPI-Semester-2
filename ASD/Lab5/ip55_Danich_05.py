import random
import string
from collections import Counter

class Node:
    def __init__(self, value):
        self.value = value
        self.children = []

    def __repr__(self):
        return f"Node('{self.value}')"

def build_frequency_tree(text):
    if not text:
        return None

    counts = Counter(text)
    
    freq_map = {}
    for char, freq in counts.items():
        if freq not in freq_map:
            freq_map[freq] = []
        freq_map[freq].append(char)

    sorted_frequencies = sorted(freq_map.keys(), reverse=True)

    root = Node("Root")
    current_level_nodes = [root]

    for freq in sorted_frequencies:
        chars = freq_map[freq]
        new_level_nodes = []
        
        for char in chars:
            new_node = Node(f"{char} ({freq}x)")
            current_level_nodes[0].children.append(new_node)
            new_level_nodes.append(new_node)
            
        current_level_nodes = new_level_nodes

    return root

def print_tree(node, level=0):
    print("  " * level + "|-- " + node.value)
    for child in node.children:
        print_tree(child, level + 1)

def main():
    print("Оберіть спосіб отримання рядка:")
    print("1. Ввести вручну")
    print("2. Згенерувати випадково")
    
    choice = input("Ваш вибір (1/2): ")

    if choice == "1":
        input_string = input("Введіть рядок: ")
    elif choice == "2":
        length = int(input("Введіть довжину рядка: "))
        input_string = ''.join(random.choices(string.ascii_letters, k=length))
        print(f"Згенерований рядок: {input_string}")
    else:
        print("Помилка вибору")
        return

    tree = build_frequency_tree(input_string)
    if tree:
        print("\nПобудоване дерево:")
        print_tree(tree)

if __name__ == "__main__":
    main()
