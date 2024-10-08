#include<iostream>
using namespace std;
class binary
{
private:int ele,n,loc,m,e,b,i,a[20];
public:void input();
void logic();
void output();
};
void binary::input()
{
  cout<<"enter element to be found in array"<<endl;
  cin>>ele;
  cout<<"Enter array size"<<endl;
  cin>>n;
  cout<<"Enter array elements";
  for(i=0;i<=n-1;i++)
  cin>>a[i];
}
void binary::logic()
{
  b=0;
  e=n-1;
  loc=-1;
  while(b<=e)
 {
  m=int((b+e)/2);
  if(ele==a[m])
   {
      loc=m;
      break;
   }
  if(ele>a[m])
   b=m+1;
  else
    e=m-1;
  }
}
void binary::output()
{
  if(loc==-1)
    cout<<"Element not found ";
  else
    cout<<"Element found at "<<loc;
}
void main()
{
  binary b;
  b.input();
  b.logic();
  b.output();
  getch();
}
//Sample input values:
//Enter element to be found: 2
//Enter array size: 4
//Enter array elements: 5 6 7 2

//Sample output:
//Element found at 3
