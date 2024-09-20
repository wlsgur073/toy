<%@ page language="java" contentType="text/html; charset=UTF-8" pageEncoding="UTF-8"%>
<div class="menu">
	<ul>
		<c:choose>
			<c:when test="${not empty userVO }">
				<li><a href="/logout">로그아웃</a></li>
			</c:when>
			<c:otherwise>
				<li><a href="/login">로그인</a></li>
			</c:otherwise>
		</c:choose>
		<li><a href="#">게시판</a></li>
	</ul>
</div>