/** 
Problem Statement :
Given an array of integers asteroids, where each integer represents an asteroid in a row, determine the state of the asteroids after all collisions. In this array, the absolute value represents the size of the asteroid, and the sign represents its direction (positive meaning right and negative meaning left). All asteroids move at the same speed.
When two asteroids meet, the smaller one will explode. If they are the same size, both will explode. Asteroids moving in the same direction will never meet.
Example 1
Input: asteroids = [2, -2]
Output: []
Explanation: The asteroid with size 2 and the one with size -2 collide, exploding each other.
Example 2
Input: asteroids = [10, 20, -10]
Output: [10, 20]
Explanation: The asteroid with size 20 and the one with size -10 collide, resulting in the remaining asteroid with size 20. The asteroids with sizes 10 and 20 never collide.
Example 3
Input: asteroids = [10, 2, -5]
Output:
[10]
**/

// Solution
#include <bits/stdc++.h>
using namespace std;
class Solution
{
public:
    vector<int> asteroidCollision(vector<int> &asteroids)
    {
        vector<int> st;
        for (int i = 0; i < asteroids.size(); i++)
        {
            if (asteroids[i] > 0)
                st.push_back(asteroids[i]);
            else
            {
                while (!st.empty() && st.back() > 0 && st.back() < abs(asteroids[i]))
                    st.pop_back();
                if (!st.empty() && st.back() == abs(asteroids[i]))
                    st.pop_back();
                else if (st.empty() || st.back() < 0)
                    st.push_back(asteroids[i]);
            }
        }
        return st;
    }
};

//Approach
//1. We will use vector like a stack to solve this problem.
//2. We will iterate through the array and check if the asteroid is positive or negative.
//3. If the asteroid is positive we will push it into the stack.
//4. If previous condition is false then we will use a while loop to check if the stack is not empty and the last element of the stack is positive and the last element of the stack is less than the absolute value of the current asteroid. 
//5. If the above condition is true then we will pop the last element of the stack.
//6. If the last element of the stack is equal to the absolute value of the current asteroid then we will pop the last element of the stack.
//7. Otherwise, If the stack is empty or the last element of the stack is negative then we will push the current asteroid into the stack.
//8. Finally, we will return the stack.
//9. This is the optimal approach with O(n) time complexity.
