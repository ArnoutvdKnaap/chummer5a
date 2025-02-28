/*  This file is part of Chummer5a.
 *
 *  Chummer5a is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  Chummer5a is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with Chummer5a.  If not, see <http://www.gnu.org/licenses/>.
 *
 *  You can obtain the full source code for Chummer5a at
 *  https://github.com/chummer5a/chummer5a
 */

using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chummer
{
    // We use TextBox because notes are often displayed as TreeNode tooltips, and because TreeNode tooltips
    // only support plaintext and not any kind of formatting, frmNotes and Notes items in general have to use
    // plaintext instead of RTF or HTML formatted text.
    public partial class EditNotes : Form
    {
        // Set to DPI-based 640 in constructor, needs to be there because of DPI dependency
        private static int _intWidth = int.MinValue;

        // Set to DPI-based 360 in constructor, needs to be there because of DPI dependency
        private static int _intHeight = int.MinValue;

        private bool _blnLoading = true;
        private string _strNotes;
        private Color _colNotes;

        private readonly CancellationToken _objMyToken;

        #region Control Events

        public EditNotes(string strOldNotes, CancellationToken objMyToken = default) : this(strOldNotes, ColorManager.HasNotesColor, objMyToken)
        {
        }

        public EditNotes(string strOldNotes, Color colNotes, CancellationToken objMyToken = default)
        {
            _objMyToken = objMyToken;
            InitializeComponent();
            this.UpdateLightDarkMode(objMyToken);
            this.TranslateWinForm(token: objMyToken);
            txtNotes.Text = _strNotes = strOldNotes.NormalizeLineEndings();

            btnColorSelect.Enabled = _strNotes.Length > 0;

            _colNotes = colNotes;
            if (_colNotes.IsEmpty)
                _colNotes = ColorManager.HasNotesColor;
        }

        private async void EditNotes_Load(object sender, EventArgs e)
        {
            try
            {
                if (_intWidth <= 0 || _intHeight <= 0)
                {
                    using (Graphics g = CreateGraphics())
                    {
                        if (_intWidth <= 0)
                            _intWidth = (int)(640 * g.DpiX / 96.0f);
                        if (_intHeight <= 0)
                            _intHeight = (int)(360 * g.DpiY / 96.0f);
                    }
                }
                Width = _intWidth;
                Height = _intHeight;
                await UpdateColorRepresentation(_objMyToken).ConfigureAwait(false);
                _blnLoading = false;
            }
            catch (OperationCanceledException)
            {
                //swallow this
            }
        }

        private void txtNotes_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    Close();
                    break;

                case Keys.Enter:
                    {
                        if (e.Control)
                            btnOK_Click(sender, e);
                        break;
                    }
            }
        }

        private void EditNotes_Resize(object sender, EventArgs e)
        {
            if (_blnLoading)
                return;

            _intWidth = Width;
            _intHeight = Height;
        }

        private void EditNotes_Shown(object sender, EventArgs e)
        {
            txtNotes.Focus();
            txtNotes.SelectionLength = 0;
            txtNotes.SelectionStart = txtNotes.TextLength;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            _strNotes = txtNotes.Text;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private async void btnColorSelect_Click(object sender, EventArgs e)
        {
            try
            {
                _colNotes = dlgColor
                    .Color; //Selected color is always how it is shown in light mode, use the stored one for it.
                if (await this.DoThreadSafeFuncAsync(x => dlgColor.ShowDialog(x), token: _objMyToken)
                              .ConfigureAwait(false) != DialogResult.OK)
                    return;
                _colNotes = ColorManager.GenerateModeIndependentColor(dlgColor.Color);
                await UpdateColorRepresentation(_objMyToken).ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                //swallow this
            }
        }

        private void txtNotes_TextChanged(object sender, EventArgs e)
        {
            btnColorSelect.Enabled = txtNotes.TextLength > 0;
        }

        #endregion Control Events

        #region Properties

        /// <summary>
        /// Notes.
        /// </summary>
        public string Notes => _strNotes;

        public Color NotesColor => _colNotes;

        #endregion Properties

        private Task UpdateColorRepresentation(CancellationToken token = default)
        {
            token.ThrowIfCancellationRequested();
            Color objColor = ColorManager.GenerateCurrentModeColor(_colNotes);
            return txtNotes.DoThreadSafeAsync(x => x.ForeColor = objColor, token);
        }
    }
}
