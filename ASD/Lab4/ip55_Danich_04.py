import random

class DSU:
    def __init__(self, n):
        self.parent = list(range(n))
        self.rank = [0] * n

    def find(self, i):
        if self.parent[i] == i:
            return i
        self.parent[i] = self.find(self.parent[i])
        return self.parent[i]

    def union(self, i, j):
        root_i = self.find(i)
        root_j = self.find(j)
        
        if root_i != root_j:
            if self.rank[root_i] < self.rank[root_j]:
                self.parent[root_i] = root_j
            elif self.rank[root_i] > self.rank[root_j]:
                self.parent[root_j] = root_i
            else:
                self.parent[root_j] = root_i
                self.rank[root_i] += 1
            return True
        return False

def generate_random_graph(n, max_weight=20):
    matrix = [[0] * n for _ in range(n)]
    for i in range(n):
        for j in range(i + 1, n):
            if random.random() < 0.7: 
                weight = random.randint(1, max_weight)
                matrix[i][j] = weight
                matrix[j][i] = weight
    return matrix

def get_manual_input():
    try:
        n = int(input("Введіть n: "))
        matrix = [[0] * n for _ in range(n)]
        for i in range(n):
            while True:
                row = list(map(int, input(f"v{i}: ").split()))
                if len(row) == n:
                    matrix[i] = row
                    break
        return n, matrix
    except ValueError:
        return get_manual_input()

def print_matrix(matrix):
    n = len(matrix)
    print("\n     ", end="")
    for i in range(n):
        print(f"v{i:>2} ", end="")
    print("\n" + "-" * (5 + n * 4))
    for i in range(n):
        print(f"v{i} | ", end="")
        for j in range(n):
            val = str(matrix[i][j]) if matrix[i][j] != 0 else "."
            print(f"{val:>3} ", end="") 
        print()
    print("-" * (5 + n * 4))

def boruvka(matrix):
    n = len(matrix)
    dsu = DSU(n)
    num_components = n
    mst_weight = 0
    mst_edges = []

    while num_components > 1:
        cheapest = [[-1, -1, float('inf')] for _ in range(n)]
        for u in range(n):
            for v in range(u + 1, n):
                weight = matrix[u][v]
                if weight > 0:
                    set_u = dsu.find(u)
                    set_v = dsu.find(v)
                    if set_u != set_v:
                        if weight < cheapest[set_u][2]:
                            cheapest[set_u] = [u, v, weight]
                        if weight < cheapest[set_v][2]:
                            cheapest[set_v] = [u, v, weight]

        edges_added = False
        for i in range(n):
            if cheapest[i][2] != float('inf'):
                u, v, weight = cheapest[i]
                set_u = dsu.find(u)
                set_v = dsu.find(v)
                if set_u != set_v:
                    dsu.union(set_u, set_v)
                    mst_weight += weight
                    mst_edges.append((u, v, weight))
                    num_components -= 1
                    edges_added = True

        if not edges_added:
            print("\nГраф не є зв'язним!")
            return

    print(f"\nВага МПД: {mst_weight}")
    for u, v, w in mst_edges:
        print(f"v{u}-v{v}: {w}")

if __name__ == "__main__":
    choice = input("1 - рандом, 2 - вручну: ")
    if choice == '2':
        n, graph = get_manual_input()
    else:
        n = 8
        graph = generate_random_graph(n)
    print_matrix(graph)
    boruvka(graph)
