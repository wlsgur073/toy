'use strict';

/*
    1. Function declaration
    function name(param1, param2){body ... return;}
    one function === one thing
    naming : doSomething, command, verb
*/

function printHello(){
    console.log('hello');
}

function log(message){
    console.log(message);
}
log('Hello@');
log(1234);

// 2. Parameters
function changeName(obj){
    obj.name = 'coder';
}
const kim = { name: 'kim' };
changeName(kim);
console.log(kim);

// 3. Default parameters (added in ES6)
function showMessage(msg, from = 'unknown'){
    console.log(`${msg} by ${from}`);
}
showMessage('Hi!');

// 4. Rest parameters (added in ES6)
function printAll(...args){
    for(let i = 0; i < args.length; i++){
        console.log(args[i]);
    }

    for(const arg of args){
        console.log(arg);
    }

    args.forEach((arg) => console.log(arg));
}

printAll('dream', 'coding', 'kim');

// 6. Return a value
// 모든 함수에는 void처럼 return이 따로 없으면
// return undefined로 명시되어 있다.

// First-class function

// 1. Function expression
const print = function () {
    console.log('print');
}; // anonymous function
print();
const again = print;
console.log(again);
// 자바스크립트에서는 function declaration이 hoisting된다.

// 2. Callback function using function expression
function randomQuiz(answer, printYes, printNo) {
    if(answer === 'love you'){
        printYes();
    } else {
        printNo();
    }
}

const printYes = function () {
    console.log('yes!');
}


// named function
// better debugging in debugger's stact traces
// resursions 이름있는 함수는 재귀할때 사용한다.
const printNo = function print () {
    console.log('no!');
}

// Arrow function
// always anonymous
const simplePrint = () => console.log('simple');
const add = (a, b) => a + b;
const simple = (a, b) => {
    //do something more
    return a * b;
}; // {} 블록을 사용하게 되면 return을 명시해줘야 한다.

// IIFE : Immediately Invoked Function Expression
(function hello(){
    console.log('IIFE');
})(); // 함수를 바로바로 실행시킬 수 있다.