using EtrianOdysseyPC2.DataContexts;
using EtrianOdysseyPC2.EventHandlers;
using EtrianOdysseyPC2.Interfaces;
using EtrianOdysseyPC2.Managers;
using Komponent.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace EtrianOdysseyPC2.UiElements
{
    class GameChoiceElement : IUiElement
    {
        private List<(string name, ZipArchive content)> _games;
        private int _currentIndex;
        private int _maxItemsDisplayed = 3;

        private Canvas _canvas;

        public string Name => "GameChoice";

        public ElementContext ElementContext { get; private set; }

        public event SwitchUiElementEventHandler SwitchUiElement;

        public GameChoiceElement()
        {
            SetupView();

            _games = LoadGames().ToList();
            _currentIndex = 0;

            UpdateList();

            // TODO: Load game zips from "games" directory
            // TODO: Setup game choice view
            // TODO: Setup game choice list
        }

        private void SetupView()
        {
            ElementContext = new ElementContext();

            var viewbox = new Viewbox();
            _canvas = new Canvas();
            viewbox.Child = _canvas;

            ElementContext.Grid.Children.Add(viewbox);
        }

        private IEnumerable<(string name, ZipArchive content)> LoadGames()
        {
            var path = "Games";

            foreach (var archivePath in Directory.GetFiles(path))
                if (Path.GetExtension(archivePath) == ".zip")
                {
                    var archive = ZipFile.OpenRead(archivePath);

                    var metaFile = archive.GetEntry("Meta.txt");
                    if (metaFile != null)
                    {
                        var openMeta = metaFile.Open();
                        var parsedMeta = ParseMetaFile(openMeta);
                        yield return (parsedMeta["GameName"], archive.GetEntry("Content").Archive);
                    }
                }
        }

        private Dictionary<string, string> ParseMetaFile(Stream meta)
        {
            string text = Encoding.ASCII.GetString(new BinaryReaderX(meta).ReadAllBytes());
            return text.Replace(" ", "").Replace("\t", "").Split('\n').ToDictionary(x => x.Split('=').First(), y => y.Split('=').Skip(0).First());
        }

        // TODO: Make list element
        private void UpdateList()
        {
            _canvas.Children.Clear();

            var upColor = Brushes.Green;
            if (_currentIndex == 0)
                upColor = Brushes.LightGreen;
            var upArrow = new Rectangle() { Fill = upColor, Width = 100, Height = 20 };
            Canvas.SetLeft(upArrow, _canvas.Width / 2 - 50);
            Canvas.SetTop(upArrow, _canvas.Height / 2 - (_maxItemsDisplayed + 2) * 20 / 2);
            _canvas.Children.Add(upArrow);

            var firstIndexDisplayed = (int)Math.Max(0, _currentIndex - (Math.Ceiling((double)_maxItemsDisplayed / 2) - 1));
            for (int i = firstIndexDisplayed; i < Math.Min(firstIndexDisplayed + _maxItemsDisplayed, firstIndexDisplayed + (_games.Count - firstIndexDisplayed)); i++)
            {
                var element = new Rectangle() { };
            }
        }

        public void KeyPress(KeyEventArgs e)
        {
            // TODO: Remove debug load stub
            if (e.Key == KeyBindingManager.Global.MenuBindings.Confirm)
            {
                //SwitchUiElement?.Invoke(this, new SwitchUiElementEventArgs(new TownElement()));
            }
            else if (e.Key == KeyBindingManager.Global.MenuBindings.Down)
            {
                if (_currentIndex > 0)
                {
                    --_currentIndex;
                    UpdateList();
                }
            }
            else if (e.Key == KeyBindingManager.Global.MenuBindings.Up)
            {
                if (_currentIndex < _games.Count - 1)
                {
                    ++_currentIndex;
                    UpdateList();
                }
            }
        }
    }
}
