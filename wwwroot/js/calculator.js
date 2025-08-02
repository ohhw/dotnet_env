// calculator.js - 계산기 관련 JavaScript

let display = document.getElementById('display');
let currentInput = '';
let operator = null;
let firstOperand = null;
let waitingForOperand = false;

// 숫자 입력 함수
function inputNumber(num) {
    if (waitingForOperand) {
        display.value = num;
        waitingForOperand = false;
    } else {
        display.value = display.value === '0' ? num : display.value + num;
    }
}

// 연산자 입력 함수
function inputOperator(nextOperator) {
    const inputValue = parseFloat(display.value);

    if (firstOperand === null) {
        firstOperand = inputValue;
    } else if (operator) {
        const currentValue = firstOperand || 0;
        const newValue = performCalculation[operator](currentValue, inputValue);

        display.value = String(newValue);
        firstOperand = newValue;
    }

    waitingForOperand = true;
    operator = nextOperator;
}

// 계산 수행 객체
const performCalculation = {
    '/': (firstOperand, secondOperand) => firstOperand / secondOperand,
    '*': (firstOperand, secondOperand) => firstOperand * secondOperand,
    '+': (firstOperand, secondOperand) => firstOperand + secondOperand,
    '-': (firstOperand, secondOperand) => firstOperand - secondOperand,
    '=': (firstOperand, secondOperand) => secondOperand
};

// 등호(=) 버튼 처리
function calculate() {
    const inputValue = parseFloat(display.value);

    if (firstOperand !== null && operator && !waitingForOperand) {
        const newValue = performCalculation[operator](firstOperand, inputValue);
        display.value = String(newValue);
        firstOperand = null;
        operator = null;
        waitingForOperand = true;
    }
}

// 초기화 함수
function clearCalculator() {
    display.value = '0';
    firstOperand = null;
    operator = null;
    waitingForOperand = false;
}

// 백스페이스 함수
function backspace() {
    if (display.value.length > 1) {
        display.value = display.value.slice(0, -1);
    } else {
        display.value = '0';
    }
}

// 소수점 입력
function inputDecimal() {
    if (waitingForOperand) {
        display.value = '0.';
        waitingForOperand = false;
    } else if (display.value.indexOf('.') === -1) {
        display.value += '.';
    }
}

// 키보드 이벤트 처리 (전역적으로 사용 가능하도록)
function setupCalculatorKeyboard() {
    document.addEventListener('keydown', function(event) {
        const key = event.key;
        
        // 숫자 키
        if (key >= '0' && key <= '9') {
            event.preventDefault();
            inputNumber(key);
        }
        
        // 연산자 키
        if (['+', '-', '*', '/'].includes(key)) {
            event.preventDefault();
            inputOperator(key);
        }
        
        // Enter 또는 = 키로 계산
        if (key === 'Enter' || key === '=') {
            event.preventDefault();
            calculate();
        }
        
        // Escape 키로 초기화
        if (key === 'Escape') {
            event.preventDefault();
            clearCalculator();
        }
        
        // Backspace 키
        if (key === 'Backspace') {
            event.preventDefault();
            backspace();
        }
        
        // 소수점
        if (key === '.') {
            event.preventDefault();
            inputDecimal();
        }
    });
}

// 페이지 로드 시 키보드 이벤트 설정
document.addEventListener('DOMContentLoaded', function() {
    display = document.getElementById('display');
    if (display) {
        setupCalculatorKeyboard();
    }
});
