<%@ page language="java" contentType="text/html; charset=UTF-8"
    pageEncoding="UTF-8"%>
<!-- board detail -->
      <div class="container">
        <h2>게시글 수정 화면</h2>
        <form id="board_update_form">
          <table class="board_detail" style="width: 1019px;" border="1">
            <colgroup>
              <col width="10%">
              <col width="30%">
              <col width="10%">
              <col width="15%">
              <col width="10%">
              <col width="10%">
            </colgroup>
            <tbody>
              <tr>
                <td scope="row">제목</td>
                <td>
                  <input type="text" name="bdTitle" value="${boardVO.bdTitle}">
                </td>
                <td scope="row">등록자</td>
                <td>
                  ${boardVO.bdWriter}
                </td>
                <td scope="row">비밀번호</td>
                <td>
                  <input type="text" name="bdPw" value="${boardVO.bdPw}">
                </td>
              </tr>
              <tr>
                <th scope="row">내용</th>
                <td colspan="5">
                  <textarea name="bdContent" id="" cols="125" rows="15">${boardVO.bdContent}</textarea>
                </td>
              </tr>
              <tr>
                <th scope="row">첨부파일</th>
                <td colspan="5">
                  <input type="file" id="bdAttach" name="file" value="${boardVO.file}"/>
                </td>
              </tr>
            </tbody>
          </table>
          <input type="hidden" name="bdNo" id="bdNo">
        </form>

        <div class="buttonList">
          <button type="button" class="saveBtn" form="boardFrm">저장</button>
          <button type="button" class="deleteBtn">삭제</button>
          <button type="button" class="goList" onclick="location.href='/board'">목록</button>
        </div>
      </div>
      
      <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery.form/4.3.0/jquery.form.min.js"></script>
<script>
    const saveBtn = document.querySelector('.saveBtn');
    const deleteBtn = document.querySelector('.deleteBtn');
    const boardFrm = document.getElementById('boardFrm');
    
    let query = window.location.search;
    let param = new URLSearchParams(query);
    let bdNo = param.get('bdNo');

    document.getElementById('bdNo').value = bdNo;
    
    saveBtn.addEventListener('click', () => {
      $.ajax({
        type: "post",
        url: "/boardUpdate.do",
        data: new FormData($('#board_update_form')[0]),
        processData: false,
        contentType: false,
        success: function (resp) {
          if(resp.success === "success"){
            alert("게시글이 수정되었습니다.")
            location.href = "/board";
          }
        },
        error: function (err) {
          console.log("boardInsert error : " + err.status);
        }
      });
    });

    deleteBtn.addEventListener('click', () => {
      $.ajax({
        type: "post",
        url: "/boardDelete",
        enctype: 'multipart/form-data',
        data: {"bdNo" : bdNo},
        success: function (resp) {
          if(resp){
            alert("게시글이 삭제되었습니다.");
            location.href = "/board";
          }
        },
        error: function name(err) {
          console.error("boardDelete error : " + err.status);
        }
      });
    });

</script>