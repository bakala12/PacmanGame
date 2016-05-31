using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PacmanGame.Highscores;
using PacmanGame.MainInterfaces;
using Xunit;

namespace PacmanProjectTests.OtherTests
{
    public class HighscoresTest
    {
        private enum TestHighscoreListType : uint
        {
            Empty=0, PartiallyFilled=5, FullyFilled=10
        }

        [Theory]
        [InlineData(TestHighscoreListType.Empty, 10, 10, true)]
        [InlineData(TestHighscoreListType.Empty, 10, 15555, true)]
        [InlineData(TestHighscoreListType.PartiallyFilled, 10, 10, true)]
        [InlineData(TestHighscoreListType.PartiallyFilled, 10, 15555, true)]
        [InlineData(TestHighscoreListType.FullyFilled, 10, 155555, true)]
        [InlineData(TestHighscoreListType.FullyFilled, 10, 10, false)]
        private void IsNewHighscoreTest(TestHighscoreListType type,int time, int points, bool isNew)
        {
            Highscore highscore = new Highscore()
            {
                Points = (uint)points,
                GameTime = TimeSpan.FromSeconds(time),
                PlayerName = "player"
            };
            var list = ProvideList((int) type);
            var clone = new List<Highscore>(list);
            ISettingsProvider provider = new TestSettingsProvider();
            provider.Highscores = list;
            provider.Save();
            HighscoreList highscoreList = new HighscoreList(provider);
            bool b= highscoreList.IsNewHighscore(highscore);
            Assert.Equal(isNew,b);
            if(isNew)highscoreList.AddHighscore(highscore);
            Assert.True(highscoreList.Highscores.Count<=10);
            if (isNew)clone.Add(highscore);
            clone.Sort(new HighscoreComparer());
            clone = clone.Take(10).ToList();
            Assert.Equal(clone.Count, highscoreList.Highscores.Count);
            for (int i = 0; i < list.Count; i++)
            {
                Assert.Equal(clone[i], highscoreList.Highscores[i]);
            }
        }

        private List<Highscore> ProvideList(int num)
        {
            var list = new List<Highscore>();
            for (int i = 0; i < num;i++)
            {
                Highscore h = new Highscore()
                {
                    Points = (uint)(500*(i+1)),
                    PlayerName = "player",
                    GameTime = TimeSpan.FromSeconds(i*2+10)
                };
                list.Add(h);
            }
            return list;
        }
    }
}
