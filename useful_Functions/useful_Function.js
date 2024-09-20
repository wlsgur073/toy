'use strict';

// shift() 방식으로 [0]번째 요소를 계속 삭제하여 해당 NodeList를 비워준다. 
function removeElementsByName(name){
	const elements = document.getElementsByName(name);
	while(elements.length > 0){
		elements[0].parentNode.removeChild(elements[0]);
	}
}
