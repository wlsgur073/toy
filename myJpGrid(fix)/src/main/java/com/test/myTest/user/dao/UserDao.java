package com.test.myTest.user.dao;

import java.sql.SQLException;

import com.test.myTest.user.vo.UserVO;

public interface UserDao {

	public UserVO selectUserVOById(String userId) throws SQLException;

}
