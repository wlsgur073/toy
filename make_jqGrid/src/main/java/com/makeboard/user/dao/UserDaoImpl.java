package com.makeboard.user.dao;

import java.sql.SQLException;

import org.apache.ibatis.session.SqlSession;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Repository;

import com.makeboard.user.vo.UserVO;

@Repository
public class UserDaoImpl implements UserDao{

	@Autowired
	private SqlSession sqlSession;
	
//	public void setSqlSession(SqlSession sqlSession) {
//		this.sqlSession = sqlSession;
//	}

	@Override
	public UserVO selectUserById(String userId) throws SQLException {
		return sqlSession.selectOne("User-Mapper.selectUserById", userId);
	}

	
}
