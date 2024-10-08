nterms=int(input("Enter number of terms in sequence"))
n1=0
n2=1
count=0
if nterms<0:
  print("Enter a positive number")
elif nterms==0:
  print("Fibonacci sequence is:")
  print(n1)
else :
  print("Fibonacci sequence is:")
  while count<nterms:
    print(n1)
    nterms=n1+n2
    n1=n2
    n2=nterms
    count+=1
    
  
  
