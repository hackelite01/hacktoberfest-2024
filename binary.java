import java.util.Scanner;
public class binary {
    /**
     * prints the binary value of corresponding int arg
     * @param value
     */
    public static void displayBinary(int value) {
        int b = value % 2;
        int q = value / 2;
        
        if (q != 0) {
            displayBinary(q);
        }
        System.out.print(b);


    }
    
    public static void main(String[] args) {
        //test
        Scanner in = new Scanner(System.in);
        System.out.println("Please enter the value to test");

        int t = in.nextInt();
        in.close();
        
        System.out.println("the bin value of this should be ");
        displayBinary(t);
    }
}
