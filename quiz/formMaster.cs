﻿using static quiz.formMaster.MenuType;
namespace quiz;

public partial class formMaster : Form
{
    private PictureBox currentCanvas { get; set; } = new();
    public formMaster()
    {
        InitializeComponent();
        Size = new(Size.Width, Size.Height + 175);
        CenterToScreen();

        Helper.timeSinceLastClick.Start();

        var initialView = ViewBuilder[MainMenu](this);
        initialView.Initialize();
        initialView.Run();
        currentCanvas = initialView.Canvas;

        Controls.Add(currentCanvas);

        System.Timers.Timer timer = new(16);

        timer.Elapsed += (s, e) =>
        {
            Helper.MouseLocation = PointToClient(MousePosition);
        };

        timer.Start();
    }

    public enum MenuType
    {
        MainMenu,
        Quiz,
        Leaderboard,
        Profile
    }

    
    private static Dictionary<MenuType, Func<formMaster, View>> ViewBuilder = new()
    {
        {
            MainMenu, (form) => new View(new List<ICanvasElement>()
            {
                new CanvasText("Main Menu", Helper.Fonts[3], Helper.Colourscheme[4], (_c) => _c.GetCenter(0, _c.Top - 225)),
                new CanvasButton("Play Quiz!", Helper.Fonts[2], Helper.Colourscheme[1], Helper.Colourscheme[3], (_c) => _c.GetDefaultBoxSize(), (_c) => _c.GetCenter(0, -125), () =>
                {
                    var quiz = ViewBuilder?[Quiz](form);
                    View.Current.Stop();

                    quiz?.Initialize();
                    quiz?.Run();
                }),
                new CanvasButton("Leaderboard", Helper.Fonts[2], Helper.Colourscheme[1], Helper.Colourscheme[3], (_c) => _c.GetDefaultBoxSize(), (_c) => _c.GetCenter(0, 0), () =>
                {
                    var leaderboard = ViewBuilder?[Leaderboard](form);
                    View.Current.Stop();

                    leaderboard?.Initialize();
                    leaderboard?.Run();
                }),
                new CanvasButton("Settings", Helper.Fonts[2], Helper.Colourscheme[1], Helper.Colourscheme[3], (_c) => _c.GetDefaultBoxSize(), (_c) => _c.GetCenter(0, 125), () =>
                {
                }),
                new CanvasButton("Quiz", Helper.Fonts[2], Helper.Colourscheme[1], Helper.Colourscheme[3], (_c) => _c.GetDefaultBoxSize(), (_c) => _c.GetCenter(0, 250), () =>
                {
                    View.Current.Stop();
                    Environment.Exit(0);
                }),
            }, form.currentCanvas)
        },
        {
            Quiz, (form) => new View(new List<ICanvasElement>()
            {
                new CanvasText("Question Title", Helper.Fonts[3], Helper.Colourscheme[4], (_c) => _c.GetCenter(0, _c.Top - 175)),
                new CanvasButton("Answer 1", Helper.Fonts[2], Helper.Colourscheme[1], Helper.Colourscheme[3], (_c) => _c.GetDefaultBoxSize(), (_c) => _c.GetCenter(-125, -75), () =>
                {
                    var main = ViewBuilder?[MainMenu](form);
                    View.Current.Stop();
                    main?.Initialize();
                    main?.Run();
                }),
                new CanvasButton("Answer 2", Helper.Fonts[2], Helper.Colourscheme[1], Helper.Colourscheme[3], (_c) => _c.GetDefaultBoxSize(), (_c) => _c.GetCenter(125, -75), () =>
                {

                }),
                new CanvasButton("Answer 3", Helper.Fonts[2], Helper.Colourscheme[1], Helper.Colourscheme[3], (_c) => _c.GetDefaultBoxSize(), (_c) => _c.GetCenter(-125, 50), () =>
                {

                }),
                new CanvasButton("Answer 4", Helper.Fonts[2], Helper.Colourscheme[1], Helper.Colourscheme[3], (_c) => _c.GetDefaultBoxSize(), (_c) => _c.GetCenter(125, 50), () =>
                {

                }),
            }, form.currentCanvas)
        },
        {
            Profile, (form) => new View(new List<ICanvasElement>()
            {
                new CanvasText("Users' Name", Helper.Fonts[3], Helper.Colourscheme[4], (_c) => _c.GetCenter(0, _c.Top - 175)),
            }, form.currentCanvas)
        },
        {
            Leaderboard, (form) => new View(new List<ICanvasElement>()
            {
                new CanvasText("Leaderboard", Helper.Fonts[3], Helper.Colourscheme[4], (_c) => _c.GetCenter(0, _c.Top - 175)),
                new CanvasButton("Back", Helper.Fonts[2], Helper.Colourscheme[1], Helper.Colourscheme[3], (_c) => _c.GetDefaultBoxSize(), (_c) => _c.GetCenter(0, 0), () =>
                {
                    var main = ViewBuilder?[MainMenu](form);
                    View.Current.Stop();
                    main?.Initialize();
                    main?.Run();

                }),
            }, form.currentCanvas)
        }
    };
}
