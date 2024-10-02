import pandas as pd
import matplotlib.pyplot as plt


data = pd.read_csv('data.csv')

print("Data Preview:")
print(data.head())

print("\nSummary Statistics:")
print(data.describe())

plt.figure(figsize=(10, 5))
plt.hist(data['age'], bins=20, color='blue', edgecolor='black')
plt.title('Age Distribution')
plt.xlabel('Age')
plt.ylabel('Frequency')
plt.show()
