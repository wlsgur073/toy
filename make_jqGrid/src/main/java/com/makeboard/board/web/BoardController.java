package com.makeboard.board.web;

import java.io.BufferedInputStream;
import java.io.BufferedOutputStream;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.net.URLEncoder;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.UUID;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.apache.commons.io.FilenameUtils;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.ResponseBody;
import org.springframework.web.servlet.ModelAndView;

import com.makeboard.board.service.BoardService;
import com.makeboard.board.vo.BoardFileVO;
import com.makeboard.board.vo.BoardVO;

@Controller
public class BoardController {

	private static final Logger logger = LoggerFactory.getLogger(BoardController.class);
	
	@Autowired
	private BoardService boardService;
	
	@ResponseBody
	@RequestMapping(value= "/getBoardDatas", method = RequestMethod.POST)
	public List<BoardVO> getBoardDatas() throws SQLException{
		List<BoardVO> boardList = new ArrayList<>();
		
		boardList = boardService.getAllBoardList();
		
		
		return boardList;
	}
	@RequestMapping(value = "/board", method = RequestMethod.GET)
	public String board(){
		String url = "board";
		
		return url;
	}
	
	@ResponseBody
	@RequestMapping(value = "/boardInsert.do", method = RequestMethod.POST)
	public Map<String, String> boardInsert(BoardVO boardVO) throws SQLException, IllegalStateException, IOException{
		Map<String, String> retMap = new HashMap<>();

//		if file exist
		if(!boardVO.getFile().isEmpty()){
			String filePath = System.getProperty("user.dir") + "\\src\\main\\resources\\static\\files";
			
			UUID uuid = UUID.randomUUID();
			
			String fileName = File.separator + uuid + "_" + boardVO.getFile().getOriginalFilename();
			String fileType =FilenameUtils.getExtension(boardVO.getFile().getOriginalFilename()); 
			File saveFile = new File(filePath, fileName);
			
			if(!saveFile.exists()){
				saveFile.mkdirs();
			}
			
			boardVO.getFile().transferTo(saveFile);
			
//			save the datas to boardFileVO
			BoardFileVO bfVO = new BoardFileVO();
			bfVO.setFileNm(fileName);
			bfVO.setUploadPath(filePath + "\\" + fileName);
			bfVO.setUuid(uuid.toString());
			bfVO.setWriter(boardVO.getBdWriter());
			bfVO.setBdNo(boardVO.getBdNo());
			bfVO.setFileType(fileType);
			boardVO.setBdAttach(filePath + "\\" + fileName);
			
			boardService.insertBoardFileVO(bfVO);
		}
		boardService.addBoard(boardVO);
		
		retMap.put("success", "success");
		
		return retMap;
	}
	
	@RequestMapping(value = "/boardInsert", method = RequestMethod.GET)
	public String boardInsertPage(){
		String url = "boardInsert";
		
		return url;
	}
	
	@RequestMapping(value = "/boardDetail.do", method = RequestMethod.GET)
	public ModelAndView boardDetail(ModelAndView mv, HttpServletRequest req) throws SQLException, IllegalStateException, IOException{
		BoardVO boardVO = new BoardVO();
		
		String bdNo = req.getParameter("bdNo");
		
		boardVO = boardService.getBoardBybdNo(bdNo);
		logger.debug("boardVO.getBdAttach() = > " + boardVO.getBdAttach());
		
		mv.addObject("boardVO", boardVO);
		mv.addObject("fileName", boardVO.getBdAttach().substring(boardVO.getBdAttach().lastIndexOf("_") + 1));
		mv.setViewName("/boardDetail");
		
		return mv;
	}
	
	@RequestMapping(value = "/boardDetail", method = RequestMethod.GET)
	public String boardDetailPage(){
		String url = "boardDetail";
		return url;
	}
	
	@RequestMapping(value = "/boardUpdate", method = RequestMethod.GET)
	public ModelAndView boardUpdatePage(ModelAndView mv, HttpServletRequest req) throws SQLException{
		BoardVO boardVO = new BoardVO();
		
		String bdNo = req.getParameter("bdNo");
		boardVO = boardService.getBoardBybdNo(bdNo);
	
		mv.addObject("boardVO", boardVO);
		mv.setViewName("boardUpdate"); 	
		
		return mv;
	}
	
	@ResponseBody
	@RequestMapping(value = "/boardUpdate.do", method = RequestMethod.POST)
	public Map<String, String> boardUpdate(BoardVO boardVO) throws SQLException, IOException{
		Map<String, String> retMap = new HashMap<>();
		
//		if file exist
		if(!boardVO.getFile().isEmpty()){
			String filePath = System.getProperty("user.dir") + "\\src\\main\\resources\\static\\files";
			
			UUID uuid = UUID.randomUUID();
			
			String fileName = File.separator + uuid + "_" + boardVO.getFile().getOriginalFilename();
			String fileType =FilenameUtils.getExtension(boardVO.getFile().getOriginalFilename()); 
			File saveFile = new File(filePath, fileName);
			
			if(!saveFile.exists()){
				saveFile.mkdirs();
			}
			
			boardVO.getFile().transferTo(saveFile);
			
//			save the datas to boardFileVO
			BoardFileVO bfVO = new BoardFileVO();
			bfVO.setFileNm(fileName);
			bfVO.setUploadPath(filePath + "\\" + fileName);
			bfVO.setUuid(uuid.toString());
			bfVO.setWriter(boardVO.getBdWriter());
			bfVO.setBdNo(boardVO.getBdNo());
			bfVO.setFileType(fileType);
			boardVO.setBdAttach(filePath + "\\" + fileName);
			
			if(boardService.selectBoardFileBybdNo(boardVO.getBdNo()) > 0){
				boardService.updateBoardFileBybdNo(bfVO);
			} else {
				boardService.insertBoardFileVO(bfVO);
			}
		}
		
		boardService.updateBoardBybdNo(boardVO);
		
		retMap.put("success", "success");
		return retMap;
	}
	
	@ResponseBody
	@RequestMapping(value = "/boardDelete", method = RequestMethod.POST)
	public Map<String, String> boardDelete(@RequestParam("bdNo") String bdNo) throws SQLException{
		
		Map<String, String> retMap = new HashMap<>();
		boardService.deleteBoardBybdNo(bdNo);
		boardService.deleteBoardFileBybdNo(bdNo);
		
		retMap.put("success", "success");
		return retMap;
	}
	
	@ResponseBody
	@RequestMapping(value = "/search.do", method = RequestMethod.POST)
	public List<BoardVO> search(@RequestParam("searchTitle") String searchTitle, @RequestParam("searchWriter") String searchWriter, @RequestParam("startDate") String startDate, @RequestParam("endDate") String endDate) throws SQLException{
		List<BoardVO> boardList = new ArrayList<>();
		Map<String, String> searchMap = new HashMap<>();

		searchMap.put("searchTitle", searchTitle);
		searchMap.put("searchWriter", searchWriter);
		searchMap.put("startDate", startDate);
		searchMap.put("endDate", endDate);
		
		boardList = boardService.selectSearchBoardList(searchMap);
		
		return boardList;
	}
	
	@RequestMapping(value = "/download")
	public void fileDownload(HttpServletRequest req, HttpServletResponse resp) throws Exception {
		
		try{
			BoardVO boardVO = new BoardVO();

			String bdNo = req.getParameter("bdNo");
			
			boardVO = boardService.getBoardBybdNo(bdNo);
//			logger.debug("boardVO.getBdAttach() = > " + boardVO.getBdAttach());
			String filePath = boardVO.getBdAttach();
			File file = new File(filePath);
			String fileName = boardVO.getBdAttach().substring(boardVO.getBdAttach().lastIndexOf("_") + 1);
//			logger.debug("fileName => " + fileName);
			int fSize = (int) file.length();
			
			if(fSize > 0){
//				파일명을 URLEncoder 하여 attachment, Content-Disposition Header로 설정
				String encodedFileName = "attachment; filename*=" + "UTF-8" + "''" + URLEncoder.encode(fileName, "UTF-8");
				
				// ContentType 설정
				resp.setContentType("application/octet-stream; charset=utf-8");
				
				// header 설정
				resp.setHeader("Content-Disposition", encodedFileName);
				
				// ContentLength 설정
				resp.setContentLengthLong(fSize);
				
				BufferedInputStream in = null;
				BufferedOutputStream out = null;
				
				in = new BufferedInputStream(new FileInputStream(file));
				out = new BufferedOutputStream(resp.getOutputStream());
				
				try {
					byte[] buffer = new byte[4096];
					int bytesRead = 0;
					
					while((bytesRead = in.read(buffer)) != -1){
						out.write(buffer, 0, bytesRead);
					}
					out.flush();
				} finally {
					in.close();
					out.close();
				}
			} else {
				resp.getWriter().print("<script>location.href='/board'</script>");
				throw new FileNotFoundException("파일이 없습니다.");
			}
		} catch(Exception e){
			logger.info(e.getMessage());
		}
		
	}
	
}
