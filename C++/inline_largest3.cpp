#include<iostream>
using namespace std;
inline int max(int a,int b,int c)
{
    if (a>b&&a>c)
    {
        return a;
    }
    else if (b>b&&b>c)
    {
        return b;
    }
    else
    {
        return c;
    }
}
int main()
{
    system("CLS");
    int a,b,c,d;
    cout<<"Enter Three Numbers:";
    cin>>a>>b>>c;
    d=max(a,b,c);
    cout<<"Maximum is: "<<d<<endl;
    return 0;
}