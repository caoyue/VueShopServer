using System;
using System.Linq;
using VueShopServer.Api.Data;
using VueShopServer.Api.Entities;

namespace VueShopServer.Api.Services.Impl
{
    public class PageService : IPageService
    {
        private readonly IRepository<Page> _pageRepository;
        public PageService(IRepository<Page> pageRepository)
        {
            _pageRepository = pageRepository;
            Init();
        }

        public Page GetByTag(string tag) =>
            _pageRepository.AsQueryable.FirstOrDefault(s =>
                string.Equals(s.Tag, tag, StringComparison.OrdinalIgnoreCase));

        private void Init()
        {
            if (!_pageRepository.AsQueryable.Any())
            {
                // just for testing
                _pageRepository.Insert(new Page
                {
                    Id = 1,
                    Tag = "Home",
                    Content = "<h2>A simple vue project.</h2>"
                });
                _pageRepository.Insert(new Page
                {
                    Id = 2,
                    Tag = "About",
                    Content = "<h1>project <a href=\"https://github.com/caoyue/vue-shop\" target=\"_blank\">vue_shop</a></h1>"
                });
            }
        }
    }
}
