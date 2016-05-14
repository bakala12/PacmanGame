using Autofac;
using PacmanGame.Highscores;
using PacmanGame.Serialization;
using PacmanGame.Validation;
using PacmanGame.ViewModels;

namespace PacmanGame
{
    internal static class Configuration
    {
        internal static IContainer Configure()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterInstance(new GameSerializer()).AsImplementedInterfaces();
            builder.RegisterInstance(new KeysValidator()).AsImplementedInterfaces();
            builder.RegisterInstance(new SettingsProvider()).AsImplementedInterfaces();
            builder.RegisterInstance(new SimpleGameBuilder()).AsImplementedInterfaces();

            builder.RegisterType<HighscoreList>().AsSelf();

            builder.RegisterType<StartMenuViewModel>().AsSelf();
            builder.RegisterType<GameViewModel>().AsSelf();
            builder.RegisterType<EndGameViewModel>().AsSelf();
            builder.RegisterType<PauseViewModel>().AsSelf();
            builder.RegisterType<HighscoresViewModel>().AsSelf();
            builder.RegisterType<OptionsViewModel>().AsSelf();
            builder.RegisterType<HelpViewModel>().AsSelf();

            builder.RegisterType<MainWindowViewModel>().AsImplementedInterfaces().AsSelf();

            return builder.Build();
        }
    }
}