'use strict';

const arr = [1, 2, 3, 4, 5];

// Addtion, deletion, copy
// push : add an item to the end
arr.push(6);
console.log(arr);
// pop : remove an item from the end
arr.pop();
console.log(arr);

// shift : remove an item from the benigging
arr.shift();
console.log(arr);

// unshift : add an item to the benigging
arr.unshift(1);
console.log(arr);
// note! shift, unshift are slower than pop, push

// splice : remove an item by index position
arr.push('hello', 'world');
console.log(arr);
arr.splice(1, 1);
console.log(arr);
arr.splice(1, 1, '2');
console.log(arr);

// combine two arrays
const arr2 = ['lotto', 'billion'];
const newArr = arr.concat(arr2);
console.log(newArr);

// Searching
// find the index
console.clear();
console.log(arr);
console.log(arr.indexOf('hello'));
console.log(arr.includes('hello'));
console.log(arr.includes('he'));

// lastIndexOf
console.clear();
arr.push(1);
console.log(arr);
console.log(arr.indexOf(1));
console.log(arr.lastIndexOf(1));
