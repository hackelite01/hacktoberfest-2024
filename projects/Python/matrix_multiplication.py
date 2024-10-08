def matrix_multiplication(A, B):
    result = [[0 for _ in range(len(B[0]))] for _ in range(len(A))]

    for i in range(len(A)):
        for j in range(len(B[0])):
            for k in range(len(B)):
                result[i][j] += A[i][k] * B[k][j]
    
    return result

A = [[1, 2, 3],
     [4, 5, 6],
     [7, 8, 9]]

B = [[9, 8, 7],
     [6, 5, 4],
     [3, 2, 1]]

result = matrix_multiplication(A, B)

print("Matrix A:")
for row in A:
    print(row)
print("\nMatrix B:")
for row in B:
    print(row)
print("\nResult of A * B:")
for row in result:
    print(row)
