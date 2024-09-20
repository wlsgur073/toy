const loginBtn = document.querySelector('.loginBtn');
const userId = document.querySelector('#userId');
const userPw = document.querySelector('#userPw');

loginBtn.addEventListener('click', () => {

  checkInputs();
  if(!checkInputError()){
    return false;
  }
  $.ajax({
    type: "post",
    url: "/login.do",
    data: {
      "userId" : userId.value,
      "userPw" : userPw.value
    },
    dataType: "json",
    success: function (resp) {
    	 if(resp.fail === "fail"){
    		 userId.value = "";
    		 userPw.value = "";
    	        Swal.fire({
    	          icon : "error",
    	          title : "아이디나 또는 비밀번호를 정확히 입력하세요.",
								showConfirmButton : false,
								timer : 1500
    	        });
    	      } else {
    	    		// add_welcome_message(userId.value);
    	        Swal.fire({
								title : '환영합니다!',
								icon : 'success',
								showConfirmButton : false,
								timer : 1500
							});
							location.href = '/';
    	      }
    },
    error : function(err){
      console.log("로그인 실패 : " + err.status);
    }
  });

});


function setErrorFor(input, message) {
	const formControl = input.parentElement;
	const small = formControl.querySelector('small');
	formControl.className = 'form-group validation error';
	small.innerText = message;
}

function setSuccessFor(input) {
	const formControl = input.parentElement;
	formControl.className = 'form-group validation success';
}

function checkInputs() {
	const idValue = userId.value.trim();
	const pwValue = userPw.value.trim();
	
	if(idValue === "") {
		setErrorFor(userId, '아이디를 입력하세요.');
	} else {
		setSuccessFor(userId);
	}
	
	if(pwValue === '') {
		setErrorFor(userPw, '비밀번호를 입력하세요.');
	} else {
		setSuccessFor(userPw);
	}
}

function checkInputError() {
  let checkError = true;
  
  for (let i = 0; i < $('#loginForm div').length; i++) {
    let temp = $('#loginForm div:eq('+i+')').attr('class');
    if(temp === "form-group validation error"){
      checkError = false;
    }
  }
  
  return checkError;
}

function isPwd(pass) {
  return /^(?=.*[a-zA-Z])(?=.*[0-9])(?=.*[!@#$%^&*])[a-zA-Z0-9!@#$%^&*]{8,16}$/.test(pass);  
}
function isName(username) {
  return /^(?=.*[a-zA-Z0-9가-힣])[a-zA-Z0-9가-힣]{2,20}$/.test(username);
}