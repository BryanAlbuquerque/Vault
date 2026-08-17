using Vault.Models;
using Vault.Services;

namespace Vault.Telas;

public class FormCadastroFilme : Form
{
    private readonly FilmeService _filmeService;
    private readonly FranquiaService _franquiaService;

    private readonly Filme? _filmeEdicao;

    private TextBox _txtNome = null!;
    private NumericUpDown _numAvaliacao = null!;
    private CheckBox _chkAssistido = null!;
    private ComboBox _cmbFranquia = null!;

    #region CORES
    private static readonly Color Fundo =
        Color.FromArgb(11, 13, 18);

    private static readonly Color FundoPainel =
        Color.FromArgb(18, 21, 28);

    private static readonly Color FundoInput =
        Color.FromArgb(22, 25, 33);

    private static readonly Color FundoInputHover =
        Color.FromArgb(27, 30, 40);

    private static readonly Color Roxo =
        Color.FromArgb(124, 92, 255);

    private static readonly Color RoxoHover =
        Color.FromArgb(143, 113, 255);

    private static readonly Color RoxoEscuro =
        Color.FromArgb(54, 43, 105);

    private static readonly Color TextoPrincipal =
        Color.FromArgb(240, 242, 247);

    private static readonly Color TextoSecundario =
        Color.FromArgb(150, 157, 172);

    private static readonly Color Borda =
        Color.FromArgb(45, 49, 60);
    #endregion

    public FormCadastroFilme(
        FilmeService filmeService,
        FranquiaService franquiaService,
        Filme? filme = null)
    {
        _filmeService = filmeService;
        _franquiaService = franquiaService;
        _filmeEdicao = filme;

        InicializarFormulario();
        CriarInterface();
        CarregarFranquias();

        if (_filmeEdicao != null)
        {
            CarregarDados();
        }
    }


    // FORMULÁRIO
    private void InicializarFormulario()
    {
        Text = _filmeEdicao == null
            ? "Novo filme"
            : "Editar filme";

        StartPosition =
            FormStartPosition.CenterParent;

        Size = new Size(620, 560);

        MinimumSize =
            new Size(620, 560);

        MaximumSize =
            new Size(620, 560);

        MaximizeBox = false;
        MinimizeBox = false;

        BackColor = Fundo;

        Font =
            new Font(
                "Segoe UI",
                10F);

        FormBorderStyle =
            FormBorderStyle.FixedSingle;

        ShowInTaskbar = false;
    }


    // INTERFACE
    private void CriarInterface()
    {
        var layoutPrincipal =
            new TableLayoutPanel
            {
                Dock = DockStyle.Fill,

                ColumnCount = 1,

                RowCount = 3,

                BackColor = Fundo,

                Padding =
                    new Padding(
                        28,
                        24,
                        28,
                        24)
            };


        layoutPrincipal.RowStyles.Add(
            new RowStyle(
                SizeType.Absolute,
                75));


        layoutPrincipal.RowStyles.Add(
            new RowStyle(
                SizeType.Percent,
                100));


        layoutPrincipal.RowStyles.Add(
            new RowStyle(
                SizeType.Absolute,
                55));


        Controls.Add(layoutPrincipal);


        // =====================================================
        // CABEÇALHO
        // =====================================================

        Panel cabecalho =
            CriarCabecalho();

        layoutPrincipal.Controls.Add(
            cabecalho,
            0,
            0);


        // =====================================================
        // CONTEÚDO
        // =====================================================

        Panel conteudo =
            CriarConteudo();

        layoutPrincipal.Controls.Add(
            conteudo,
            0,
            1);


        // =====================================================
        // BOTÕES
        // =====================================================

        Panel botoes =
            CriarBotoes();

        layoutPrincipal.Controls.Add(
            botoes,
            0,
            2);
    }


    // CABEÇALHO
    private Panel CriarCabecalho()
    {
        var painel =
            new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent
            };


        Label icone =
            new Label
            {
                Text = "▣",

                AutoSize = false,

                Width = 45,

                Height = 55,

                Location =
                    new Point(
                        0,
                        3),

                ForeColor = Roxo,

                Font =
                    new Font(
                        "Segoe UI Semibold",
                        22F),

                TextAlign =
                    ContentAlignment.MiddleCenter
            };


        Label titulo =
            new Label
            {
                Text =
                    _filmeEdicao == null
                        ? "Novo filme"
                        : "Editar filme",

                AutoSize = true,

                Location =
                    new Point(
                        50,
                        3),

                ForeColor =
                    TextoPrincipal,

                Font =
                    new Font(
                        "Segoe UI Semibold",
                        20F)
            };


        Label subtitulo =
            new Label
            {
                Text =
                    _filmeEdicao == null
                        ? "Adicione um filme ao seu catálogo"
                        : "Atualize as informações deste filme",

                AutoSize = true,

                Location =
                    new Point(
                        52,
                        38),

                ForeColor =
                    TextoSecundario,

                Font =
                    new Font(
                        "Segoe UI",
                        8.5F)
            };


        painel.Controls.Add(icone);
        painel.Controls.Add(titulo);
        painel.Controls.Add(subtitulo);


        return painel;
    }


    // CONTEÚDO
    private Panel CriarConteudo()
    {
        var painel =
            new Panel
            {
                Dock = DockStyle.Fill,

                BackColor =
                    FundoPainel,

                Padding =
                    new Padding(
                        22)
            };


        var layout =
            new TableLayoutPanel
            {
                Dock = DockStyle.Fill,

                ColumnCount = 1,

                RowCount = 4,

                BackColor =
                    Color.Transparent
            };


        layout.RowStyles.Add(
            new RowStyle(
                SizeType.Absolute,
                72));


        layout.RowStyles.Add(
            new RowStyle(
                SizeType.Absolute,
                72));


        layout.RowStyles.Add(
            new RowStyle(
                SizeType.Absolute,
                72));


        layout.RowStyles.Add(
            new RowStyle(
                SizeType.Percent,
                100));


        painel.Controls.Add(layout);


        // =====================================================
        // NOME
        // =====================================================

        _txtNome =
            CriarTextBox();

        layout.Controls.Add(
            CriarCampo(
                "NOME DO FILME",
                _txtNome),
            0,
            0);


        // =====================================================
        // AVALIAÇÃO
        // =====================================================

        _numAvaliacao =
            new NumericUpDown
            {
                Dock = DockStyle.Fill,

                Minimum = 0,

                Maximum = 10,

                DecimalPlaces = 1,

                Increment = 0.5M,

                Value = 0,

                BackColor =
                    FundoInput,

                ForeColor =
                    TextoPrincipal,

                Font =
                    new Font(
                        "Segoe UI Semibold",
                        11F),

                TextAlign =
                    HorizontalAlignment.Left,

                BorderStyle =
                    BorderStyle.FixedSingle
            };


        layout.Controls.Add(
            CriarCampo(
                "AVALIAÇÃO",
                _numAvaliacao),
            0,
            1);


        // =====================================================
        // FRANQUIA
        // =====================================================

        _cmbFranquia =
            new ComboBox
            {
                Dock = DockStyle.Fill,

                DropDownStyle =
                    ComboBoxStyle.DropDownList,

                BackColor =
                    FundoInput,

                ForeColor =
                    TextoPrincipal,

                Font =
                    new Font(
                        "Segoe UI",
                        10F),

                FlatStyle =
                    FlatStyle.Flat
            };


        layout.Controls.Add(
            CriarCampo(
                "FRANQUIA",
                _cmbFranquia),
            0,
            2);


        // =====================================================
        // ASSISTIDO
        // =====================================================

        Panel painelAssistido =
            CriarPainelAssistido();


        layout.Controls.Add(
            painelAssistido,
            0,
            3);


        return painel;
    }


    // CAMPO
    private Panel CriarCampo(string titulo, Control controle)
    {
        var painel =
            new Panel
            {
                Dock = DockStyle.Fill,

                BackColor =
                    Color.Transparent
            };


        Label label =
            new Label
            {
                Text = titulo,

                Dock = DockStyle.Top,

                Height = 22,

                ForeColor =
                    TextoSecundario,

                Font =
                    new Font(
                        "Segoe UI Semibold",
                        8F)
            };


        controle.Dock =
            DockStyle.Fill;


        painel.Controls.Add(controle);
        painel.Controls.Add(label);


        // Pequeno efeito visual ao entrar no campo.

        controle.Enter += (_, _) =>
        {
            if (controle is TextBox)
            {
                controle.BackColor =
                    FundoInputHover;
            }
        };


        controle.Leave += (_, _) =>
        {
            if (controle is TextBox)
            {
                controle.BackColor =
                    FundoInput;
            }
        };


        return painel;
    }


    // CHECKBOX ASSISTIDO
    private Panel CriarPainelAssistido()
    {
        var painel =
            new Panel
            {
                Dock = DockStyle.Fill,

                BackColor =
                    Color.Transparent
            };


        var caixa =
            new Panel
            {
                Dock = DockStyle.Fill,

                BackColor =
                    FundoInput,

                Padding =
                    new Padding(
                        14,
                        0,
                        14,
                        0)
            };


        _chkAssistido =
            new CheckBox
            {
                Text =
                    "Já assisti este filme",

                Dock = DockStyle.Left,

                Width = 220,

                ForeColor =
                    TextoPrincipal,

                Font =
                    new Font(
                        "Segoe UI Semibold",
                        10F),

                AutoSize = false,

                FlatStyle =
                    FlatStyle.Standard,

                Cursor =
                    Cursors.Hand
            };


        caixa.Controls.Add(
            _chkAssistido);


        Label descricao =
            new Label
            {
                Text =
                    "Marque esta opção quando o filme já estiver concluído.",

                Dock = DockStyle.Fill,

                ForeColor =
                    TextoSecundario,

                Font =
                    new Font(
                        "Segoe UI",
                        8.5F),

                TextAlign =
                    ContentAlignment.MiddleRight
            };


        caixa.Controls.Add(
            descricao);


        painel.Controls.Add(
            caixa);


        return painel;
    }


    // TEXTBOX
    private TextBox CriarTextBox()
    {
        return new TextBox
        {
            Dock = DockStyle.Fill,

            BackColor =
                FundoInput,

            ForeColor =
                TextoPrincipal,

            BorderStyle =
                BorderStyle.FixedSingle,

            Font =
                new Font(
                    "Segoe UI",
                    10F),

            Margin =
                new Padding(0)
        };
    }


    // BOTÕES
    private Panel CriarBotoes()
    {
        var painel =
            new Panel
            {
                Dock = DockStyle.Fill,

                BackColor =
                    Color.Transparent
            };


        Button btnCancelar =
            new Button
            {
                Text = "Cancelar",

                Width = 120,

                Height = 40,

                Anchor =
                    AnchorStyles.Top |
                    AnchorStyles.Right,

                BackColor =
                    Color.FromArgb(
                        30,
                        34,
                        43),

                ForeColor =
                    TextoPrincipal,

                FlatStyle =
                    FlatStyle.Flat,

                Font =
                    new Font(
                        "Segoe UI Semibold",
                        9.5F),

                Cursor =
                    Cursors.Hand
            };


        btnCancelar.FlatAppearance.BorderSize = 1;

        btnCancelar.FlatAppearance.BorderColor =
            Borda;


        btnCancelar.MouseEnter += (_, _) =>
        {
            btnCancelar.BackColor =
                Color.FromArgb(
                    38,
                    42,
                    52);
        };


        btnCancelar.MouseLeave += (_, _) =>
        {
            btnCancelar.BackColor =
                Color.FromArgb(
                    30,
                    34,
                    43);
        };


        btnCancelar.Click += (_, _) =>
        {
            DialogResult =
                DialogResult.Cancel;

            Close();
        };


        Button btnSalvar =
            new Button
            {
                Text = "Salvar filme",

                Width = 145,

                Height = 40,

                Anchor =
                    AnchorStyles.Top |
                    AnchorStyles.Right,

                BackColor =
                    Roxo,

                ForeColor =
                    Color.White,

                FlatStyle =
                    FlatStyle.Flat,

                Font =
                    new Font(
                        "Segoe UI Semibold",
                        9.5F),

                Cursor =
                    Cursors.Hand
            };


        btnSalvar.FlatAppearance.BorderSize = 0;


        btnSalvar.MouseEnter += (_, _) =>
        {
            btnSalvar.BackColor =
                RoxoHover;
        };


        btnSalvar.MouseLeave += (_, _) =>
        {
            btnSalvar.BackColor =
                Roxo;
        };


        btnSalvar.Click += (_, _) =>
        {
            Salvar();
        };


        painel.Controls.Add(
            btnCancelar);

        painel.Controls.Add(
            btnSalvar);


        painel.Resize += (_, _) =>
        {
            btnSalvar.Left =
                painel.ClientSize.Width -
                btnSalvar.Width;

            btnSalvar.Top = 5;


            btnCancelar.Left =
                btnSalvar.Left -
                btnCancelar.Width -
                10;

            btnCancelar.Top = 5;
        };


        return painel;
    }


    // FRANQUIAS
    private void CarregarFranquias()
    {
        _cmbFranquia.Items.Clear();


        _cmbFranquia.Items.Add(
            new ItemFranquia(
                null,
                "Nenhuma franquia"));


        foreach (
            Franquia franquia
            in _franquiaService
                .ObterTodos()
                .OrderBy(f => f.Nome))
        {
            _cmbFranquia.Items.Add(
                new ItemFranquia(
                    franquia.Id,
                    franquia.Nome));
        }


        _cmbFranquia.SelectedIndex = 0;
    }


    // CARREGAR DADOS
    private void CarregarDados()
    {
        _txtNome.Text =
            _filmeEdicao!.Nome;


        _numAvaliacao.Value =
            _filmeEdicao.Avaliacao;


        _chkAssistido.Checked =
            _filmeEdicao.Assistido;


        if (_filmeEdicao.FranquiaId.HasValue)
        {
            for (
                int i = 0;
                i < _cmbFranquia.Items.Count;
                i++)
            {
                if (
                    _cmbFranquia.Items[i]
                        is ItemFranquia item &&
                    item.Id ==
                        _filmeEdicao.FranquiaId)
                {
                    _cmbFranquia.SelectedIndex =
                        i;

                    break;
                }
            }
        }
    }


    // SALVAR
    private void Salvar()
    {
        try
        {
            if (string.IsNullOrWhiteSpace(
                _txtNome.Text))
            {
                MessageBox.Show(
                    "Informe o nome do filme.",
                    "Validação",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                _txtNome.Focus();

                return;
            }


            var itemFranquia =
                _cmbFranquia.SelectedItem
                as ItemFranquia;


            int? franquiaId =
                itemFranquia?.Id;


            // =================================================
            // NOVO FILME
            // =================================================

            if (_filmeEdicao == null)
            {
                var filme =
                    new Filme
                    {
                        Nome =
                            _txtNome.Text.Trim(),

                        Avaliacao =
                            _numAvaliacao.Value,

                        Assistido =
                            _chkAssistido.Checked,

                        FranquiaId =
                            franquiaId
                    };


                _filmeService.Adicionar(
                    filme);
            }


            // =================================================
            // EDIÇÃO
            // =================================================

            else
            {
                _filmeEdicao.Nome =
                    _txtNome.Text.Trim();


                _filmeEdicao.Avaliacao =
                    _numAvaliacao.Value;


                _filmeEdicao.Assistido =
                    _chkAssistido.Checked;


                _filmeEdicao.FranquiaId =
                    franquiaId;


                _filmeService.Atualizar(
                    _filmeEdicao);
            }


            DialogResult =
                DialogResult.OK;


            Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                ex.Message,
                "Erro ao salvar filme",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }


    // ITEM DA COMBOBOX
    private class ItemFranquia
    {
        public int? Id { get; }

        public string Nome { get; }


        public ItemFranquia(
            int? id,
            string nome)
        {
            Id = id;

            Nome = nome;
        }


        public override string ToString()
        {
            return Nome;
        }
    }
}