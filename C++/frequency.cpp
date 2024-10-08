#include<iostream>
using namespace std;
class fre
{
private:int n,i,a[20],count,ele;
public:void input();
       void logic();
       void output();
};
void fre::input()
{
  count=0;
  cout<<"Enter element"<<endl;
  cin>>ele;
  cout<<"Enter array size"<<endl;
  cin>>n;
  cout<<"Enter array elements";
  for(i=0;i<=n-1;i++)
   cin>>a[i];
}
void fre::logic()
{
  for(i=0;i<=n-1;i++)
   {
     if(a[i]==ele)
      count++;
   }
}
void fre::output()
{
  cout<<"Element found"<<count<<"times";
}
void main()
{
  fre f;
  f.input();
  f.logic();
  f.output();
  getch();
}
//Sample input:
//Enter element 4
//Enter array size 5
//Enter array elements 3 4 5 4 4

//Sample output:
//Element found 3 times
    
   
