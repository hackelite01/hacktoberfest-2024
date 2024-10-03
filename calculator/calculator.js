// Function to display result
function displayResult(result) {
    document.getElementById('result').innerText = "Result: " + result;
}

// Addition
function add() {
    const num1 = parseFloat(document.getElementById('num1').value);
    const num2 = parseFloat(document.getElementById('num2').value);
    displayResult(num1 + num2);
}

// Subtraction
function subtract() {
    const num1 = parseFloat(document.getElementById('num1').value);
    const num2 = parseFloat(document.getElementById('num2').value);
    displayResult(num1 - num2);
}

// Multiplication
function multiply() {
    const num1 = parseFloat(document.getElementById('num1').value);
    const num2 = parseFloat(document.getElementById('num2').value);
    displayResult(num1 * num2);
}

// Division
function divide() {
    const num1 = parseFloat(document.getElementById('num1').value);
    const num2 = parseFloat(document.getElementById('num2').value);
    if (num2 === 0) {
        displayResult("Cannot divide by zero!");
    } else {
        displayResult(num1 / num2);
    }
}

// Exponentiation (num1 ^ num2)
function exponent() {
    const num1 = parseFloat(document.getElementById('num1').value);
    const num2 = parseFloat(document.getElementById('num2').value);
    displayResult(Math.pow(num1, num2));
}

// Factorial
function factorial() {
    const num1 = parseInt(document.getElementById('num1').value);
    if (num1 < 0) {
        displayResult("Factorial not defined for negative numbers!");
        return;
    }
    let fact = 1;
    for (let i = 1; i <= num1; i++) {
        fact *= i;
    }
    displayResult(fact);
}
