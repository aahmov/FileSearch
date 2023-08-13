using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileSearchApp
{
    public partial class MainForm : Form
    {
        private CancellationTokenSource _cancellationTokenSource;
        private readonly ConcurrentDictionary<string, DirectoryResult> _results = new ConcurrentDictionary<string, DirectoryResult>();
        private readonly object _resultsLock = new object();

        public MainForm()
        {
            InitializeComponent();
            LoadDrives();
        }

        private void LoadDrives()
        {
            foreach (var drive in DriveInfo.GetDrives())
            {
                driveComboBox.Items.Add(drive.Name);
            }
        }

        private async void searchButton_Click(object sender, EventArgs e)
        {
            if (_cancellationTokenSource != null)
            {
                MessageBox.Show("A search is already in progress.");
                return;
            }

            string selectedDrive = driveComboBox.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(selectedDrive))
            {
                MessageBox.Show("Please select a drive.");
                return;
            }

            _cancellationTokenSource = new CancellationTokenSource();

            try
            {
                await SearchDirectoriesAsync(selectedDrive, _cancellationTokenSource.Token);
            }
            catch (OperationCanceledException)
            {
                resultListBox.Invoke(new Action(() =>
                {
                    resultListBox.Items.Add("Search was canceled.");
                }));
            }
            finally
            {
                _cancellationTokenSource.Dispose();
                _cancellationTokenSource = null;
            }
        }

        private async Task SearchDirectoriesAsync(string rootDirectory, CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                try
                {
                    Parallel.ForEach(Directory.EnumerateDirectories(rootDirectory, "*", SearchOption.AllDirectories),
                        new ParallelOptions { CancellationToken = cancellationToken }, directory =>
                        {
                            if (cancellationToken.IsCancellationRequested)
                                return;

                            try
                            {
                                SearchAndDisplay(directory);
                                MonitorDirectory(directory);
                            }
                            catch (UnauthorizedAccessException)
                            {
                                HandleAccessDeniedError(directory);
                            }
                        });
                }
                catch (UnauthorizedAccessException)
                {
                    HandleAccessDeniedError(rootDirectory);
                }
            }, cancellationToken);
        }


        private void SearchAndDisplay(string directory)
        {
            try
            {
                string[] allFiles = Directory.GetFiles(directory, "*", SearchOption.AllDirectories);

                var largeFiles = allFiles.Where(file => new FileInfo(file).Length > 10 * 1024 * 1024).ToArray();

                if (largeFiles.Length > 0)
                {
                    var result = new DirectoryResult
                    {
                        Directory = directory,
                        FileCount = largeFiles.Length,
                        TotalFileSize = CalculateTotalFileSize(largeFiles)
                    };

                    _results.TryAdd(directory, result);

                    UpdateResultsUI();
                }
            }
            catch (UnauthorizedAccessException)
            {
                HandleAccessDeniedError(directory);
            }
        }

        private long CalculateTotalFileSize(string[] fileNames)
        {
            long totalSize = 0;
            foreach (var fileName in fileNames)
            {
                totalSize += new FileInfo(fileName).Length;
            }
            return totalSize;
        }

        private void MonitorDirectory(string directory)
        {
            try
            {
                var watcher = new FileSystemWatcher(directory);
                watcher.NotifyFilter = NotifyFilters.DirectoryName | NotifyFilters.FileName | NotifyFilters.Size;

                watcher.Changed += (sender, e) => UpdateResult(directory);
                watcher.Created += (sender, e) => UpdateResult(directory);
                watcher.Deleted += (sender, e) => RemoveResult(directory);

                watcher.EnableRaisingEvents = true;
            }
            catch (UnauthorizedAccessException)
            {
                HandleAccessDeniedError(directory);
            }
        }

        private void UpdateResult(string directory)
        {
            if (_results.TryGetValue(directory, out DirectoryResult result))
            {
                result.FileCount = Directory.GetFiles(directory, "*", SearchOption.TopDirectoryOnly).Length;
                result.TotalFileSize = CalculateTotalFileSize(Directory.GetFiles(directory, "*", SearchOption.TopDirectoryOnly));

                UpdateResultsUI();
            }
        }

        private void RemoveResult(string directory)
        {
            _results.TryRemove(directory, out _);
            UpdateResultsUI();
        }

        private void UpdateResultsUI()
        {
            resultListBox.Invoke(new Action(() =>
            {
                resultListBox.Items.Clear();
                foreach (var result in _results.Values)
                {
                    resultListBox.Items.Add(result.ToString());
                }
            }));
        }

        private void pauseButton_Click(object sender, EventArgs e)
        {
            _cancellationTokenSource?.Cancel();
        }

        private void resumeButton_Click(object sender, EventArgs e)
        {
            if (_cancellationTokenSource != null && _cancellationTokenSource.IsCancellationRequested)
            {
                _cancellationTokenSource.Dispose();
                _cancellationTokenSource = new CancellationTokenSource();

                string selectedDrive = driveComboBox.SelectedItem?.ToString();
                if (!string.IsNullOrEmpty(selectedDrive))
                {
                    _ = SearchDirectoriesAsync(selectedDrive, _cancellationTokenSource.Token);
                }
            }
        }

        private void HandleAccessDeniedError(string directory)
        {
            resultListBox.Invoke(new Action(() =>
            {
                resultListBox.Items.Add($"Access Denied: {directory}");
            }));
        }
    }
}
