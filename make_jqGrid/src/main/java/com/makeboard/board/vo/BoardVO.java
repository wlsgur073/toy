package com.makeboard.board.vo;

import java.util.Date;
import java.util.List;

import org.springframework.web.multipart.MultipartFile;

public class BoardVO {
	private String bdNo;
	private String bdTitle;
	private String bdContent;
	private String bdWriter;
	private Date bdRegdate;
	private Date bdUpdatedate;
	private String bdAttach= "";
	private String bdPw = "";
	private MultipartFile file;
	
	public String getBdNo() {
		return bdNo;
	}
	public void setBdNo(String bdNo) {
		this.bdNo = bdNo;
	}
	public String getBdTitle() {
		return bdTitle;
	}
	public void setBdTitle(String bdTitle) {
		this.bdTitle = bdTitle;
	}
	public String getBdContent() {
		return bdContent;
	}
	public void setBdContent(String bdContent) {
		this.bdContent = bdContent;
	}
	public String getBdWriter() {
		return bdWriter;
	}
	public void setBdWriter(String bdWriter) {
		this.bdWriter = bdWriter;
	}
	public Date getBdRegdate() {
		return bdRegdate;
	}
	public void setBdRegdate(Date bdRegdate) {
		this.bdRegdate = bdRegdate;
	}
	public Date getBdUpdatedate() {
		return bdUpdatedate;
	}
	public void setBdUpdatedate(Date bdUpdatedate) {
		this.bdUpdatedate = bdUpdatedate;
	}
	public String getBdAttach() {
		return bdAttach;
	}
	public void setBdAttach(String bdAttach) {
		this.bdAttach = bdAttach;
	}
	public String getBdPw() {
		return bdPw;
	}
	public void setBdPw(String bdPw) {
		this.bdPw = bdPw;
	
	}
	public MultipartFile getFile() {
		return file;
	}
	public void setFile(MultipartFile file) {
		this.file = file;
	}
	@Override
	public String toString() {
		return "BoardVO [bdNo=" + bdNo + ", bdTitle=" + bdTitle + ", bdContent=" + bdContent + ", bdWriter=" + bdWriter
				+ ", bdRegdate=" + bdRegdate + ", bdUpdatedate=" + bdUpdatedate + ", bdAttach=" + bdAttach + ", bdPw="
				+ bdPw + ", file=" + file + "]";
	}
	
}
