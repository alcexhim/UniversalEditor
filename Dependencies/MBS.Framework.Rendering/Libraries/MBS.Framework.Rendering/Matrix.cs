using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MBS.Framework.Rendering
{
    public abstract class Matrix
    {
		protected abstract void SetMatrixModeInternal(MatrixMode mode);

        private MatrixMode mvarMode = MatrixMode.ModelView;
        public MatrixMode Mode
        {
            get { return mvarMode; }
            set
            {
                mvarMode = value;
                SetMatrixModeInternal(value);
            }
        }

		protected abstract void PushInternal();
        public void Push()
        {
			PushInternal();
        }
		protected abstract void PopInternal();
        public void Pop()
        {
			PopInternal();
        }
		protected abstract void ResetInternal();
        public void Reset()
        {
			ResetInternal();
        }

		protected abstract void MultiplyInternal(double[] values);
        public void Multiply(double[] values)
        {
			MultiplyInternal(values);
        }
		protected abstract void MultiplyInternal(float[] values);
        public void Multiply(float[] values)
		{
			MultiplyInternal(values);
		}
    }
}
