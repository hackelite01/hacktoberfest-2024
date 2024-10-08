def display_tasks():
    with open("tasks.txt", "r") as f:
        tasks = f.readlines()
        if not tasks:
            print("No tasks in the to-do list.")
        else:
            print("To-Do List:")
            for i, task in enumerate(tasks):
                print(f"{i + 1}. {task.strip()}")

def add_task(task):
    with open("tasks.txt", "a") as f:
        f.write(task + "\n")
    print("Task added successfully!")

def remove_task(task_number):
    with open("tasks.txt", "r") as f:
        tasks = f.readlines()
    if task_number > len(tasks) or task_number < 1:
        print("Invalid task number!")
    else:
        del tasks[task_number - 1]
        with open("tasks.txt", "w") as f:
            f.writelines(tasks)
        print("Task removed successfully!")

def main():
    while True:
        print("\n1. View Tasks")
        print("2. Add Task")
        print("3. Remove Task")
        print("4. Quit")
        
        choice = input("Enter your choice: ")
        
        if choice == "1":
            display_tasks()
        elif choice == "2":
            task = input("Enter the task: ")
            add_task(task)
        elif choice == "3":
            task_number = int(input("Enter task number to remove: "))
            remove_task(task_number)
        elif choice == "4":
            break
        else:
            print("Invalid choice!")

if __name__ == "__main__":
    main()
