// Java program to find the
// area of the triangle

// Importing java libraries
import java.io.*;

class GFG {

    // Function to calculate the
    // area of the triangle
    static double area(double h, double b)
    {
        // Function returning the value that is
        // area of a triangle
        return (h * b) / 2;
    }

    // Main driver code
    public static void main(String[] args)
    {
        // Custom inputs- height and base values

        // Height of the triangle
        double h = 10;

        // Base of the triangle
        double b = 5;

        // Calling area function and
        // printing value corresponding area
        System.out.println("Area of the triangle: "
                           + area(h, b));
    }
}
