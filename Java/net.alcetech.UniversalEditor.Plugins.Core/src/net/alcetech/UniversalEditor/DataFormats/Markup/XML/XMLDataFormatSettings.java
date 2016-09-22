package net.alcetech.UniversalEditor.DataFormats.Markup.XML;

import java.util.ArrayList;

import net.alcetech.ApplicationFramework.Collections.Generic.BidirectionalDictionary;
import net.alcetech.ApplicationFramework.Collections.Generic.KeyValuePair;

public class XMLDataFormatSettings {
	
	private BidirectionalDictionary<String, String> _entities = new BidirectionalDictionary<String, String>();
	public void addEntity(String name, String value) {
		_entities.add(name, value);
	}
	public String getEntityValue(String name) {
		return _entities.getValue2(name);
	}
	public String getEntityName(String value) {
		return _entities.getValue1(value);
	}
	
	private ArrayList<String> _autoCloseTagNames = new ArrayList<String>();
	public void addAutoCloseTagName(String name) {
		_autoCloseTagNames.add(name);
	}
	public boolean containsAutoCloseTagName(String name) {
		return _autoCloseTagNames.contains(name);
	}

	private char _TagBeginChar = '<';
	public char getTagBeginChar() { return _TagBeginChar; }
	public void setTagBeginChar(char value) { _TagBeginChar = value; }
	
	private char _TagCloseChar = '/';
	public char getTagCloseChar() { return _TagCloseChar; }
	public void setTagCloseChar(char value) { _TagCloseChar = value; }
	
	private char _TagEndChar = '>';
	public char getTagEndChar() { return _TagEndChar; }
	public void setTagEndChar(char value) { _TagEndChar = value; }
	
	private char _TagSpecialDeclarationStartChar = '!';
	public char getTagSpecialDeclarationStartChar() {
		return _TagSpecialDeclarationStartChar;
	}
	public void setTagSpecialDeclarationStartChar(char value) {
		_TagSpecialDeclarationStartChar = value;
	}
	
	private String _TagSpecialDeclarationCommentStart = "--";
	public String getTagSpecialDeclarationCommentStart() {
		return _TagSpecialDeclarationCommentStart;
	}
	public void setTagSpecialDeclarationCommentStart(String value) {
		_TagSpecialDeclarationCommentStart = value;
	}
	
	private char _PreprocessorChar = '?';
	public char getPreprocessorChar() {
		return _PreprocessorChar;
	}
	public void setPreprocessorChar(char value) {
		_PreprocessorChar = value;
	}
	
	private char _CDataBeginChar = '[';
	public char getCDataBeginChar() { return _CDataBeginChar; }
	public void setCDataBeginChar(char value) { _CDataBeginChar = value; }
	
	private char _CDataEndChar = ']';
	public char getCDataEndChar() { return _CDataEndChar; }
	public void setCDataEndChar(char value) { _CDataEndChar = value; }
	
	public KeyValuePair<String, String>[] getEntities() {
		return _entities.toArray();
	}
	
	private char _EntityBeginChar = '&';
	public char getEntityBeginChar() { return _EntityBeginChar; }
	public void setEntityBeginChar(char value) { _EntityBeginChar = value; }

	private char _EntityEndChar = ';';
	public char getEntityEndChar() { return _EntityEndChar; }
	public void setEntityEndChar(char value) { _EntityEndChar = value; }
	
	private char _AttributeNameValueSeparatorChar = '=';
	public char getAttributeNameValueSeparatorChar() { return _AttributeNameValueSeparatorChar; }
	public void setAttributeNameValueSeparatorChar(char value) { _AttributeNameValueSeparatorChar = value; }
}
