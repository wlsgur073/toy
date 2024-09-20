package com.makeboard.user.service;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.makeboard.user.dao.UserDao;
import com.makeboard.user.vo.UserVO;

/**
 * @author 김진혁
 * @since 2022-03-29
 */
@Service
public class UserServiceImpl implements UserService{

	@Autowired
	private UserDao userDao;
	
	public void setUserDao(UserDao userDao) {
		this.userDao = userDao;
	}

	@Override
	public UserVO getUserVOById(String userId) throws Exception {
		UserVO userVO = null;

		userVO = userDao.selectUserById(userId);
		if (userVO == null) {
			userVO = new UserVO();
		}
		return userVO;
	}

}
