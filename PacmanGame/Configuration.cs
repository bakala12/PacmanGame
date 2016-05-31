using Autofac;
using PacmanGame.Highscores;
using PacmanGame.Serialization;
using PacmanGame.Validation;
using PacmanGame.ViewModels;

namespace PacmanGame
{
    /// <summary>
    /// Configuration of dependency injection container used in the application.
    /// </summary>
    internal static class Configuration
    {
        /// <summary>
        /// Register application types and returs builded container.
        /// </summary>
        /// <returns>The container which can be used in the application.</returns>
        internal static IContainer Configure()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterInstance(new GameSerializer()).AsImplementedInterfaces();
            builder.RegisterInstance(new KeysValidator()).AsImplementedInterfaces();
            builder.RegisterInstance(new SettingsProvider()).AsImplementedInterfaces();
            builder.RegisterType<SimpleGameBuilder>().AsImplementedInterfaces();

            builder.RegisterType<HighscoreList>().AsSelf();

            builder.RegisterType<StartMenuViewModel>().AsSelf();
            builder.RegisterType<GameViewModel>().AsSelf();
            builder.RegisterType<EndGameViewModel>().AsSelf();
            builder.RegisterType<PauseViewModel>().AsSelf();
            builder.RegisterType<HighscoresViewModel>().AsSelf();
            builder.RegisterType<OptionsViewModel>().AsSelf();

            builder.RegisterType<MainWindowViewModel>().AsImplementedInterfaces().AsSelf();

            return builder.Build();
        }
    }
}