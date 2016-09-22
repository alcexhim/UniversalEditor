package net.alcetech.UniversalEditor.Core;

import java.io.IOException;
import java.util.Stack;

public abstract class DataFormat
{
	public DataFormatReference getDataFormatReference() {
		return new DataFormatReference(this.getClass().getName());
	}
	
	private Accessor _accessor = null;
	public Accessor getAccessor() {
		return _accessor;
	}
	public void setAccessor(Accessor value) {
		_accessor = value;
	}
	
	protected void beforeLoadInternal(Stack<ObjectModel> objectModels) throws IOException {
		// virtual
	}
	protected void afterLoadInternal(Stack<ObjectModel> objectModels ) throws IOException {
		// virtual
	}
	protected abstract void loadInternal(ObjectModel objectModel) throws IOException;
	
	public void load(ObjectModel objectModel) throws IOException {
		if (objectModel == null)
			throw new NullPointerException();
		
		Stack<ObjectModel> stack = new Stack<ObjectModel>();
		stack.push(objectModel);
		beforeLoadInternal(stack);
		
		ObjectModel omb = stack.pop();
		loadInternal(omb);
		stack.push(omb);
		
		afterLoadInternal(stack);
	}

	protected void beforeSaveInternal(Stack<ObjectModel> objectModels) throws IOException {
		// virtual
	}
	protected void afterSaveInternal(Stack<ObjectModel> objectModels) throws IOException {
		// virtual
	}
	protected abstract void saveInternal(ObjectModel objectModel) throws IOException;
	
	public void save(ObjectModel objectModel) throws IOException {
		if (objectModel == null)
			throw new NullPointerException();
		
		Stack<ObjectModel> stack = new Stack<ObjectModel>();
		stack.push(objectModel);
		beforeSaveInternal(stack);
		
		ObjectModel omb = stack.pop();
		saveInternal(omb);
		stack.push(omb);
		
		afterSaveInternal(stack);
	}
}
