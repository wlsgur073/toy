package com.makeboard.user.dao;

import java.sql.SQLException;

import com.makeboard.user.vo.UserVO;

/**
 * USER 테이블의 DAO입니다.
 * @author 김진혁
 * @since 2022-03-29
 */
public interface UserDao {
	
	/**
	 * String 타입의 파라미터를 받아 UserVO를 리턴한다.
	 * @param userId
	 * @return UserVO
	 * @throws SQLException
	 */
	public UserVO selectUserById(String userId) throws SQLException;
	
}
