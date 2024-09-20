let searchResultColNames = ['순서', '제목', '작성자', '등록일'];
let searchResultColModel = [
  { name: "bdNo", index: "bdNo", align: "center", width : "12%" },
  { name: "bdTitle", index: "bdTitle", align: "left", width: "50%" },
  { name: "bdWriter", index: "bdWriter", align: "center", width: "12%" },
  { name: "bdRegdate", index: "bdRegdate", align: "center", width: "14%", formatter:'date', formatoptions: {srcformat: 'U/1000', newformat:'Y/m/d H:i:s'}},
];


$(document).ready(function () {
  test();
});


function test(){	
	$("#mainGrid").jqGrid({
    url : '/getBoardDatas',
		datatype : "json",
		mtype: "post",
		colNames: searchResultColNames,
		colModel: searchResultColModel,
		pager: "#pager",
		emptyrecords : "데이터가 없습니다.",
		caption: "게시글 목록",
		rowNum  : 10,
		multiselect : true,
		multiboxonly : true,
		viewrecords : true,
		loadonce:true,
//		sortname : 'bdNo',
//		sortorder : "desc",
//		sortable:true,
		width: 1019,
		height: 261,
	});
}

$('.detailBtn').click(function (e) { 
  e.preventDefault();
  let info = $("#mainGrid").jqGrid("getGridParam", "selrow");
  let rowInfo = $("#mainGrid").jqGrid('getRowData', info);
  if(rowInfo.bdNo == null) return false;
  location.href = "/boardDetail.do?bdNo=" + rowInfo.bdNo;
});

var timeoutHnd;
var flAuto = true;

function doSearch(ev){
	if(!flAuto)
		return;
	if(timeoutHnd)
		clearTimeout(timeoutHnd)
	timeoutHnd = setTimeout(gridReload,500)
}

function gridReload(){
	let searchTitle = $("#search_title").val();
	let searchWriter = $("#search_writer").val();
	let startDate = $('#start_date').val();
	let endDate = $('#end_date').val();

	// $("#mainGrid").jqGrid('setGridParam',{url:"/search.do?searchTitle="+searchTitle+"&searchWriter="+searchWriter + "&startDate=" + startDate + "&endDate=" + endDate, page:1});
	$("#mainGrid").jqGrid('setGridParam',{url:"/search.do?searchTitle="+searchTitle+"&searchWriter="+searchWriter + "&startDate=" + startDate + "&endDate=" + endDate, page:1}).trigger("reloadGrid");
}
