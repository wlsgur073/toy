package com.makeboard.board.dao;

import java.sql.SQLException;
import java.util.List;
import java.util.Map;

import org.apache.ibatis.session.SqlSession;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Repository;

import com.makeboard.board.vo.BoardFileVO;
import com.makeboard.board.vo.BoardVO;
import com.makeboard.user.web.UserController;

@Repository
public class BoardDaoImpl implements BoardDao{

	@Autowired
	private SqlSession sqlSession;
	
	@Override
	public List<BoardVO> selectAllBoardList() throws SQLException {
		return sqlSession.selectList("Board-Mapper.selectAllBoardList");
	}

	@Override
	public void insertBoard(BoardVO boardVO) throws SQLException {
		sqlSession.insert("Board-Mapper.insertBoard", boardVO);
	}

	@Override
	public BoardVO selectBoardBybdNo(String bdNo) throws SQLException {
		return sqlSession.selectOne("Board-Mapper.selectBoardBybdNo", bdNo);
	}

	@Override
	public void updateBoardBybdNo(BoardVO boardVO) throws SQLException {
		sqlSession.update("Board-Mapper.updateBoardBybdNo", boardVO);
	}

	@Override
	public void deleteBoardBybdNo(String bdNo) throws SQLException {
		sqlSession.delete("Board-Mapper.deleteBoardBybdNo", bdNo);
	}

	@Override
	public void insertBoardFileVO(BoardFileVO bfVO) throws SQLException {
		sqlSession.insert("Board-Mapper.insertBoardFileVO", bfVO);
	}

	@Override
	public void updateBoardFileBybdNo(BoardFileVO bfVO) throws SQLException {
		sqlSession.update("Board-Mapper.updateBoardFileBybdNo", bfVO);
	}

	@Override
	public void deleteBoardFileBybdNo(String bdNo) throws SQLException {
		sqlSession.delete("Board-Mapper.deleteBoardFileBybdNo", bdNo);
	}

	@Override
	public int selectBoardFileBybdNo(String bdNo) throws SQLException {
		return (Integer)sqlSession.selectOne("Board-Mapper.selectBoardFileBybdNo", bdNo) == null ? 0 : 1;
	}

	@Override
	public List<BoardVO> selectSearchBoardList(Map<String, String> searchMap) throws SQLException {
		return sqlSession.selectList("Board-Mapper.selectSearchBoardList", searchMap);
	}

}
