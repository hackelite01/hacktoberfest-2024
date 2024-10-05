# import modules
import string
import random
string1 = list(string.ascii_lowercase)
string2 = list(string.ascii_uppercase)
string3 = list(string.digits)
string4 = list(string.punctuation)

user_input = input("How many characters do you want in your password? ")


# check this input is it number? is it more than 8?
while True:
	try:
		characters_number = int(user_input)
		if characters_number < 8:
			print("Your number should be at least 8.")
			user_input = input("Please, Enter your number again: ")
		else:
			break
	except:
		print("Please, Enter numbers only.")
		user_input = input("How many characters do you want in your password? ")

# shuffle all lists
random.shuffle(string1)
random.shuffle(string2)
random.shuffle(string3)
random.shuffle(string4)

# calculate 30% & 20% of number of characters
part1 = round(characters_number * (30/100))
part2 = round(characters_number * (20/100))


# generation of the password (60% letters and 40% digits & punctuations)
result = []
for x in range(part1):
	result.append(string1[x])
	result.append(string2[x])
for x in range(part2):
	result.append(string3[x])
	result.append(string4[x])


# shuffle result
random.shuffle(result)
# join result
password = "".join(result)
print("Strong Password: ", password)
