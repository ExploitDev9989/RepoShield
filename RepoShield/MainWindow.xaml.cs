using System;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media.Effects;
using System.Windows.Threading;

namespace RepoShield
{
    public partial class MainWindow : Window
    {
        private readonly OllamaService _ollamaService = new OllamaService();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Scan_Repo_Click(object sender, RoutedEventArgs e)
        {
            string url = GitHub_URL.Text;

            RepoScanner scanner = new RepoScanner();
            ScanResult result = scanner.ScanRepo(url);

            FindingsListBox.ItemsSource = result.Findings;
            RiskScoreTextBlock.Text = $"Risk Score: {result.RiskScore}/100";
            AiFeedbackTextBox.Text = result.Report;
        }

        private async void AnalyzeAiButton_Click(object sender, RoutedEventArgs e)
        {
            if (FindingsListBox.SelectedItem is not RiskFinding selectedFinding)
            {
                System.Windows.MessageBox.Show("Select a finding first.");
                return;
            }

            if (!File.Exists(selectedFinding.FilePath))
            {
                System.Windows.MessageBox.Show("Could not find the file to analyze.");
                return;
            }

            string code = File.ReadAllText(selectedFinding.FilePath);

            string prompt =
                $"Analyze this suspicious file from RepoShield.\n\n" +
                $"Severity: {selectedFinding.Severity}\n" +
                $"Issue: {selectedFinding.Issue}\n" +
                $"Reason: {selectedFinding.Reason}\n" +
                $"File: {selectedFinding.FilePath}\n" +
                $"Matched Text: {selectedFinding.MatchedText}\n\n" +
                $"--- FILE CONTENT START ---\n" +
                code +
                $"\n--- FILE CONTENT END ---\n\n" +
                $"Explain what this code is actually doing in simple terms. " +
                $"Focus on whether it downloads files, steals data, runs commands, changes settings, hides itself, or contacts the internet.";

            AnalyzeAiButton.IsEnabled = false;
            AnalyzeAiButton.Content = "Analyzing code...";
            AiFeedbackTextBox.Text = "Opening file and sending code to local AI...";

            try
            {
                string aiResponse = await _ollamaService.AnalyzeFindingAsync(prompt);
                AiFeedbackTextBox.Text = aiResponse;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"AI analysis failed:\n{ex.Message}");
            }
            finally
            {
                AnalyzeAiButton.IsEnabled = true;
                AnalyzeAiButton.Content = "Analyze with AI";
            }
        }

        private void GitHub_URL_GotFocus(object sender, RoutedEventArgs e)
        {
            if (GitHub_URL.Text == "https://github.com/owner/repository")
            {
                GitHub_URL.Text = "";
                GitHub_URL.Foreground = System.Windows.Media.Brushes.White;
            }
        }

        private void GitHub_URL_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(GitHub_URL.Text))
            {
                GitHub_URL.Text = "https://github.com/owner/repository";
                GitHub_URL.Foreground = System.Windows.Media.Brushes.Gray;
            }
        }

        private void Browse_Folder_Click(object sender, RoutedEventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.Description = "Select a folder to scan";

                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    GitHub_URL.Text = dialog.SelectedPath;
                    GitHub_URL.Foreground = System.Windows.Media.Brushes.White;
                }
            }
        }
    }
}