//
//  HyperCardDataFormat.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2020 Mike Becker's Software
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System;
using System.Collections.Generic;
using UniversalEditor.Accessors;
using UniversalEditor.ObjectModels.Chunked;
using UniversalEditor.Plugins.Office.DataFormats.Presentation.HyperCard.Internal.STAK;
using UniversalEditor.Plugins.Office.ObjectModels.Presentation;

namespace UniversalEditor.Plugins.Office.DataFormats.Presentation.HyperCard
{
	public class HyperCardDataFormat : Internal.HyperCardChunkedDataFormat
	{
		private static DataFormatReference _dfr;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = new DataFormatReference(GetType());
				_dfr.Capabilities.Add(typeof(PresentationObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}
		protected override void BeforeLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeLoadInternal(objectModels);
			objectModels.Push(new ChunkedObjectModel());
		}
		protected override void AfterLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.AfterLoadInternal(objectModels);

			ChunkedObjectModel chunked = (objectModels.Pop() as ChunkedObjectModel);
			PresentationObjectModel pres = (objectModels.Pop() as PresentationObjectModel);

			RIFFDataChunk chunk_stak = (chunked.Chunks["STAK"] as RIFFDataChunk);
			STAKDataFormat stak_df = new STAKDataFormat();
			STAKObjectModel stak_om = new STAKObjectModel();

			Document.Load(stak_om, stak_df, new MemoryAccessor(chunk_stak.Data));


		}
		protected override void BeforeSaveInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeSaveInternal(objectModels);

			PresentationObjectModel pres = (objectModels.Pop() as PresentationObjectModel);
			ChunkedObjectModel chunked = new ChunkedObjectModel();

			objectModels.Push(chunked);
		}
	}
}
