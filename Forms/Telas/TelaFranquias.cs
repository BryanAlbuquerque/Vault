using Vault.Models;
using Vault.Services;

namespace Vault.Telas;

public class TelaFranquias : UserControl
{
    private readonly FranquiaService _franquiaService;
    private readonly FilmeService _filmeService;
    private readonly SerieService _serieService;

    private DataGridView _grid = null!;
    private TextBox _txtBusca = null!;

    private List<Franquia> _franquias = [];

    #region CORES
    private static readonly Color Fundo = Color.FromArgb(11, 11, 22);
    private static readonly Color Superficie = Color.FromArgb(23, 23, 37);
    private static readonly Color SuperficieElevada = Color.FromArgb(29, 29, 46);

    private static readonly Color Roxo = Color.FromArgb(124, 92, 255);
    private static readonly Color RoxoHover = Color.FromArgb(146, 120, 255);

    private static readonly Color Texto = Color.FromArgb(245, 243, 255);
    private static readonly Color TextoSecundario = Color.FromArgb(170, 167, 189);

    private static readonly Color Linha = Color.FromArgb(41, 40, 58);
    private static readonly Color Selecao = Color.FromArgb(59, 46, 104);
    #endregion
    public TelaFranquias()
    {
        _franquiaService = new FranquiaService();
        _filmeService = new FilmeService();
        _serieService = new SerieService();

        Dock = DockStyle.Fill;
        BackColor = Fundo;

        CriarInterface();
        CarregarFranquias();
    }

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
            new RowStyle(
                SizeType.Absolute,
                58));

        layout.RowStyles.Add(
            new RowStyle(
                SizeType.Absolute,
                52));

        layout.RowStyles.Add(
            new RowStyle(
                SizeType.Percent,
                100));

        Controls.Add(layout);

        layout.Controls.Add(
            CriarBarraAcoes(),
            0,
            0);

        layout.Controls.Add(
            CriarBusca(),
            0,
            1);

        _grid = CriarGrid();

        layout.Controls.Add(
            _grid,
            0,
            2);
    }

    private Panel CriarBarraAcoes()
    {
        var painel = new Panel
        {
            Dock = DockStyle.Fill,
            BackColor = Fundo
        };

        Button novo = new Button
        {
            Text = "＋  Nova franquia",
            Width = 145,
            Height = 38,
            BackColor = Roxo,
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Font = new Font(
                "Segoe UI Semibold",
                9.5F),
            Anchor =
                AnchorStyles.Top |
                AnchorStyles.Right,
            Cursor = Cursors.Hand
        };

        novo.FlatAppearance.BorderSize = 0;

        novo.MouseEnter += (_, _) =>
        {
            novo.BackColor = RoxoHover;
        };

        novo.MouseLeave += (_, _) =>
        {
            novo.BackColor = Roxo;
        };

        painel.Controls.Add(novo);

        painel.Resize += (_, _) =>
        {
            novo.Left =
                painel.ClientSize.Width -
                novo.Width;

            novo.Top = 5;
        };

        novo.Click += (_, _) =>
        {
            AbrirCadastro();
        };

        return painel;
    }

    private Panel CriarBusca()
    {
        var painel = new Panel
        {
            Dock = DockStyle.Fill,
            BackColor = Fundo
        };

        _txtBusca = new TextBox
        {
            Width = 380,
            Height = 32,
            Location = new Point(0, 7),
            BackColor = Superficie,
            ForeColor = Texto,
            BorderStyle = BorderStyle.FixedSingle,
            Font = new Font(
                "Segoe UI",
                9.5F),
            PlaceholderText = "Pesquisar franquia..."
        };

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

            BackgroundColor = Superficie,

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

            AutoSizeRowsMode =
                DataGridViewAutoSizeRowsMode.None,

            AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill,

            ColumnHeadersHeight = 46,

            RowTemplate =
            {
                Height = 48
            }
        };

        grid.ColumnHeadersDefaultCellStyle =
            new DataGridViewCellStyle
            {
                BackColor = SuperficieElevada,
                ForeColor = TextoSecundario,

                Font = new Font(
                    "Segoe UI Semibold",
                    9F),

                Padding =
                    new Padding(
                        14,
                        0,
                        14,
                        0),

                Alignment =
                    DataGridViewContentAlignment.MiddleLeft,

                SelectionBackColor =
                    SuperficieElevada,

                SelectionForeColor =
                    TextoSecundario
            };

        grid.DefaultCellStyle =
            new DataGridViewCellStyle
            {
                BackColor = Superficie,
                ForeColor = Texto,

                SelectionBackColor = Selecao,
                SelectionForeColor = Color.White,

                Font = new Font(
                    "Segoe UI",
                    9.5F),

                Padding =
                    new Padding(
                        14,
                        0,
                        14,
                        0),

                Alignment =
                    DataGridViewContentAlignment.MiddleLeft,

                NullValue = ""
            };

        grid.AlternatingRowsDefaultCellStyle =
            new DataGridViewCellStyle
            {
                BackColor =
                    Color.FromArgb(
                        20,
                        20,
                        33),

                ForeColor = Texto,

                SelectionBackColor = Selecao,
                SelectionForeColor = Color.White
            };

        grid.Columns.Add(
            new DataGridViewTextBoxColumn
            {
                HeaderText = "FRANQUIA",
                DataPropertyName = "Nome",

                AutoSizeMode =
                    DataGridViewAutoSizeColumnMode.Fill,

                FillWeight = 65,

                SortMode =
                    DataGridViewColumnSortMode.NotSortable
            });

        grid.Columns.Add(
            new DataGridViewTextBoxColumn
            {
                HeaderText = "FILMES",
                DataPropertyName = "QuantidadeFilmes",

                AutoSizeMode =
                    DataGridViewAutoSizeColumnMode.Fill,

                FillWeight = 17,

                SortMode =
                    DataGridViewColumnSortMode.NotSortable,

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
                HeaderText = "SÉRIES",
                DataPropertyName = "QuantidadeSeries",

                AutoSizeMode =
                    DataGridViewAutoSizeColumnMode.Fill,

                FillWeight = 18,

                SortMode =
                    DataGridViewColumnSortMode.NotSortable,

                DefaultCellStyle =
                    new DataGridViewCellStyle
                    {
                        Alignment =
                            DataGridViewContentAlignment.MiddleCenter
                    }
            });

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

    private void CarregarFranquias()
    {
        _franquias =
            _franquiaService.ObterTodos();

        AtualizarGrid(_franquias);
    }

    private void AtualizarGrid(List<Franquia> franquias)
    {
        var filmes =
            _filmeService.ObterTodos();

        var series =
            _serieService.ObterTodos();

        var dados = franquias
            .Select(franquia => new FranquiaGridItem
            {
                Franquia = franquia,

                Nome = franquia.Nome,

                QuantidadeFilmes =
                    filmes.Count(f =>
                        f.FranquiaId ==
                        franquia.Id),

                QuantidadeSeries =
                    series.Count(s =>
                        s.FranquiaId ==
                        franquia.Id)
            })
            .ToList();

        _grid.DataSource = null;
        _grid.DataSource = dados;

        // Garante a altura das linhas após o DataSource
        foreach (DataGridViewRow row in _grid.Rows)
        {
            row.Height = 48;
        }
    }

    private void AplicarBusca()
    {
        string busca =
            _txtBusca.Text.Trim();

        if (string.IsNullOrWhiteSpace(busca))
        {
            AtualizarGrid(_franquias);
            return;
        }

        var resultado =
            _franquias
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
            new FormCadastroFranquia(
                _franquiaService);

        if (form.ShowDialog() ==
            DialogResult.OK)
        {
            CarregarFranquias();
        }
    }

    private void EditarSelecionado()
    {
        if (_grid.CurrentRow?
            .DataBoundItem is not FranquiaGridItem item)
        {
            return;
        }

        using var form =
            new FormCadastroFranquia(
                _franquiaService,
                item.Franquia);

        if (form.ShowDialog() ==
            DialogResult.OK)
        {
            CarregarFranquias();
        }
    }

    private void ExcluirSelecionado()
    {
        if (_grid.CurrentRow?
            .DataBoundItem is not FranquiaGridItem item)
        {
            return;
        }

        Franquia franquia =
            item.Franquia;

        int filmes =
            _filmeService
                .ObterTodos()
                .Count(f =>
                    f.FranquiaId ==
                    franquia.Id);

        int series =
            _serieService
                .ObterTodos()
                .Count(s =>
                    s.FranquiaId ==
                    franquia.Id);

        if (filmes > 0 || series > 0)
        {
            MessageBox.Show(
                $"A franquia possui {filmes} filme(s) " +
                $"e {series} série(s) associados.\n\n" +
                "Remova os vínculos antes de excluir a franquia.",
                "Franquia em uso",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);

            return;
        }

        DialogResult resultado =
            MessageBox.Show(
                $"Deseja excluir a franquia \"{franquia.Nome}\"?",
                "Excluir franquia",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

        if (resultado != DialogResult.Yes)
        {
            return;
        }

        try
        {
            _franquiaService.Excluir(
                franquia.Id);

            CarregarFranquias();
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

    private class FranquiaGridItem
    {
        public Franquia Franquia { get; set; } = null!;

        public string Nome { get; set; } = string.Empty;

        public int QuantidadeFilmes { get; set; }

        public int QuantidadeSeries { get; set; }
    }
}