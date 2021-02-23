﻿using System;
using Emignatik.NxFileViewer.Model.TreeItems.Impl;
using Emignatik.NxFileViewer.Utils;
using Emignatik.NxFileViewer.Views.ObjectPropertyViewer;

namespace Emignatik.NxFileViewer.Views.TreeItems.Impl
{
    public class PartitionFileEntryItemViewModel : ItemViewModel
    {
        private readonly PartitionFileEntryItem _partitionFileEntryItem;

        public PartitionFileEntryItemViewModel(PartitionFileEntryItem partitionFileEntryItem, IServiceProvider serviceProvider)
            : base(partitionFileEntryItem, serviceProvider)
        {
            _partitionFileEntryItem = partitionFileEntryItem;
        }

        [PropertyView]
        public string Size => _partitionFileEntryItem.Size.ToFileSize();
    }
}