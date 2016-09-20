package net.alcetech.UniversalEditor.Core;

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
	
	protected void beforeLoadInternal(Stack<ObjectModel> objectModels) {
		// virtual
	}
	protected void afterLoadInternal(Stack<ObjectModel> objectModels ) {
		// virtual
	}
	protected abstract void loadInternal(ObjectModel objectModel);
	
	public void load(ObjectModel objectModel) {
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

	protected void beforeSaveInternal(Stack<ObjectModel> objectModels) {
		// virtual
	}
	protected void afterSaveInternal(Stack<ObjectModel> objectModels ) {
		// virtual
	}
	protected abstract void saveInternal(ObjectModel objectModel);
	
	public void save(ObjectModel objectModel) {
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
