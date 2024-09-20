package com.test.myTest.user.service;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.test.myTest.user.dao.UserDao;
import com.test.myTest.user.vo.UserVO;

@Service
public class UserServiceImpl implements UserService{

	@Autowired
	UserDao userDao;
	
	@Override
	public UserVO getUserVOById(String userId) throws Exception {
		UserVO userVO = null;

		userVO = userDao.selectUserVOById(userId);
		
		if (userVO == null) {
			userVO = new UserVO();
			userVO.setUserId("");
			userVO.setUserPw("");
		}
		return userVO;
	}

}
