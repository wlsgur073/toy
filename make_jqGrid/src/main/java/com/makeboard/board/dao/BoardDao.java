package com.makeboard.board.dao;

import java.sql.SQLException;
import java.util.List;
import java.util.Map;

import com.makeboard.board.vo.BoardFileVO;
import com.makeboard.board.vo.BoardVO;

/**
 * board 테이블 dao
 * @author 김진혁
 * @since 2022-03-03
 */
public interface BoardDao {
	
	public List<BoardVO> selectAllBoardList() throws SQLException;
	
	public void insertBoard(BoardVO boardVO) throws SQLException;
	
	public BoardVO selectBoardBybdNo(String bdNo) throws SQLException;
	
	public void updateBoardBybdNo(BoardVO boardVO) throws SQLException;
	
	public void deleteBoardBybdNo(String bdNo) throws SQLException;
	
	public void insertBoardFileVO(BoardFileVO bfVO) throws SQLException;
	
	public void updateBoardFileBybdNo(BoardFileVO bfVO) throws SQLException;
	
	public void deleteBoardFileBybdNo(String bdNo) throws SQLException;
	
	public int selectBoardFileBybdNo(String bdNo) throws SQLException;
	
	public List<BoardVO> selectSearchBoardList(Map<String, String> searchMap) throws SQLException;
	
}
