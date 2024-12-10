using EL.Domain;
using EL.Repository.Contracts;
using EL.Service.Contract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL.Service.Implementation
{
    public class SuggestedOptionsService : ISuggestedOptionsService
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IPublisherRepository _publisherRepository;
        private readonly IAuthorRepository _authorRepository;
        public SuggestedOptionsService(
            [FromServices] IGenreRepository genreRepository,
            [FromServices] IPublisherRepository publisherRepository,
            [FromServices] IAuthorRepository authorRepository)
        {
            _genreRepository = genreRepository;
            _publisherRepository = publisherRepository;
            _authorRepository = authorRepository;
        }
        public IEnumerable<string> GetAllGenres()
        {
            IEnumerable<string> genreNames = from g in _genreRepository.GetAll()
                                             select g.Name;
            return genreNames;
        }

        public IEnumerable<DTO.AuthorDTO> GetAllAuthors()
        {
            IEnumerable<DTO.AuthorDTO> authors = from a in _authorRepository.GetAll()
                                                 select new DTO.AuthorDTO
                                                 {
                                                     Name = a.Name,
                                                     Surname = a.Surname
                                                 };
            return authors;
        }
        public IEnumerable<string> GetAllPublishers()
        {
            IEnumerable<string> publishersNames = from p in _publisherRepository.GetAll()
                                                  select p.Name;
            return publishersNames;
        }
    }
}
