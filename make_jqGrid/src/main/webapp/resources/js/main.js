

let nickname = "";

$(document).ready(function () {
	if(window.location.pathname !== "/login"){
		$('.welcome_message').css('display', 'block');
	}
});

function logout(){
	location.href="/logout";
}


function add_welcome_message(nickname) { 
	  let div = document.createElement('div');
	  div.className = 'welcome_message';
	  let text = document.createTextNode(`${nickname}님이 로그인 했습니다.`);
	  div.appendChild(text);
	  document.body.appendChild(div);
}
