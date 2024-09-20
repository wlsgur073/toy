package com.makeboard.user.service;

import com.makeboard.user.vo.UserVO;

/**
 * User의 service를 다룹니다.
 * @author 김진혁
 * @since 2022-03-29
 */
public interface UserService {
	
	public UserVO getUserVOById(String userId) throws Exception;

}
