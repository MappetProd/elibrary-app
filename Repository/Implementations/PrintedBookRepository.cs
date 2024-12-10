using EL.Domain;
using EL.Repository.Contracts;
using EL.Repository.DTO;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL.Repository.Implementations
{
    public class PrintedBookRepository : GenericRepository<PrintedBook>, IPrintedBookRepository
    {
        public PrintedBookRepository(ElibraryContext context) : base(context) { }


        /*public IEnumerable<PrintedBook> GetByGenres(IEnumerable<string> genreNames)
        {

        }
        public IEnumerable<PrintedBook> GetByAuthors(IEnumerable<AuthorDTO> authors)
        {

        }
        public IEnumerable<PrintedBook> GetByPublishers(IEnumerable<string> publisherNames)
        {

        }*/

        public IEnumerable<PrintedBook> Filter(string? genres, string? authors, string? publisherNames)
        {
            var predicate = PredicateBuilder.New<PrintedBook>();
            if (genres != null)
            {
                IEnumerable<string> filteringGenres = genres.Split(',');
                predicate.And(pb => pb.Book.Genres.Where(g => filteringGenres.Contains(g.Name)).Any());
            }

            if (authors != null)
            {
                string[] filteringAuthors = authors.Split(',');
                foreach (string str_author in filteringAuthors)
                {
                    string[] initials = str_author.Split(' ');
                    string name = initials[1];
                    string surname = initials[0];
                    predicate.Or(pb => pb.Book.Authors.Where(a => a.Name == name && a.Surname == surname).Any());
                }
                //predicate.And(pb => pb.Book.Authors.Where(a => filteringAuthors.Where(astr => astr.Split(' ')[1] == a.Name && astr.Split(' ')[0] == a.Surname).Any()).Any());

            }

            if (publisherNames != null)
            {
                IEnumerable<string> filteringPublishers = publisherNames.Split(',');
                predicate.And(pb => filteringPublishers.Contains(pb.Publisher.Name));
            }

            return entities.Where(predicate).ToList();

            /*return from pb in entities
                   where pb.Book.Genres.Where(g => genres.Contains(g.Name)).Any()
                   && pb.Book.Authors.Where(a => authors.Where(dto => dto.Name == a.Name && dto.Surname == a.Surname).Any()).Any()
                   && pb.Publisher.Name == publisherName
                   select pb;*/
        }
    }
}
