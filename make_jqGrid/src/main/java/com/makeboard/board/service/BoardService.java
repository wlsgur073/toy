package com.makeboard.board.service;

import java.sql.SQLException;
import java.util.List;
import java.util.Map;

import com.makeboard.board.vo.BoardFileVO;
import com.makeboard.board.vo.BoardVO;

public interface BoardService {

	public List<BoardVO> getAllBoardList() throws SQLException;
	
	public void addBoard(BoardVO boardVO) throws SQLException;
	
	public BoardVO getBoardBybdNo(String bdNo) throws SQLException;
	
	public void updateBoardBybdNo(BoardVO boardVO) throws SQLException;
	
	public void deleteBoardBybdNo(String bdNo) throws SQLException;
	
	public void insertBoardFileVO(BoardFileVO bfVO) throws SQLException;
	
	public void updateBoardFileBybdNo(BoardFileVO bfVO) throws SQLException;
	
	public void deleteBoardFileBybdNo(String bdNo) throws SQLException;
	
	public int selectBoardFileBybdNo(String bdNo) throws SQLException;

	public List<BoardVO> selectSearchBoardList(Map<String, String> searchMap) throws SQLException;
	
}
