<%@ page language="java" contentType="text/html; charset=UTF-8"
    pageEncoding="UTF-8"%>
<%@ taglib uri="http://java.sun.com/jsp/jstl/core" prefix="c" %>
<!-- board detail -->
      <div class="container">
        <h2>게시글 상세 화면</h2>
        <form id="board_insert_form">
          <table class="board_detail" style="width: 1019px;" border="1">
            <colgroup>
              <col width="10%">
              <col width="30%">
              <col width="10%">
              <col width="15%">
            </colgroup>
            <tbody>
              <tr>
                <td scope="row">제목</td>
                <td>
                  ${boardVO.bdTitle}
                </td>
                <td scope="row">등록자</td>
                <td>${boardVO.bdWriter}</td>
              </tr>
              <tr>
                <th scope="row">내용</th>
                <td colspan="5">
                  <textarea name="bdContent" id="" cols="125" rows="15" readonly>${boardVO.bdContent}</textarea>
                </td>
              </tr>
              <tr>
                <th scope="row">첨부파일</th>
                <td colspan="5">
                  <c:choose>
                  	<c:when test="${not empty boardVO.bdAttach}">
                  		<a href="/download?bdNo=${boardVO.bdNo}">${fileName}</a>
                  	</c:when>
                  	<c:otherwise>
 		                 <input type="file" id="bdAttach" name="file" readonly/>
                  	</c:otherwise>
                  </c:choose>
                </td>
              </tr>
            </tbody>
          </table>

        </form>

        <div class="buttonList">
          <button type="button" class="updateBtn">수정</button>
          <button type="button" class="answerBtn">답변</button>
          <button type="button" class="goList" onclick="location.href='/board'">목록</button>
        </div>

      </div>
      
<script>
    
    document.querySelector('.updateBtn').addEventListener('click', () => {
      location.href = `/boardUpdate?bdNo=${boardVO.bdNo}`;
    });
</script>