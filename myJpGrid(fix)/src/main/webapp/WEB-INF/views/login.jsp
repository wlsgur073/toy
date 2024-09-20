<%@ page language="java" contentType="text/html; charset=UTF-8"
    pageEncoding="UTF-8"%>
<div class="login">
    <form id="loginForm" class="login_form" onsubmit="return false;">

        <div class="form_group validation">
            <label for="userId">아이디 : </label>
            <input type="text" name="userId" id="userId" autocomplete="off"> <br/>
            <small>error message</small>
        </div>

        <div class="form_group validation">
            <label for="userPw">비밀번호 : </label>
            <input type="password" name="userPw" id="userPw"> <br/>
            <small>error message</small>
        </div>

        <input type="submit" id="loginBtn" value="로그인">
    </form>
</div>

<script src="/resources/assets/js/login.js"></script>