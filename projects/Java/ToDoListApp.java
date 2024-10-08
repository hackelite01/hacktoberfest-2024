import java.util.ArrayList;
import java.util.Scanner;

public class ToDoListApp {

    private static ArrayList<String> toDoList = new ArrayList<>();
    
    public static void main(String[] args) {
        Scanner sc = new Scanner(System.in);
        boolean running = true;

        System.out.println("\n TO-DO LIST");

        while (running) {

            System.out.println("----------------------------------------------------------------");
            System.out.println("1. Add Task");
            System.out.println("2. View Tasks");
            System.out.println("3. Remove Task");
            System.out.println("4. Exit");
            System.out.println("----------------------------------------------------------------");
            System.out.print("Enter your choice: ");
            int choice = sc.nextInt();
            sc.nextLine();

            switch (choice) {
                case 1:
                    System.out.print("Enter the task to add: ");
                    String task = sc.nextLine();
                    toDoList.add(task);
                    System.out.println("Task added successfully.");
                    break;

                case 2:
                    viewTasks();
                    break;

                case 3:
                    System.out.print("\nEnter the task number to remove: ");
                    int taskNumber = sc.nextInt();
                    try {
                        removeTask(taskNumber); 
                    } catch (Exception e) {
                        System.out.println("Invalid task number.");
                    }
                    break;

                case 4:
                    running = false;
                    System.out.println("\nExited the To-Do List App.\n");
                    break;

                default:
                    System.out.println("\nInvalid choice. Please try again.");
            }
        }
        sc.close();
    }

    private static void viewTasks() {
        if (toDoList.isEmpty()) {
            System.out.println("Your To-Do List is empty.");
        } else {
            System.out.println("\nYour To-Do List:");
            for (int i = 0; i < toDoList.size(); i++) {
                System.out.println((i + 1) + ". " + toDoList.get(i));
            }
        }
    }

    private static void removeTask(int taskNumber) {
        if (taskNumber <= 0 || taskNumber > toDoList.size()) {
            System.out.println("Invalid task number.");
        } else {
            String removedTask = toDoList.remove(taskNumber - 1);
            System.out.println("Task '" + removedTask + "' removed successfully.");
        }
    }
}
