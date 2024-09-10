using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using ContractMonthlyClaimSystemWPF.Models;

namespace ContractMonthlyClaimSystemWPF
{
    public partial class MainWindow : Window
    {
        private ObservableCollection<Contract> Contracts { get; set; }
        private ObservableCollection<MonthlyClaim> MonthlyClaims { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Contracts = new ObservableCollection<Contract>();
            MonthlyClaims = new ObservableCollection<MonthlyClaim>();
            ContractListView.ItemsSource = Contracts;
            ClaimListView.ItemsSource = MonthlyClaims;
            ClaimStatusListView.ItemsSource = MonthlyClaims;
        }

        private void AddContract_Click(object sender, RoutedEventArgs e)
        {
            var contract = new Contract
            {
                Id = Contracts.Count + 1,
                ContractName = "New Contract",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(6),
                TotalValue = 10000
            };
            Contracts.Add(contract);
        }

        private void EditContract_Click(object sender, RoutedEventArgs e)
        {
            if (ContractListView.SelectedItem is Contract selectedContract)
            {
                selectedContract.ContractName = "Edited Contract";
                selectedContract.TotalValue += 1000;
                ContractListView.Items.Refresh();
            }
        }

        private void DeleteContract_Click(object sender, RoutedEventArgs e)
        {
            if (ContractListView.SelectedItem is Contract selectedContract)
            {
                Contracts.Remove(selectedContract);
            }
        }

        private void AddClaim_Click(object sender, RoutedEventArgs e)
        {
            if (ContractListView.SelectedItem is Contract selectedContract)
            {
                var claim = new MonthlyClaim
                {
                    Id = MonthlyClaims.Count + 1,
                    ContractId = selectedContract.Id,
                    Contract = selectedContract,
                    ClaimDate = DateTime.Now,
                    Amount = 500,
                    Status = ClaimStatus.Pending,
                    Description = "Monthly Claim Description"
                };
                MonthlyClaims.Add(claim);
            }
            else
            {
                MessageBox.Show("Please select a contract to add a claim.", "No Contract Selected", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void UploadDocument_Click(object sender, RoutedEventArgs e)
        {
            if (ClaimListView.SelectedItem is MonthlyClaim selectedClaim)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "PDF files (*.pdf)|*.pdf|All files (*.*)|*.*";
                if (openFileDialog.ShowDialog() == true)
                {
                    string destinationPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Documents");

                    // Ensure the Documents directory exists
                    if (!Directory.Exists(destinationPath))
                    {
                        Directory.CreateDirectory(destinationPath);
                    }

                    destinationPath = Path.Combine(destinationPath, Path.GetFileName(openFileDialog.FileName));
                    File.Copy(openFileDialog.FileName, destinationPath, true);
                    selectedClaim.SupportingDocumentPath = destinationPath;
                    ClaimListView.Items.Refresh();
                }
            }
            else
            {
                MessageBox.Show("Please select a claim to upload a document.", "No Claim Selected", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void VerifyClaim_Click(object sender, RoutedEventArgs e)
        {
            if (ClaimListView.SelectedItem is MonthlyClaim selectedClaim)
            {
                if (selectedClaim.Status == ClaimStatus.Pending)
                {
                    selectedClaim.Status = ClaimStatus.Verified;
                    ClaimListView.Items.Refresh();
                    ClaimStatusListView.Items.Refresh();
                }
                else
                {
                    MessageBox.Show("Only pending claims can be verified.", "Invalid Action", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private void ApproveClaim_Click(object sender, RoutedEventArgs e)
        {
            if (ClaimListView.SelectedItem is MonthlyClaim selectedClaim)
            {
                if (selectedClaim.Status == ClaimStatus.Verified)
                {
                    selectedClaim.Status = ClaimStatus.Approved;
                    ClaimListView.Items.Refresh();
                    ClaimStatusListView.Items.Refresh();
                }
                else
                {
                    MessageBox.Show("Only verified claims can be approved.", "Invalid Action", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }
    }
}
