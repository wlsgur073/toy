'use strict';

/*
    Object-oriendted programming
    class : template
    object : tnstance of a class
    JavaScript classes
    - introduced in ES6
    - syntactical sugar over prototype-based inheritance
    기존에 존재하던 자바스크립트 프로토타입 위에
    추가된 것이기에 syntactical sugar이라고 한다.
*/

// 1. Class declarations
class Person{
    // constructor
    constructor(name, age){
        // fields
        this.name = name;
        this.age = age;
    }

    // methods
    speak() {
        console.log(`${this.name} : hello!`);
    }
}

const kim = new Person('kim', 20);
console.log(kim.name);
console.log(kim.age);
kim.speak();

// 2. Getter and setters
class User {
    constructor(firstName, lastName, age){
        this.firstName = firstName;
        this.lastName = lastName;
        this.age = age;
    }

    get age(){
        return this._age;
    }

    set age(value){
        // if(value < 0) {
        //     throw Error('age can not be negative');
        // }
        this._age = value < 0 ? 0 : value;
    }
}

const user1 = new User('Steve', 'Job', -1);
console.log(user1.age);

// 3. Fields (public, private)
class Experiment {
    publicField = 2;
    #privateField = 0;
}

const experiment = new Experiment();
console.log(experiment.publicField);
console.log(experiment.privateField); // undefined

// 4. Static properties and methods
class Article {
    static publisher = 'Dream Coding';
    constructor(articleNumber) {
        this.articleNumber = articleNumber;
    }

    static printPublisher() {
        console.log(Article.publisher);
    }
}

// 5. Inheritance
// a way for one class to extend another class.
class Shape {
    constructor(width, height, color){
        this.width = width;
        this.height = height;
        this.color = color;
    }

    draw() {
        console.log(`drawing ${this.color} color of`);
    }

    getArea() {
        return this.width * this.height;
    }
}

class Rectangle extends Shape {}
class Triangle extends Shape {
    draw(){
        super.draw();
        console.log('he');
    }
    getArea() {
        return (this.width * this.height) / 2;
    }

    toString() {
        return `Triangle ${this.color}`;
    }
}
const rectangle = new Rectangle(20, 20, 'blue');
rectangle.draw();
console.log(rectangle.getArea());

const triangle = new Triangle(20, 20, 'red');
triangle.draw();
console.log(triangle.getArea());


// 6. Class checking : instanceOf

console.log(rectangle instanceof Rectangle);
console.log(triangle.toString());