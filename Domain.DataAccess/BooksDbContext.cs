using System;
using System.Net;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Domain.DataAccess
{
    public class BooksDbContext : IdentityDbContext<User>
    {
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<BookUser> BookUser { get; set; }


        public BooksDbContext(DbContextOptions<BooksDbContext> options) : base(options)
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            base.OnModelCreating(modelBuilder);

            var genre1 = new Genre()
            {
                Title = "Фэнтэзи"
            };

            var genre2 = new Genre()
            {
                Title = "Детектив"
            };

            var genre3 = new Genre()
            {
                Title = "Боевик"
            };

            var genre4 = new Genre()
            {
                Title = "Эзотерика"
            };

            var genre5 = new Genre()
            {
                Title = "Развитие личности"
            };


            var book1 = new Book();
            var book2 = new Book();
            var book3 = new Book();
            var book4 = new Book();
            var book5 = new Book();

            var book6 = new Book();

            var book7 = new Book();
            using (WebClient client = new WebClient())
            {

                book1 = new Book()
                {
                    Title = "Таблеточку, Ваше Темнейшество?",
                    Description = "Прислонилась к таблеточной машине и попала в другой мир ? Не теряйся! Провизор ты или кто ? Зря латынь учила, что ли ? Сойдёшь за ведьму! Аптеку откроешь. Светлые тут, конечно, те еще… Да и Темные, говорят, ничуть не лучше.Ну хоть не как дома - никто не просит на себе показать, куда вставлять ректальные свечи! Разберусь! Что ? За случайно спасенного Темного можно в тюрьму попасть ? И что делать ? Разумеется, переходить на темную сторону!",
                    Author = "Лира Алая",
                    Year = 2021,
                    GenreId = genre1.Id,
                    Price = 3500,
                    Photo = client.DownloadData("https://rust.litnet.com/uploads/covers/220/1618489378_41.jpg"),
                };

                book2 = new Book()
                {
                    Title = "Дракон с подарком. Королевская академия Драко",
                    Description = "Дракон - не подарок, а пара тонн яростной мощи, бесконечной самоуверенности и хищной наглости.Король и королева желают познакомиться с лучшими выпускниками академий и милостиво объявляют, что лично подберут мужа для уникальной девушки, пострадавшей от проклятия.",
                    Author = "Светлана Суббота",
                    Year = 2021,
                    GenreId = genre1.Id,
                    Price = 2500,
                    Photo = client.DownloadData("https://rust.litnet.com/uploads/covers/220/1623320509_28.jpg")
                };

                book3 = new Book()
                {
                    Title = "Женаты против воли",
                    Description = "Коннор Хоторн великий воин и темный, завоевавший себе славу отвагой и силой. Ему прочили быструю карьеру, почет и выгодный брак…а ему вручили меня…светлую, которую он ненавидел всем сердцем. Приказ, противиться которому не мог никто. Союз, в который никто не верил. Пять лет вдали друг от друга и случайная встреча, которая изменила всё.",
                    Author = "Татьяна Серганова",
                    Year = 2020,
                    GenreId = genre1.Id,
                    Price = 3000,
                    Photo = client.DownloadData("https://rust.litnet.com/uploads/covers/220/1607248936_49.jpg")
                };

                book4 = new Book()
                {
                    Title = "Генерал Скала и сиротка",
                    Description = "Скандально, но что—то в этом есть! Я и мои две сестры, по приказу короля-завоевателя выезжаем в столицу. Порочный и циничный незнакомец принимает меня за служанку, а затем за младшую наследницу, незаконнорожденную. И приходит в ярость от моего решительного отказа на его неприличные притязания. Каково же мое удивление, когда мы встречаемся во дворце, куда по приказу его величества прибыли наследники покоренных герцогств и княжеств.",
                    Author = "Светлана Суббота",
                    Year = 2021,
                    GenreId = genre1.Id,
                    Price = 2750,
                    Photo = client.DownloadData("https://rust.litnet.com/uploads/covers/220/1617469578_19.jpg")
                };

                book5 = new Book()
                {
                    Title = "Особое предложение",
                    Description = "Чтобы защитить земли от жадных родственников, пришлось рискнуть и тайком поехать к Императору с прошением. Если бы я только знала, чем закончится моя поездка! Наш правитель - маг холода. Его стихия: снег и лед. Таким же стало его сердце, после потери возлюбленной. Но что случится, если лед встретится с огнем живой души ? ",
                    Author = "Франциска Вудворт, Екатерина Васина",
                    Year = 2021,
                    GenreId = genre1.Id,
                    Price = 2150,
                    Photo = client.DownloadData("https://rust.litnet.com/uploads/covers/220/1610189541_71.jpg")
                };



                book6 = new Book()
                {
                    Title = "Я случайно, господин инквизитор! или Охота на Тени",
                    Description = "Мой новый куратор - элитный инквизитор и опасный охотник. И не только на нечисть, но и на юных девиц вроде меня. Впрочем, каюсь: сама виновата! Постоянно притягиваю к себе неприятности, а они снежным комом налетели в первый же день моей стажировки. Мне приходится расследовать череду загадочных смертей среди инквизиторов, и в моих силах распутать этот клубок тайн, вот только... Ой. Я случайно, господин инквизитор!!",
                    Author = "Леси Филеберт",
                    Year = 2021,
                    GenreId = genre2.Id,
                    Price = 2145,
                    Photo = client.DownloadData("https://rust.litnet.com/uploads/covers/220/1615701379_79.jpg")
                };

                book7 = new Book()
                {
                    Title = "Арсанты. Дети богов",
                    Description = "Детектив Джек Стоун расследует загадочное происшествие, которое знакомит его с деятельностью тайного Ордена, раскинувшего свою агентурную сеть по всему миру. На их службе всё — мистические ритуалы, тайные знания, безграничные финансовые ресурсы и высочайшие технологии. Но кто эти люди — новая надежда человечества или ее погибель?",
                    Author = "Антон Фарутин",
                    Year = 2018,
                    Price = 2355,
                    GenreId = genre3.Id,
                    Photo = client.DownloadData("https://rust.litnet.com/uploads/covers/220/1620590513_62.jpg")
                };
            }


            modelBuilder.Entity<Genre>().HasData(
                    new Genre[]
                    {
                        genre1, genre2, genre3, genre4, genre5
                    }
                );
            
            modelBuilder.Entity<Book>().HasData(
                    new Book[]
                    {
                        book1, book2, book3, book4, book5, book6, book7
                    }
                );


        }

    }
}
