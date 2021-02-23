﻿using System;
using Emignatik.NxFileViewer.Localization;
using Emignatik.NxFileViewer.Model;
using Emignatik.NxFileViewer.Model.Overview;
using Emignatik.NxFileViewer.Model.TreeItems;
using Microsoft.Extensions.Logging;

namespace Emignatik.NxFileViewer.FileLoading
{
    internal class FileLoader : IFileLoader
    {
        private readonly IFileTypeAnalyzer _fileTypeAnalyzer;
        private readonly IFileItemLoader _fileItemLoader;
        private readonly IFileOverviewLoader _fileOverviewLoader;
        private readonly ILogger _logger;

        public FileLoader(ILoggerFactory loggerFactory, IFileTypeAnalyzer fileTypeAnalyzer, IFileItemLoader fileItemLoader, IFileOverviewLoader fileOverviewLoader)
        {
            _fileTypeAnalyzer = fileTypeAnalyzer ?? throw new ArgumentNullException(nameof(fileTypeAnalyzer));
            _fileItemLoader = fileItemLoader ?? throw new ArgumentNullException(nameof(fileItemLoader));
            _fileOverviewLoader = fileOverviewLoader ?? throw new ArgumentNullException(nameof(fileOverviewLoader));
            _logger = (loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory))).CreateLogger(this.GetType());
        }

        public NxFile Load(string filePath)
        {
            _logger.LogInformation(LocalizationManager.Instance.Current.Keys.Log_OpeningFile.SafeFormat(filePath));

            IItem rootItem;
            FileOverview fileOverview;
            switch (_fileTypeAnalyzer.GetFileType(filePath))
            {
                case FileType.UNKNOWN:
                    throw new FileNotSupportedException(filePath);

                case FileType.XCI:
                    var xciItem = _fileItemLoader.LoadXci(filePath);
                    fileOverview = _fileOverviewLoader.Load(xciItem);
                    rootItem = xciItem;

                    break;
                case FileType.NSP:
                    var nspItem = _fileItemLoader.LoadNsp(filePath);
                    fileOverview = _fileOverviewLoader.Load(nspItem);
                    rootItem = nspItem;
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            LoadAllChildren(rootItem);

            var openedFile = new NxFile(filePath, rootItem, fileOverview);
            return openedFile;
        }

        private static void LoadAllChildren(IItem rootItem)
        {
            var childItems = rootItem.SafeLoadChildItems();
            foreach (var childItem in childItems)
            {
                LoadAllChildren(childItem);
            }
        }
    }
}