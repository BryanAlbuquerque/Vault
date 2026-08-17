using Vault.Forms.Telas;
using Vault.Models;
using Vault.Services;
using Vault.Telas;

namespace Vault;

public class FormPrincipal : Form
{
    private readonly FilmeService _filmeService;
    private readonly SerieService _serieService;
    private readonly FranquiaService _franquiaService;

    private Panel _painelConteudo = null!;

    private Label _lblTituloPagina = null!;
    private Label _lblSubtituloPagina = null!;

    private Label _lblQuantidadeFilmes = null!;
    private Label _lblQuantidadeSeries = null!;
    private Label _lblQuantidadeFranquias = null!;

    private ListBox _listaFilmesFavoritos = null!;
    private ListBox _listaSeriesFavoritas = null!;

    private Button _btnMenuAtivo = null!;

    #region CORES
    private static readonly Color CorFundo =
        Color.FromArgb(9, 11, 16);

    private static readonly Color CorSidebar =
        Color.FromArgb(16, 19, 26);

    private static readonly Color CorPainel =
        Color.FromArgb(21, 25, 34);

    private static readonly Color CorPainelElevado =
        Color.FromArgb(27, 31, 41);

    private static readonly Color CorBorda =
        Color.FromArgb(39, 43, 53);

    private static readonly Color CorRoxo =
        Color.FromArgb(139, 92, 246);

    private static readonly Color CorRoxoEscuro =
        Color.FromArgb(124, 58, 237);

    private static readonly Color CorAzul =
        Color.FromArgb(56, 189, 248);

    private static readonly Color CorVerde =
        Color.FromArgb(52, 211, 153);

    private static readonly Color CorTexto =
        Color.FromArgb(244, 244, 245);

    private static readonly Color CorTextoSecundario =
        Color.FromArgb(161, 161, 170);

    private static readonly Color CorTextoDiscreto =
        Color.FromArgb(113, 113, 122);


    #endregion

    // CONSTRUTOR
    public FormPrincipal()
    {
        _filmeService = new FilmeService();
        _serieService = new SerieService();
        _franquiaService = new FranquiaService();

        InicializarFormulario();
        CriarInterface();

        // Abre diretamente no Dashboard.
        MostrarDashboard();
    }

    // FORMULÁRIO
    private void InicializarFormulario()
    {
        Text = "Vault";

        StartPosition =
            FormStartPosition.CenterScreen;

        MinimumSize =
            new Size(1150, 720);

        Size =
            new Size(1450, 850);

        BackColor =
            CorFundo;

        ForeColor =
            CorTexto;

        Font =
            new Font(
                "Segoe UI",
                10F);

        FormBorderStyle =
            FormBorderStyle.Sizable;

        WindowState =
            FormWindowState.Maximized;

        DoubleBuffered = true;
    }

    // INTERFACE PRINCIPAL
    private void CriarInterface()
    {
        var layoutPrincipal =
            new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                BackColor = CorFundo,
                Margin = Padding.Empty,
                Padding = Padding.Empty
            };

        layoutPrincipal.ColumnStyles.Add(
            new ColumnStyle(
                SizeType.Absolute,
                245));

        layoutPrincipal.ColumnStyles.Add(
            new ColumnStyle(
                SizeType.Percent,
                100));

        Controls.Add(
            layoutPrincipal);

        Panel sidebar =
            CriarSidebar();

        layoutPrincipal.Controls.Add(
            sidebar,
            0,
            0);

        Panel areaPrincipal =
            CriarAreaPrincipal();

        layoutPrincipal.Controls.Add(
            areaPrincipal,
            1,
            0);
    }

    // SIDEBAR
    private Panel CriarSidebar()
    {
        var sidebar =
            new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = CorSidebar,
                Padding =
                    new Padding(
                        22,
                        24,
                        18,
                        20)
            };

        var layoutSidebar =
            new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 5,
                BackColor = Color.Transparent,
                Margin = Padding.Empty,
                Padding = Padding.Empty
            };

        layoutSidebar.RowStyles.Add(
            new RowStyle(
                SizeType.Absolute,
                75));

        layoutSidebar.RowStyles.Add(
            new RowStyle(
                SizeType.Absolute,
                30));

        layoutSidebar.RowStyles.Add(
            new RowStyle(
                SizeType.Absolute,
                285));

        layoutSidebar.RowStyles.Add(
            new RowStyle(
                SizeType.Percent,
                100));

        layoutSidebar.RowStyles.Add(
            new RowStyle(
                SizeType.Absolute,
                55));

        sidebar.Controls.Add(
            layoutSidebar);

        Panel logo =
            CriarLogo();

        layoutSidebar.Controls.Add(
            logo,
            0,
            0);

        Label lblMenu =
            new Label
            {
                Text = "BIBLIOTECA",
                Dock = DockStyle.Fill,
                ForeColor =
                    CorTextoDiscreto,
                Font =
                    new Font(
                        "Segoe UI Semibold",
                        8F),
                TextAlign =
                    ContentAlignment.MiddleLeft,
                Padding =
                    new Padding(
                        12,
                        0,
                        0,
                        0)
            };

        layoutSidebar.Controls.Add(
            lblMenu,
            0,
            1);

        Panel menu =
            CriarMenu();

        layoutSidebar.Controls.Add(
            menu,
            0,
            2);

        Panel espaco =
            new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent
            };

        layoutSidebar.Controls.Add(
            espaco,
            0,
            3);

        Button btnConfiguracoes =
            CriarBotaoMenu(
                "⚙   Configurações");

        btnConfiguracoes.Dock =
            DockStyle.Fill;

        btnConfiguracoes.Click += (_, _) =>
        {
            MessageBox.Show(
                "Configurações será implementado posteriormente.",
                "Vault",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        };

        layoutSidebar.Controls.Add(
            btnConfiguracoes,
            0,
            4);

        return sidebar;
    }

    private Panel CriarLogo()
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
                Text = "◆",
                AutoSize = true,
                Location =
                    new Point(
                        8,
                        20),
                ForeColor =
                    CorRoxo,
                Font =
                    new Font(
                        "Segoe UI Semibold",
                        16F)
            };

        Label nome =
            new Label
            {
                Text = "Vault",
                AutoSize = true,
                Location =
                    new Point(
                        34,
                        19),
                ForeColor =
                    CorTexto,
                Font =
                    new Font(
                        "Segoe UI Semibold",
                        18F)
            };


        painel.Controls.Add(icone);
        painel.Controls.Add(nome);

        return painel;
    }

    // MENU
    private Panel CriarMenu()
    {
        var menu =
            new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 5,
                BackColor = Color.Transparent,
                Margin = Padding.Empty,
                Padding = Padding.Empty
            };

        for (int i = 0; i < 5; i++)
        {
            menu.RowStyles.Add(
                new RowStyle(
                    SizeType.Absolute,
                    52));
        }

        Button btnDashboard =
            CriarBotaoMenu(
                "▦   Dashboard");

        Button btnFilmes =
            CriarBotaoMenu(
                "▣   Filmes");

        Button btnSeries =
            CriarBotaoMenu(
                "▤   Séries");

        Button btnFranquias =
            CriarBotaoMenu(
                "◆   Franquias");

        

        btnDashboard.Click += (_, _) =>
        {
            AtivarMenu(btnDashboard);

            MostrarDashboard();
        };

        btnFilmes.Click += (_, _) =>
        {
            AtivarMenu(btnFilmes);

            MostrarTela(
                new TelaFilmes(),
                "Filmes",
                "Gerencie os filmes que você já assistiu");
        };

        btnSeries.Click += (_, _) =>
        {
            AtivarMenu(btnSeries);

            MostrarTela(
                new TelaSeries(),
                "Séries",
                "Gerencie as séries que você já assistiu");
        };

        btnFranquias.Click += (_, _) =>
        {
            AtivarMenu(btnFranquias);

            MostrarTela(
                new TelaFranquias(),
                "Franquias",
                "Organize filmes e séries pertencentes ao mesmo universo");
        };


        menu.Controls.Add(
            btnDashboard,
            0,
            0);

        menu.Controls.Add(
            btnFilmes,
            0,
            1);

        menu.Controls.Add(
            btnSeries,
            0,
            2);

        menu.Controls.Add(
            btnFranquias,
            0,
            3);

        AtivarMenu(btnDashboard);

        return menu;
    }

    private Button CriarBotaoMenu(string texto)
    {
        var botao =
            new Button
            {
                Text = texto,
                Dock = DockStyle.Fill,
                FlatStyle =
                    FlatStyle.Flat,
                BackColor =
                    Color.Transparent,
                ForeColor =
                    CorTextoSecundario,
                Font =
                    new Font(
                        "Segoe UI Semibold",
                        9.5F),
                TextAlign =
                    ContentAlignment.MiddleLeft,
                Padding =
                    new Padding(
                        13,
                        0,
                        0,
                        0),
                Cursor =
                    Cursors.Hand,
                Margin =
                    new Padding(
                        0,
                        3,
                        0,
                        3)
            };

        botao.FlatAppearance.BorderSize = 0;

        botao.MouseEnter += (_, _) =>
        {
            if (botao != _btnMenuAtivo)
            {
                botao.BackColor =
                    CorPainelElevado;

                botao.ForeColor =
                    CorTexto;
            }
        };

        botao.MouseLeave += (_, _) =>
        {
            if (botao != _btnMenuAtivo)
            {
                botao.BackColor =
                    Color.Transparent;

                botao.ForeColor =
                    CorTextoSecundario;
            }
        };

        return botao;
    }

    private void AtivarMenu(Button botao)
    {
        if (_btnMenuAtivo != null)
        {
            _btnMenuAtivo.BackColor =
                Color.Transparent;

            _btnMenuAtivo.ForeColor =
                CorTextoSecundario;
        }

        _btnMenuAtivo =
            botao;

        _btnMenuAtivo.BackColor =
            Color.FromArgb(
                38,
                29,
                68);

        _btnMenuAtivo.ForeColor =
            Color.White;
    }

    // ÁREA PRINCIPAL
    private Panel CriarAreaPrincipal()
    {
        var area =
            new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = CorFundo,
                Padding =
                    new Padding(
                        30,
                        25,
                        30,
                        25)
            };

        var layout =
            new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2,
                BackColor = Color.Transparent,
                Margin = Padding.Empty,
                Padding = Padding.Empty
            };

        layout.RowStyles.Add(
            new RowStyle(
                SizeType.Absolute,
                90));

        layout.RowStyles.Add(
            new RowStyle(
                SizeType.Percent,
                100));

        area.Controls.Add(layout);

        Panel cabecalho =
            CriarCabecalho();

        layout.Controls.Add(
            cabecalho,
            0,
            0);

        _painelConteudo =
            new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent
            };

        layout.Controls.Add(
            _painelConteudo,
            0,
            1);

        return area;
    }

    private Panel CriarCabecalho()
    {
        var cabecalho =
            new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent
            };

        _lblTituloPagina =
            new Label
            {
                Text = "Dashboard",
                AutoSize = true,
                Location =
                    new Point(
                        0,
                        3),
                ForeColor =
                    CorTexto,
                Font =
                    new Font(
                        "Segoe UI Semibold",
                        22F)
            };

        _lblSubtituloPagina =
            new Label
            {
                Text =
                    "Sua biblioteca pessoal de filmes e séries",
                AutoSize = true,
                Location =
                    new Point(
                        2,
                        43),
                ForeColor =
                    CorTextoSecundario,
                Font =
                    new Font(
                        "Segoe UI",
                        9.5F)
            };

        Label indicador =
            new Label
            {
                Text =
                    "●  BIBLIOTECA LOCAL",
                AutoSize = true,
                Anchor =
                    AnchorStyles.Top |
                    AnchorStyles.Right,
                ForeColor =
                    CorVerde,
                Font =
                    new Font(
                        "Segoe UI Semibold",
                        8F)
            };

        cabecalho.Controls.Add(
            _lblTituloPagina);

        cabecalho.Controls.Add(
            _lblSubtituloPagina);

        cabecalho.Controls.Add(
            indicador);

        cabecalho.Resize += (_, _) =>
        {
            indicador.Left =
                cabecalho.ClientSize.Width -
                indicador.Width;

            indicador.Top = 10;
        };

        return cabecalho;
    }

    // NAVEGAÇÃO
    private void MostrarTela(Control tela, string titulo, string subtitulo)
    {
        _lblTituloPagina.Text =
            titulo;

        _lblSubtituloPagina.Text =
            subtitulo;

        _painelConteudo.Controls.Clear();

        tela.Dock =
            DockStyle.Fill;

        _painelConteudo.Controls.Add(
            tela);
    }

    private void MostrarDashboard()
    {
        _lblTituloPagina.Text =
            "Dashboard";

        _lblSubtituloPagina.Text =
            "Sua biblioteca pessoal de filmes e séries";

        CriarConteudoDashboard();

        AtualizarDashboard();
    }

    // DASHBOARD
    private void CriarConteudoDashboard()
    {
        _painelConteudo.Controls.Clear();

        var layout =
            new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 3,
                BackColor = Color.Transparent,
                Margin = Padding.Empty,
                Padding = Padding.Empty
            };

        layout.RowStyles.Add(
            new RowStyle(
                SizeType.Absolute,
                125));

        layout.RowStyles.Add(
            new RowStyle(
                SizeType.Absolute,
                45));

        layout.RowStyles.Add(
            new RowStyle(
                SizeType.Percent,
                100));

        _painelConteudo.Controls.Add(
            layout);

        Panel cards =
            CriarCardsResumo();

        layout.Controls.Add(
            cards,
            0,
            0);

        Label tituloFavoritos =
            new Label
            {
                Text =
                    "Melhores avaliações",
                Dock =
                    DockStyle.Fill,
                ForeColor =
                    CorTexto,
                Font =
                    new Font(
                        "Segoe UI Semibold",
                        13F),
                TextAlign =
                    ContentAlignment.MiddleLeft
            };

        layout.Controls.Add(
            tituloFavoritos,
            0,
            1);

        Panel favoritos =
            CriarAreaFavoritos();

        layout.Controls.Add(
            favoritos,
            0,
            2);
    }

    private Panel CriarCardsResumo()
    {
        var painel =
            new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 3,
                RowCount = 1,
                BackColor = Color.Transparent,
                Margin = Padding.Empty,
                Padding = Padding.Empty
            };

        painel.ColumnStyles.Add(
            new ColumnStyle(
                SizeType.Percent,
                33.33F));

        painel.ColumnStyles.Add(
            new ColumnStyle(
                SizeType.Percent,
                33.33F));

        painel.ColumnStyles.Add(
            new ColumnStyle(
                SizeType.Percent,
                33.34F));

        _lblQuantidadeFilmes =
            new Label();

        _lblQuantidadeSeries =
            new Label();

        _lblQuantidadeFranquias =
            new Label();

        painel.Controls.Add(
            CriarCard(
                "FILMES",
                _lblQuantidadeFilmes,
                "▣",
                CorRoxo),
            0,
            0);

        painel.Controls.Add(
            CriarCard(
                "SÉRIES",
                _lblQuantidadeSeries,
                "▤",
                CorAzul),
            1,
            0);

        painel.Controls.Add(
            CriarCard(
                "FRANQUIAS",
                _lblQuantidadeFranquias,
                "◆",
                CorVerde),
            2,
            0);

        return painel;
    }

    private Panel CriarCard(string titulo, Label valor, string icone, Color corIcone)
    {
        var painel =
            new Panel
            {
                Dock = DockStyle.Fill,
                BackColor =
                    CorPainel,
                Margin =
                    new Padding(
                        0,
                        0,
                        12,
                        0),
                Padding =
                    new Padding(20)
            };

        Panel indicador =
            new Panel
            {
                Width = 3,
                Dock = DockStyle.Left,
                BackColor =
                    corIcone
            };

        Label lblIcone =
            new Label
            {
                Text = icone,
                Dock = DockStyle.Left,
                Width = 48,
                ForeColor =
                    corIcone,
                Font =
                    new Font(
                        "Segoe UI",
                        21F),
                TextAlign =
                    ContentAlignment.MiddleCenter
            };

        Label lblTitulo =
            new Label
            {
                Text = titulo,
                AutoSize = true,
                Location =
                    new Point(
                        78,
                        20),
                ForeColor =
                    CorTextoDiscreto,
                Font =
                    new Font(
                        "Segoe UI Semibold",
                        8F)
            };

        valor.Text = "0";

        valor.AutoSize = true;

        valor.Location =
            new Point(
                78,
                43);

        valor.ForeColor =
            CorTexto;

        valor.Font =
            new Font(
                "Segoe UI Semibold",
                22F);

        painel.Controls.Add(
            lblTitulo);

        painel.Controls.Add(
            valor);

        painel.Controls.Add(
            lblIcone);

        painel.Controls.Add(
            indicador);

        return painel;
    }

    // FAVORITOS / MELHORES AVALIAÇÕES
    private Panel CriarAreaFavoritos()
    {
        var layout =
            new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                BackColor = Color.Transparent,
                Margin = Padding.Empty,
                Padding = Padding.Empty
            };

        layout.ColumnStyles.Add(
            new ColumnStyle(
                SizeType.Percent,
                50));

        layout.ColumnStyles.Add(
            new ColumnStyle(
                SizeType.Percent,
                50));

        _listaFilmesFavoritos =
            CriarListaFavoritos();

        _listaSeriesFavoritas =
            CriarListaFavoritos();

        layout.Controls.Add(
            CriarPainelFavoritos(
                "Filmes favoritos",
                _listaFilmesFavoritos,
                CorRoxo),
            0,
            0);

        layout.Controls.Add(
            CriarPainelFavoritos(
                "Séries favoritas",
                _listaSeriesFavoritas,
                CorAzul),
            1,
            0);

        return layout;
    }

    private ListBox CriarListaFavoritos()
    {
        var lista =
            new ListBox
            {
                Dock = DockStyle.Fill,
                BorderStyle =
                    BorderStyle.None,
                BackColor =
                    CorPainel,
                ForeColor =
                    CorTexto,
                Font =
                    new Font(
                        "Segoe UI",
                        10F),
                IntegralHeight = false,
                DrawMode =
                    DrawMode.OwnerDrawFixed,
                ItemHeight = 38
            };

        lista.DrawItem += (_, e) =>
        {
            if (e.Index < 0)
            {
                return;
            }

            e.DrawBackground();

            string texto =
                lista.Items[e.Index]
                    ?.ToString() ?? string.Empty;

            using var fonte =
                new Font(
                    "Segoe UI",
                    9.5F);

            using var pincel =
                new SolidBrush(
                    e.State.HasFlag(
                        DrawItemState.Selected)
                        ? Color.White
                        : CorTexto);

            e.Graphics.DrawString(
                texto,
                fonte,
                pincel,
                new Point(
                    e.Bounds.X + 12,
                    e.Bounds.Y + 10));

            e.DrawFocusRectangle();
        };

        return lista;
    }

    private Panel CriarPainelFavoritos(string titulo, ListBox lista, Color cor)
    {
        var painel =
            new Panel
            {
                Dock = DockStyle.Fill,
                BackColor =
                    CorPainel,
                Margin =
                    new Padding(
                        0,
                        0,
                        12,
                        0),
                Padding =
                    new Padding(20)
            };

        Label lblTitulo =
            new Label
            {
                Text = titulo,
                Dock =
                    DockStyle.Top,
                Height = 40,
                ForeColor =
                    CorTexto,
                Font =
                    new Font(
                        "Segoe UI Semibold",
                        11.5F)
            };

        Panel linha =
            new Panel
            {
                Dock =
                    DockStyle.Top,
                Height = 2,
                BackColor =
                    cor
            };

        painel.Controls.Add(lista);
        painel.Controls.Add(linha);
        painel.Controls.Add(lblTitulo);

        return painel;
    }

    // ATUALIZAÇÃO DOS DADOS
    private void AtualizarDashboard()
    {
        try
        {
            var filmes =
                _filmeService.ObterTodos();

            var series =
                _serieService.ObterTodos();

            var franquias =
                _franquiaService.ObterTodos();

            if (_lblQuantidadeFilmes != null)
            {
                _lblQuantidadeFilmes.Text =
                    filmes.Count.ToString();
            }

            if (_lblQuantidadeSeries != null)
            {
                _lblQuantidadeSeries.Text =
                    series.Count.ToString();
            }

            if (_lblQuantidadeFranquias != null)
            {
                _lblQuantidadeFranquias.Text =
                    franquias.Count.ToString();
            }

            if (_listaFilmesFavoritos != null)
            {
                _listaFilmesFavoritos.Items.Clear();

                var favoritosFilmes =
                    _filmeService
                        .ObterFavoritos()
                        .OrderByDescending(
                            f => f.Avaliacao)
                        .ToList();

                foreach (Filme filme in favoritosFilmes)
                {
                    _listaFilmesFavoritos.Items.Add(
                        $"{filme.Nome}    •    {filme.Avaliacao:0.0}");
                }

                if (favoritosFilmes.Count == 0)
                {
                    _listaFilmesFavoritos.Items.Add(
                        "Nenhum filme favorito.");
                }
            }

            if (_listaSeriesFavoritas != null)
            {
                _listaSeriesFavoritas.Items.Clear();

                var favoritasSeries =
                    _serieService
                        .ObterFavoritas()
                        .OrderByDescending(
                            s => s.Avaliacao)
                        .ToList();

                foreach (Serie serie in favoritasSeries)
                {
                    _listaSeriesFavoritas.Items.Add(
                        $"{serie.Nome}    •    {serie.Avaliacao:0.0}");
                }

                if (favoritasSeries.Count == 0)
                {
                    _listaSeriesFavoritas.Items.Add(
                        "Nenhuma série favorita.");
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                $"Não foi possível carregar o Dashboard.\n\n{ex.Message}",
                "Erro ao carregar Dashboard",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }

}