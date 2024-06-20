using HatFClient.Repository;
using System;
using System.Windows.Forms;
using HatFClient.CustomModels;

namespace HatFClient.CustomControls
{
    public partial class GridPatternUI : UserControl
    {
        private GridPatternRepo patternRepo;

        public event EventHandler<PatternInfo> OnPatternSelected;

        public GridPatternUI()
        {
            InitializeComponent();
        }
        [Obsolete("いつか消します")]
        public void Init(GridPatternRepo _patternRepo, EventHandler<PatternInfo> onSelectHandler)
        {
            // SET REPO
            this.patternRepo = _patternRepo;
            // SET SELECT HANDLER
            OnPatternSelected += onSelectHandler;

            initPatternUI();
        }
        public void Init(GridPatternRepo _patternRepo)
        {
            // SET REPO
            this.patternRepo = _patternRepo;

            initPatternUI();
        }

        private void PatternSaveHandler(object sender, PatternInfo patternInfo)
        {
            patternRepo.SavePattern(patternInfo);
            initPatternUI();
        }

        private void ShowPatternSettingDialog(PatternInfo patternInfo)
        {
            // Form.ShowDialog する場合は Dispose が必要です。
            using (var dlg = new GridPatternForm(patternRepo, patternInfo))
            {
                dlg.OnSavePattern += new EventHandler<PatternInfo>(PatternSaveHandler);

                dlg.ShowDialog();
            }
        }

        private void initPatternUI()
        {
            cmbPattern.BeginUpdate();
            cmbPattern.Items.Clear();

            var patterns = patternRepo.LoadPatterns().ToArray();
            cmbPattern.DisplayMember = "Pattern";
            cmbPattern.Items.AddRange(patterns);
            if (patterns.Length > 0)
            {
                cmbPattern.SelectedIndex = 0;
                cmbPattern.Enabled = true;
                btnPatternEdit.Enabled = true;
                btnPatternCopy.Enabled = true;
                btnPatternNew.Enabled = true;
                btnPatternDelete.Enabled = true;
            }
            else
            {
                cmbPattern.SelectedIndex = -1;
                cmbPattern.Enabled = false;
                cmbPattern.Text = string.Empty;
                btnPatternEdit.Enabled = false;
                btnPatternCopy.Enabled = false;
                btnPatternNew.Enabled = true;
                btnPatternDelete.Enabled = false;
            }
            cmbPattern.EndUpdate();
        }

        private void btnPatternEdit_Click(object sender, EventArgs e)
        {
            var pattern = (PatternInfo)cmbPattern.SelectedItem;
            if (pattern != null)
            {
                ShowPatternSettingDialog(pattern);
            }
        }

        private void btnPatternNew_Click(object sender, EventArgs e)
        {
            var pattern = new PatternInfo
            {
                Pattern = "新規パターン",
                ClassName = patternRepo.ModelName,
            };
            ShowPatternSettingDialog(pattern);
        }

        private void btnPatternCopy_Click(object sender, EventArgs e)
        {
            var pattern = (PatternInfo)cmbPattern.SelectedItem;
            if (pattern != null)
            {
                var copied = PatternInfo.copy(pattern);
                ShowPatternSettingDialog(copied);
            }
        }

        private void btnPatternDelete_Click(object sender, EventArgs e)
        {
            var pattern = (PatternInfo)cmbPattern.SelectedItem;
            if (pattern != null)
            {
                patternRepo.DeletePattern(pattern);
                initPatternUI();
            }
        }

        //public PatternInfo Selectedpattern  {
        //    return (PatternInfo)cmbPattern.SelectedItem;
        //}
        public PatternInfo SelectedPattern
        { get { return (PatternInfo)cmbPattern.SelectedItem; } }

        private void cmbPattern_SelectedValueChanged(object sender, EventArgs e)
        {
            if (SelectedPattern != null)
            {
                patternRepo.SavePattern(SelectedPattern);
                if (OnPatternSelected != null)
                {
                    OnPatternSelected.Invoke(this, SelectedPattern);
                }
            }
        }
    }
}
