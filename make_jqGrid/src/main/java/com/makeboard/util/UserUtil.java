package com.makeboard.util;

import org.springframework.beans.factory.annotation.Autowired;

import com.makeboard.user.service.UserService;

public class UserUtil {

	@Autowired
	private UserService userService;
	
	/**
	 * pass2와 pass1이 일치하는지 비교하는 boolean 함수
	 * @param pass1
	 * @param pass2
	 * @return boolean
	 */
	public boolean comparePwd(String pass1, String pass2){
		boolean flag = false;
		
		if(pass1 == pass2){
			flag = true;
		}
		
		return flag;
	}
	
}
