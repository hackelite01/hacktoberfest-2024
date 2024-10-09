import java.util.Scanner;

class Question {
    String question;
    String[] options;
    char correctAnswer;

    public Question(String question, String[] options, char correctAnswer) {
        this.question = question;
        this.options = options;
        this.correctAnswer = correctAnswer;
    }

    public boolean isCorrect(char answer) {
        return answer == correctAnswer;
    }

    public void display() {
        System.out.println(question);
        for (int i = 0; i < options.length; i++) {
            System.out.println((char) ('A' + i) + ". " + options[i]);
        }
    }
}

public class QuizApp {

    public static void main(String[] args) {
        Question[] questions = {
            new Question("What is the capital of France?", new String[]{"Paris", "London", "Berlin", "Rome"}, 'A'),
            new Question("Who developed Java?", new String[]{"Microsoft", "Sun Microsystems", "Apple", "Google"}, 'B'),
            new Question("Which planet is known as the Red Planet?", new String[]{"Earth", "Mars", "Jupiter", "Saturn"}, 'B'),
            new Question("What is the largest ocean on Earth?", new String[]{"Atlantic", "Indian", "Arctic", "Pacific"}, 'D')
        };

        int score = 0;
        Scanner scanner = new Scanner(System.in);

        for (Question question : questions) {
            question.display();
            System.out.print("Enter your answer (A/B/C/D): ");
            char answer = scanner.next().toUpperCase().charAt(0);

            if (question.isCorrect(answer)) {
                System.out.println("Correct!\n");
                score++;
            } else {
                System.out.println("Wrong! The correct answer is: " + question.correctAnswer + "\n");
            }
        }

        System.out.println("Quiz Over! Your Score: " + score + " out of " + questions.length);

        scanner.close();
    }
}
