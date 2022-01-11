﻿using System;
using System.Collections.Generic;
using LibHac.Ncm;
using LibHac.Tools.Ncm;

namespace Emignatik.NxFileViewer.Model.TreeItems.Impl
{
    public class CnmtContentEntryItem : ItemBase
    {
        private readonly CnmtContentEntry _cnmtContentEntry;
        private readonly int _index;

        public CnmtContentEntryItem(CnmtContentEntry cnmtContentEntry, CnmtItem parentItem, int index)
        {
            _cnmtContentEntry = cnmtContentEntry ?? throw new ArgumentNullException(nameof(cnmtContentEntry));
            _index = index;
            ParentItem = parentItem ?? throw new ArgumentNullException(nameof(parentItem));
        }

        public override string DisplayName => $"Entry {_index}";

        public override string Name => _index.ToString();

        public ContentType NcaContentType => _cnmtContentEntry.Type;

        public byte[] NcaId => _cnmtContentEntry.NcaId;

        public byte[] NcaHash => _cnmtContentEntry.Hash;

        public long NcaSize => _cnmtContentEntry.Size;

        public override CnmtItem ParentItem { get; }

        public override string LibHacTypeName => nameof(CnmtContentEntry);

        public override string? LibHacUnderlyingTypeName => null;

        public override IEnumerable<IItem> ChildItems { get { yield break; } }

    }
}