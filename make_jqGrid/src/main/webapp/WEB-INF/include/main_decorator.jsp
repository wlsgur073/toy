<%@ page language="java" contentType="text/html; charset=UTF-8"
    pageEncoding="UTF-8"%>
<%@ taglib prefix="decorator" uri="http://www.opensymphony.com/sitemesh/decorator" %>
<%@ taglib uri="http://java.sun.com/jsp/jstl/core" prefix="c" %>

<!DOCTYPE html>
<html lang="ko">
<head>
  <meta charset="UTF-8">
  <meta http-equiv="X-UA-Compatible" content="IE=edge">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <title>김진혁의 게시판</title>
  <link rel="stylesheet" href="<c:url value='/resources/css/main.css'/>"/>
  <script src="https://code.jquery.com/jquery-3.6.0.js" integrity="sha256-H+K7U5CnXl1h5ywQfKtSj8PCmoN9aaq30gDh27Xc0jk=" crossorigin="anonymous"></script>
  <decorator:head />
</head>
<body>
 <%@ include file="/WEB-INF/include/header.jsp" %>

  <main>
	<%@ include file="/WEB-INF/include/menu.jsp" %>
	
    <section>
		<decorator:body />
    </section>
    
    <c:if test="${not empty userVO}">
      <div class="welcome_message">
        ${userVO.nickname}님이 로그인 했습니다.
      </div>
    </c:if>
  </main>
</body>
  <script src="//cdn.jsdelivr.net/npm/sweetalert2@11"></script>
  <script src="<c:url value='/resources/js/main.js'/>"></script>
</html>

