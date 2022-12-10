using Library.DataRepository;
using Library.Models;
using Moq;

namespace Tests
{

    internal class MockRepositoryWrapper
    {
        public static Joke jokeToAdd { get; } = new Joke
        {
            Id = 4
        };

        public static Joke jokeToModify { get; } = new Joke
        {
            Id = 1,
            JokeQuestion = "Can February march?",
            JokeAnswer = "No, but April may."
        };

        public static Mock<IRepository<Joke>> GetMock()
        {
            var mock = new Mock<IRepository<Joke>>();

            // Setup the mock
            var jokes = new List<Joke>()
            {
                new Joke()
                {
                    Id = 1,
                    JokeQuestion = "You know why you never see elephants hiding up in trees?",
                    JokeAnswer = "Because they’re really good at it.",
                },
                new Joke()
                {
                    Id = 2,
                    JokeQuestion = "What is red and smells like blue paint?",
                    JokeAnswer = "Red paint.",
                },
                new Joke()
                {
                    Id = 3,
                    JokeQuestion = "Why aren’t koalas actual bears?",
                    JokeAnswer = "The don’t meet the koalafications.",
                }
            };

            mock.Setup(m => m.GetAll()).Returns(() => jokes);

            mock.Setup(m => m.Get(1)).Returns(() => jokes[0]);
            mock.Setup(m => m.Get(2)).Returns(() => jokes[1]);
            mock.Setup(m => m.Get(3)).Returns(() => jokes[2]);

            mock.Setup(m => m.Update(jokeToModify)).Returns(() => jokeToModify);

            mock.Setup(m => m.Exists(1)).Returns(() => true);
            mock.Setup(m => m.Exists(4)).Returns(() => false);
            mock.Setup(m => m.Find(j => j.Id == 2)).Returns(() => new List<Joke>() { jokes[1] });

            mock.Setup(m => m.Add(jokeToAdd)).Callback<Joke>((_joke) => jokes.Add(_joke));

            mock.Setup(m => m.Get(4)).Returns(() => { if (jokes.Count > 3) return jokes[3]; return null; });

            mock.Setup(m => m.Remove(1)).Callback(() => jokes.RemoveAt(0));


            mock.Setup(m => m.Find(j => j.Id == 1)).Returns(() => new List<Joke> { jokes[0] });
            mock.Setup(m => m.Find(j => j.Id == 5)).Returns(() => null);

            return mock;
        }
    }

    public class JokesControllerTests
    {
        [Fact]
        public void TestGetAll()
        {
            var repoWrapper = MockRepositoryWrapper.GetMock();
            var repository = repoWrapper.Object;
            Assert.NotNull(repository.GetAll());
            Assert.NotEmpty(repository.GetAll());
            Assert.Equal(3, repository.GetAll().Count);
        }

        [Fact]
        public void TestGet()
        {
            var repoWrapper = MockRepositoryWrapper.GetMock();
            var repository = repoWrapper.Object;
            Assert.NotNull(repository.Get(1));
            Assert.Null(repository.Get(4));
            Assert.Equal(1, repository.Get(1).Id);
        }

        [Fact]
        public void TestExists()
        {
            var repoWrapper = MockRepositoryWrapper.GetMock();
            var repository = repoWrapper.Object;
            Assert.True(repository.Exists(1));
            Assert.False(repository.Exists(5));
        }

        [Fact]
        public void TestRemove()
        {
            var repoWrapper = MockRepositoryWrapper.GetMock();
            var repository = repoWrapper.Object;
            repository.Remove(1);
            Assert.Equal(2, repository.GetAll().Count);
        }

        [Fact]
        public void TestAdd()
        {
            var repoWrapper = MockRepositoryWrapper.GetMock();
            var repository = repoWrapper.Object;

            repository.Add(MockRepositoryWrapper.jokeToAdd);
            Assert.NotNull(repository.Get(4));
        }

        [Fact]
        public void TestUpdate()
        {
            var repoWrapper = MockRepositoryWrapper.GetMock();
            var repository = repoWrapper.Object;

            var joke = repository.Update(MockRepositoryWrapper.jokeToModify);
            Assert.Equal("Can February march?", joke.JokeQuestion);
        }

        [Fact]
        public void TestFind()
        {
            var repoWrapper = MockRepositoryWrapper.GetMock();
            var repository = repoWrapper.Object;

            var joke1 = repository.Find(j => j.Id == 1);
            var joke2 = repository.Find(j => j.Id == 5);
            Assert.NotNull(joke1);
            Assert.Null(joke2);
        }
    }
}