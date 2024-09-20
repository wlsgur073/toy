const loginForm = document.getElementById('loginForm');
const userId = document.getElementById('userId');
const userPw = document.getElementById('userPw');

document.querySelector('#loginBtn').addEventListener('click',() => {
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
		success: function (resp) {
			if(resp.fail === "fail"){
				userId.value = "";
				userPw.value = "";
				Swal.fire({
					icon: "error",
					title: "아이디나 또는 비밀번호를 정확히 입력하세요.",
					showConfirmButton: false,
					timer: 1500,
				});
				} else {
					Swal.fire({
						title: "환영합니다!",
						icon: "success",
						showConfirmButton: false,
						timer: 1000,
					});
					setTimeout(() => {
						location.href = "/";
					}, 1000);
				}
			}
		});
	});


function checkInputs() {
	// trim to remove the whitespaces
	const userId_val = userId.value.trim();
	const userPw_val = userPw.value.trim();
	
	if(userId_val === "") {
		setErrorFor(userId, '아이디를 입력하세요.');
	} else {
		setSuccessFor(userId);
	}
	
	if(userPw_val === '') {
		setErrorFor(userPw, '비밀번호를 입력하세요.');
	} else {
		setSuccessFor(userPw);
	}
}

function isPwd(pass) {
  return /^(?=.*[a-zA-Z])(?=.*[0-9])(?=.*[!@#$%^&*])[a-zA-Z0-9!@#$%^&*]{8,16}$/.test(pass);  
}
function isName(username) {
  return /^(?=.*[a-zA-Z0-9가-힣])[a-zA-Z0-9가-힣]{2,16}$/.test(username);
}
function setErrorFor(input, message) {
	const formControl = input.parentElement;
	const small = formControl.querySelector('small');
	formControl.className = 'form_group validation error';
	small.innerText = message;
}

function setSuccessFor(input) {
	const formControl = input.parentElement;
	formControl.className = 'form_group validation success';
}

function checkInputError() {
	let flag = true;
	
	for (let i = 0; i < $('.login .login_form div').length; i++) {
		let temp = $('.login .login_form div:eq('+i+')').attr('class');
		if(temp === "form_group validation error"){
			flag = false;
		}
	}
	
	return flag;
}