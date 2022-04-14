using Blog.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Blog.Data
{
    public static class TestData
    {
        public static void EnterDataToBlogDb()
        {
            using (var context = new BlogContext())
            {               
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var role1 = new Role { Name = "Administrator" };
                var role2 = new Role { Name = "Moderator" };
                var role3 = new Role { Name = "User" };

                context.Roles.AddRange(role1, role2, role3);

                var user1 = new User { FirstName = "Alexander", LastName = "Frolov", Email = "alex@yandex.ru", DisplayName = "AlexFr", Password = "qwerty123", Roles = new List<Role> { role1 } };
                var user2 = new User { FirstName = "Maksim", LastName = "Maksimov", Email = "maksim@mail.ru", DisplayName = "MaksimMaks1", Password = "zxc13", Roles = new List<Role> { role2 } };
                var user3 = new User { FirstName = "Artem", LastName = "Olovian", Email = "artem@gmail.com", DisplayName = "Artem70", Password = "123easy123", Roles = new List<Role> { role3 } };

                context.Users.AddRange(user1, user2, user3);

                var tag1 = new Tag { Name = "Образование", User = user1 };
                var tag2 = new Tag { Name = "Наука", User = user3};
                var tag3 = new Tag { Name = "Для детей", User = user2 };
                var tag4 = new Tag { Name = "История", User = user1 };
                var tag5 = new Tag { Name = "Психология", User = user1 };

                context.Tags.AddRange(tag1, tag2, tag3, tag4, tag5);

                var post1 = new Post
                {
                    Title = "10 секретов счастливого брака",
                    ShortDescription = "Статья об отношениях.",
                    Tags = new List<Tag> { tag1, tag5 },
                    User = user1,
                    Content = "1. Даже после свадьбы нельзя сказать, что человек находится в полной вашей власти. Не стоит переделывать человека, которого вы выбрали." +
                    " 2.Абсолютно неверно утверждение, что руководить семьей должна женщина.Муж должен принимать все самые важные решения, советуясь со своей женой." +
                    " 3.Даже в минуты самой сильной ярости не стоит упоминать обо всем плохом, что у вас было.Скорее всего, конфликт сгладиться, а слова, сказанные на эмоциях," +
                    " отпечатаются в памяти надолго."
                };
                var post2 = new Post
                {
                    Title = "Путешествие в великолепный леголенд",
                    ShortDescription = "Одно из самых привлекательных мест в Америке для маленьких туристов – это Леголенд",
                    Tags = new List<Tag> { tag3 },
                    User = user2,
                    Content = "В калифорнийском Карлсбаде, который расположен недалеко от Лос-Анджелеса, есть одно из самых привлекательных мест в Америке для маленьких" +
                    " туристов – это Леголенд. Но если честно даже взрослые ценители Lego вряд не останутся равнодушными. Для того, чтобы создать этот удивительный парк" +
                    " потратили более 45 млн.деталей конструктора! Наверное, это самый лучший парк в Калифорнии для приятного и веселого семейного отдыха."
                };
                var post3 = new Post
                {
                    Title = "Как добиться финансовой независимости?",
                    ShortDescription = "Вашему вниманию предлагаем несколько простых способов для достижения любой финансовой цели.",
                    Tags = new List<Tag> { tag2 },
                    User = user3,
                    Content = "Никто из нас не хочет оказаться погрязшим в долговых обязательствах, мы хотим быть финансово независимыми, хотим иметь достаточно" +
                    " возможностей для оплаты различных счетов, и, в конце концов, раньше уйти на пенсию. Сегодня вашему вниманию предлагаем несколько простых способов" +
                    " для достижения любой финансовой цели. Разработайте детальный, но гибкий план Ваш финансовый план должен быть подробным, он должен учитывать все" +
                    " аспекты вашей жизни, но, несмотря на свою детализацию, он должен быть очень гибким.Никто не знает, что приготовлено для нас в будущем, именно" +
                    " поэтому гибкость плана должна быть одной из главных характеристик."
                };
                var post4 = new Post
                {
                    Title = "География гавайских островов",
                    ShortDescription = "Статья об гавайских островах.",
                    Tags = new List<Tag> { tag1, tag4 },
                    User = user1,
                    Content = "Гавайи являются самой длинной цепью в мире, протяженность островов составляет 1,600 миль. Основные острова расположены немного южнее" +
                    " Тропика Рака и находятся на одной широте с Кубой и Мексикой. Большинство земельной массы островов разделено между 8 островами. Заселенными" +
                    " являются только семь островов. Некоторые туристы боятся Гавайев, потому что это всего лишь маленькая часть суши в огромном океане. Почти вся" +
                    " земельная масса островов находится на восьми основных островах.Население живет на семи из них."
                };

                context.Posts.AddRange(post1, post2, post3, post4);

                var comment1 = new Comment { Content = "Спасибо! Очень занимательная статья!", User = user1, Post = post1 };
                var comment2 = new Comment { Content = "Теперь буду знать куда поехать отдыхать со своимим детьми:)", User = user1, Post = post2 };
                var comment3 = new Comment { Content = "Попробую использовать эти правила в своем браке.", User = user3, Post = post1 };
                var comment4 = new Comment { Content = "Мне кажется автор статьи совсем далек от понимания реалий сегодняшнего дня для простого работяги.", User = user3, Post = post3 };
                var comment5 = new Comment { Content = "А вот у меня в принципе нет проблем т.к. я холост и прекрасно себя чувствую!", User = user2, Post = post1 };

                context.Comments.AddRange(comment1, comment2, comment3, comment4, comment5);

                context.SaveChanges();
            }
        }
        
    }
}
