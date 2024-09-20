'use strict';

const name = 'kim';
const age = 4;
print(name, age);
function print(name, age){
    console.log(name);
    console.log(age);
}

// 매개변수가 많으면 많아질수록 호출하는데 까다롭다.
// 그러기에 object를 쓰는 것이다.

// 1. Literals and properties
// object = { key : value };

const kim = { _name: 'kim', _age : 4};
function _print(person) {
    console.log(person._name);
    console.log(person._age);
}
_print(kim);

const obj1 = {};            // 'object literal' syntax
const obj2 = new Object();  // 'object constructor' syntax

// 2. Computed properties
// key should be alwawys string type
console.log(kim._name);
console.log(kim['_name']); 
kim['hasJob'] = true;
console.log(kim.hasJob);
// 실시간으로 원하는 key의 값을 받아오고 싶다면
// 그 때는 Computed property를 사용한다.
function printValue(obj, key) {
    console.log(obj[key]);
}
printValue(kim, '_name');

// 3. Property value shorthand
const person1 = { name: 'bob', age: 2 };
const person2 = { name: 'steve', age: 3 };
const person3 = { name: 'dave', age: 4 };
// person ... 너무 반복된다.

// 4. constructor function
function Person(name, age) {
    this.name =name;
    this.age = age;
}

// 5. in operator : property existence check (key in obj)
console.log('name' in kim);
console.log('_age' in kim);
console.log('random' in kim);

// 6. for..in vs for..of
// for (key in obj)
console.clear();
for(let key in kim){
    console.log(key);
}

// for(value of iterable)
const arr = [1, 2, 3, 4];
for (let value of arr){
    console.log(value);
}

// 7. Cloning
// Object.assign(dest, [obj1, obj2, obj3 ...])
const user = { name : 'kim', age: '20' };
const user2 = user;
user2.name = 'coder';
console.log(user); // user2의 레퍼런스는 user와 같기때문이다.

// old way
const user3 = {};
for (let key in user){
    user3[key] = user[key];
}
console.clear();
console.log(user3);

// present way
const user4 = Object.assign({}, user);
console.log(user4);