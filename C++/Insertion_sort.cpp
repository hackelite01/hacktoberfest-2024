#include<iostream>
using namespace std;
class insort
{
private:int s,c,i,a[20],n,t;
public:void input();
       void logic();
       void output();
};
void insort::input()
{
  cout<<"Enter array size"<<endl;
  cin>>n;
  cout<<"Enter array elements";
  for(i=0;i<=n-1;i++)
     cin>>a[i];
}
void insort::logic()
{
   for(s=1;s<=n-1;s++)
    {
       c=s;
    while(c>=1)
      {
         if(a[c]<=a[c-1])
            {
              t=a[c];
              a[c]=a[c-1];
              a[c-1]=t;
            }
         c=c-1;
        }
    }
 }
void insort::output()
{
  cout<<"Elements after sorting are";
  for(i=o;i<=n-1;i++)
    cout<<a[i[;
}    
 void main()
{
  insort i;
  i.input();
  i.logic();
  i.output();
  getch();
}
//Sample input values:
//Enter array size 5
//Enter array elements 6 5 8 1

//Sample output:
//Elements after sorting are 1 3 5 6 8
