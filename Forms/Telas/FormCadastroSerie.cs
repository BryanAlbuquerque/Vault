using Vault.Models;
using Vault.Services;

namespace Vault.Telas;

public class FormCadastroSerie : Form
{
    private readonly SerieService _serieService;
    private readonly FranquiaService _franquiaService;

    private readonly Serie? _serieEdicao;

    private TextBox _txtNome = null!;
    private NumericUpDown _numTemporadas = null!;
    private NumericUpDown _numAvaliacao = null!;
    private CheckBox _chkAssistida = null!;
    private CheckBox _chkFinalizada = null!;
    private ComboBox _cmbFranquia = null!;


    #region CORES
    private static readonly Color Fundo =
        Color.FromArgb(13, 13, 16);

    private static readonly Color FundoInput =
        Color.FromArgb(25, 25, 30);

    private static readonly Color FundoSecundario =
        Color.FromArgb(28, 28, 33);

    private static readonly Color Roxo =
        Color.FromArgb(139, 92, 246);

    private static readonly Color RoxoEscuro =
        Color.FromArgb(103, 68, 190);

    private static readonly Color Texto =
        Color.FromArgb(245, 245, 248);

    private static readonly Color TextoSecundario =
        Color.FromArgb(160, 160, 175);
    #endregion

    public FormCadastroSerie(
        SerieService serieService,
        FranquiaService franquiaService,
        Serie? serie = null)
    {
        _serieService = serieService;
        _franquiaService = franquiaService;
        _serieEdicao = serie;

        InicializarFormulario();
        CriarInterface();
        CarregarFranquias();

        if (_serieEdicao != null)
        {
            CarregarDados();
        }
    }


    // FORMULÁRIO
    private void InicializarFormulario()
    {
        Text = _serieEdicao == null
            ? "Nova Série"
            : "Editar Série";

        StartPosition =
            FormStartPosition.CenterParent;

        Size =
            new Size(570, 600);

        MinimumSize =
            new Size(570, 600);

        MaximizeBox = false;
        MinimizeBox = false;

        BackColor = Fundo;

        Font =
            new Font(
                "Segoe UI",
                10F);
    }


    // INTERFACE
    private void CriarInterface()
    {
        var layout =
            new TableLayoutPanel
            {
                Dock = DockStyle.Fill,

                Padding =
                    new Padding(
                        32,
                        28,
                        32,
                        28),

                ColumnCount = 1,
                RowCount = 8,

                BackColor = Fundo
            };

        layout.RowStyles.Add(
            new RowStyle(
                SizeType.Absolute,
                60));

        layout.RowStyles.Add(
            new RowStyle(
                SizeType.Absolute,
                70));

        layout.RowStyles.Add(
            new RowStyle(
                SizeType.Absolute,
                70));

        layout.RowStyles.Add(
            new RowStyle(
                SizeType.Absolute,
                70));

        layout.RowStyles.Add(
            new RowStyle(
                SizeType.Absolute,
                48));

        layout.RowStyles.Add(
            new RowStyle(
                SizeType.Absolute,
                48));

        layout.RowStyles.Add(
            new RowStyle(
                SizeType.Percent,
                100));

        layout.RowStyles.Add(
            new RowStyle(
                SizeType.Absolute,
                52));

        Controls.Add(layout);


        // --------------------------------------------------------
        // TÍTULO
        // --------------------------------------------------------

        Label titulo =
            new Label
            {
                Text =
                    _serieEdicao == null
                        ? "Nova série"
                        : "Editar série",

                Dock = DockStyle.Fill,

                ForeColor = Texto,

                Font =
                    new Font(
                        "Segoe UI Semibold",
                        21F),

                TextAlign =
                    ContentAlignment.MiddleLeft
            };

        layout.Controls.Add(
            titulo,
            0,
            0);


        // --------------------------------------------------------
        // NOME
        // --------------------------------------------------------

        _txtNome =
            CriarTextBox();

        layout.Controls.Add(
            CriarCampo(
                "Nome da série",
                _txtNome),
            0,
            1);


        // --------------------------------------------------------
        // TEMPORADAS
        // --------------------------------------------------------

        _numTemporadas =
            CriarNumerico(
                1,
                100);

        layout.Controls.Add(
            CriarCampo(
                "Quantidade de temporadas",
                _numTemporadas),
            0,
            2);


        // --------------------------------------------------------
        // AVALIAÇÃO
        // --------------------------------------------------------

        _numAvaliacao =
            CriarNumerico(
                0,
                10);

        _numAvaliacao.DecimalPlaces =
            1;

        _numAvaliacao.Increment =
            0.5M;

        layout.Controls.Add(
            CriarCampo(
                "Sua avaliação",
                _numAvaliacao),
            0,
            3);


        // --------------------------------------------------------
        // CHECKBOX ASSISTIDA
        // --------------------------------------------------------

        _chkAssistida =
            CriarCheckBox(
                "Já assisti");

        layout.Controls.Add(
            _chkAssistida,
            0,
            4);


        // --------------------------------------------------------
        // CHECKBOX FINALIZADA
        // --------------------------------------------------------

        _chkFinalizada =
            CriarCheckBox(
                "Série finalizada");

        layout.Controls.Add(
            _chkFinalizada,
            0,
            5);


        // --------------------------------------------------------
        // FRANQUIA
        // --------------------------------------------------------

        _cmbFranquia =
            new ComboBox
            {
                Dock = DockStyle.Fill,

                DropDownStyle =
                    ComboBoxStyle.DropDownList,

                BackColor =
                    FundoInput,

                ForeColor =
                    Texto,

                FlatStyle =
                    FlatStyle.Flat,

                Font =
                    new Font(
                        "Segoe UI",
                        10F)
            };

        layout.Controls.Add(
            CriarCampo(
                "Franquia / Universo",
                _cmbFranquia),
            0,
            6);


        // --------------------------------------------------------
        // BOTÕES
        // --------------------------------------------------------

        var botoes =
            new TableLayoutPanel
            {
                Dock = DockStyle.Fill,

                ColumnCount = 2,
                RowCount = 1,

                BackColor =
                    Color.Transparent
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
            CriarBotao(
                "Cancelar",
                FundoSecundario);

        Button salvar =
            CriarBotao(
                "Salvar série",
                Roxo);


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
            7);
    }


    // TEXTBOX
    private TextBox CriarTextBox()
    {
        var campo =
            new TextBox
            {
                Dock = DockStyle.Fill,

                BackColor =
                    FundoInput,

                ForeColor =
                    Texto,

                BorderStyle =
                    BorderStyle.FixedSingle,

                Font =
                    new Font(
                        "Segoe UI",
                        10.5F)
            };

        return campo;
    }


    // NUMERIC
    private NumericUpDown CriarNumerico(decimal minimo, decimal maximo)
    {
        return new NumericUpDown
        {
            Dock = DockStyle.Fill,

            Minimum = minimo,

            Maximum = maximo,

            BackColor =
                FundoInput,

            ForeColor =
                Texto,

            BorderStyle =
                BorderStyle.FixedSingle,

            Font =
                new Font(
                    "Segoe UI",
                    10.5F)
        };
    }


    // CHECKBOX
    private CheckBox CriarCheckBox(string texto)
    {
        var check =
            new CheckBox
            {
                Text = texto,

                Dock = DockStyle.Fill,

                ForeColor =
                    Texto,

                Font =
                    new Font(
                        "Segoe UI Semibold",
                        9.5F),

                AutoSize = false,

                Cursor =
                    Cursors.Hand
            };

        check.CheckedChanged += (_, _) =>
        {
            if (check.Checked)
            {
                check.ForeColor = Roxo;
            }
            else
            {
                check.ForeColor = Texto;
            }
        };

        return check;
    }


    // BOTÃO
    private Button CriarBotao( string texto,Color fundo)
    {
        var botao =
            new Button
            {
                Text = texto,

                Dock = DockStyle.Fill,

                BackColor =
                    fundo,

                ForeColor =
                    Color.White,

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
                        5,
                        0,
                        5,
                        0)
            };

        botao.FlatAppearance.BorderSize = 0;

        if (fundo == Roxo)
        {
            botao.MouseEnter += (_, _) =>
            {
                botao.BackColor =
                    RoxoEscuro;
            };

            botao.MouseLeave += (_, _) =>
            {
                botao.BackColor =
                    Roxo;
            };
        }

        return botao;
    }


    // CAMPO
    private Panel CriarCampo( string titulo, Control controle)
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
                        8.5F)
            };

        controle.Dock =
            DockStyle.Fill;

        painel.Controls.Add(
            controle);

        painel.Controls.Add(
            label);

        return painel;
    }


    // FRANQUIAS
    private void CarregarFranquias()
    {
        _cmbFranquia.Items.Clear();

        _cmbFranquia.Items.Add(
            new ItemFranquia(
                null,
                "Nenhuma"));

        foreach (
            var franquia
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


    // CARREGAR EDIÇÃO
    private void CarregarDados()
    {
        _txtNome.Text =
            _serieEdicao!.Nome;

        _numTemporadas.Value =
            _serieEdicao
                .QuantidadeTemporadas;

        _numAvaliacao.Value =
            _serieEdicao.Avaliacao;

        _chkAssistida.Checked =
            _serieEdicao.Assistida;

        _chkFinalizada.Checked =
            _serieEdicao.Finalizada;

        if (_serieEdicao.FranquiaId.HasValue)
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
                        _serieEdicao.FranquiaId)
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
                    "Informe o nome da série.",
                    "Validação",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                _txtNome.Focus();

                return;
            }


            if (
                _chkFinalizada.Checked &&
                !_chkAssistida.Checked)
            {
                MessageBox.Show(
                    "Uma série finalizada precisa estar marcada como assistida.",
                    "Validação",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }


            var item =
                _cmbFranquia.SelectedItem
                as ItemFranquia;

            int? franquiaId =
                item?.Id;


            if (_serieEdicao == null)
            {
                var serie =
                    new Serie
                    {
                        Nome =
                            _txtNome.Text.Trim(),

                        QuantidadeTemporadas =
                            (int)_numTemporadas.Value,

                        Avaliacao =
                            _numAvaliacao.Value,

                        Assistida =
                            _chkAssistida.Checked,

                        Finalizada =
                            _chkFinalizada.Checked,

                        FranquiaId =
                            franquiaId
                    };

                _serieService.Adicionar(
                    serie);
            }
            else
            {
                _serieEdicao.Nome =
                    _txtNome.Text.Trim();

                _serieEdicao.QuantidadeTemporadas =
                    (int)_numTemporadas.Value;

                _serieEdicao.Avaliacao =
                    _numAvaliacao.Value;

                _serieEdicao.Assistida =
                    _chkAssistida.Checked;

                _serieEdicao.Finalizada =
                    _chkFinalizada.Checked;

                _serieEdicao.FranquiaId =
                    franquiaId;

                _serieService.Atualizar(
                    _serieEdicao);
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


    // ITEM FRANQUIA
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