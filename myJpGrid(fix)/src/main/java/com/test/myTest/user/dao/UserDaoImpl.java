package com.test.myTest.user.dao;

import java.sql.SQLException;

import org.apache.ibatis.session.SqlSession;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Repository;

import com.test.myTest.user.vo.UserVO;

@Repository
public class UserDaoImpl implements UserDao{

	@Autowired
	SqlSession sqlSession;
	
	private String namespace = "User-Mapper.";
	
	@Override
	public UserVO selectUserVOById(String userId) throws SQLException {
		return sqlSession.selectOne(namespace + "selectUserVOById", userId);
	}

}
