package com.test.myTest.user.vo;

public class UserVO {
	private String userId;
	private String userPw;
	private String userNm;
	
	public String getUserId() {
		return userId;
	}
	public void setUserId(String userId) {
		this.userId = userId;
	}
	public String getUserPw() {
		return userPw;
	}
	public void setUserPw(String userPw) {
		this.userPw = userPw;
	}
	public String getUserNm() {
		return userNm;
	}
	public void setUserNm(String userNm) {
		this.userNm = userNm;
	}
	@Override
	public String toString() {
		return "UserVO [userId=" + userId + ", userPw=" + userPw + ", userNm=" + userNm + "]";
	}
}
