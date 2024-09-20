package com.test.myTest.user.web;

import java.util.HashMap;
import java.util.Map;

import javax.servlet.http.HttpSession;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.ResponseBody;

import com.test.myTest.user.service.UserService;
import com.test.myTest.user.vo.UserVO;

@Controller
public class UserController {
	
	@Autowired
	UserService userService;
	
	@RequestMapping(value = "/login")
	public String login(){
		String url = "login";
		return url;
	}
	
	@ResponseBody
	@RequestMapping(value = "/login.do", method = RequestMethod.POST)
	public Map<String, String> loginDo(@RequestParam("userId") String userId, @RequestParam("userPw") String userPw, HttpSession session){
		Map<String, String> retMap = new HashMap<String, String>();
		UserVO userVO = new UserVO();
		
		try {
			userVO = userService.getUserVOById(userId);
			boolean flag = true;
			flag = userVO.getUserPw().equals(userPw) ? false : true;
			
			if(userVO.getUserId().equals("") || flag){
				retMap.put("fail", "fail");
				return retMap;
			}
		} catch (Exception e) {
			e.printStackTrace();
		}
		
		retMap.put("success", "success");
		session.setAttribute("userVO", userVO);
		
		return retMap; 
	}
	
	@RequestMapping(value = "/logout", method = RequestMethod.GET)
	public String logout (HttpSession session){
		String url = "redirect:/";
		session.removeAttribute("userVO");
		return url;
	}

}
