// 1. String concatenation
console.log('my' + 'cat');
console.log('1' + 2);
console.log(`string literals: 1 + 2 = ${1 + 2}`);

// 2. Numeric operators
console.log(1 + 1); // add ...

// 3. increment add decrement operators
let counter = 1;
const post = counter++;

// 4. Assignment operators
let x = 3;
let y = 6;
x += y;

// 5. Comparison operators
console.log(10 < 6);

/*
    6. Logical operators : ||, &&, !
    console.log(`or: ${value1 || value2 || check()}`);
    위처럼 연산이 많은 함수가 논리 연산 조건 순서상 뒤에 있다면,
    가장 늦게 실행되기에 좋지 못하다. 그러니 함수의 경우에는 항상
    우선적으로 논리 연산을 검사하도록 하자.
*/

// 7. Equality
const stringFive = '5';
const numberFive = 5;

// == loose equality, with type conversion
console.log(stringFive == numberFive);
console.log(stringFive != numberFive);

// === strict equality, no type conversion
console.log(stringFive === numberFive);
console.log(stringFive !== numberFive);

// object equality by reference
const kim1 = { name: 'kim'};
const kim2 = { name: 'kim'};
const kim3 = kim1;
console.log(kim1 == kim2);
console.log(kim1 === kim2);
console.log(kim1 === kim3);
// kim1과 kim2는 데이터가 같아보이지만
// 실제로 메모리는 각각 다른 주소를 가지고 있게 된다. 



