using University.Data;
using University.Repository.Interface;
using University.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University.Service
{
    public class ProductVideoService: IProductVideoService
    {
        private readonly IProductVideoRepository _productVideoRepository;
        public ProductVideoService(IProductVideoRepository productVideoRepository)
        {
            _productVideoRepository = productVideoRepository;

        }
        public List<ProductVideos> GetProductVideoList()
        {
            return _productVideoRepository.GetProductVideoList();
        }
        public IEnumerable<ProductVideoModel> GetUserVideosList() 
        {
            return _productVideoRepository.GetUserVideosList();
        }
    }
}
