<%@ page language="java" contentType="text/html; charset=UTF-8"
    pageEncoding="UTF-8"%>
<div class="login">
	<form id="loginForm">
		<div class="form-group validation">
			<label for="userId">아이디 : </label>
			<input type="text" id="userId" name="userId" autocomplete="off">
			<small>Error message</small>
		</div>

		<div class="form-group validation">
			<label for="userPw">비밀번호 : </label>
			<input type="password" id="userPw" name="userPw">
			<small>Error message</small>
		</div>
	</form>
	<button type="button" form="loginForm" class="loginBtn">로그인</button>
</div>


<script src="/resources/js/login.js"></script>