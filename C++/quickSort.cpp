#include<iostream>
using namespace std;
int partition(int arr[], int start, int end){
     int pivot = arr[end];
     int i = start - 1;
     for(int j = start; j<end; j++){
        if(arr[j]<pivot){
            i++;
            swap(arr[i], arr[j]);
        }
       
     }
      swap(arr[i+1], arr[end]);
        return i+1;
}
void quickSort(int arr[], int start, int end){
    if(start<end){
        int p = partition(arr, start, end);
        quickSort(arr,start,p-1);
        quickSort(arr,p+1, end);

    }
}
 void printArray(int arr[], int n){
    for(int i=0; i<n; i++){
 cout<<arr[i]<<" ";
    }
   
 }
int main(){
int n;
cin>>n;
int arr[n];
for(int i=0; i<n; i++){
    cin>>arr[i];
}
cout<<"original array :- ";
printArray(arr,n);
quickSort(arr,0,n-1);
cout<<"sorted array :- ";
printArray(arr,n);

}