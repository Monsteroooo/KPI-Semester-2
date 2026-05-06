import math
import heapq

cities = [
    "Луанда", "Уамбо", "Лобіту", "Бенгела", "Куїто",
    "Лубанго", "Маланже", "Намібе", "Сойо", "Кабінда",
    "Уїже", "Сауримо", "Сумбе", "Менонге", "Луена"
]

coords = {
    "Луанда": (0, 0),
    "Уамбо": (400, -300),
    "Лобіту": (300, -400),
    "Бенгела": (300, -450),
    "Куїто": (500, -300),
    "Лубанго": (200, -600),
    "Маланже": (350, 0),
    "Намібе": (100, -700),
    "Сойо": (0, 200),
    "Кабінда": (0, 300),
    "Уїже": (200, 100),
    "Сауримо": (700, -100),
    "Сумбе": (200, -200),
    "Менонге": (600, -500),
    "Луена": (800, -300)
}

graph = {city: {} for city in cities}
edges = [
    ("Луанда", "Сумбе", 300),
    ("Луанда", "Маланже", 400),
    ("Луанда", "Уїже", 350),
    ("Луанда", "Сойо", 400),
    ("Сойо", "Кабінда", 150),
    ("Уїже", "Маланже", 250),
    ("Маланже", "Сауримо", 500),
    ("Сауримо", "Луена", 300),
    ("Сумбе", "Лобіту", 200),
    ("Лобіту", "Бенгела", 50),
    ("Бенгела", "Лубанго", 350),
    ("Лубанго", "Намібе", 200),
    ("Сумбе", "Уамбо", 350),
    ("Уамбо", "Куїто", 150),
    ("Куїто", "Луена", 400),
    ("Куїто", "Менонге", 350),
    ("Уамбо", "Менонге", 450),
    ("Лубанго", "Менонге", 500)
]

for u, v, d in edges:
    graph[u][v] = d
    graph[v][u] = d

def straight_line_dist(c1, c2):
    x1, y1 = coords[c1]
    x2, y2 = coords[c2]
    return math.sqrt((x2 - x1)**2 + (y2 - y1)**2)

def greedy_search(start, goal):
    frontier = [(straight_line_dist(start, goal), start)]
    came_from = {start: None}
    path_cost = {start: 0}
    visited = set()

    while frontier:
        _, current = heapq.heappop(frontier)
        
        if current in visited:
            continue
        visited.add(current)

        if current == goal:
            path = []
            while current:
                path.append(current)
                current = came_from[current]
            return path[::-1], path_cost[goal]

        for next_node, weight in graph[current].items():
            if next_node not in visited:
                if next_node not in came_from:
                    came_from[next_node] = current
                    path_cost[next_node] = path_cost[current] + weight
                heapq.heappush(frontier, (straight_line_dist(next_node, goal), next_node))
    return None, 0

def a_star_search(start, goal):
    frontier = [(straight_line_dist(start, goal), 0, start)]
    came_from = {start: None}
    path_cost = {start: 0}

    while frontier:
        _, current_cost, current = heapq.heappop(frontier)

        if current == goal:
            path = []
            while current:
                path.append(current)
                current = came_from[current]
            return path[::-1], path_cost[goal]

        for next_node, weight in graph[current].items():
            new_cost = path_cost[current] + weight
            if next_node not in path_cost or new_cost < path_cost[next_node]:
                path_cost[next_node] = new_cost
                priority = new_cost + straight_line_dist(next_node, goal)
                heapq.heappush(frontier, (priority, new_cost, next_node))
                came_from[next_node] = current
    return None, 0

for i in range(len(cities)):
    for j in range(i + 1, len(cities)):
        start = cities[i]
        goal = cities[j]
        
        g_path, g_cost = greedy_search(start, goal)
        if g_path:
            path_str = " -> ".join(g_path)
            print(f"Жадібний пошук: ({start}-{goal} Відстань: {int(g_cost)}км Маршрут: {path_str})")
            
        a_path, a_cost = a_star_search(start, goal)
        if a_path:
            path_str = " -> ".join(a_path)
            print(f"Пошук A*: ({start}-{goal} Відстань: {int(a_cost)}км Маршрут: {path_str})")
