'use strict';

/*
    1. Variable, rw(read/write)
    let (added in ES6)
    요즘은 ES6이전의 문법은 잘 사용하지 않는다.

    <var vs let>
    자바스크립트 var는 선언하기 전에 값을
    초기화할 수 있다(var hoisting).
    hoisting : move declaration from bottom to top

    let은 그런 기능이 불가능하다.
*/


/*
    2. Constant, r(read only)
    use const whenever possible.
    only use let if variable needs to change.

    
    Mutable data type을 Immutable data type으로 만든다.
    자바스크립트에서는 data type이 두 가지이다.
    Mutable type의 'let'과, Immutable type의 const가 있다.
*/

/*
    Note!
    Immutable data types: primitive types, frozen objects (i.e. object.freeze())
    Mutable data types: all objects by default are mutable in JS
    favor immutable data type always for a few reasons:
    - Security
    - Thread safety
    - Reduce human mistakes
*/
/*
    3. Variable types
    primitive, single item: number, string, boolean, null, symbol ..
    object, box container
    function, first-class function

    undefined와 null은 자바스크립트에서 개념이 다르다.
    null은 텅텅 비어있는 상태이고,
    undefined는 아직 초기화가 되어 있지 않은 상태를 의미한다.

    symbol, creat unique identifiers for objects
    심볼은 다른 자료구조에서 고유한 식별자가 필요하거나,
    동시 다발적으로 일어날 수 있는 코드에서 우선순위를 주기 위해
    사용한다.

    ex.)
    const symbol1 = Symbol('id');
    const symbol2 = Symbol('id');
    console.log(symbol1 === symbol2); -> 같지 않다.

    const gSymbol1 = Symbol.for('id');
    const gSymbol2 = Symbol.for('id');
    이 두가지는 같다.
    심볼은 항상 description을 이용해서 string으로 변환해서 사용한다.
    console.log(`value : ${symbol1.description}`);
*/

/*
    Object, real-life object, data sructure
    const kim = {name: 'kim', age:20 };
    kim.age = 21;
*/

/*
    5. Dynamic typing = Javascript
*/
