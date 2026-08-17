using Vault.Models;
using Vault.Services;

namespace Vault.Telas;

public class TelaSeries : UserControl
{
    private readonly SerieService _serieService;
    private readonly FranquiaService _franquiaService;

    private DataGridView _gridSeries = null!;
    private TextBox _txtBusca = null!;

    private List<Serie> _series = [];

    #region CORES
    private static readonly Color Fundo =
        Color.FromArgb(13, 13, 16);

    private static readonly Color FundoGrid =
        Color.FromArgb(20, 20, 24);

    private static readonly Color FundoCabecalho =
        Color.FromArgb(28, 28, 33);

    private static readonly Color FundoInput =
        Color.FromArgb(25, 25, 30);

    private static readonly Color Roxo =
        Color.FromArgb(139, 92, 246);

    private static readonly Color RoxoEscuro =
        Color.FromArgb(92, 62, 170);

    private static readonly Color RoxoSelecao =
        Color.FromArgb(58, 42, 92);

    private static readonly Color Texto =
        Color.FromArgb(245, 245, 248);

    private static readonly Color TextoSecundario =
        Color.FromArgb(155, 155, 170);

    private static readonly Color Borda =
        Color.FromArgb(48, 48, 55);

    #endregion
    public TelaSeries()
    {
        _serieService = new SerieService();
        _franquiaService = new FranquiaService();

        Dock = DockStyle.Fill;
        BackColor = Fundo;

        CriarInterface();
        CarregarSeries();
    }


    // INTERFACE
    private void CriarInterface()
    {
        var layout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 1,
            RowCount = 3,
            BackColor = Fundo,
            Padding = new Padding(0)
        };

        layout.RowStyles.Add(
            new RowStyle(SizeType.Absolute, 62));

        layout.RowStyles.Add(
            new RowStyle(SizeType.Absolute, 58));

        layout.RowStyles.Add(
            new RowStyle(SizeType.Percent, 100));

        Controls.Add(layout);

        layout.Controls.Add(
            CriarBarraAcoes(),
            0,
            0);

        layout.Controls.Add(
            CriarBarraBusca(),
            0,
            1);

        _gridSeries = CriarGrid();

        layout.Controls.Add(
            _gridSeries,
            0,
            2);
    }


    // BARRA DE AÇÕES
    private Panel CriarBarraAcoes()
    {
        var painel = new Panel
        {
            Dock = DockStyle.Fill,
            BackColor = Color.Transparent
        };

        Button btnNovo = new Button
        {
            Text = "+  Nova série",
            Width = 135,
            Height = 40,
            BackColor = Roxo,
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Font = new Font(
                "Segoe UI Semibold",
                9.5F),
            Cursor = Cursors.Hand,
            Anchor =
                AnchorStyles.Top |
                AnchorStyles.Right
        };

        btnNovo.FlatAppearance.BorderSize = 0;

        painel.Controls.Add(btnNovo);

        painel.Resize += (_, _) =>
        {
            btnNovo.Left =
                painel.ClientSize.Width -
                btnNovo.Width;

            btnNovo.Top = 8;
        };

        btnNovo.MouseEnter += (_, _) =>
        {
            btnNovo.BackColor = RoxoEscuro;
        };

        btnNovo.MouseLeave += (_, _) =>
        {
            btnNovo.BackColor = Roxo;
        };

        btnNovo.Click += (_, _) =>
        {
            AbrirCadastro();
        };

        return painel;
    }


    // BUSCA
    private Panel CriarBarraBusca()
    {
        var painel = new Panel
        {
            Dock = DockStyle.Fill,
            BackColor = Color.Transparent
        };

        _txtBusca = new TextBox
        {
            Width = 380,
            Height = 34,
            Location = new Point(0, 8),
            BackColor = FundoInput,
            ForeColor = Texto,
            BorderStyle = BorderStyle.FixedSingle,
            Font = new Font(
                "Segoe UI",
                10F),
            PlaceholderText =
                "Pesquisar série..."
        };

        _txtBusca.TextChanged += (_, _) =>
        {
            AplicarBusca();
        };

        painel.Controls.Add(_txtBusca);

        return painel;
    }


    // GRID
    private DataGridView CriarGrid()
    {
        var grid = new DataGridView
        {
            Dock = DockStyle.Fill,

            BackgroundColor = FundoGrid,

            BorderStyle =
                BorderStyle.None,

            GridColor = Borda,

            AllowUserToAddRows = false,
            AllowUserToDeleteRows = false,
            AllowUserToResizeRows = false,

            ReadOnly = true,

            SelectionMode =
                DataGridViewSelectionMode.FullRowSelect,

            MultiSelect = false,

            AutoGenerateColumns = false,

            RowHeadersVisible = false,

            EnableHeadersVisualStyles = false,

            AutoSizeRowsMode =
                DataGridViewAutoSizeRowsMode.None,

            ColumnHeadersHeight = 44,

            RowTemplate =
            {
                Height = 48
            }
        };

        // --------------------------------------------------------
        // CABEÇALHO
        // --------------------------------------------------------

        grid.ColumnHeadersDefaultCellStyle =
            new DataGridViewCellStyle
            {
                BackColor = FundoCabecalho,
                ForeColor = TextoSecundario,

                Font = new Font(
                    "Segoe UI Semibold",
                    9F),

                Alignment =
                    DataGridViewContentAlignment.MiddleLeft,

                Padding = new Padding(12, 0, 12, 0),

                SelectionBackColor =
                    FundoCabecalho,

                SelectionForeColor =
                    TextoSecundario
            };

        // --------------------------------------------------------
        // CÉLULAS
        // --------------------------------------------------------

        grid.DefaultCellStyle =
            new DataGridViewCellStyle
            {
                BackColor = FundoGrid,
                ForeColor = Texto,

                SelectionBackColor =
                    RoxoSelecao,

                SelectionForeColor =
                    Color.White,

                Font = new Font(
                    "Segoe UI",
                    9.5F),

                Alignment =
                    DataGridViewContentAlignment.MiddleLeft,

                Padding =
                    new Padding(12, 0, 12, 0),

                NullValue = ""
            };

        grid.AlternatingRowsDefaultCellStyle =
            new DataGridViewCellStyle
            {
                BackColor =
                    Color.FromArgb(
                        23,
                        23,
                        28),

                ForeColor = Texto,

                SelectionBackColor =
                    RoxoSelecao,

                SelectionForeColor =
                    Color.White
            };


        // --------------------------------------------------------
        // COLUNAS
        // --------------------------------------------------------

        grid.Columns.Add(
            new DataGridViewTextBoxColumn
            {
                HeaderText = "Nome",
                DataPropertyName = "Nome",

                AutoSizeMode =
                    DataGridViewAutoSizeColumnMode.Fill,

                MinimumWidth = 250
            });

        grid.Columns.Add(
            new DataGridViewTextBoxColumn
            {
                HeaderText = "Temporadas",
                DataPropertyName =
                    "QuantidadeTemporadas",

                Width = 130,

                DefaultCellStyle =
                    new DataGridViewCellStyle
                    {
                        Alignment =
                            DataGridViewContentAlignment.MiddleCenter
                    }
            });

        grid.Columns.Add(
            new DataGridViewTextBoxColumn
            {
                HeaderText = "Avaliação",
                DataPropertyName =
                    "Avaliacao",

                Width = 125,

                DefaultCellStyle =
                    new DataGridViewCellStyle
                    {
                        Format = "0.0",
                        Alignment =
                            DataGridViewContentAlignment.MiddleCenter
                    }
            });

        grid.Columns.Add(
            new DataGridViewTextBoxColumn
            {
                HeaderText = "Finalizada",
                DataPropertyName =
                    "Finalizada",

                Width = 125,

                DefaultCellStyle =
                    new DataGridViewCellStyle
                    {
                        Alignment =
                            DataGridViewContentAlignment.MiddleCenter
                    }
            });

        grid.Columns.Add(
            new DataGridViewTextBoxColumn
            {
                HeaderText = "Franquia",

                Width = 220
            });


        // --------------------------------------------------------
        // EVENTOS
        // --------------------------------------------------------

        grid.CellFormatting +=
            Grid_CellFormatting;

        grid.CellDoubleClick += (_, _) =>
        {
            EditarSelecionado();
        };

        grid.KeyDown += (_, e) =>
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                EditarSelecionado();
            }

            if (e.KeyCode == Keys.Delete)
            {
                ExcluirSelecionado();
            }
        };

        return grid;
    }


    // FORMATAÇÃO DO GRID
    private void Grid_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
    {
        if (e.RowIndex < 0)
        {
            return;
        }

        if (e.ColumnIndex == 3 &&
            e.Value is bool finalizada)
        {
            e.Value =
                finalizada
                    ? "Sim"
                    : "Não";

            e.FormattingApplied = true;

            return;
        }

        if (e.ColumnIndex == 4 &&
            _gridSeries.Rows[e.RowIndex]
                .DataBoundItem is Serie serie)
        {
            if (serie.FranquiaId.HasValue)
            {
                var franquia =
                    _franquiaService
                        .ObterPorId(
                            serie.FranquiaId.Value);

                e.Value =
                    franquia?.Nome ?? "—";
            }
            else
            {
                e.Value = "—";
            }

            e.FormattingApplied = true;
        }
    }


    // CARREGAMENTO
    private void CarregarSeries()
    {
        _series =
            _serieService
                .ObterTodos();

        AtualizarGrid(_series);
    }

    private void AtualizarGrid(List<Serie> series)
    {
        _gridSeries.DataSource = null;

        _gridSeries.DataSource = series;
    }


    // BUSCA
    private void AplicarBusca()
    {
        string busca =
            _txtBusca.Text.Trim();

        if (string.IsNullOrWhiteSpace(busca))
        {
            AtualizarGrid(_series);
            return;
        }

        var resultado =
            _series
                .Where(s =>
                    s.Nome.Contains(
                        busca,
                        StringComparison.OrdinalIgnoreCase))
                .ToList();

        AtualizarGrid(resultado);
    }


    // CADASTRO
    private void AbrirCadastro()
    {
        using var form =
            new FormCadastroSerie(
                _serieService,
                _franquiaService);

        if (form.ShowDialog() ==
            DialogResult.OK)
        {
            CarregarSeries();
        }
    }


    // EDIÇÃO
    private void EditarSelecionado()
    {
        if (_gridSeries.CurrentRow?
            .DataBoundItem is not Serie serie)
        {
            return;
        }

        using var form =
            new FormCadastroSerie(
                _serieService,
                _franquiaService,
                serie);

        if (form.ShowDialog() ==
            DialogResult.OK)
        {
            CarregarSeries();
        }
    }


    // EXCLUSÃO
    private void ExcluirSelecionado()
    {
        if (_gridSeries.CurrentRow?
            .DataBoundItem is not Serie serie)
        {
            return;
        }

        DialogResult resultado =
            MessageBox.Show(
                $"Deseja realmente excluir a série \"{serie.Nome}\"?",
                "Excluir série",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

        if (resultado !=
            DialogResult.Yes)
        {
            return;
        }

        try
        {
            _serieService.Excluir(
                serie.Id);

            CarregarSeries();
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