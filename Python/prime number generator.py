import json
import os

def is_prime(n):
    if n <= 1:
        return False
    for i in range(2, int(n ** 0.5) + 1):
        if n % i == 0:
            return False
    return True

def generate_primes(start, limit, existing_primes):
    primes = existing_primes
    count = max(existing_primes.values(), default=0) + 1
    for num in range(start, limit + 1):
        if is_prime(num) and num not in primes:
            primes[num] = count
            count += 1
    return primes

def load_existing_primes(filename):
    if os.path.exists(filename):
        with open(filename, 'r') as json_file:
            return json.load(json_file)
    return {}

prime_limit = int(input("Enter prime limit: "))
json_file_name = 'primes.json'

prime_dict = load_existing_primes(json_file_name)

if prime_dict:
    start_from = max(map(int, prime_dict.keys())) + 1
else:
    start_from = 2

prime_dict = generate_primes(start_from, prime_limit, prime_dict)

with open(json_file_name, 'w') as json_file:
    json.dump(prime_dict, json_file, indent=4)

print(f"Prime numbers up to {prime_limit} have been saved to '{json_file_name}'")

