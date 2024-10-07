// Problem statement
// For a given two-dimensional integer array/list of size (N x M), print it in a spiral form. That is, you need to print in the order followed for every iteration:

// a. First row(left to right)
// b. Last column(top to bottom)
// c. Last row(right to left)
// d. First column(bottom to top)
//  Mind that every element will be printed only once.

// Refer to the Image:
// Spiral path of a matrix

// Detailed explanation ( Input/output format, Notes, Images )
// Constraints :
// 1 <= t <= 10^2
// 0 <= N <= 10^3
// 0 <= M <= 10^3
// Time Limit: 1sec
// Sample Input 1:
// 1
// 4 4 
// 1 2 3 4 
// 5 6 7 8 
// 9 10 11 12 
// 13 14 15 16
// Sample Output 1:
// 1 2 3 4 8 12 16 15 14 13 9 5 6 7 11 10 
// Sample Input 2:
// 2
// 3 3 
// 1 2 3 
// 4 5 6 
// 7 8 9
// 3 1
// 10
// 20
// 30
// Sample Output 2:
// 1 2 3 6 9 8 7 4 5 
// 10 20 30 

// Java
// public class Solution {
// 	public static void spiralPrint(int mat[][]){
//         int nRows = mat.length;
//         if (nRows == 0) {
//             return;
//         }
//         int mCols = mat[0].length;
//         int i, rowStart=0, colStart = 0; 
//         int numElements = nRows * mCols;
//         int count = 0;
//         while(count < numElements)
//         {
//             for(i = colStart;i < numElements && i < mCols; i++)
//             {
//                 System.out.print(mat[rowStart][i] + " ");
//                 count++;
//             }
//             rowStart++;
//             for(i = rowStart; count < numElements && i < nRows; ++i) {
//                 System.out.print(mat[i][mCols - 1] + " ");
//                 count++;
//             }
//             mCols--;
//             for(i = mCols - 1; count < numElements && i >= colStart; --i) {
//                 System.out.print(mat[nRows - 1][i] + " ");
//                 count++;
//             }
//             nRows--;
//             for(i = nRows - 1; count < numElements && i >= rowStart; --i) {
//                 System.out.print(mat[i][colStart] + " ");
//                 count++; 
//             } 
//             colStart++;
//         }
// 	}
// }