# Python program to create a simple calculator

def add(x, y):
    return x + y

def subtract(x, y):
    return x - y

def multiply(x, y):
    return x * y

def divide(x, y):
    if y == 0:
        return "Division by zero is not allowed"
    return x / y

def power(x, y):  # Calculate x^y
    return x ** y

def factorial(x):  # Calculate factorial of a number
    if x == 1 or x == 0:
        return 1
    else:
        return x * factorial(x - 1)

def remainder(x, y):  # Calculate remainder
    return x % y

def minimum(x, y):  # Calculate minimum
    return min(x, y)

def maximum(x, y):  # Calculate maximum
    return max(x, y)


print("Select operation:")
print("1. Add")
print("2. Subtract")
print("3. Multiply")
print("4. Divide")
print("5. Power")
print("6. Factorial")
print("7. Remainder")
print("8. Minimum")
print("9. Maximum")

while True:
    choice = input("Enter choice(1/2/3/4/5/6/7/8/9): ")

    if choice in ('1', '2', '3', '4', '5', '7', '8', '9'):
        num1 = float(input("Enter first number: "))
        num2 = float(input("Enter second number: "))

        if choice == '1':
            print(f"The result is: {add(num1, num2)}")

        elif choice == '2':
            print(f"The result is: {subtract(num1, num2)}")

        elif choice == '3':
            print(f"The result is: {multiply(num1, num2)}")

        elif choice == '4':
            print(f"The result is: {divide(num1, num2)}")

        elif choice == '5':
            print(f"The result is: {power(num1, num2)}")

        elif choice == '7':
            print(f"The result is: {remainder(num1, num2)}")

        elif choice == '8':
            print(f"The minimum value is: {minimum(num1, num2)}")

        elif choice == '9':
            print(f"The maximum value is: {maximum(num1, num2)}")

    elif choice == '6':
        num = int(input("Enter a number for factorial: "))
        print(f"The factorial is: {factorial(num)}")

    else:
        print("Invalid input")
