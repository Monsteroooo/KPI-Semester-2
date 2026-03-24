import random

N = 50000

def generate_file(filename, gen_type):
    with open(filename, 'w') as f:
        f.write(f"{N}\n")
        if gen_type == "best":
            data = range(1, N + 1)
        elif gen_type == "worst":
            data = range(N, 0, -1)
        else:
            data = [random.randint(1, 1000000) for _ in range(N)]
        
        for item in data:
            f.write(f"{item}\n")

if __name__ == "__main__":
    generate_file("input_random.txt", "random")
    generate_file("input_best.txt", "best")
    generate_file("input_worst.txt", "worst")