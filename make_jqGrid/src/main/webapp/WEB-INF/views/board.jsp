<%@ page language="java" contentType="text/html; charset=UTF-8"
    pageEncoding="UTF-8"%>
<head>
	<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/jqueryui/1.12.1/jquery-ui.min.css">
	<link rel="stylesheet" href="/resources/jqGrid/css/ui.jqgrid.css">
</head>
<body>

	<article>
        <div class="search">
            <label for="search_title">제목&nbsp;:&nbsp;</label>
            <input type="text" id="search_title" name="search_title" placeholder="search..." onkeydown="doSearch(arguments[0]||event)"/>
            <label for="search_writer">작성자&nbsp;:&nbsp;</label>
            <input type="text" id="search_writer" name="search_writer" placeholder="search..." onkeydown="doSearch(arguments[0]||event)"/>
            <label for="start_date">등록일&nbsp;:&nbsp;</label>
            <input type="date" name="start_date" id="start_date">
            &nbsp;~&nbsp;
            <input type="date" name="end_date" id="end_date">
            <input type="button" value="조회" id="search" onclick="gridReload();">
        </div>
		<!-- jqGrid -->
		<div class="board">
			<div class="boardBtnDiv">
				<button type="button" class="detailBtn">상세보기</button>
				<button type="button" class="insertBtn"
					onclick="location.href='/boardInsert'">등록</button>
			</div>
			<div class="tableTest">
				<table id="mainGrid"></table>
				<div id="pager"></div>
			</div>
		</div>
	</article>
<script src="/resources/jqGrid/js/minified/jquery.jqGrid.min.js"></script>
<script src="/resources/jqGrid/js/i18n/grid.locale-kr.js"></script>
<script src="/resources/js/board.js"></script>
</body>
