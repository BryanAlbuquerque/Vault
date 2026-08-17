using Vault.Models;
using Vault.Services;
using Vault.Telas;

namespace Vault.Forms.Telas;

public class TelaFilmes : UserControl
{
    private readonly FilmeService _filmeService;
    private readonly FranquiaService _franquiaService;

    private DataGridView _gridFilmes = null!;
    private TextBox _txtBusca = null!;

    private List<Filme> _filmes = [];


    #region CORES
    private static readonly Color Fundo =
        Color.FromArgb(11, 13, 18);

    private static readonly Color FundoGrid =
        Color.FromArgb(18, 21, 28);

    private static readonly Color FundoCabecalho =
        Color.FromArgb(25, 29, 38);

    private static readonly Color FundoInput =
        Color.FromArgb(22, 25, 33);

    private static readonly Color Roxo =
        Color.FromArgb(124, 92, 255);

    private static readonly Color RoxoHover =
        Color.FromArgb(143, 113, 255);

    private static readonly Color RoxoSelecao =
        Color.FromArgb(54, 43, 105);

    private static readonly Color TextoPrincipal =
        Color.FromArgb(240, 242, 247);

    private static readonly Color TextoSecundario =
        Color.FromArgb(150, 157, 172);

    private static readonly Color Linha =
        Color.FromArgb(40, 44, 54);

#endregion
    public TelaFilmes()
    {
        _filmeService = new FilmeService();
        _franquiaService = new FranquiaService();

        Dock = DockStyle.Fill;
        BackColor = Fundo;

        CriarInterface();
        CarregarFilmes();
    }


    private void CriarInterface()
    {
        var layout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 1,
            RowCount = 3,
            BackColor = Fundo,
            Margin = new Padding(0),
            Padding = new Padding(0)
        };

        layout.RowStyles.Add(
            new RowStyle(SizeType.Absolute, 60));

        layout.RowStyles.Add(
            new RowStyle(SizeType.Absolute, 60));

        layout.RowStyles.Add(
            new RowStyle(SizeType.Percent, 100));

        Controls.Add(layout);


        Panel barraAcoes = CriarBarraAcoes();

        layout.Controls.Add(
            barraAcoes,
            0,
            0);


        Panel barraBusca = CriarBarraBusca();

        layout.Controls.Add(
            barraBusca,
            0,
            1);


        _gridFilmes = CriarGrid();

        layout.Controls.Add(
            _gridFilmes,
            0,
            2);
    }

    private Panel CriarBarraAcoes()
    {
        var painel = new Panel
        {
            Dock = DockStyle.Fill,
            BackColor = Color.Transparent
        };


        Button btnNovo = new Button
        {
            Text = "+  Novo filme",
            Width = 135,
            Height = 38,
            Anchor =
                AnchorStyles.Top |
                AnchorStyles.Right,
            BackColor = Roxo,
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Font = new Font(
                "Segoe UI Semibold",
                9.5F),
            Cursor = Cursors.Hand
        };

        btnNovo.FlatAppearance.BorderSize = 0;


        btnNovo.MouseEnter += (_, _) =>
        {
            btnNovo.BackColor = RoxoHover;
        };

        btnNovo.MouseLeave += (_, _) =>
        {
            btnNovo.BackColor = Roxo;
        };


        btnNovo.Click += (_, _) =>
        {
            AbrirCadastro();
        };


        painel.Controls.Add(btnNovo);


        painel.Resize += (_, _) =>
        {
            btnNovo.Left =
                painel.ClientSize.Width -
                btnNovo.Width;

            btnNovo.Top = 5;
        };


        return painel;
    }

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
            Location = new Point(0, 10),
            BackColor = FundoInput,
            ForeColor = TextoPrincipal,
            BorderStyle = BorderStyle.FixedSingle,
            Font = new Font(
                "Segoe UI",
                10F)
        };


        _txtBusca.PlaceholderText =
            "Pesquisar filme...";


        _txtBusca.TextChanged += (_, _) =>
        {
            AplicarBusca();
        };


        painel.Controls.Add(_txtBusca);


        return painel;
    }

    private DataGridView CriarGrid()
    {
        var grid = new DataGridView
        {
            Dock = DockStyle.Fill,

            BackgroundColor = FundoGrid,

            BorderStyle = BorderStyle.None,

            GridColor = Linha,

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

            // IMPORTANTE:
            // impede o DataGridView de recalcular
            // automaticamente a altura das linhas.
            AutoSizeRowsMode =
                DataGridViewAutoSizeRowsMode.None,

            // Altura padrão das linhas.
            RowTemplate =
            {
                Height = 50
            },

            // Altura fixa do cabeçalho.
            ColumnHeadersHeight = 44,

            ColumnHeadersHeightSizeMode =
                DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        };


        // =========================================================
        // CABEÇALHO
        // =========================================================

        grid.ColumnHeadersDefaultCellStyle =
            new DataGridViewCellStyle
            {
                BackColor = FundoCabecalho,

                ForeColor =
                    Color.FromArgb(
                        170,
                        176,
                        190),

                Font =
                    new Font(
                        "Segoe UI Semibold",
                        9F),

                Alignment =
                    DataGridViewContentAlignment.MiddleLeft,

                Padding =
                    new Padding(
                        14,
                        0,
                        14,
                        0),

                SelectionBackColor =
                    FundoCabecalho,

                SelectionForeColor =
                    Color.FromArgb(
                        170,
                        176,
                        190)
            };


        // =========================================================
        // CÉLULAS
        // =========================================================

        grid.DefaultCellStyle =
            new DataGridViewCellStyle
            {
                BackColor = FundoGrid,

                ForeColor = TextoPrincipal,

                SelectionBackColor =
                    RoxoSelecao,

                SelectionForeColor =
                    Color.White,

                Font =
                    new Font(
                        "Segoe UI",
                        10F),

                Alignment =
                    DataGridViewContentAlignment.MiddleLeft,

                Padding =
                    new Padding(
                        14,
                        0,
                        14,
                        0),

                NullValue = ""
            };


        // =========================================================
        // COLUNA NOME
        // =========================================================

        grid.Columns.Add(
            new DataGridViewTextBoxColumn
            {
                Name = "colNome",

                HeaderText = "Nome",

                DataPropertyName = "Nome",

                AutoSizeMode =
                    DataGridViewAutoSizeColumnMode.Fill,

                MinimumWidth = 250,

                SortMode =
                    DataGridViewColumnSortMode.NotSortable
            });


        // =========================================================
        // COLUNA AVALIAÇÃO
        // =========================================================

        grid.Columns.Add(
            new DataGridViewTextBoxColumn
            {
                Name = "colAvaliacao",

                HeaderText = "Avaliação",

                DataPropertyName =
                    "Avaliacao",

                Width = 130,

                MinimumWidth = 130,

                SortMode =
                    DataGridViewColumnSortMode.NotSortable,

                DefaultCellStyle =
                    new DataGridViewCellStyle
                    {
                        Format = "0.0",

                        Alignment =
                            DataGridViewContentAlignment.MiddleCenter
                    }
            });


        // =========================================================
        // COLUNA ASSISTIDO
        // =========================================================

        grid.Columns.Add(
            new DataGridViewTextBoxColumn
            {
                Name = "colAssistido",

                HeaderText = "Assistido",

                DataPropertyName =
                    "Assistido",

                Width = 130,

                MinimumWidth = 130,

                SortMode =
                    DataGridViewColumnSortMode.NotSortable,

                DefaultCellStyle =
                    new DataGridViewCellStyle
                    {
                        Alignment =
                            DataGridViewContentAlignment.MiddleCenter
                    }
            });


        // =========================================================
        // COLUNA FRANQUIA
        // =========================================================

        grid.Columns.Add(
            new DataGridViewTextBoxColumn
            {
                Name = "colFranquia",

                HeaderText = "Franquia",

                Width = 220,

                MinimumWidth = 180,

                SortMode =
                    DataGridViewColumnSortMode.NotSortable
            });


        // =========================================================
        // EVENTOS
        // =========================================================

        grid.CellFormatting +=
            GridFilmes_CellFormatting;


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

    private void GridFilmes_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
    {
        if (e.RowIndex < 0)
        {
            return;
        }


        if (_gridFilmes.Rows[e.RowIndex]
            .DataBoundItem is not Filme filme)
        {
            return;
        }


        // =========================================================
        // ASSISTIDO
        // =========================================================

        if (e.ColumnIndex == 2)
        {
            if (e.Value is bool assistido)
            {
                e.Value =
                    assistido
                        ? "Sim"
                        : "Não";

                e.FormattingApplied = true;
            }
        }


        // =========================================================
        // FRANQUIA
        // =========================================================

        if (e.ColumnIndex == 3)
        {
            if (filme.FranquiaId.HasValue)
            {
                var franquia =
                    _franquiaService.ObterPorId(
                        filme.FranquiaId.Value);

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

    private void CarregarFilmes()
    {
        _filmes =
            _filmeService.ObterTodos();

        AtualizarGrid(_filmes);
    }

    private void AtualizarGrid(List<Filme> filmes)
    {
        _gridFilmes.DataSource = null;

        _gridFilmes.DataSource = filmes;

        // Garante novamente a altura das linhas
        // depois de alterar o DataSource.
        _gridFilmes.RowTemplate.Height = 50;

        foreach (DataGridViewRow row
            in _gridFilmes.Rows)
        {
            row.Height = 50;
        }
    }

    private void AplicarBusca()
    {
        string busca =
            _txtBusca.Text.Trim();


        if (string.IsNullOrWhiteSpace(busca))
        {
            AtualizarGrid(_filmes);

            return;
        }


        var resultado =
            _filmes
                .Where(f =>
                    f.Nome.Contains(
                        busca,
                        StringComparison.OrdinalIgnoreCase))
                .ToList();


        AtualizarGrid(resultado);
    }

    private void AbrirCadastro()
    {
        using var form =
            new FormCadastroFilme(
                _filmeService,
                _franquiaService);


        if (form.ShowDialog() ==
            DialogResult.OK)
        {
            CarregarFilmes();
        }
    }

    private void EditarSelecionado()
    {
        if (_gridFilmes.CurrentRow?
            .DataBoundItem is not Filme filme)
        {
            return;
        }


        using var form =
            new FormCadastroFilme(
                _filmeService,
                _franquiaService,
                filme);


        if (form.ShowDialog() ==
            DialogResult.OK)
        {
            CarregarFilmes();
        }
    }

    private void ExcluirSelecionado()
    {
        if (_gridFilmes.CurrentRow?
            .DataBoundItem is not Filme filme)
        {
            return;
        }


        DialogResult resultado =
            MessageBox.Show(
                $"Deseja realmente excluir o filme \"{filme.Nome}\"?",
                "Excluir filme",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);


        if (resultado != DialogResult.Yes)
        {
            return;
        }


        try
        {
            _filmeService.Excluir(
                filme.Id);

            CarregarFilmes();
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