using Vault.Models;
using Vault.Services;

namespace Vault.Telas;

public class FormCadastroFranquia : Form
{
    private readonly FranquiaService _service;
    private readonly Franquia? _edicao;

    private TextBox _txtNome = null!;

    #region CORES
    private static readonly Color Fundo = Color.FromArgb(11, 11, 22);
    private static readonly Color Superficie = Color.FromArgb(23, 23, 37);
    private static readonly Color SuperficieElevada = Color.FromArgb(29, 29, 46);

    private static readonly Color Roxo = Color.FromArgb(124, 92, 255);
    private static readonly Color RoxoHover = Color.FromArgb(146, 120, 255);

    private static readonly Color Texto = Color.FromArgb(245, 243, 255);
    private static readonly Color TextoSecundario = Color.FromArgb(170, 167, 189);
    #endregion
    public FormCadastroFranquia(
        FranquiaService service,
        Franquia? franquia = null)
    {
        _service = service;
        _edicao = franquia;

        InicializarFormulario();
        CriarInterface();

        if (_edicao != null)
        {
            _txtNome.Text = _edicao.Nome;
        }
    }

    private void InicializarFormulario()
    {
        Text = _edicao == null
            ? "Nova Franquia"
            : "Editar Franquia";

        StartPosition =
            FormStartPosition.CenterParent;

        Size =
            new Size(
                540,
                320);

        MinimumSize =
            new Size(
                540,
                320);

        MaximizeBox = false;
        MinimizeBox = false;

        BackColor = Fundo;

        Font =
            new Font(
                "Segoe UI",
                10F);

        FormBorderStyle =
            FormBorderStyle.FixedDialog;

        ShowInTaskbar = false;
    }

    private void CriarInterface()
    {
        var layout =
            new TableLayoutPanel
            {
                Dock = DockStyle.Fill,

                Padding =
                    new Padding(
                        32,
                        26,
                        32,
                        26),

                ColumnCount = 1,
                RowCount = 4,

                BackColor = Fundo
            };

        layout.RowStyles.Add(
            new RowStyle(
                SizeType.Absolute,
                55));

        layout.RowStyles.Add(
            new RowStyle(
                SizeType.Absolute,
                72));

        layout.RowStyles.Add(
            new RowStyle(
                SizeType.Percent,
                100));

        layout.RowStyles.Add(
            new RowStyle(
                SizeType.Absolute,
                46));

        Controls.Add(layout);

        Label titulo =
            new Label
            {
                Text = _edicao == null
                    ? "Nova franquia"
                    : "Editar franquia",

                Dock = DockStyle.Fill,

                ForeColor = Texto,

                Font =
                    new Font(
                        "Segoe UI Semibold",
                        20F),

                TextAlign =
                    ContentAlignment.MiddleLeft
            };

        layout.Controls.Add(
            titulo,
            0,
            0);

        Panel campo =
            CriarCampoNome();

        layout.Controls.Add(
            campo,
            0,
            1);

        Panel espaco =
            new Panel
            {
                Dock = DockStyle.Fill
            };

        layout.Controls.Add(
            espaco,
            0,
            2);

        var botoes =
            new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1
            };

        botoes.ColumnStyles.Add(
            new ColumnStyle(
                SizeType.Percent,
                50));

        botoes.ColumnStyles.Add(
            new ColumnStyle(
                SizeType.Percent,
                50));

        Button cancelar =
            CriarBotaoSecundario(
                "Cancelar");

        Button salvar =
            CriarBotaoPrimario(
                "Salvar");

        cancelar.Click += (_, _) =>
        {
            DialogResult =
                DialogResult.Cancel;

            Close();
        };

        salvar.Click += (_, _) =>
        {
            Salvar();
        };

        botoes.Controls.Add(
            cancelar,
            0,
            0);

        botoes.Controls.Add(
            salvar,
            1,
            0);

        layout.Controls.Add(
            botoes,
            0,
            3);
    }

    private Panel CriarCampoNome()
    {
        var painel =
            new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent
            };

        Label label =
            new Label
            {
                Text = "Nome da franquia",

                Dock = DockStyle.Top,

                Height = 23,

                ForeColor =
                    TextoSecundario,

                Font =
                    new Font(
                        "Segoe UI Semibold",
                        9F)
            };

        _txtNome =
            new TextBox
            {
                Dock = DockStyle.Fill,

                BackColor = Superficie,

                ForeColor = Texto,

                BorderStyle =
                    BorderStyle.FixedSingle,

                Font =
                    new Font(
                        "Segoe UI",
                        10.5F),

                Padding =
                    new Padding(
                        8,
                        5,
                        8,
                        5)
            };

        _txtNome.KeyDown += (_, e) =>
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                Salvar();
            }

            if (e.KeyCode == Keys.Escape)
            {
                DialogResult =
                    DialogResult.Cancel;

                Close();
            }
        };

        painel.Controls.Add(
            _txtNome);

        painel.Controls.Add(
            label);

        return painel;
    }

    private Button CriarBotaoPrimario(string texto)
    {
        var botao =
            new Button
            {
                Text = texto,

                Dock = DockStyle.Fill,

                BackColor = Roxo,

                ForeColor = Color.White,

                FlatStyle =
                    FlatStyle.Flat,

                Font =
                    new Font(
                        "Segoe UI Semibold",
                        9.5F),

                Cursor =
                    Cursors.Hand,

                Margin =
                    new Padding(
                        6,
                        0,
                        0,
                        0)
            };

        botao.FlatAppearance.BorderSize = 0;

        botao.MouseEnter += (_, _) =>
        {
            botao.BackColor = RoxoHover;
        };

        botao.MouseLeave += (_, _) =>
        {
            botao.BackColor = Roxo;
        };

        return botao;
    }

    private Button CriarBotaoSecundario(string texto)
    {
        var botao =
            new Button
            {
                Text = texto,

                Dock = DockStyle.Fill,

                BackColor =
                    SuperficieElevada,

                ForeColor = Texto,

                FlatStyle =
                    FlatStyle.Flat,

                Font =
                    new Font(
                        "Segoe UI Semibold",
                        9.5F),

                Cursor =
                    Cursors.Hand,

                Margin =
                    new Padding(
                        0,
                        0,
                        6,
                        0)
            };

        botao.FlatAppearance.BorderSize = 0;

        botao.MouseEnter += (_, _) =>
        {
            botao.BackColor =
                Color.FromArgb(
                    39,
                    39,
                    58);
        };

        botao.MouseLeave += (_, _) =>
        {
            botao.BackColor =
                SuperficieElevada;
        };

        return botao;
    }

    private void Salvar()
    {
        string nome =
            _txtNome.Text.Trim();

        if (string.IsNullOrWhiteSpace(nome))
        {
            MessageBox.Show(
                "Informe o nome da franquia.",
                "Validação",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);

            _txtNome.Focus();

            return;
        }

        try
        {
            if (_edicao == null)
            {
                var franquia =
                    new Franquia
                    {
                        Nome = nome
                    };

                _service.Adicionar(
                    franquia);
            }
            else
            {
                _edicao.Nome = nome;

                _service.Atualizar(
                    _edicao);
            }

            DialogResult =
                DialogResult.OK;

            Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                ex.Message,
                "Erro",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }
}