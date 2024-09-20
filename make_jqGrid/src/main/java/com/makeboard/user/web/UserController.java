package com.makeboard.user.web;

import java.util.HashMap;
import java.util.Map;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpSession;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.ResponseBody;

import com.makeboard.user.service.UserService;
import com.makeboard.user.vo.UserVO;

@Controller
public class UserController {
	
	private static final Logger logger = LoggerFactory.getLogger(UserController.class);
	
	@Autowired
	private UserService userSevice;
	
	@RequestMapping(value = "/", method = RequestMethod.GET)
	public String index() {
		
		return "index";
	}

	@ResponseBody
	@RequestMapping(value = "/login.do", method = RequestMethod.POST)
	public Map<String, String> login(HttpServletRequest req) throws Exception {
		
		Map<String, String> retMap = new HashMap<String, String>();
		HttpSession session = req.getSession();
		UserVO userVO = new UserVO();
		
		String userId = req.getParameter("userId");
		String userPw = req.getParameter("userPw");
		
		userVO = userSevice.getUserVOById(userId);

		if(userVO.getUserId().equals("") || !userVO.getUserPw().equals(userPw)){
			retMap.put("fail", "fail");
			return retMap;
		}
		
		session.setAttribute("userVO", userVO);
		retMap.put("nickname", userVO.getNickname());
		return retMap;
	}
	
	@RequestMapping(value = "/login", method = RequestMethod.GET)
	public String goToLoginPage(){
		String url = "login";
		
		return url;
	}
	
	@RequestMapping(value = "/logout", method = RequestMethod.GET)
	public String logout(){
		String url = "logout";
		
		return url;
	}
}
